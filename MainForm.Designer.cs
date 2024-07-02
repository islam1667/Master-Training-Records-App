namespace MasterTrainingRecordsApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAsXLSX = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAsJSON = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCloseFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSource = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFormPanel = new System.Windows.Forms.Panel();
            this.UserControlHomePage = new MasterTrainingRecordsApp.UserControlHome();
            this.UserControlFilePage = new MasterTrainingRecordsApp.UserControlFile();
            this.UserControlHelpPage = new MasterTrainingRecordsApp.UserControlHelp();
            this.MenuStrip.SuspendLayout();
            this.MainFormPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.MenuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemSource,
            this.MenuItemHelp});
            this.MenuStrip.Name = "MenuStrip";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNewFile,
            this.MenuItemOpenFile,
            this.toolStripSeparator,
            this.MenuItemSave,
            this.MenuItemSaveAs,
            this.MenuItemCloseFile,
            this.toolStripSeparator1,
            this.MenuItemPrint,
            this.toolStripSeparator2,
            this.MenuItemExit});
            this.MenuItemFile.ForeColor = System.Drawing.Color.Black;
            this.MenuItemFile.Name = "MenuItemFile";
            resources.ApplyResources(this.MenuItemFile, "MenuItemFile");
            // 
            // MenuItemNewFile
            // 
            this.MenuItemNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemNewFile.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.MenuItemNewFile, "MenuItemNewFile");
            this.MenuItemNewFile.Name = "MenuItemNewFile";
            this.MenuItemNewFile.Click += new System.EventHandler(this.MenuItemNewFile_Click);
            // 
            // MenuItemOpenFile
            // 
            this.MenuItemOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemOpenFile.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.MenuItemOpenFile, "MenuItemOpenFile");
            this.MenuItemOpenFile.Name = "MenuItemOpenFile";
            this.MenuItemOpenFile.Click += new System.EventHandler(this.MenuItemOpenFile_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // MenuItemSave
            // 
            this.MenuItemSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemSave.ForeColor = System.Drawing.Color.Black;
            this.MenuItemSave.Name = "MenuItemSave";
            resources.ApplyResources(this.MenuItemSave, "MenuItemSave");
            this.MenuItemSave.Click += new System.EventHandler(this.MenuItemSave_Click);
            // 
            // MenuItemSaveAs
            // 
            this.MenuItemSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemSaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSaveAsXLSX,
            this.MenuItemSaveAsJSON});
            this.MenuItemSaveAs.ForeColor = System.Drawing.Color.Black;
            this.MenuItemSaveAs.Name = "MenuItemSaveAs";
            resources.ApplyResources(this.MenuItemSaveAs, "MenuItemSaveAs");
            // 
            // MenuItemSaveAsXLSX
            // 
            this.MenuItemSaveAsXLSX.Name = "MenuItemSaveAsXLSX";
            resources.ApplyResources(this.MenuItemSaveAsXLSX, "MenuItemSaveAsXLSX");
            this.MenuItemSaveAsXLSX.Click += new System.EventHandler(this.MenuItemSaveAsXLSX_Click);
            // 
            // MenuItemSaveAsJSON
            // 
            this.MenuItemSaveAsJSON.Name = "MenuItemSaveAsJSON";
            resources.ApplyResources(this.MenuItemSaveAsJSON, "MenuItemSaveAsJSON");
            this.MenuItemSaveAsJSON.Click += new System.EventHandler(this.MenuItemSaveAsJSON_Click);
            // 
            // MenuItemCloseFile
            // 
            this.MenuItemCloseFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemCloseFile.ForeColor = System.Drawing.Color.Black;
            this.MenuItemCloseFile.Name = "MenuItemCloseFile";
            resources.ApplyResources(this.MenuItemCloseFile, "MenuItemCloseFile");
            this.MenuItemCloseFile.Click += new System.EventHandler(this.MenuItemCloseFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // MenuItemPrint
            // 
            this.MenuItemPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemPrint.ForeColor = System.Drawing.Color.Black;
            this.MenuItemPrint.Name = "MenuItemPrint";
            resources.ApplyResources(this.MenuItemPrint, "MenuItemPrint");
            this.MenuItemPrint.Click += new System.EventHandler(this.MenuItemPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemExit.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.MenuItemExit, "MenuItemExit");
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // MenuItemSource
            // 
            this.MenuItemSource.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.MenuItemSource, "MenuItemSource");
            this.MenuItemSource.Name = "MenuItemSource";
            this.MenuItemSource.Click += new System.EventHandler(this.MenuItemSource_Click);
            // 
            // MenuItemHelp
            // 
            this.MenuItemHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemHelp.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.MenuItemHelp, "MenuItemHelp");
            this.MenuItemHelp.Name = "MenuItemHelp";
            this.MenuItemHelp.Click += new System.EventHandler(this.MenuItemHelp_Click);
            // 
            // MainFormPanel
            // 
            resources.ApplyResources(this.MainFormPanel, "MainFormPanel");
            this.MainFormPanel.Controls.Add(this.UserControlHomePage);
            this.MainFormPanel.Controls.Add(this.UserControlFilePage);
            this.MainFormPanel.Controls.Add(this.UserControlHelpPage);
            this.MainFormPanel.Name = "MainFormPanel";
            // 
            // UserControlHomePage
            // 
            resources.ApplyResources(this.UserControlHomePage, "UserControlHomePage");
            this.UserControlHomePage.Name = "UserControlHomePage";
            // 
            // UserControlFilePage
            // 
            resources.ApplyResources(this.UserControlFilePage, "UserControlFilePage");
            this.UserControlFilePage.Name = "UserControlFilePage";
            // 
            // UserControlHelpPage
            // 
            resources.ApplyResources(this.UserControlHelpPage, "UserControlHelpPage");
            this.UserControlHelpPage.BackColor = System.Drawing.SystemColors.Control;
            this.UserControlHelpPage.Name = "UserControlHelpPage";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.MainFormPanel);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.MainFormPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNewFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSource;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAsXLSX;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAsJSON;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCloseFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel MainFormPanel;
        private UserControlHome UserControlHomePage;
        private UserControlFile UserControlFilePage;
        private UserControlHelp UserControlHelpPage;
    }
}