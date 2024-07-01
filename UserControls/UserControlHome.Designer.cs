namespace MasterTrainingRecordsApp
{
    partial class UserControlHome
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlHome));
            this.GroupBoxRecent = new System.Windows.Forms.GroupBox();
            this.TreeViewFileHistory = new System.Windows.Forms.TreeView();
            this.GroupBoxFile = new System.Windows.Forms.GroupBox();
            this.ButtonHelp = new System.Windows.Forms.Button();
            this.ButtonOpenFile = new System.Windows.Forms.Button();
            this.ButtonNewFile = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GroupBoxRecent.SuspendLayout();
            this.GroupBoxFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBoxRecent
            // 
            this.GroupBoxRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxRecent.Controls.Add(this.TreeViewFileHistory);
            this.GroupBoxRecent.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxRecent.Location = new System.Drawing.Point(57, 88);
            this.GroupBoxRecent.Name = "GroupBoxRecent";
            this.GroupBoxRecent.Size = new System.Drawing.Size(412, 379);
            this.GroupBoxRecent.TabIndex = 0;
            this.GroupBoxRecent.TabStop = false;
            this.GroupBoxRecent.Text = "Recently Edited";
            // 
            // TreeViewFileHistory
            // 
            this.TreeViewFileHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewFileHistory.BackColor = System.Drawing.SystemColors.Control;
            this.TreeViewFileHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TreeViewFileHistory.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeViewFileHistory.HotTracking = true;
            this.TreeViewFileHistory.Location = new System.Drawing.Point(6, 13);
            this.TreeViewFileHistory.Name = "TreeViewFileHistory";
            this.TreeViewFileHistory.Size = new System.Drawing.Size(400, 360);
            this.TreeViewFileHistory.TabIndex = 0;
            // 
            // GroupBoxFile
            // 
            this.GroupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxFile.AutoSize = true;
            this.GroupBoxFile.Controls.Add(this.ButtonHelp);
            this.GroupBoxFile.Controls.Add(this.ButtonOpenFile);
            this.GroupBoxFile.Controls.Add(this.ButtonNewFile);
            this.GroupBoxFile.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxFile.Location = new System.Drawing.Point(587, 88);
            this.GroupBoxFile.Name = "GroupBoxFile";
            this.GroupBoxFile.Size = new System.Drawing.Size(290, 379);
            this.GroupBoxFile.TabIndex = 1;
            this.GroupBoxFile.TabStop = false;
            this.GroupBoxFile.Text = "Get Started";
            // 
            // ButtonHelp
            // 
            this.ButtonHelp.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("ButtonHelp.Image")));
            this.ButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonHelp.Location = new System.Drawing.Point(28, 165);
            this.ButtonHelp.Name = "ButtonHelp";
            this.ButtonHelp.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ButtonHelp.Size = new System.Drawing.Size(235, 53);
            this.ButtonHelp.TabIndex = 2;
            this.ButtonHelp.Text = "Help";
            this.ButtonHelp.UseVisualStyleBackColor = true;
            this.ButtonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
            // 
            // ButtonOpenFile
            // 
            this.ButtonOpenFile.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("ButtonOpenFile.Image")));
            this.ButtonOpenFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonOpenFile.Location = new System.Drawing.Point(28, 81);
            this.ButtonOpenFile.Name = "ButtonOpenFile";
            this.ButtonOpenFile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ButtonOpenFile.Size = new System.Drawing.Size(235, 53);
            this.ButtonOpenFile.TabIndex = 1;
            this.ButtonOpenFile.Text = "Open File";
            this.ButtonOpenFile.UseVisualStyleBackColor = true;
            this.ButtonOpenFile.Click += new System.EventHandler(this.ButtonOpenFile_Click);
            // 
            // ButtonNewFile
            // 
            this.ButtonNewFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonNewFile.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonNewFile.Image = ((System.Drawing.Image)(resources.GetObject("ButtonNewFile.Image")));
            this.ButtonNewFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonNewFile.Location = new System.Drawing.Point(28, 22);
            this.ButtonNewFile.Name = "ButtonNewFile";
            this.ButtonNewFile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ButtonNewFile.Size = new System.Drawing.Size(235, 53);
            this.ButtonNewFile.TabIndex = 0;
            this.ButtonNewFile.Text = "New File";
            this.ButtonNewFile.UseVisualStyleBackColor = true;
            this.ButtonNewFile.Click += new System.EventHandler(this.ButtonNewFile_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MasterTrainingRecordsApp.Resources.azercosmos_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(57, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 46);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // UserControlHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.GroupBoxFile);
            this.Controls.Add(this.GroupBoxRecent);
            this.Name = "UserControlHome";
            this.Size = new System.Drawing.Size(930, 510);
            this.GroupBoxRecent.ResumeLayout(false);
            this.GroupBoxFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxRecent;
        private System.Windows.Forms.GroupBox GroupBoxFile;
        private System.Windows.Forms.Button ButtonOpenFile;
        private System.Windows.Forms.Button ButtonNewFile;
        private System.Windows.Forms.TreeView TreeViewFileHistory;
        private System.Windows.Forms.Button ButtonHelp;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
