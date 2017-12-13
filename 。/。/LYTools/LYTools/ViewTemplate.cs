using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LYTools
{
    public class ViewTemplate
    {
        public ViewTemplate(DataTable dt, string namespaceStr, string className)
        {
            this.NameSpaceStr = string.Format("{0}.ViewModels.{1}_ViewModel", namespaceStr, className);
            this.Url = string.Format("/{0}/{1}/", namespaceStr.Replace("Common", "Admin"), className);
            this.ClassName = className;

            string xHeadHtml = string.Empty;
            string xBodyHtml = string.Empty;
            string xCreateHtml = string.Empty;
            string xEditHtml = string.Empty;

            //遍历数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString();
                name = name.Substring(1);

                if (name == "RowVersion" || name == "ID")
                {
                    continue;
                }
                string description = dt.Rows[i][5].ToString();
                string sqltype = dt.Rows[i][4].ToString();
                string type = LYTools.Types.GetType(sqltype, true);
                if (type != "DateTime" && name != "LastUpdateUserID" && name != "CreateID" && name != "CreatorID")
                {
                    xCreateHtml += GetCreateHtml(name, description, type);
                    xEditHtml += GetEditHtml(name, description, type);
                }
                if (name != "LastUpdateUserID" && name != "CreatorID" && name != "CreateID" && name != "LastUpdateTime")
                {
                    if (name == "CreateTime")
                    {
                        description = "创建时间";
                        name =string.Format("value:{0}()!=null?{0}().substring(0,10):''",name);
                    }
                    xHeadHtml += GetHeadHtml(name, description);
                    xBodyHtml += GetBodyHtml(name, description);
                }
            }

            this.HeadHtml = xHeadHtml;
            this.BodyHtml = xBodyHtml;
            this.CreateHtml = xCreateHtml;
            this.EditHtml = xEditHtml;

        }

        private string GetEditHtml(string name, string description, string type)
        {
            StringBuilder sb = new StringBuilder();
            //图片根据字段名称特殊处理
            if (name.ToLower().Contains("img") || name.ToLower().Contains("image") || name.ToLower().Contains("icon") || name.ToLower().Contains("photo") || name.ToLower().Contains("picture"))
            {
                type = "Images";
            }
            switch (type)
            {
                case "int":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<input type=\"text\"  onkeyup=\"this.value=this.value.replace(/\\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\\D/g,'')\" name=\"" + name + "\" data-bind=\"value:" + name + "\" value=\"\" class=\"span7\" placeholder=\"\" isNot=\"true\" verify=\"isAll\" msg=\"" + (string.IsNullOrEmpty(description) ? name : description) + "不能为空！\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                case "Double":
                case "Decimal":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<input type=\"text\"  onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" name=\"" + name + "\" data-bind=\"value:" + name + "\" value=\"\" class=\"span7\" placeholder=\"\" isNot=\"true\" verify=\"isAll\" msg=\"" + (string.IsNullOrEmpty(description) ? name : description) + "不能为空！\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                case "bool":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<span class=\"fl pdr15\"><label><i class=\"radio_btn\"><input type=\"radio\" name=\"" + name + "\" data-bind=\"attr:{ checked:IsDefault}\" value=\"true\" /></i>是</label></span>\n");
                        sb.Append("<span class=\"fl pdr15\"><label><i class=\"radio_btn\"><input type=\"radio\" name=\"" + name + "\" checked value=\"false\" /></i>否</label></span>\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                case "Images":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<span class=\"fl pdr15\"><input type=\"text\" id=\"txt_ImageUrl_" + name + "\" data-bind=\"value:" + name + "\" name=\"" + name + "\" class=\"input normal\" isNot=\"false\" readonly=\"readonly\" value=\"\" /></span>\n");
                        sb.Append("<a href=\"javascript:void(0)\" onclick=\"$('#uploadphoto').click();\" class=\"btn bgc_c\">上传图片</a>\n");
                        sb.Append("<span class=\"ts_msg c_6\"></span>\n");
                        sb.Append("<img id=\"orgimg\" src=\"/Themes/images/spinner.gif\" style=\"display:none;\" />\n");
                        sb.Append("<input type=\"file\" id=\"uploadphoto\" style=\"position:absolute;top:-999em;left:-999em;\" onchange=\"G.ImgUpload('photo', 'txt_ImageUrl_" + name + "',2,false)\" accept=\"image/*\" capture=\"camera\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                default:
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<input type=\"text\" name=\"" + name + "\" data-bind=\"value:" + name + "\" value=\"\" class=\"span7\" placeholder=\"\" isNot=\"true\" verify=\"isAll\" msg=\"" + (string.IsNullOrEmpty(description) ? name : description) + "不能为空！\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
            }
            return sb.ToString();
        }

        private string GetCreateHtml(string name, string description, string type)
        {
            StringBuilder sb = new StringBuilder();
            //图片根据字段名称特殊处理
            if (name.ToLower().Contains("img") || name.ToLower().Contains("image") || name.ToLower().Contains("icon") || name.ToLower().Contains("photo") || name.ToLower().Contains("picture"))
            {
                type = "Images";
            }
            switch (type)
            {
                case "int":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<input type=\"text\"  onkeyup=\"this.value=this.value.replace(/\\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\\D/g,'')\" name=\"" + name + "\" data-bind=\"value:" + name + "\" value=\"\" class=\"span7\" placeholder=\"\" isNot=\"true\" verify=\"isAll\" msg=\"" + (string.IsNullOrEmpty(description) ? name : description) + "不能为空！\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                case "Double":
                case "Decimal":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<input type=\"text\"  onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" name=\"" + name + "\" data-bind=\"value:" + name + "\" value=\"\" class=\"span7\" placeholder=\"\" isNot=\"true\" verify=\"isAll\" msg=\"" + (string.IsNullOrEmpty(description) ? name : description) + "不能为空！\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                case "bool":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<span class=\"fl pdr15\"><label><i class=\"radio_btn\"><input type=\"radio\" name=\"" + name + "\" data-bind=\"attr:{ checked:IsDefault}\" value=\"true\" /></i>是</label></span>\n");
                        sb.Append("<span class=\"fl pdr15\"><label><i class=\"radio_btn\"><input type=\"radio\" name=\"" + name + "\" checked value=\"false\" /></i>否</label></span>\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                case "Images":
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<span class=\"fl pdr15\"><input type=\"text\" id=\"txt_ImageUrl_" + name + "\" data-bind=\"value:" + name + "\" name=\"" + name + "\" class=\"input normal\" isNot=\"false\" readonly=\"readonly\" value=\"\" /></span>\n");
                        sb.Append("<a href=\"javascript:void(0)\" onclick=\"$('#uploadphoto').click();\" class=\"btn bgc_c\">上传图片</a>\n");
                        sb.Append("<span class=\"ts_msg c_6\"></span>\n");
                        sb.Append("<img id=\"orgimg\" src=\"/Themes/images/spinner.gif\" style=\"display:none;\" />\n");
                        sb.Append("<input type=\"file\" id=\"uploadphoto\" style=\"position:absolute;top:-999em;left:-999em;\" onchange=\"G.ImgUpload('photo', 'txt_ImageUrl_" + name + "',2,false)\" accept=\"image/*\" capture=\"camera\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
                default:
                    {
                        sb.Append("<div class=\"control-group\">\n");
                        sb.Append("<label class=\"control-label\">" + (string.IsNullOrEmpty(description) ? name : description) + "：</label>\n");
                        sb.Append("<div class=\"controls\">\n");
                        sb.Append("<input type=\"text\" name=\"" + name + "\" data-bind=\"value:" + name + "\" value=\"\" class=\"span7\" placeholder=\"\" isNot=\"true\" verify=\"isAll\" msg=\"" + (string.IsNullOrEmpty(description) ? name : description) + "不能为空！\" />\n");
                        sb.Append("</div>\n");
                        sb.Append("</div>\n");
                    }
                    break;
            }
            return sb.ToString();
        }

        private string GetBodyHtml(string name, string description)
        {
            string temp = "<td data-bind=\"text: $data.{0}\" />\n";
            return string.Format(temp, name);
        }

        private string GetHeadHtml(string name, string description)
        {
            string temp = "<th>{0}</th>\n";
            return string.Format(temp, string.IsNullOrEmpty(description) ? name : description);
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpaceStr { get; set; }
        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 头部地址
        /// </summary>
        public string HeadHtml { get; set; }

        /// <summary>
        /// 头部地址
        /// </summary>
        public string BodyHtml { get; set; }
        /// <summary>
        /// 创建
        /// </summary>
        public string CreateHtml { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>
        public string EditHtml { get; set; }
    }
}
