using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace E38EDM.TTD
{
    partial class FrmListElements
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 

        public class RoundButton : Button
        {
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                GraphicsPath grPath = new GraphicsPath();
                grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
                this.Region = new System.Drawing.Region(grPath);
                base.OnPaint(e);
            }
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Export = new System.Windows.Forms.Button();
            this.grv_category = new System.Windows.Forms.DataGridView();
            this.btn_Search = new System.Windows.Forms.Button();
            this.grv_element = new System.Windows.Forms.DataGridView();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Options = new System.Windows.Forms.Button();
            this.btn_Zoomto = new System.Windows.Forms.Button();
            this.btn_searchall = new System.Windows.Forms.Button();
            this.cb_level = new System.Windows.Forms.ComboBox();
            this.checkbox_check = new System.Windows.Forms.CheckBox();
            this.btn_updatedata = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.ctm_description = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tb_importtxt = new System.Windows.Forms.TextBox();
            this.btn_loadimporttxt = new System.Windows.Forms.Button();
            this.btn_importtxt = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lv_username = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bnt_sync = new System.Windows.Forms.Button();
            this.btn_createlocal = new System.Windows.Forms.Button();
            this.cb_projectcollaborate = new System.Windows.Forms.ComboBox();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_clearmark = new System.Windows.Forms.Button();
            this.tb_showmark = new System.Windows.Forms.TextBox();
            this.btn_import = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_connect = new System.Windows.Forms.Label();
            this.lb_swich = new System.Windows.Forms.Label();
            this.cb_project = new System.Windows.Forms.ComboBox();
            this.btn_delete = new System.Windows.Forms.Button();
            this.lv_savemark = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_loadmark = new System.Windows.Forms.Button();
            this.tb_savemark = new System.Windows.Forms.TextBox();
            this.btn_swich = new System.Windows.Forms.Button();
            this.btn_savemark = new System.Windows.Forms.Button();
            this.rbn_connect = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_filterserch = new System.Windows.Forms.Button();
            this.btn_apply = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_code1tocode6 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_family = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chb_sortall = new System.Windows.Forms.CheckBox();
            this.btn_getdataheader = new System.Windows.Forms.Button();
            this.rd_ztoa = new System.Windows.Forms.RadioButton();
            this.cb_dataheader = new System.Windows.Forms.ComboBox();
            this.rd_atoz = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_sort = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_findsamevaluetype = new System.Windows.Forms.Button();
            this.btn_findmistake = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.grv_define = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_removealldefine = new System.Windows.Forms.Button();
            this.btn_adddefine = new System.Windows.Forms.Button();
            this.btn_minusdefine = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btn_view3D = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btn_findviews = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grv_category)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_element)).BeginInit();
            this.ctm_description.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_define)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Export
            // 
            this.btn_Export.BackColor = System.Drawing.Color.White;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Location = new System.Drawing.Point(646, 620);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(140, 37);
            this.btn_Export.TabIndex = 2;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = false;
            // 
            // grv_category
            // 
            this.grv_category.AllowUserToAddRows = false;
            this.grv_category.AllowUserToDeleteRows = false;
            this.grv_category.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grv_category.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grv_category.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_category.Location = new System.Drawing.Point(2, 82);
            this.grv_category.MultiSelect = false;
            this.grv_category.Name = "grv_category";
            this.grv_category.ReadOnly = true;
            this.grv_category.RowTemplate.Height = 150;
            this.grv_category.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grv_category.Size = new System.Drawing.Size(162, 575);
            this.grv_category.TabIndex = 3;
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.White;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(169, 245);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(79, 37);
            this.btn_Search.TabIndex = 2;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // grv_element
            // 
            this.grv_element.AllowUserToAddRows = false;
            this.grv_element.AllowUserToDeleteRows = false;
            this.grv_element.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grv_element.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grv_element.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grv_element.ColumnHeadersHeight = 20;
            this.grv_element.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grv_element.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grv_element.Location = new System.Drawing.Point(254, 12);
            this.grv_element.Name = "grv_element";
            this.grv_element.RowTemplate.Height = 150;
            this.grv_element.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grv_element.Size = new System.Drawing.Size(929, 601);
            this.grv_element.TabIndex = 4;
            this.grv_element.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grv_element_CellClick);
            this.grv_element.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grv_element_CellValueChanged);
            this.grv_element.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.grv_element_RowPrePaint);
            this.grv_element.SelectionChanged += new System.EventHandler(this.grv_element_SelectionChanged);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.White;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(1039, 620);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(140, 37);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Close";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Options
            // 
            this.btn_Options.BackColor = System.Drawing.Color.White;
            this.btn_Options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Options.Location = new System.Drawing.Point(169, 303);
            this.btn_Options.Name = "btn_Options";
            this.btn_Options.Size = new System.Drawing.Size(79, 37);
            this.btn_Options.TabIndex = 2;
            this.btn_Options.Text = "Options";
            this.btn_Options.UseVisualStyleBackColor = false;
            this.btn_Options.Click += new System.EventHandler(this.btn_Options_Click);
            // 
            // btn_Zoomto
            // 
            this.btn_Zoomto.BackColor = System.Drawing.Color.White;
            this.btn_Zoomto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Zoomto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Zoomto.Location = new System.Drawing.Point(254, 620);
            this.btn_Zoomto.Name = "btn_Zoomto";
            this.btn_Zoomto.Size = new System.Drawing.Size(140, 37);
            this.btn_Zoomto.TabIndex = 2;
            this.btn_Zoomto.Text = "Go To Model";
            this.btn_Zoomto.UseVisualStyleBackColor = false;
            this.btn_Zoomto.Click += new System.EventHandler(this.btn_Zoomto_Click);
            // 
            // btn_searchall
            // 
            this.btn_searchall.BackColor = System.Drawing.Color.White;
            this.btn_searchall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_searchall.Location = new System.Drawing.Point(169, 360);
            this.btn_searchall.Name = "btn_searchall";
            this.btn_searchall.Size = new System.Drawing.Size(79, 37);
            this.btn_searchall.TabIndex = 2;
            this.btn_searchall.Text = "Search All";
            this.btn_searchall.UseVisualStyleBackColor = false;
            this.btn_searchall.Click += new System.EventHandler(this.btn_searchall_Click);
            // 
            // cb_level
            // 
            this.cb_level.BackColor = System.Drawing.Color.White;
            this.cb_level.Enabled = false;
            this.cb_level.ForeColor = System.Drawing.Color.Black;
            this.cb_level.FormattingEnabled = true;
            this.cb_level.Location = new System.Drawing.Point(169, 196);
            this.cb_level.Name = "cb_level";
            this.cb_level.Size = new System.Drawing.Size(79, 21);
            this.cb_level.TabIndex = 5;
            this.cb_level.SelectedValueChanged += new System.EventHandler(this.cb_level_SelectedValueChanged);
            // 
            // checkbox_check
            // 
            this.checkbox_check.AutoSize = true;
            this.checkbox_check.BackColor = System.Drawing.Color.Gray;
            this.checkbox_check.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkbox_check.Location = new System.Drawing.Point(170, 170);
            this.checkbox_check.Name = "checkbox_check";
            this.checkbox_check.Size = new System.Drawing.Size(66, 20);
            this.checkbox_check.TabIndex = 6;
            this.checkbox_check.Text = "By Level";
            this.checkbox_check.UseVisualStyleBackColor = false;
            this.checkbox_check.CheckedChanged += new System.EventHandler(this.checkbox_check_CheckedChanged);
            // 
            // btn_updatedata
            // 
            this.btn_updatedata.BackColor = System.Drawing.Color.White;
            this.btn_updatedata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_updatedata.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_updatedata.Location = new System.Drawing.Point(846, 620);
            this.btn_updatedata.Name = "btn_updatedata";
            this.btn_updatedata.Size = new System.Drawing.Size(140, 37);
            this.btn_updatedata.TabIndex = 2;
            this.btn_updatedata.Text = "Update Data";
            this.btn_updatedata.UseVisualStyleBackColor = false;
            this.btn_updatedata.Click += new System.EventHandler(this.btn_updatedata_Click);
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.White;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(448, 620);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(140, 37);
            this.btn_print.TabIndex = 2;
            this.btn_print.Text = "Import Data";
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // ctm_description
            // 
            this.ctm_description.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descriptionToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.sendEmailToolStripMenuItem});
            this.ctm_description.Name = "ctm_description";
            this.ctm_description.Size = new System.Drawing.Size(135, 70);
            // 
            // descriptionToolStripMenuItem
            // 
            this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
            this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.descriptionToolStripMenuItem.Text = "Description";
            this.descriptionToolStripMenuItem.Click += new System.EventHandler(this.descriptionToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attachDescriptionToolStripMenuItem,
            this.noDescriptionToolStripMenuItem});
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            // 
            // attachDescriptionToolStripMenuItem
            // 
            this.attachDescriptionToolStripMenuItem.Name = "attachDescriptionToolStripMenuItem";
            this.attachDescriptionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.attachDescriptionToolStripMenuItem.Text = "Attach Description";
            this.attachDescriptionToolStripMenuItem.Click += new System.EventHandler(this.attachDescriptionToolStripMenuItem_Click);
            // 
            // noDescriptionToolStripMenuItem
            // 
            this.noDescriptionToolStripMenuItem.Name = "noDescriptionToolStripMenuItem";
            this.noDescriptionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.noDescriptionToolStripMenuItem.Text = "No Description";
            this.noDescriptionToolStripMenuItem.Click += new System.EventHandler(this.noDescriptionToolStripMenuItem_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gray;
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(207, 618);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Online";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox11.Controls.Add(this.button2);
            this.groupBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.Location = new System.Drawing.Point(3, 162);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(206, 86);
            this.groupBox11.TabIndex = 3;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Export To HTML";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(27, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 34);
            this.button2.TabIndex = 0;
            this.button2.Text = "Export";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox7.Controls.Add(this.tb_importtxt);
            this.groupBox7.Controls.Add(this.btn_loadimporttxt);
            this.groupBox7.Controls.Add(this.btn_importtxt);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(206, 153);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Get Data";
            // 
            // tb_importtxt
            // 
            this.tb_importtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_importtxt.Location = new System.Drawing.Point(6, 22);
            this.tb_importtxt.Multiline = true;
            this.tb_importtxt.Name = "tb_importtxt";
            this.tb_importtxt.Size = new System.Drawing.Size(195, 57);
            this.tb_importtxt.TabIndex = 1;
            // 
            // btn_loadimporttxt
            // 
            this.btn_loadimporttxt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_loadimporttxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_loadimporttxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loadimporttxt.Location = new System.Drawing.Point(120, 96);
            this.btn_loadimporttxt.Name = "btn_loadimporttxt";
            this.btn_loadimporttxt.Size = new System.Drawing.Size(82, 34);
            this.btn_loadimporttxt.TabIndex = 0;
            this.btn_loadimporttxt.Text = "Load";
            this.btn_loadimporttxt.UseVisualStyleBackColor = false;
            this.btn_loadimporttxt.Click += new System.EventHandler(this.btn_loadimporttxt_Click);
            // 
            // btn_importtxt
            // 
            this.btn_importtxt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_importtxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_importtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_importtxt.Location = new System.Drawing.Point(7, 96);
            this.btn_importtxt.Name = "btn_importtxt";
            this.btn_importtxt.Size = new System.Drawing.Size(82, 34);
            this.btn_importtxt.TabIndex = 0;
            this.btn_importtxt.Text = "Import";
            this.btn_importtxt.UseVisualStyleBackColor = false;
            this.btn_importtxt.Click += new System.EventHandler(this.btn_importtxt_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gray;
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(207, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Edit Data";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.lv_username);
            this.groupBox6.Controls.Add(this.bnt_sync);
            this.groupBox6.Controls.Add(this.btn_createlocal);
            this.groupBox6.Controls.Add(this.cb_projectcollaborate);
            this.groupBox6.Controls.Add(this.btn_upload);
            this.groupBox6.Controls.Add(this.btn_download);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(2, 266);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(204, 214);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Collaborate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Projects";
            // 
            // lv_username
            // 
            this.lv_username.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lv_username.AllowDrop = true;
            this.lv_username.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lv_username.CheckBoxes = true;
            this.lv_username.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lv_username.Font = new System.Drawing.Font("Arial Narrow", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_username.FullRowSelect = true;
            this.lv_username.GridLines = true;
            this.lv_username.HideSelection = false;
            this.lv_username.Location = new System.Drawing.Point(4, 20);
            this.lv_username.Name = "lv_username";
            this.lv_username.Size = new System.Drawing.Size(195, 117);
            this.lv_username.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_username.TabIndex = 13;
            this.lv_username.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "User Name";
            this.columnHeader2.Width = 200;
            // 
            // bnt_sync
            // 
            this.bnt_sync.BackColor = System.Drawing.Color.Silver;
            this.bnt_sync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnt_sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_sync.Location = new System.Drawing.Point(162, 173);
            this.bnt_sync.Name = "bnt_sync";
            this.bnt_sync.Size = new System.Drawing.Size(37, 33);
            this.bnt_sync.TabIndex = 14;
            this.bnt_sync.UseVisualStyleBackColor = false;
            this.bnt_sync.Click += new System.EventHandler(this.bnt_sync_Click);
            // 
            // btn_createlocal
            // 
            this.btn_createlocal.BackColor = System.Drawing.Color.Silver;
            this.btn_createlocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_createlocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_createlocal.Location = new System.Drawing.Point(30, 173);
            this.btn_createlocal.Name = "btn_createlocal";
            this.btn_createlocal.Size = new System.Drawing.Size(37, 33);
            this.btn_createlocal.TabIndex = 14;
            this.btn_createlocal.UseVisualStyleBackColor = false;
            this.btn_createlocal.Click += new System.EventHandler(this.btn_createlocal_Click);
            // 
            // cb_projectcollaborate
            // 
            this.cb_projectcollaborate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_projectcollaborate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_projectcollaborate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_projectcollaborate.FormattingEnabled = true;
            this.cb_projectcollaborate.Items.AddRange(new object[] {
            "Default"});
            this.cb_projectcollaborate.Location = new System.Drawing.Point(72, 144);
            this.cb_projectcollaborate.Name = "cb_projectcollaborate";
            this.cb_projectcollaborate.Size = new System.Drawing.Size(126, 24);
            this.cb_projectcollaborate.TabIndex = 15;
            this.cb_projectcollaborate.SelectedIndexChanged += new System.EventHandler(this.cb_projectcollaborate_SelectedIndexChanged);
            this.cb_projectcollaborate.SelectedValueChanged += new System.EventHandler(this.cb_projectcollaborate_SelectedValueChanged);
            // 
            // btn_upload
            // 
            this.btn_upload.BackColor = System.Drawing.Color.Silver;
            this.btn_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_upload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_upload.Location = new System.Drawing.Point(75, 173);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(37, 33);
            this.btn_upload.TabIndex = 14;
            this.btn_upload.UseVisualStyleBackColor = false;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // btn_download
            // 
            this.btn_download.BackColor = System.Drawing.Color.Silver;
            this.btn_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_download.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_download.Location = new System.Drawing.Point(119, 173);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(36, 33);
            this.btn_download.TabIndex = 14;
            this.btn_download.UseVisualStyleBackColor = false;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox5.Controls.Add(this.btn_clearmark);
            this.groupBox5.Controls.Add(this.tb_showmark);
            this.groupBox5.Controls.Add(this.btn_import);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(2, 486);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(203, 126);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mark/UnMark";
            // 
            // btn_clearmark
            // 
            this.btn_clearmark.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_clearmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clearmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clearmark.Location = new System.Drawing.Point(112, 87);
            this.btn_clearmark.Name = "btn_clearmark";
            this.btn_clearmark.Size = new System.Drawing.Size(85, 31);
            this.btn_clearmark.TabIndex = 14;
            this.btn_clearmark.Text = "Clear Mark";
            this.btn_clearmark.UseVisualStyleBackColor = false;
            this.btn_clearmark.Click += new System.EventHandler(this.btn_clearmark_Click);
            // 
            // tb_showmark
            // 
            this.tb_showmark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_showmark.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_showmark.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_showmark.Location = new System.Drawing.Point(3, 25);
            this.tb_showmark.Multiline = true;
            this.tb_showmark.Name = "tb_showmark";
            this.tb_showmark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_showmark.Size = new System.Drawing.Size(195, 55);
            this.tb_showmark.TabIndex = 12;
            // 
            // btn_import
            // 
            this.btn_import.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_import.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import.Location = new System.Drawing.Point(3, 86);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(103, 32);
            this.btn_import.TabIndex = 2;
            this.btn_import.Text = "Mark/UnMark";
            this.btn_import.UseVisualStyleBackColor = false;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.lb_connect);
            this.groupBox4.Controls.Add(this.lb_swich);
            this.groupBox4.Controls.Add(this.cb_project);
            this.groupBox4.Controls.Add(this.btn_delete);
            this.groupBox4.Controls.Add(this.lv_savemark);
            this.groupBox4.Controls.Add(this.btn_loadmark);
            this.groupBox4.Controls.Add(this.tb_savemark);
            this.groupBox4.Controls.Add(this.btn_swich);
            this.groupBox4.Controls.Add(this.btn_savemark);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(2, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 253);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Save Data";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Projects";
            // 
            // lb_connect
            // 
            this.lb_connect.AutoSize = true;
            this.lb_connect.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_connect.Location = new System.Drawing.Point(22, 55);
            this.lb_connect.Name = "lb_connect";
            this.lb_connect.Size = new System.Drawing.Size(35, 15);
            this.lb_connect.TabIndex = 15;
            this.lb_connect.Text = "Local";
            // 
            // lb_swich
            // 
            this.lb_swich.AutoSize = true;
            this.lb_swich.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_swich.Location = new System.Drawing.Point(119, 54);
            this.lb_swich.Name = "lb_swich";
            this.lb_swich.Size = new System.Drawing.Size(34, 15);
            this.lb_swich.TabIndex = 15;
            this.lb_swich.Text = "Local";
            this.lb_swich.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cb_project
            // 
            this.cb_project.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_project.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_project.FormattingEnabled = true;
            this.cb_project.Location = new System.Drawing.Point(72, 79);
            this.cb_project.Name = "cb_project";
            this.cb_project.Size = new System.Drawing.Size(126, 24);
            this.cb_project.TabIndex = 15;
            this.cb_project.SelectedIndexChanged += new System.EventHandler(this.cb_project_SelectedIndexChanged);
            this.cb_project.SelectedValueChanged += new System.EventHandler(this.cb_project_SelectedValueChanged);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(139, 211);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(62, 31);
            this.btn_delete.TabIndex = 14;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // lv_savemark
            // 
            this.lv_savemark.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lv_savemark.AllowDrop = true;
            this.lv_savemark.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lv_savemark.CheckBoxes = true;
            this.lv_savemark.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lv_savemark.ContextMenuStrip = this.ctm_description;
            this.lv_savemark.Font = new System.Drawing.Font("Arial Narrow", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_savemark.FullRowSelect = true;
            this.lv_savemark.GridLines = true;
            this.lv_savemark.HideSelection = false;
            this.lv_savemark.Location = new System.Drawing.Point(5, 106);
            this.lv_savemark.Name = "lv_savemark";
            this.lv_savemark.Size = new System.Drawing.Size(195, 99);
            this.lv_savemark.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_savemark.TabIndex = 13;
            this.lv_savemark.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Data Name";
            this.columnHeader1.Width = 200;
            // 
            // btn_loadmark
            // 
            this.btn_loadmark.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_loadmark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_loadmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loadmark.Location = new System.Drawing.Point(74, 211);
            this.btn_loadmark.Name = "btn_loadmark";
            this.btn_loadmark.Size = new System.Drawing.Size(59, 31);
            this.btn_loadmark.TabIndex = 14;
            this.btn_loadmark.Text = "Load";
            this.btn_loadmark.UseVisualStyleBackColor = false;
            this.btn_loadmark.Click += new System.EventHandler(this.btn_loadmark_Click);
            // 
            // tb_savemark
            // 
            this.tb_savemark.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_savemark.Location = new System.Drawing.Point(3, 21);
            this.tb_savemark.Name = "tb_savemark";
            this.tb_savemark.Size = new System.Drawing.Size(196, 23);
            this.tb_savemark.TabIndex = 12;
            this.tb_savemark.Click += new System.EventHandler(this.tb_savemark_Click);
            // 
            // btn_swich
            // 
            this.btn_swich.BackColor = System.Drawing.Color.LightGreen;
            this.btn_swich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_swich.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_swich.Location = new System.Drawing.Point(161, 44);
            this.btn_swich.Name = "btn_swich";
            this.btn_swich.Size = new System.Drawing.Size(37, 33);
            this.btn_swich.TabIndex = 14;
            this.btn_swich.UseVisualStyleBackColor = false;
            this.btn_swich.BackColorChanged += new System.EventHandler(this.btn_swich_BackColorChanged);
            this.btn_swich.Click += new System.EventHandler(this.btn_swich_Click);
            // 
            // btn_savemark
            // 
            this.btn_savemark.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_savemark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_savemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_savemark.Location = new System.Drawing.Point(6, 211);
            this.btn_savemark.Name = "btn_savemark";
            this.btn_savemark.Size = new System.Drawing.Size(62, 31);
            this.btn_savemark.TabIndex = 14;
            this.btn_savemark.Text = "Save";
            this.btn_savemark.UseVisualStyleBackColor = false;
            this.btn_savemark.Click += new System.EventHandler(this.btn_savemark_Click);
            // 
            // rbn_connect
            // 
            this.rbn_connect.BackColor = System.Drawing.Color.Teal;
            this.rbn_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbn_connect.Location = new System.Drawing.Point(7, 58);
            this.rbn_connect.Name = "rbn_connect";
            this.rbn_connect.Size = new System.Drawing.Size(10, 11);
            this.rbn_connect.TabIndex = 17;
            this.rbn_connect.Text = "roundButton1";
            this.rbn_connect.UseVisualStyleBackColor = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gray;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(207, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Processing";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox1.Controls.Add(this.btn_filterserch);
            this.groupBox1.Controls.Add(this.btn_apply);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cb_code1tocode6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_family);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 220);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btn_filterserch
            // 
            this.btn_filterserch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_filterserch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_filterserch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_filterserch.ForeColor = System.Drawing.Color.Black;
            this.btn_filterserch.Location = new System.Drawing.Point(50, 164);
            this.btn_filterserch.Name = "btn_filterserch";
            this.btn_filterserch.Size = new System.Drawing.Size(113, 37);
            this.btn_filterserch.TabIndex = 10;
            this.btn_filterserch.Text = "Search";
            this.btn_filterserch.UseVisualStyleBackColor = false;
            this.btn_filterserch.Click += new System.EventHandler(this.btn_filterserch_Click);
            // 
            // btn_apply
            // 
            this.btn_apply.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_apply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_apply.ForeColor = System.Drawing.Color.Black;
            this.btn_apply.Location = new System.Drawing.Point(4, 24);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(186, 33);
            this.btn_apply.TabIndex = 10;
            this.btn_apply.Text = "Get Data Table / Reset";
            this.btn_apply.UseVisualStyleBackColor = false;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkGray;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Code1ToCode6";
            // 
            // cb_code1tocode6
            // 
            this.cb_code1tocode6.AllowDrop = true;
            this.cb_code1tocode6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_code1tocode6.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_code1tocode6.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_code1tocode6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_code1tocode6.Enabled = false;
            this.cb_code1tocode6.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_code1tocode6.FormattingEnabled = true;
            this.cb_code1tocode6.Items.AddRange(new object[] {
            "None"});
            this.cb_code1tocode6.Location = new System.Drawing.Point(89, 120);
            this.cb_code1tocode6.Name = "cb_code1tocode6";
            this.cb_code1tocode6.Size = new System.Drawing.Size(101, 21);
            this.cb_code1tocode6.Sorted = true;
            this.cb_code1tocode6.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(4, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Family";
            // 
            // cb_family
            // 
            this.cb_family.AllowDrop = true;
            this.cb_family.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_family.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_family.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_family.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_family.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_family.FormattingEnabled = true;
            this.cb_family.Location = new System.Drawing.Point(50, 76);
            this.cb_family.Name = "cb_family";
            this.cb_family.Size = new System.Drawing.Size(141, 21);
            this.cb_family.Sorted = true;
            this.cb_family.TabIndex = 8;
            this.cb_family.SelectedIndexChanged += new System.EventHandler(this.cb_family_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox3.Controls.Add(this.chb_sortall);
            this.groupBox3.Controls.Add(this.btn_getdataheader);
            this.groupBox3.Controls.Add(this.rd_ztoa);
            this.groupBox3.Controls.Add(this.cb_dataheader);
            this.groupBox3.Controls.Add(this.rd_atoz);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btn_sort);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(6, 395);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 217);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sorting";
            // 
            // chb_sortall
            // 
            this.chb_sortall.AutoSize = true;
            this.chb_sortall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_sortall.ForeColor = System.Drawing.Color.Black;
            this.chb_sortall.Location = new System.Drawing.Point(78, 180);
            this.chb_sortall.Name = "chb_sortall";
            this.chb_sortall.Size = new System.Drawing.Size(97, 19);
            this.chb_sortall.TabIndex = 13;
            this.chb_sortall.Text = "For Sort All";
            this.chb_sortall.UseVisualStyleBackColor = true;
            // 
            // btn_getdataheader
            // 
            this.btn_getdataheader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_getdataheader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_getdataheader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_getdataheader.ForeColor = System.Drawing.Color.Black;
            this.btn_getdataheader.Location = new System.Drawing.Point(4, 32);
            this.btn_getdataheader.Name = "btn_getdataheader";
            this.btn_getdataheader.Size = new System.Drawing.Size(186, 33);
            this.btn_getdataheader.TabIndex = 10;
            this.btn_getdataheader.Text = "Get Data Table / Reset";
            this.btn_getdataheader.UseVisualStyleBackColor = false;
            this.btn_getdataheader.Click += new System.EventHandler(this.btn_getdataheader_Click);
            // 
            // rd_ztoa
            // 
            this.rd_ztoa.AutoSize = true;
            this.rd_ztoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ztoa.ForeColor = System.Drawing.Color.Black;
            this.rd_ztoa.Location = new System.Drawing.Point(4, 149);
            this.rd_ztoa.Name = "rd_ztoa";
            this.rd_ztoa.Size = new System.Drawing.Size(59, 19);
            this.rd_ztoa.TabIndex = 12;
            this.rd_ztoa.Text = "Z-->A";
            this.rd_ztoa.UseVisualStyleBackColor = true;
            // 
            // cb_dataheader
            // 
            this.cb_dataheader.AllowDrop = true;
            this.cb_dataheader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_dataheader.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_dataheader.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_dataheader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dataheader.Font = new System.Drawing.Font("Times New Roman", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_dataheader.FormattingEnabled = true;
            this.cb_dataheader.Location = new System.Drawing.Point(50, 89);
            this.cb_dataheader.Name = "cb_dataheader";
            this.cb_dataheader.Size = new System.Drawing.Size(141, 21);
            this.cb_dataheader.Sorted = true;
            this.cb_dataheader.TabIndex = 8;
            this.cb_dataheader.SelectedIndexChanged += new System.EventHandler(this.cb_family_SelectedIndexChanged);
            // 
            // rd_atoz
            // 
            this.rd_atoz.AutoSize = true;
            this.rd_atoz.Checked = true;
            this.rd_atoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_atoz.ForeColor = System.Drawing.Color.Black;
            this.rd_atoz.Location = new System.Drawing.Point(4, 126);
            this.rd_atoz.Name = "rd_atoz";
            this.rd_atoz.Size = new System.Drawing.Size(59, 19);
            this.rd_atoz.TabIndex = 12;
            this.rd_atoz.TabStop = true;
            this.rd_atoz.Text = "A-->Z";
            this.rd_atoz.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Header";
            // 
            // btn_sort
            // 
            this.btn_sort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_sort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sort.ForeColor = System.Drawing.Color.Black;
            this.btn_sort.Location = new System.Drawing.Point(77, 128);
            this.btn_sort.Name = "btn_sort";
            this.btn_sort.Size = new System.Drawing.Size(113, 37);
            this.btn_sort.TabIndex = 10;
            this.btn_sort.Text = "Sorting";
            this.btn_sort.UseVisualStyleBackColor = false;
            this.btn_sort.Click += new System.EventHandler(this.btn_sort_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox2.Controls.Add(this.btn_findsamevaluetype);
            this.groupBox2.Controls.Add(this.btn_findmistake);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(5, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 160);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Check Mistakes";
            // 
            // btn_findsamevaluetype
            // 
            this.btn_findsamevaluetype.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_findsamevaluetype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_findsamevaluetype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_findsamevaluetype.ForeColor = System.Drawing.Color.Black;
            this.btn_findsamevaluetype.Location = new System.Drawing.Point(7, 96);
            this.btn_findsamevaluetype.Name = "btn_findsamevaluetype";
            this.btn_findsamevaluetype.Size = new System.Drawing.Size(183, 37);
            this.btn_findsamevaluetype.TabIndex = 10;
            this.btn_findsamevaluetype.Text = "Value Type+Des+Code";
            this.btn_findsamevaluetype.UseVisualStyleBackColor = false;
            this.btn_findsamevaluetype.Click += new System.EventHandler(this.btn_findsamevaluetype_Click);
            // 
            // btn_findmistake
            // 
            this.btn_findmistake.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_findmistake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_findmistake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_findmistake.ForeColor = System.Drawing.Color.Black;
            this.btn_findmistake.Location = new System.Drawing.Point(7, 34);
            this.btn_findmistake.Name = "btn_findmistake";
            this.btn_findmistake.Size = new System.Drawing.Size(183, 36);
            this.btn_findmistake.TabIndex = 10;
            this.btn_findmistake.Text = "Same Type + Code";
            this.btn_findmistake.UseVisualStyleBackColor = false;
            this.btn_findmistake.Click += new System.EventHandler(this.btn_findmistake_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(1183, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(215, 644);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Gray;
            this.tabPage4.Controls.Add(this.groupBox8);
            this.tabPage4.Controls.Add(this.groupBox10);
            this.tabPage4.Controls.Add(this.groupBox9);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(207, 618);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Define";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox8.Controls.Add(this.grv_define);
            this.groupBox8.Controls.Add(this.btn_removealldefine);
            this.groupBox8.Controls.Add(this.btn_adddefine);
            this.groupBox8.Controls.Add(this.btn_minusdefine);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(0, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(205, 191);
            this.groupBox8.TabIndex = 17;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Define Parameters";
            // 
            // grv_define
            // 
            this.grv_define.AllowUserToAddRows = false;
            this.grv_define.AllowUserToDeleteRows = false;
            this.grv_define.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_define.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.grv_define.Location = new System.Drawing.Point(4, 21);
            this.grv_define.Name = "grv_define";
            this.grv_define.Size = new System.Drawing.Size(163, 162);
            this.grv_define.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Parameter";
            this.Column1.Name = "Column1";
            // 
            // btn_removealldefine
            // 
            this.btn_removealldefine.BackColor = System.Drawing.Color.Silver;
            this.btn_removealldefine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_removealldefine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_removealldefine.Location = new System.Drawing.Point(171, 99);
            this.btn_removealldefine.Name = "btn_removealldefine";
            this.btn_removealldefine.Size = new System.Drawing.Size(33, 33);
            this.btn_removealldefine.TabIndex = 15;
            this.btn_removealldefine.UseVisualStyleBackColor = false;
            this.btn_removealldefine.Click += new System.EventHandler(this.btn_removealldefine_Click_1);
            // 
            // btn_adddefine
            // 
            this.btn_adddefine.BackColor = System.Drawing.Color.Silver;
            this.btn_adddefine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_adddefine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_adddefine.Location = new System.Drawing.Point(171, 21);
            this.btn_adddefine.Name = "btn_adddefine";
            this.btn_adddefine.Size = new System.Drawing.Size(33, 33);
            this.btn_adddefine.TabIndex = 15;
            this.btn_adddefine.UseVisualStyleBackColor = false;
            this.btn_adddefine.Click += new System.EventHandler(this.btn_adddefine_Click_1);
            // 
            // btn_minusdefine
            // 
            this.btn_minusdefine.BackColor = System.Drawing.Color.Silver;
            this.btn_minusdefine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minusdefine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_minusdefine.Location = new System.Drawing.Point(171, 60);
            this.btn_minusdefine.Name = "btn_minusdefine";
            this.btn_minusdefine.Size = new System.Drawing.Size(33, 33);
            this.btn_minusdefine.TabIndex = 15;
            this.btn_minusdefine.UseVisualStyleBackColor = false;
            this.btn_minusdefine.Click += new System.EventHandler(this.btn_minusdefine_Click_1);
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox10.Controls.Add(this.btn_view3D);
            this.groupBox10.Controls.Add(this.button1);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(0, 281);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(205, 118);
            this.groupBox10.TabIndex = 16;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Display Parameters";
            // 
            // btn_view3D
            // 
            this.btn_view3D.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_view3D.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_view3D.Location = new System.Drawing.Point(6, 72);
            this.btn_view3D.Name = "btn_view3D";
            this.btn_view3D.Size = new System.Drawing.Size(193, 34);
            this.btn_view3D.TabIndex = 0;
            this.btn_view3D.Text = "View 3D";
            this.btn_view3D.UseVisualStyleBackColor = false;
            this.btn_view3D.Click += new System.EventHandler(this.btn_view3D_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Quick Watch";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox9.Controls.Add(this.btn_findviews);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(0, 202);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(205, 73);
            this.groupBox9.TabIndex = 16;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Find Views";
            // 
            // btn_findviews
            // 
            this.btn_findviews.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_findviews.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_findviews.Location = new System.Drawing.Point(6, 25);
            this.btn_findviews.Name = "btn_findviews";
            this.btn_findviews.Size = new System.Drawing.Size(193, 34);
            this.btn_findviews.TabIndex = 0;
            this.btn_findviews.Text = "Find Referring View";
            this.btn_findviews.UseVisualStyleBackColor = false;
            this.btn_findviews.Click += new System.EventHandler(this.btn_findviews_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(2, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(246, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // FrmListElements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1402, 667);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkbox_check);
            this.Controls.Add(this.cb_level);
            this.Controls.Add(this.grv_element);
            this.Controls.Add(this.grv_category);
            this.Controls.Add(this.btn_searchall);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Options);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.btn_Zoomto);
            this.Controls.Add(this.btn_updatedata);
            this.Controls.Add(this.btn_Export);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmListElements";
            this.Text = "5D Management";
            this.Load += new System.EventHandler(this.frmListElements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grv_category)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_element)).EndInit();
            this.ctm_description.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv_define)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridView grv_category;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.DataGridView grv_element;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Options;
        private System.Windows.Forms.Button btn_Zoomto;
        private System.Windows.Forms.Button btn_searchall;
        private System.Windows.Forms.ComboBox cb_level;
        private System.Windows.Forms.CheckBox checkbox_check;
        private System.Windows.Forms.Button btn_updatedata;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.ContextMenuStrip ctm_description;
        private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attachDescriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noDescriptionToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tb_importtxt;
        private System.Windows.Forms.Button btn_loadimporttxt;
        private System.Windows.Forms.Button btn_importtxt;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lv_username;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button bnt_sync;
        private System.Windows.Forms.Button btn_createlocal;
        private System.Windows.Forms.ComboBox cb_projectcollaborate;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_clearmark;
        private System.Windows.Forms.TextBox tb_showmark;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.GroupBox groupBox4;
        private RoundButton btn_connect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_connect;
        private System.Windows.Forms.Label lb_swich;
        private System.Windows.Forms.ComboBox cb_project;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.ListView lv_savemark;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btn_loadmark;
        private System.Windows.Forms.TextBox tb_savemark;
        private System.Windows.Forms.Button btn_swich;
        private System.Windows.Forms.Button btn_savemark;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_filterserch;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_code1tocode6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_family;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chb_sortall;
        private System.Windows.Forms.Button btn_getdataheader;
        private System.Windows.Forms.RadioButton rd_ztoa;
        private System.Windows.Forms.ComboBox cb_dataheader;
        private System.Windows.Forms.RadioButton rd_atoz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_sort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_findsamevaluetype;
        private System.Windows.Forms.Button btn_findmistake;
        private System.Windows.Forms.TabControl tabControl1;
        // có thay được thành btn mặc định ko?

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btn_findviews;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.DataGridView grv_define;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button btn_removealldefine;
        private System.Windows.Forms.Button btn_adddefine;
        private System.Windows.Forms.Button btn_minusdefine;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btn_view3D;
        private Button rbn_connect;
    }
}