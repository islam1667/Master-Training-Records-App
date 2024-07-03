using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MasterTrainingRecordsApp.UserControls
{

    /// <summary>
    /// This Dialog is for adding and removing databases
    /// </summary>
    public partial class DatabaseEditDialog : Form
    {
        public DatabaseEditDialog()
        {
            InitializeComponent();
            // Set start position to center of main app
            this.StartPosition = FormStartPosition.CenterParent;
            this.Load += Form_Load;
        }

        /// <summary>
        /// Fill the form with database list when dialog showed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e) {
            if (ExcelOperations.DataBaseExcelFilePath == null) return;
            ListBoxDatabase.Items.AddRange(ExcelOperations.DataBaseExcelFilePath.ToArray());
        }


        /// <summary>
        /// Prompts open file dialog to select db files, adds them to list box and saves them to
        /// DatabaseList in ExcelOperations class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            // Prompt to select files, if not selected return
            List<string> paths = Program.PromptOpenMultipleExcelFile();
            if (paths == null)
            {
                MessageBox.Show("File not selected.");
                return;
            }

            // If user select the files that are already in db, that s why check if added file is
            // new then add it to this list, at the end add this to list box
            List<string> toBeAdded = new List<string>();

            // Check if user selected the files that are already in the list
            // This cheks by file path, but if same file copied to another path, unfortunately 
            // it will be added to list twice
            foreach (string addedPath in paths) {
                bool found = false; // Flag to know if it already exist in the list
                foreach (var pathInList in ListBoxDatabase.Items) {
                    if (pathInList.ToString() == addedPath)
                    {
                        found = true;
                        continue;
                    }
                }
                // If it does not exist add it to list that will be added to list box
                if(!found) toBeAdded.Add(addedPath);
            }

            // Add them to list box
            ListBoxDatabase.Items.AddRange(toBeAdded.ToArray());
            
            // Also renew database list in ExcelOperations class
            SaveToDatabaseList();
        }

        /// <summary>
        /// Remove file from list box, get items as a list, save that list to main list in ExcelOperations class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            // If nothing selected in the list box, do nothing
            if (ListBoxDatabase.SelectedIndex == -1)
            {
                MessageBox.Show("Nothing selected.");
                return;
            }
            // Remove from list box
            ListBoxDatabase.Items.RemoveAt(ListBoxDatabase.SelectedIndex);

            // Also renew database list in ExcelOperations class
            SaveToDatabaseList();
        }

        /// <summary>
        /// Method for saving list box items to main list in ExcelOperations class
        /// </summary>
        private void SaveToDatabaseList() {
            // Get items from list box and add them to list
            List<string> paths = new List<string>();
            foreach (var path in ListBoxDatabase.Items) paths.Add(path.ToString());

            // if there was no items in the list do nothing, return
            if (paths.Count == 0) {
                return;
            }
            // Save the list to the main list in ExcelOperations class
            ExcelOperations.DataBaseExcelFilePath = paths;
        }
    }
}
