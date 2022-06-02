namespace E38EDM.TTD
{
    partial class frmFindReferringView
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
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.STT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.chb_Zoomto = new System.Windows.Forms.CheckBox();
            this.rd_allview = new System.Windows.Forms.RadioButton();
            this.rd_viewinsheet = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.STT,
            this.ViewName});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(314, 501);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // STT
            // 
            this.STT.Text = "STT";
            // 
            // ViewName
            // 
            this.ViewName.Text = "ViewName";
            this.ViewName.Width = 250;
            // 
            // btn_Open
            // 
            this.btn_Open.BackColor = System.Drawing.Color.White;
            this.btn_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Open.Location = new System.Drawing.Point(159, 569);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(83, 27);
            this.btn_Open.TabIndex = 1;
            this.btn_Open.Text = "Open";
            this.btn_Open.UseVisualStyleBackColor = false;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.White;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(45, 569);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(83, 27);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Close";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // chb_Zoomto
            // 
            this.chb_Zoomto.AutoSize = true;
            this.chb_Zoomto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chb_Zoomto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_Zoomto.Location = new System.Drawing.Point(264, 575);
            this.chb_Zoomto.Name = "chb_Zoomto";
            this.chb_Zoomto.Size = new System.Drawing.Size(63, 17);
            this.chb_Zoomto.TabIndex = 2;
            this.chb_Zoomto.Text = "ZoomTo";
            this.chb_Zoomto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_Zoomto.UseVisualStyleBackColor = true;
            // 
            // rd_allview
            // 
            this.rd_allview.AutoSize = true;
            this.rd_allview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rd_allview.Location = new System.Drawing.Point(27, 13);
            this.rd_allview.Name = "rd_allview";
            this.rd_allview.Size = new System.Drawing.Size(61, 17);
            this.rd_allview.TabIndex = 3;
            this.rd_allview.Text = "All View";
            this.rd_allview.UseVisualStyleBackColor = true;
            this.rd_allview.CheckedChanged += new System.EventHandler(this.rd_allview_CheckedChanged);
            // 
            // rd_viewinsheet
            // 
            this.rd_viewinsheet.AutoSize = true;
            this.rd_viewinsheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rd_viewinsheet.Location = new System.Drawing.Point(133, 13);
            this.rd_viewinsheet.Name = "rd_viewinsheet";
            this.rd_viewinsheet.Size = new System.Drawing.Size(90, 17);
            this.rd_viewinsheet.TabIndex = 3;
            this.rd_viewinsheet.Text = "View In Sheet";
            this.rd_viewinsheet.UseVisualStyleBackColor = true;
            this.rd_viewinsheet.CheckedChanged += new System.EventHandler(this.rd_viewinsheet_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rd_allview);
            this.groupBox1.Controls.Add(this.rd_viewinsheet);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(18, 521);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 39);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // frmFindReferringView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(342, 608);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chb_Zoomto);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Open);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFindReferringView";
            this.Text = "FindReferringView";
            this.Load += new System.EventHandler(this.frmFindReferringView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader STT;
        private System.Windows.Forms.ColumnHeader ViewName;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.CheckBox chb_Zoomto;
        private System.Windows.Forms.RadioButton rd_allview;
        private System.Windows.Forms.RadioButton rd_viewinsheet;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}