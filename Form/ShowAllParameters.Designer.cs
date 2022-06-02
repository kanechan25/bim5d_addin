namespace E38EDM.CEGVN
{
    partial class ShowAllParameters
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
            this.propertyListView = new System.Windows.Forms.ListView();
            this.okButton = new System.Windows.Forms.Button();
            this.picbox_display = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.propertyListViewtype = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_display)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyListView
            // 
            this.propertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyListView.FullRowSelect = true;
            this.propertyListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.propertyListView.HideSelection = false;
            this.propertyListView.Location = new System.Drawing.Point(12, 30);
            this.propertyListView.MultiSelect = false;
            this.propertyListView.Name = "propertyListView";
            this.propertyListView.Size = new System.Drawing.Size(518, 269);
            this.propertyListView.TabIndex = 4;
            this.propertyListView.UseCompatibleStateImageBehavior = false;
            this.propertyListView.View = System.Windows.Forms.View.Details;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Location = new System.Drawing.Point(680, 565);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(81, 32);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "&Close";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // picbox_display
            // 
            this.picbox_display.Location = new System.Drawing.Point(536, 30);
            this.picbox_display.Name = "picbox_display";
            this.picbox_display.Size = new System.Drawing.Size(224, 176);
            this.picbox_display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbox_display.TabIndex = 5;
            this.picbox_display.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Instance Parameters";
            // 
            // propertyListViewtype
            // 
            this.propertyListViewtype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyListViewtype.FullRowSelect = true;
            this.propertyListViewtype.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.propertyListViewtype.HideSelection = false;
            this.propertyListViewtype.Location = new System.Drawing.Point(12, 329);
            this.propertyListViewtype.MultiSelect = false;
            this.propertyListViewtype.Name = "propertyListViewtype";
            this.propertyListViewtype.Size = new System.Drawing.Size(518, 268);
            this.propertyListViewtype.TabIndex = 4;
            this.propertyListViewtype.UseCompatibleStateImageBehavior = false;
            this.propertyListViewtype.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Type Parameters";
            // 
            // ShowAllParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 609);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propertyListViewtype);
            this.Controls.Add(this.picbox_display);
            this.Controls.Add(this.propertyListView);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowAllParameters";
            this.Text = "Display Parameters";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShowAllParameters_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox picbox_display;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView propertyListViewtype;
        private System.Windows.Forms.Label label2;
    }
}