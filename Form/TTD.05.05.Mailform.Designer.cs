namespace E38EDM.TTD
{
    partial class Mailform
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
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_remember = new System.Windows.Forms.CheckBox();
            this.tb_sendto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_cc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_content = new System.Windows.Forms.TextBox();
            this.tb_subject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.tbn_close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_bcc = new System.Windows.Forms.TextBox();
            this.lb_attach = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_username
            // 
            this.tb_username.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_username.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_username.Location = new System.Drawing.Point(86, 20);
            this.tb_username.Multiline = true;
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(277, 32);
            this.tb_username.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name";
            // 
            // tb_password
            // 
            this.tb_password.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_password.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_password.Location = new System.Drawing.Point(86, 68);
            this.tb_password.Multiline = true;
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(277, 32);
            this.tb_password.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "PassWord";
            // 
            // chb_remember
            // 
            this.chb_remember.AutoSize = true;
            this.chb_remember.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_remember.Location = new System.Drawing.Point(268, 110);
            this.chb_remember.Name = "chb_remember";
            this.chb_remember.Size = new System.Drawing.Size(95, 21);
            this.chb_remember.TabIndex = 2;
            this.chb_remember.Text = "Remember";
            this.chb_remember.UseVisualStyleBackColor = true;
            this.chb_remember.CheckedChanged += new System.EventHandler(this.chb_remember_CheckedChanged);
            // 
            // tb_sendto
            // 
            this.tb_sendto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_sendto.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_sendto.Location = new System.Drawing.Point(86, 74);
            this.tb_sendto.Multiline = true;
            this.tb_sendto.Name = "tb_sendto";
            this.tb_sendto.Size = new System.Drawing.Size(277, 32);
            this.tb_sendto.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Send To";
            // 
            // tb_cc
            // 
            this.tb_cc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_cc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_cc.Location = new System.Drawing.Point(86, 127);
            this.tb_cc.Multiline = true;
            this.tb_cc.Name = "tb_cc";
            this.tb_cc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_cc.Size = new System.Drawing.Size(277, 90);
            this.tb_cc.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cc";
            // 
            // tb_content
            // 
            this.tb_content.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_content.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_content.Location = new System.Drawing.Point(412, 53);
            this.tb_content.Multiline = true;
            this.tb_content.Name = "tb_content";
            this.tb_content.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_content.Size = new System.Drawing.Size(409, 475);
            this.tb_content.TabIndex = 7;
            // 
            // tb_subject
            // 
            this.tb_subject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_subject.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_subject.Location = new System.Drawing.Point(86, 22);
            this.tb_subject.Multiline = true;
            this.tb_subject.Name = "tb_subject";
            this.tb_subject.Size = new System.Drawing.Size(277, 32);
            this.tb_subject.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Subject";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(409, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Content";
            // 
            // btn_send
            // 
            this.btn_send.BackColor = System.Drawing.Color.White;
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send.Location = new System.Drawing.Point(169, 509);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(88, 33);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tbn_close
            // 
            this.tbn_close.BackColor = System.Drawing.Color.White;
            this.tbn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbn_close.Location = new System.Drawing.Point(273, 509);
            this.tbn_close.Name = "tbn_close";
            this.tbn_close.Size = new System.Drawing.Size(88, 33);
            this.tbn_close.TabIndex = 3;
            this.tbn_close.Text = "Close";
            this.tbn_close.UseVisualStyleBackColor = false;
            this.tbn_close.Click += new System.EventHandler(this.tbn_close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_username);
            this.groupBox1.Controls.Add(this.tb_password);
            this.groupBox1.Controls.Add(this.chb_remember);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 134);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sign In";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tb_sendto);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_subject);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tb_bcc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_cc);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 334);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Andress";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 234);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "BCc";
            // 
            // tb_bcc
            // 
            this.tb_bcc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_bcc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_bcc.Location = new System.Drawing.Point(86, 228);
            this.tb_bcc.Multiline = true;
            this.tb_bcc.Name = "tb_bcc";
            this.tb_bcc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_bcc.Size = new System.Drawing.Size(277, 90);
            this.tb_bcc.TabIndex = 6;
            // 
            // lb_attach
            // 
            this.lb_attach.AutoSize = true;
            this.lb_attach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_attach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lb_attach.Location = new System.Drawing.Point(18, 7);
            this.lb_attach.Name = "lb_attach";
            this.lb_attach.Size = new System.Drawing.Size(79, 15);
            this.lb_attach.TabIndex = 1;
            this.lb_attach.Text = "User Name";
            // 
            // Mailform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(835, 549);
            this.Controls.Add(this.lb_attach);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbn_close);
            this.Controls.Add(this.tb_content);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_send);
            this.Name = "Mailform";
            this.Text = "Send Email";
            this.Load += new System.EventHandler(this.Mailform_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_remember;
        private System.Windows.Forms.TextBox tb_sendto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_cc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_content;
        private System.Windows.Forms.TextBox tb_subject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button tbn_close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_bcc;
        private System.Windows.Forms.Label lb_attach;
    }
}