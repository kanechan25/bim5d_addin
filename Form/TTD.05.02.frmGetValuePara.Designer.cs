namespace E38EDM.TTD
{
    partial class frmGetValuePara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGetValuePara));
            this.btn_SetValue = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_CreateFamily = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rd_qs = new System.Windows.Forms.RadioButton();
            this.rd_engi = new System.Windows.Forms.RadioButton();
            this.btn_ok = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SetValue
            // 
            this.btn_SetValue.BackColor = System.Drawing.Color.White;
            this.btn_SetValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SetValue.Location = new System.Drawing.Point(5, 91);
            this.btn_SetValue.Name = "btn_SetValue";
            this.btn_SetValue.Size = new System.Drawing.Size(390, 49);
            this.btn_SetValue.TabIndex = 0;
            this.btn_SetValue.Text = "Set System Family or Loadable Family";
            this.btn_SetValue.UseVisualStyleBackColor = false;
            this.btn_SetValue.Click += new System.EventHandler(this.btn_SetValue_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.White;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(146, 161);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(104, 40);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_CreateFamily
            // 
            this.btn_CreateFamily.BackColor = System.Drawing.Color.White;
            this.btn_CreateFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateFamily.Location = new System.Drawing.Point(5, 16);
            this.btn_CreateFamily.Name = "btn_CreateFamily";
            this.btn_CreateFamily.Size = new System.Drawing.Size(390, 49);
            this.btn_CreateFamily.TabIndex = 0;
            this.btn_CreateFamily.Text = "Create Parameters";
            this.btn_CreateFamily.UseVisualStyleBackColor = false;
            this.btn_CreateFamily.Click += new System.EventHandler(this.btn_CreateFamily_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 247);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_ok);
            this.tabPage1.Controls.Add(this.rd_engi);
            this.tabPage1.Controls.Add(this.rd_qs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 221);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_CreateFamily);
            this.tabPage2.Controls.Add(this.btn_Close);
            this.tabPage2.Controls.Add(this.btn_SetValue);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 221);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rd_qs
            // 
            this.rd_qs.AutoSize = true;
            this.rd_qs.Checked = true;
            this.rd_qs.Font = new System.Drawing.Font("Times New Roman", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_qs.Location = new System.Drawing.Point(93, 43);
            this.rd_qs.Name = "rd_qs";
            this.rd_qs.Size = new System.Drawing.Size(130, 19);
            this.rd_qs.TabIndex = 0;
            this.rd_qs.TabStop = true;
            this.rd_qs.Text = "Quantity Surveying";
            this.rd_qs.UseVisualStyleBackColor = true;
            this.rd_qs.CheckedChanged += new System.EventHandler(this.rd_qs_CheckedChanged);
            // 
            // rd_engi
            // 
            this.rd_engi.AutoSize = true;
            this.rd_engi.Font = new System.Drawing.Font("Times New Roman", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_engi.Location = new System.Drawing.Point(93, 83);
            this.rd_engi.Name = "rd_engi";
            this.rd_engi.Size = new System.Drawing.Size(89, 19);
            this.rd_engi.TabIndex = 0;
            this.rd_engi.Text = "Engineering";
            this.rd_engi.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Location = new System.Drawing.Point(281, 167);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(98, 33);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // frmGetValuePara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(413, 246);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGetValuePara";
            this.Text = "Get Value Parameters";
            this.Load += new System.EventHandler(this.frmGetValuePara_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SetValue;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_CreateFamily;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.RadioButton rd_engi;
        private System.Windows.Forms.RadioButton rd_qs;
    }
}