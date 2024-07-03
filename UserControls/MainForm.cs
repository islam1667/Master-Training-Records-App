using System;
using System.Linq;
using System.Windows.Forms;
using MasterTrainingRecordsApp.UserControls;

/*
 * If you cannot understand the code, mail me islam.ibrahimov.2000 at gmail.com
 */

namespace MasterTrainingRecordsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            // Initialization
            SaveButtonsEnabled(false);
            this.FormClosing += MenuItemCloseFile_Click;
        }

        /// <summary>
        /// Stores current file path, used for saving
        /// </summary>
        public string CurrentFilePath { get; set; } = null;


        private bool _isSaved = true;
        /// <summary>
        /// Changes window name to track unsaved changes when getter, setter called
        /// </summary>
        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                if (CurrentFilePath != null)
                {
                    this.Text = "Master Training Records - " + CurrentFilePath.Split('/').Last() + (!value ? "*" : "");
                }
                else
                {
                    this.Text = "Master Training Records - Unsaved_File*";
                }
                _isSaved = value;
            }
        }

        /// <summary>
        /// Closes Program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Prompts window to open and calls MenuItemOpenFile_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MenuItemOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileFrom(Program.PromptOpenExcelFile());
        }

        /// <summary>
        /// Initializes file page from excel file and opens page
        /// </summary>
        /// <param name="filePath"></param>
        public void OpenFileFrom(string filePath)
        {
            // Check if file is unsaved and do procedures
            if (!IsSaved)
            {
                switch (Program.PromptUnsavedChanges())
                {
                    case DialogResult.Yes:
                        UserControlFilePage.Save(true);
                        break;
                    case DialogResult.No:
                        UserControlFilePage.ResetAll();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            CurrentFilePath = null;

            if (filePath == null)
            {
                MessageBox.Show("File is not chosen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CurrentFilePath = filePath;
            // Initializing page so DataGridView's filled with data when page opens
            UserControlFilePage.InitializePage(filePath);
            UserControlFilePage.BringToFront();
            SaveButtonsEnabled(true);
        }

        /// <summary>
        /// Initializes file page and opens page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MenuItemNewFile_Click(object sender, EventArgs e)
        {
            // Check if file is unsaved and do procedures
            if (!IsSaved)
            {
                switch (Program.PromptUnsavedChanges())
                {
                    case DialogResult.Yes:
                        UserControlFilePage.Save(true);
                        break;
                    case DialogResult.No:
                        UserControlFilePage.ResetAll();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            CurrentFilePath = null;

            UserControlFilePage.InitializePage();
            UserControlFilePage.BringToFront();
            SaveButtonsEnabled(true);
        }

        /// <summary>
        /// Is called when app opened and loads database if exists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ExcelOperations.DataBaseExcelFilePath != null)
            {
                MenuItemSource.Text = "DBs (Loaded)";
                ExcelOperations.ReadDBExcelFile(ExcelOperations.DataBaseExcelFilePath);
                UserControlFilePage.InitializeListBoxTask();
            }
            else
            {
                MenuItemSource.Text = "DBs (Not Loaded)";
            }
        }

        /// <summary>
        /// Prompt window to select source database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSource_Click(object sender, EventArgs e)
        {
            // Prompt user to select database file
            //List<string> paths = Program.PromptOpenMultipleExcelFile();
            // if user canceled to select, return
            //if (paths == null) return;
            //ExcelOperations.DataBaseExcelFilePath = paths;

            using (DatabaseEditDialog dialog = new DatabaseEditDialog()) {
                DialogResult result = dialog.ShowDialog();
            }

            MessageBox.Show("DBs source loaded succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MenuItemSource.Text = "DBs (Loaded)";
            ExcelOperations.ReadDBExcelFile(ExcelOperations.DataBaseExcelFilePath);

            // Initializing list box again to fill it with records previously read database
            UserControlFilePage.InitializeListBoxTask();
        }

        /// <summary>
        /// Saves file, if there is a change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            // If already saved, return
            if (IsSaved) return;
            // is save was successful, then it is saved
            IsSaved = UserControlFilePage.Save(false);
        }

        /// <summary>
        /// Saves file as excel, filter index=1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSaveAsXLSX_Click(object sender, EventArgs e)
        {
            // filter index 1=xlsx
            UserControlFilePage.SaveAs(1);
        }

        /// <summary>
        /// Saves file as json, filter index=2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSaveAsJSON_Click(object sender, EventArgs e)
        {
            // Filter index 2=json
            UserControlFilePage.SaveAs(2);
        }

        /// <summary>
        /// Prints document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemPrint_Click(object sender, EventArgs e)
        {
            UserControlFilePage.Print();
        }

        /// <summary>
        /// Enables save buttons, called when file closed
        /// </summary>
        /// <param name="enabled"></param>
        private void SaveButtonsEnabled(bool enabled)
        {
            // Enables save buttons and print, this method called when file page toggles
            MenuItemSave.Enabled = enabled;
            MenuItemSaveAs.Enabled = enabled;
            MenuItemPrint.Enabled = enabled;
        }

        /// <summary>
        /// Closes file and opens home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCloseFile_Click(object sender, EventArgs e)
        {
            // If there is unsaved changes, prompt user that there is unsaved changes
            // if user click yes save file, if clicks no, just proceed without saving,
            // if click cancel return and does not proceed method
            if (!IsSaved)
            {
                switch (Program.PromptUnsavedChanges())
                {
                    case DialogResult.Yes:
                        UserControlFilePage.Save(true);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        // If it is called when app is closing, cancel app to close
                        if (e is FormClosingEventArgs) (e as FormClosingEventArgs).Cancel = true;
                        return;
                }
            }
            // After file closed, set currently working file to null
            CurrentFilePath = null;

            // Reset file page, send it to back to make home page visible and update tree view to show latest history
            UserControlFilePage.ResetAll();
            UserControlFilePage.SendToBack();
            UserControlHomePage.UpdateTreeView();
            SaveButtonsEnabled(false);

            // Set is saved to true because user didnt change anything, it is caused by code to be false
            IsSaved = true;
            this.Text = "Master Training Records";
        }

        /// <summary>
        /// Toggles Help page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MenuItemHelp_Click(object sender, EventArgs e)
        {
            // Toggle help page
            if (MenuItemHelp.Checked)
            {
                UserControlHelpPage.SendToBack();
                // Make menu item background color change to show it toggled
                MenuItemHelp.BackColor = System.Drawing.Color.FromArgb(1, 225, 225, 225);
            }
            else
            {
                UserControlHelpPage.BringToFront();
                MenuItemHelp.BackColor = System.Drawing.Color.SkyBlue;
            }
            MenuItemHelp.Checked = !MenuItemHelp.Checked;
        }
    }
}