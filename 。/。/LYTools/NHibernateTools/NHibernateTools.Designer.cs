namespace NHibernateTools
{
    partial class NHibernateTools
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_ConnString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.lv_table = new System.Windows.Forms.ListView();
            this.sel_tabs = new System.Windows.Forms.CheckBox();
            this.txt_NameSpace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.cbx_temp = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Generate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Path = new System.Windows.Forms.TextBox();
            this.ckbIsGenerateViewModel = new System.Windows.Forms.CheckBox();
            this.lbl_SkipStartName = new System.Windows.Forms.Label();
            this.txtStartName = new System.Windows.Forms.TextBox();
            this.lblTips = new System.Windows.Forms.Label();
            this.ckb_IsNewVersion = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txt_ConnString
            // 
            this.txt_ConnString.Location = new System.Drawing.Point(107, 12);
            this.txt_ConnString.Name = "txt_ConnString";
            this.txt_ConnString.Size = new System.Drawing.Size(479, 21);
            this.txt_ConnString.TabIndex = 3;
            this.txt_ConnString.Text = "Server=(local);initial catalog=hibDB;Integrated Security=SSPI;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据库字符串：";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(14, 253);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(61, 23);
            this.btn_Connect.TabIndex = 5;
            this.btn_Connect.Text = "连接";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // lv_table
            // 
            this.lv_table.AllowColumnReorder = true;
            this.lv_table.AllowDrop = true;
            this.lv_table.LabelWrap = false;
            this.lv_table.Location = new System.Drawing.Point(431, 70);
            this.lv_table.MaximumSize = new System.Drawing.Size(155, 392);
            this.lv_table.MinimumSize = new System.Drawing.Size(155, 392);
            this.lv_table.Name = "lv_table";
            this.lv_table.Scrollable = false;
            this.lv_table.Size = new System.Drawing.Size(155, 392);
            this.lv_table.TabIndex = 6;
            this.lv_table.UseCompatibleStateImageBehavior = false;
            // 
            // sel_tabs
            // 
            this.sel_tabs.AutoSize = true;
            this.sel_tabs.Location = new System.Drawing.Point(431, 48);
            this.sel_tabs.Name = "sel_tabs";
            this.sel_tabs.Size = new System.Drawing.Size(60, 16);
            this.sel_tabs.TabIndex = 7;
            this.sel_tabs.Text = "所有表";
            this.sel_tabs.UseVisualStyleBackColor = true;
            this.sel_tabs.CheckedChanged += new System.EventHandler(this.sel_tabs_CheckedChanged);
            // 
            // txt_NameSpace
            // 
            this.txt_NameSpace.Location = new System.Drawing.Point(107, 48);
            this.txt_NameSpace.Name = "txt_NameSpace";
            this.txt_NameSpace.Size = new System.Drawing.Size(279, 21);
            this.txt_NameSpace.TabIndex = 8;
            this.txt_NameSpace.Text = "Com.Sureba.NHibernate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "输入命名空间:";
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Location = new System.Drawing.Point(107, 253);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveConfig.TabIndex = 10;
            this.btn_SaveConfig.Text = "保存配置";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // cbx_temp
            // 
            this.cbx_temp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_temp.FormattingEnabled = true;
            this.cbx_temp.Location = new System.Drawing.Point(107, 297);
            this.cbx_temp.Name = "cbx_temp";
            this.cbx_temp.Size = new System.Drawing.Size(160, 20);
            this.cbx_temp.TabIndex = 17;
            this.cbx_temp.SelectedIndexChanged += new System.EventHandler(this.cbx_temp_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(12, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "加载现有配置：";
            // 
            // btn_Generate
            // 
            this.btn_Generate.Location = new System.Drawing.Point(14, 374);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(75, 23);
            this.btn_Generate.TabIndex = 18;
            this.btn_Generate.Text = "立即生成";
            this.btn_Generate.UseVisualStyleBackColor = true;
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "输入保存位置:";
            // 
            // txt_Path
            // 
            this.txt_Path.Location = new System.Drawing.Point(107, 100);
            this.txt_Path.Name = "txt_Path";
            this.txt_Path.Size = new System.Drawing.Size(279, 21);
            this.txt_Path.TabIndex = 20;
            this.txt_Path.Text = "D:\\NHibernate\\Project\\GeneratedCode";
            // 
            // ckbIsGenerateViewModel
            // 
            this.ckbIsGenerateViewModel.AutoSize = true;
            this.ckbIsGenerateViewModel.Location = new System.Drawing.Point(151, 380);
            this.ckbIsGenerateViewModel.Name = "ckbIsGenerateViewModel";
            this.ckbIsGenerateViewModel.Size = new System.Drawing.Size(90, 16);
            this.ckbIsGenerateViewModel.TabIndex = 21;
            this.ckbIsGenerateViewModel.Text = "是否生成DTO";
            this.ckbIsGenerateViewModel.UseVisualStyleBackColor = true;
            this.ckbIsGenerateViewModel.Visible = false;
            // 
            // lbl_SkipStartName
            // 
            this.lbl_SkipStartName.AutoSize = true;
            this.lbl_SkipStartName.Location = new System.Drawing.Point(12, 151);
            this.lbl_SkipStartName.Name = "lbl_SkipStartName";
            this.lbl_SkipStartName.Size = new System.Drawing.Size(83, 12);
            this.lbl_SkipStartName.TabIndex = 22;
            this.lbl_SkipStartName.Text = "过滤表名前缀:";
            // 
            // txtStartName
            // 
            this.txtStartName.Location = new System.Drawing.Point(107, 151);
            this.txtStartName.Multiline = true;
            this.txtStartName.Name = "txtStartName";
            this.txtStartName.Size = new System.Drawing.Size(279, 58);
            this.txtStartName.TabIndex = 23;
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(105, 224);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(167, 12);
            this.lblTips.TabIndex = 24;
            this.lblTips.Text = "*多个前缀名称以英文分号隔开";
            // 
            // ckb_IsNewVersion
            // 
            this.ckb_IsNewVersion.AutoSize = true;
            this.ckb_IsNewVersion.Location = new System.Drawing.Point(512, 48);
            this.ckb_IsNewVersion.Name = "ckb_IsNewVersion";
            this.ckb_IsNewVersion.Size = new System.Drawing.Size(72, 16);
            this.ckb_IsNewVersion.TabIndex = 25;
            this.ckb_IsNewVersion.Text = "是否新版";
            this.ckb_IsNewVersion.UseVisualStyleBackColor = true;
            // 
            // NHibernateTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(618, 470);
            this.Controls.Add(this.ckb_IsNewVersion);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.txtStartName);
            this.Controls.Add(this.lbl_SkipStartName);
            this.Controls.Add(this.ckbIsGenerateViewModel);
            this.Controls.Add(this.txt_Path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Generate);
            this.Controls.Add(this.cbx_temp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_SaveConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_NameSpace);
            this.Controls.Add(this.sel_tabs);
            this.Controls.Add(this.lv_table);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txt_ConnString);
            this.Controls.Add(this.label1);
            this.Name = "NHibernateTools";
            this.Text = "NHibernateTools";
            this.Load += new System.EventHandler(this.NHibernateTools_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ConnString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.ListView lv_table;
        private System.Windows.Forms.CheckBox sel_tabs;
        private System.Windows.Forms.TextBox txt_NameSpace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_SaveConfig;
        private System.Windows.Forms.ComboBox cbx_temp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Path;
        private System.Windows.Forms.CheckBox ckbIsGenerateViewModel;
        private System.Windows.Forms.Label lbl_SkipStartName;
        private System.Windows.Forms.TextBox txtStartName;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.CheckBox ckb_IsNewVersion;
    }
}

