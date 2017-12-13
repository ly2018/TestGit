using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LYTools;
using System.Xml.Linq;

namespace NHibernateTools
{
    public partial class NHibernateTools : Form
    {
        public static string ServerPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string conStr = ServerPath + "config.xml";//配置文件

        public static string modelPath = ServerPath + "Services.txt";//Services.cs模板路径

        public static string controllerPath = ServerPath + "Controller.txt";//Controller.cs模板路径
        public NHibernateTools()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitCbxData()
        {
            cbx_temp.Items.Clear();
            cbx_temp.Items.Insert(0, "请选择");
            cbx_temp.SelectedIndex = 0;
            if (!System.IO.File.Exists(conStr))
            {
                System.IO.File.AppendAllText(conStr, "<?xml version='1.0' encoding='utf-8'?><root maxid='1'/>", System.Text.Encoding.UTF8);
            }
            XDocument xdom = XDocument.Load(conStr);
            var List = xdom.Descendants("db");
            if (List != null)
            {
                foreach (var x in List)
                {
                    int id = Convert.ToInt32(x.Attribute("id").Value);
                    cbx_temp.Items.Insert(id, x.Attribute("name").Value);
                }
            }
        }
        private void NHibernateTools_Load(object sender, EventArgs e)
        {
            InitCbxData();
        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            if (!IsValidateForm())
            {
                return;
            }
            string classNameARR = string.Empty;
            string EntityDtoMapper = string.Empty;
            string ServiceInject = string.Empty;
            string ModelBuilder = string.Empty;
            foreach (Control item in lv_table.Controls)
            {
                CheckBox thisItem = ((CheckBox)item);
                if (thisItem.Checked)
                {
                    string className = thisItem.Text;   // 表名及实体名称
                    string[] startNames = txtStartName.Text.Split(';');
                    string currStartName = string.Empty;//当前表前缀名称
                    bool IfContinue = false;
                    for (int i = 0; i < startNames.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(startNames[i]) && className.StartsWith(startNames[i]))
                        {
                            currStartName = startNames[i];
                            IfContinue = true;
                        }
                    }
                    if (!IfContinue) continue;//结束循环
                    string namespaceStr = txt_NameSpace.Text;
                    string path = txt_Path.Text + "\\";
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    //取到属性表
                    DataTable dt = DBHelper.DBHelper.GetDataTableBySql(txt_ConnString.Text, DBHelper.DBHelper.GetTableAttrSql(className));

                    if (ckb_IsNewVersion.Checked)
                    {
                        string ApplicationDtos = "Application.Dtos";
                        string ApplicationDtosPath = path + "\\" + ApplicationDtos + "\\";
                        if (!System.IO.Directory.Exists(ApplicationDtosPath))
                            System.IO.Directory.CreateDirectory(ApplicationDtosPath);
                        LYTools.NewVersionmMap.GetTemplateText(dt, className, ServerPath + ApplicationDtos + ".txt", txt_NameSpace.Text, currStartName, ApplicationDtosPath, "{0}Dto.cs");

                        string ApplicationIServices = "Application.IServices";
                        string ApplicationIServicesPath = path + "\\" + ApplicationIServices + "\\";
                        if (!System.IO.Directory.Exists(ApplicationIServicesPath))
                            System.IO.Directory.CreateDirectory(ApplicationIServicesPath);
                        LYTools.NewVersionmMap.GetTemplateText(null, className, ServerPath + ApplicationIServices + ".txt", txt_NameSpace.Text, currStartName, ApplicationIServicesPath, "I{0}AppService.cs");

                        string ApplicationServices = "Application.Services";
                        string ApplicationServicesPath = path + "\\" + ApplicationServices + "\\";
                        if (!System.IO.Directory.Exists(ApplicationServicesPath))
                            System.IO.Directory.CreateDirectory(ApplicationServicesPath);
                        LYTools.NewVersionmMap.GetTemplateText(null, className, ServerPath + ApplicationServices + ".txt", txt_NameSpace.Text, currStartName, ApplicationServicesPath, "{0}AppService.cs");

                        string DataRepositories = "Data.Repositories";
                        string DataRepositoriesPath = path + "\\" + DataRepositories + "\\";
                        if (!System.IO.Directory.Exists(DataRepositoriesPath))
                            System.IO.Directory.CreateDirectory(DataRepositoriesPath);
                        LYTools.NewVersionmMap.GetTemplateText(dt, className, ServerPath + DataRepositories + ".txt", txt_NameSpace.Text, currStartName, DataRepositoriesPath, "{0}Repository.cs");

                        string DomainEntities = "Domain.Entities";
                        string DomainEntitiesPath = path + "\\" + DomainEntities + "\\";
                        if (!System.IO.Directory.Exists(DomainEntitiesPath))
                            System.IO.Directory.CreateDirectory(DomainEntitiesPath);
                        LYTools.NewVersionmMap.GetTemplateText(dt, className, ServerPath + DomainEntities + ".txt", txt_NameSpace.Text, currStartName, DomainEntitiesPath, "{0}.cs");

                        string DomainIRepositories = "Domain.IRepositories";
                        string DomainIRepositoriesPath = path + "\\" + DomainIRepositories + "\\";
                        if (!System.IO.Directory.Exists(DomainIRepositoriesPath))
                            System.IO.Directory.CreateDirectory(DomainIRepositoriesPath);
                        LYTools.NewVersionmMap.GetTemplateText(null, className, ServerPath + DomainIRepositories + ".txt", txt_NameSpace.Text, currStartName, DomainIRepositoriesPath, "I{0}Repository.cs");

                        //无前缀类名称
                        string noPrefixName = className.Replace(currStartName, "");

                        //生成视图
                        string genView = LYTools.NewVersionmMap.GetViewsText(dt, namespaceStr, className, currStartName, ServerPath, path + "\\Views\\");
                        //生成控制器
                        string ControllersPath = path + "\\Controllers\\";
                        if (!System.IO.Directory.Exists(ControllersPath))
                            System.IO.Directory.CreateDirectory(ControllersPath);
                        string myController = LYTools.NHibernateMap.GetServiceModelCSText(dt, noPrefixName, controllerPath.Replace("Controller", "NewController"), txt_NameSpace.Text, true);
                        LYTools.Common.CreateTextFile(ControllersPath + noPrefixName + "Controller.cs", myController, true);

                        string dataMap = string.Format("\t\t\t\tcfg.CreateMap<{0}Dto, {0}>();\n\t\t\t\tcfg.CreateMap<{0}, {0}Dto>();\n\n", noPrefixName);
                        //生成EntityDtoMapper
                        LYTools.Common.CreateTextFile(path + "\\" + "EntityDtoMapper.cs", dataMap, false);

                        string dataBuilder = "\t\tpublic DbSet<#> # { get; set; }\n\n".Replace("#", noPrefixName);
                        //生成ModelBuilder
                        LYTools.Common.CreateTextFile(path + "\\" + "ModelBuilder.cs", dataBuilder, false);

                        string dataInject = string.Format("\t\t\tservices.AddScoped<I{0}Repository, {0}Repository>();\n\t\t\tservices.AddScoped<I{0}AppService, {0}AppService>();\n\n", noPrefixName);
                        //生成ServiceInject
                        LYTools.Common.CreateTextFile(path + "\\" + "ServiceInject.cs", dataInject, false);
                    }
                    else
                    {
                        //生成接口
                        String InterfacePath = path + "\\Contracts\\";
                        String ServicePath = path + "\\Services\\";
                        String ControllerFilePath = path + "\\Controller\\";
                        String ModelsFilePath = path + "\\Models\\";
                        String ViewModelsFilePath = path + "\\ViewModels\\";
                        String ViewsFilePath = path + "\\Views\\";
                        if (!System.IO.Directory.Exists(InterfacePath))
                        {
                            System.IO.Directory.CreateDirectory(InterfacePath);
                        }
                        if (!System.IO.Directory.Exists(ServicePath))
                        {
                            System.IO.Directory.CreateDirectory(ServicePath);
                        }
                        if (!System.IO.Directory.Exists(ControllerFilePath))
                        {
                            System.IO.Directory.CreateDirectory(ControllerFilePath);
                        }
                        if (!System.IO.Directory.Exists(ModelsFilePath))
                        {
                            System.IO.Directory.CreateDirectory(ModelsFilePath);
                        }
                        if (!System.IO.Directory.Exists(ViewModelsFilePath))
                        {
                            System.IO.Directory.CreateDirectory(ViewModelsFilePath);
                        }
                        if (!System.IO.Directory.Exists(ViewsFilePath))
                        {
                            System.IO.Directory.CreateDirectory(ViewsFilePath);
                        }
                        //生成视图
                        string genView = LYTools.NHibernateMap.GetViewsText(dt, namespaceStr, className, currStartName, ServerPath, ViewsFilePath);

                        //生成ViewModel
                        //if (ckbIsGenerateViewModel.Checked)
                        {
                            string strFalse = LYTools.NHibernateMap.GetCSText(dt, namespaceStr + ".ViewModels", className, true, currStartName);
                            LYTools.Common.CreateTextFile(ViewModelsFilePath + className + ".cs", strFalse, true);
                        }
                        //else
                        {

                            //生成Models
                            string str = LYTools.NHibernateMap.GetCSText(dt, namespaceStr + ".Models", className, false, currStartName);
                            LYTools.Common.CreateTextFile(ModelsFilePath + className + ".cs", str, true);

                            if (!string.IsNullOrEmpty(currStartName))
                            {
                                className = className.Replace(currStartName, "");
                            }
                            string interfaceName = "I" + className + "Contract";//定义接口名称
                            String serviceName = className + "Service";//定义实现名称

                            //生成接口Contracts
                            string myInterface = LYTools.NHibernateMap.GetInterfaceCsText(txt_NameSpace.Text + ".Contracts", className, currStartName);
                            LYTools.Common.CreateTextFile(InterfacePath + interfaceName + ".cs", myInterface, true);

                            //生成服务Service
                            //string myService = LYTools.NHibernateMap.GetServiceCsText("IBMS.Common.Services", className);
                            //LYTools.Common.CreateTextFile(ServicePath + serviceName + ".cs", myService, true);

                            string myService = LYTools.NHibernateMap.GetServiceModelCSText(dt, className, modelPath, txt_NameSpace.Text);
                            LYTools.Common.CreateTextFile(ServicePath + serviceName + ".cs", myService, true);

                            //生成控制器Controller
                            string myController5 = LYTools.NHibernateMap.GetServiceModelCSText(dt, className, controllerPath, txt_NameSpace.Text, true);
                            LYTools.Common.CreateTextFile(ControllerFilePath + className + "Controller.cs", myController5, true);
                            //classNameARR += ",\"" + className + "\"";

                        }
                    }
                }
            }
            MessageBox.Show("生成成功！");//+ classNameARR
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (IsValidateForm())
            {
                if (btn_Connect.Text == "断开")
                {
                    lv_table.Clear();
                    txt_Path.Text = "";
                    txt_NameSpace.Text = "";
                    txt_ConnString.Text = "";
                    sel_tabs.Checked = false;
                    btn_Connect.Text = "连接";
                }
                else
                {
                    // 所有表名
                    DataTable allTable = DBHelper.DBHelper.GetDataTableBySql(txt_ConnString.Text, DBHelper.DBHelper.GetAllTableSql);
                    lv_table.Clear();
                    lv_table.BeginUpdate();
                    for (int i = 0; i < allTable.Rows.Count; i++)
                    {
                        CheckBox cb = new CheckBox();
                        cb.Text = allTable.Rows[i][0].ToString();
                        cb.Top = i * 25;
                        cb.Width = lv_table.Width;
                        lv_table.Controls.Add(cb);
                    }
                    lv_table.Scrollable = true;
                    lv_table.EndUpdate();
                    btn_Connect.Text = "断开";
                }
            }
            else
            {
                return;
            }
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            if (!IsValidateForm())
            {
                return;
            }
            if (!System.IO.File.Exists(conStr))
            {
                System.IO.File.AppendAllText(conStr, "<?xml version='1.0' encoding='utf-8'?><root maxid='1'/>", System.Text.Encoding.UTF8);
            }
            XDocument xdom = XDocument.Load(conStr);
            int MaxID = Convert.ToInt32(xdom.Root.Attribute("maxid").Value);
            xdom.Root.Add(new XElement("db", new XAttribute("id", MaxID.ToString()),
                new XAttribute("name", txt_NameSpace.Text),
                new XAttribute("path", txt_Path.Text),
                new XAttribute("startStr", txtStartName.Text),
                new XAttribute("constr", txt_ConnString.Text)));
            xdom.Root.SetAttributeValue("maxid", (MaxID + 1).ToString());
            xdom.Save(conStr);
            MessageBox.Show("保存成功");
            InitCbxData();
        }

        private bool IsValidateForm()
        {
            if (string.IsNullOrEmpty(txt_ConnString.Text))
            {
                MessageBox.Show("请输入数据库连接字符串！");
                return false;
            }
            else if (string.IsNullOrEmpty(txt_NameSpace.Text))
            {
                MessageBox.Show("请输入项目的命名空间！");
                return false;
            }
            else if (string.IsNullOrEmpty(txt_Path.Text))
            {
                MessageBox.Show("请输入生成文件保存的位置！");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void cbx_temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = cbx_temp.SelectedIndex.ToString();
            XDocument xdom = XDocument.Load(conStr);
            var List = xdom.Descendants("db");
            if (List != null)
            {
                foreach (var x in List)
                {
                    if (x.Attribute("id").Value == id)
                    {
                        txt_NameSpace.Text = x.Attribute("name").Value;
                        txt_Path.Text = x.Attribute("path").Value;
                        txt_ConnString.Text = x.Attribute("constr").Value;
                        txtStartName.Text = x.Attribute("startStr").Value;
                        break;
                    }
                }
            }
        }

        private void sel_tabs_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control item in lv_table.Controls)
            {
                ((CheckBox)item).Checked = sel_tabs.Checked;
            }
        }

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    string id = cbx_temp.SelectedIndex.ToString();
        //    XDocument xdom = XDocument.Load(conStr);
        //    var List = xdom.Descendants("db");
        //    if (List != null)
        //    {
        //        foreach (var x in List)
        //        {
        //            if (x.Attribute("id").Value == id)
        //            {
        //                x.Remove();//删除当前选中连接
        //            }
        //        }
        //    }
        //    xdom.Save(conStr);
        //    MessageBox.Show("删除成功！");
        //}
    }
}
