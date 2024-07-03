using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

/*
 * GENERAL NOTES:
 *  
 *   If you want to remove item, remove it from the data table of data view of list box DataSource,
 *   then call UpdateAndSync() it will sync it with the data grid view, removing directly from data grid 
 *   view will cause errors.
 *   
 *   
*/


namespace MasterTrainingRecordsApp
{
    public partial class UserControlFile : UserControl
    {
        public UserControlFile()
        {
            InitializeComponent();
        }

        private void UserControlFile_Load(object sender, EventArgs e)
        {
            InitializeListBoxTask();
            InitializeComboBoxFilter();
            InitializeDataGridView();

            // Subscribe events to track unsaved changes
            DataGridViewTrainingRecord.CellValueChanged += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;
            DataGridViewTrainingRecord.RowsAdded += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;
            DataGridViewTrainingRecord.RowsRemoved += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;

            TextBoxTrainee.TextChanged += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;
            TextBoxPosition.TextChanged += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;
            TextBoxManager.TextChanged += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;
            TextBoxCourse.TextChanged += (sender2, e2) => (ParentForm as MainForm).IsSaved = false;

            // Subscribe to event to preserve checked state
            ListBoxTask.ItemCheck += ListBoxTask_ItemCheck;
        }

        /// <summary>
        /// Initializes data grid view, colors, columns etc, enough to call when page load
        /// </summary>
        private void InitializeDataGridView()
        {
            // Some properties
            DataGridViewTrainingRecord.ReadOnly = false;
            DataGridViewTrainingRecord.RowHeadersWidth = 25;
            DataGridViewTrainingRecord.DefaultCellStyle.DataSourceNullValue = string.Empty;

            // Bound class to make it show column headers and define columns
            DataGridViewTrainingRecord.DataSource = new BindingList<TrainingRecord>();

            // Enable sort mode when clicked column header
            for (int i = 0; i < 10; i++) DataGridViewTrainingRecord.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;

            // Giving custom weights to columns to auto size based on weight
            DataGridViewTrainingRecord.Columns[0].FillWeight = 7;
            DataGridViewTrainingRecord.Columns[1].FillWeight = 20;
            DataGridViewTrainingRecord.Columns[2].FillWeight = 6;
            DataGridViewTrainingRecord.Columns[3].FillWeight = 4;
            DataGridViewTrainingRecord.Columns[4].FillWeight = 8;
            DataGridViewTrainingRecord.Columns[5].FillWeight = 8;
            DataGridViewTrainingRecord.Columns[6].FillWeight = 10;
            DataGridViewTrainingRecord.Columns[7].FillWeight = 10;
            DataGridViewTrainingRecord.Columns[8].FillWeight = 6;
            DataGridViewTrainingRecord.Columns[9].FillWeight = 6;

            // Configure readonly property and colors
            int[] columns = { 0 };
            foreach (int col in columns)
            {
                DataGridViewTrainingRecord.Columns[col].ReadOnly = true;
                DataGridViewTrainingRecord.Columns[col].DefaultCellStyle.BackColor = Color.IndianRed;
            }
            columns = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int col in columns)
            {
                DataGridViewTrainingRecord.Columns[col].DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 255, 153);
            }

            // Subscribe this event to delete rows when 'delete' pressed
            DataGridViewTrainingRecord.KeyDown += DataGridView_RemoveRow;

            // Subscribe event to enable selected cells bulk edit
            DataGridViewTrainingRecord.CellValueChanged += DataGridView_CellEdit;

            // Subscribe those to validate appropriate columns
            DataGridViewTrainingRecord.CellValidating += DataGridView_DateCellValidate;
            DataGridViewTrainingRecord.CellValidating += DataGridView_NumberCellValidate;
        }

        /// <summary>
        /// Initializes filter combo box, enough to call when page load
        /// </summary>
        private void InitializeComboBoxFilter()
        {
            foreach (PropertyInfo p in typeof(TrainingRecord).GetProperties())
                ComboBoxFilter.Items.Add(p.Name);

            ComboBoxFilter.SelectedIndex = 1;
        }

        /// <summary>
        /// Initializes List box, fills list box with trainings from database file, enough to call when page opened
        /// </summary>
        public void InitializeListBoxTask()
        {
            // initialize dataTable object for suggestion list box, stores task name, checked state and record itself
            var dt = new DataTable();

            // Main three columns in Data Table
            dt.Columns.Add("Checked", typeof(bool));
            dt.Columns.Add("Object", typeof(TrainingRecord));

            // Add other columns with the same names with properties of TrainingRecord
            foreach (PropertyInfo p in typeof(TrainingRecord).GetProperties())
            {
                dt.Columns.Add(p.Name, typeof(string));
            }

            // Add row, data is for columns we created above
            foreach (TrainingRecord record in ExcelOperations.RecordsDatabase)
                dt.Rows.Add(false,
                    record,
                    record.Reference,
                    record.Task,
                    record.Category,
                    record.Type,
                    record.StartTime,
                    record.EndTime,
                    record.TrainerInitials,
                    record.CertifierInitials,
                    record.CertifierScore,
                    record.RequiredScore);

            // Accept changes to commit into Data Table
            dt.AcceptChanges();

            // Set data source of list box as data view to make sorting, filtering possible
            ListBoxTask.DataSource = dt?.DefaultView;

            // Set display member as Task to make list box show Tasks column as a list, but will carry value of Object
            // It is Task because we populated data table of list with property names of TrainingRecord and it have task property
            ListBoxTask.DisplayMember = "Task";
            ListBoxTask.ValueMember = "Object";

            // If there is data in data grid view, it means database loaded after opening file and
            // when loading database it will call this function, so if there is data in data grid view
            // check appropriate items in list box to sync them
            if (DataGridViewTrainingRecord.Rows.Count != 0)
            {
                foreach (DataGridViewRow r in DataGridViewTrainingRecord.Rows)
                {
                    foreach (DataRow dr in (ListBoxTask.DataSource as DataView).Table.Rows)
                    {
                        if ((string)dr["Reference"] == (string)r.Cells[0].Value)
                        {
                            dr["Checked"] = true;
                            break;
                        }
                    }
                }
            }

            // To refresh check state of list box
            UpdateAndSync();
        }

        /// <summary>
        /// Initializes page from appropriate file, fills grid, list box and text boxes
        /// </summary>
        /// <param name="fromFilePath">File to be opened</param>
        public void InitializePage(string fromFilePath)
        {
            // Reset all beforehead
            ResetAll();

            // Set text box value as file path to show
            TextBoxFilePathInfo.Text = fromFilePath;

            // Get TrainingRecord and MemberInfo from excel file
            MemberInfo memberInfo = ExcelOperations.ReadMemberInfoFromExcel(fromFilePath);
            BindingList<TrainingRecord> tRecords = ExcelOperations.ReadTrainingRecordsFromExcel(fromFilePath);

            // Fill DataGridView with the data from excel file
            DataGridViewTrainingRecord.DataSource = tRecords;

            // Fill text Boxes with the member data from excel file
            TextBoxTrainee.Text = memberInfo.Trainee;
            TextBoxPosition.Text = memberInfo.Position;
            TextBoxManager.Text = memberInfo.Manager;
            TextBoxCourse.Text = memberInfo.Course;

            // Checking items that we get from file, checking items in the list will automatically add
            // them into data grid view after UpdateAndSync() called

            DataTable dt = (ListBoxTask.DataSource as DataView).Table;
            foreach (DataRow dr in dt.Rows)
            {
                foreach (TrainingRecord record in tRecords)
                {
                    if (record.Reference == (dr["Object"] as TrainingRecord).Reference)
                    {
                        dr["Checked"] = true;
                        continue;
                    }
                }
            }
            // Update and sync with data grid view
            UpdateAndSync();

            // Initialization process makes isSaved false, but user didnt cause any change, so make it true
            (ParentForm as MainForm).IsSaved = true;
        }

        /// <summary>
        /// Initializes page, resets all
        /// </summary>
        public void InitializePage()
        {
            // Reset the page and make IsSaved true because user didnt cause any change
            ResetAll();
            (ParentForm as MainForm).IsSaved = true;
        }

        /// <summary>
        /// Tries to parse string to date
        /// </summary>
        /// <param name="dateString">Date as string</param>
        /// <param name="date">Date to be out</param>
        /// <returns>Success if parsing was successful</returns>
        private bool TryParseDate(string dateString, out DateTime date)
        {
            // Prevent errors occur with null check
            if (dateString == null)
            {
                date = new DateTime();
                return false;
            }

            // Preserve only numbers in string, so if user enters 12-12-2012 it will be converted into 12 12 2012 ot make it valid
            dateString = Regex.Replace(dateString, @"[^0-9]", " ").Trim();

            // List of acceptable date formats
            string[] formats = { "d M yyyy", "dd MM yyyy", "d MM yyyy", "dd M yyyy", "d M yy", "dd MM yy", "d MM yy", "dd MM yy" };

            return DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        /// <summary>
        /// Validates appropriate columns for valid number characters, blocks user to enter character which can cause error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_DateCellValidate(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // This validation is only for 4th and 5th columns, date columns
            if (e.ColumnIndex != 4 && e.ColumnIndex != 5) return;

            // Prevent errors with null check, only validate when edit finished
            // If cell is not edit mode does not continue because we use EditingControl property to
            // change text of currently editing cell, and when cell is not edited it will be null and
            // cause errors
            if (!DataGridViewTrainingRecord.IsCurrentCellInEditMode) return;
            if (string.IsNullOrEmpty(e.FormattedValue.ToString().Trim())) return;

            if (TryParseDate(e.FormattedValue.ToString().Trim(), out DateTime tempDate))
            {
                DataGridViewTrainingRecord.EditingControl.Text = tempDate.ToString("dd.MM.yyyy");
            }
            else
            {
                // Cancel validation to keep user in edit mode
                e.Cancel = true;
                // Change content of cell to valid data to not cause data error if user closes file
                DataGridViewTrainingRecord.EditingControl.Text = string.Empty;
                MessageBox.Show("Please enter the date in dd.MM.yyyy format.", "Invalid Date format", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates appropriate columns for valid format of date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_NumberCellValidate(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // This validation only for 8th and 9th columns, number columns
            if (e.ColumnIndex != 8 && e.ColumnIndex != 9) return;

            // Prevent errors with null check, only validate when edit finished
            // If cell is not edit mode does not continue because we use EditingControl property to
            // change text of currently editing cell, and when cell is not edited it will be null and
            // cause errors
            if (!DataGridViewTrainingRecord.IsCurrentCellInEditMode) return;

            if (!int.TryParse(e.FormattedValue?.ToString(), out int _temp))
            {
                // Cancel validation to keep user in edit mode
                e.Cancel = true;
                // Change content of cell to valid data to not cause data error if user closes file
                DataGridViewTrainingRecord.EditingControl.Text = "0";
                MessageBox.Show("Value is not number.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// This Method allows for bulk edit, when user edits and commit, this method will change 
        /// content of all selected cells if suitable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Unsubscribe the event to avoid recursive call of cell value change, because we are changing
            // cell value below and it will cause this event to fire again, so unsubscribe and resubscribe
            DataGridViewTrainingRecord.CellValueChanged -= DataGridView_CellEdit;

            string value = DataGridViewTrainingRecord[e.ColumnIndex, e.RowIndex].Value?.ToString().Trim();

            // Iterate through selected cells to change them all in bulk edit
            foreach (DataGridViewCell cell in DataGridViewTrainingRecord.SelectedCells)
            {
                // If cell is read-only does not change its value, pass
                if (cell.ReadOnly) continue;

                // If cell is in date columns, change it only when if parsing is successful
                if (cell.ColumnIndex == 4 || cell.ColumnIndex == 5)
                {
                    if (TryParseDate(value, out DateTime tempDate))
                        cell.Value = tempDate.ToString("dd.MM.yyyy");

                    continue;
                }

                // If cell is number in number columns, change it only if parsing is successful
                if (cell.ColumnIndex == 8 || cell.ColumnIndex == 9)
                {
                    if (int.TryParse(value, out int tempInt))
                        cell.Value = tempInt;

                    continue;
                }

                // If selected cell does not belong above, just change it value
                cell.Value = value;
            }

            // Resubscribe event
            DataGridViewTrainingRecord.CellValueChanged += DataGridView_CellEdit;
        }

        /// <summary>
        /// Removes row from data grid view, also removes check from list box and syncs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_RemoveRow(object sender, KeyEventArgs e)
        {
            // Delete rows only when delete key is pressed and rows selected
            if (e.KeyCode != Keys.Delete) return;
            if (DataGridViewTrainingRecord.SelectedRows.Count == 0) return;

            // End edit to commit changes to not cause data error while deleting
            DataGridViewTrainingRecord.EndEdit();
            var rows = DataGridViewTrainingRecord.SelectedRows;

            // Iterate through selected rows and uncheck them from data view of list box, 
            // unchecking them from list box and calling UpdateAndSync() will automatically remove from 
            // data grid view, but for some case if the row does not exist inside data view of list box
            // it will directly remove it from data grid view, thats why flag is used to show if it deleted
            foreach (DataGridViewRow r in rows)
            {
                bool flag = true;
                string reference = r.Cells[0].Value as string;
                foreach (DataRow dr in (ListBoxTask.DataSource as DataView).Table.Rows)
                {
                    if ((dr["Object"] as TrainingRecord).Reference == reference)
                    {
                        dr["Checked"] = false;
                        flag = false;
                        break;
                    }
                }
                if (flag) DataGridViewTrainingRecord.Rows.Remove(r);
            }

            // Update list box to sync data grid view
            UpdateAndSync();
        }

        /// <summary>
        /// Adds record to data grid view, checks record similarity by reference
        /// </summary>
        /// <param name="tRecord">Record to be removen</param>
        private void DataGridViewAddRecord(TrainingRecord tRecord)
        {
            // Get the choosen records and add, update dataGridView
            BindingList<TrainingRecord> dataGridViewRecords = DataGridViewTrainingRecord.DataSource as BindingList<TrainingRecord>;

            // If a record already exist in the DataGridView it doesnt add it, it checks by reference number
            TrainingRecord foundRecord = dataGridViewRecords.SingleOrDefault(r => r.Reference == tRecord.Reference);

            // It automatically updates DataGridView when it is added as it is BindingList
            if (foundRecord == null) dataGridViewRecords.Add(tRecord.Clone());
        }

        /// <summary>
        /// Removes given record from data grid view, checks record similarity by reference
        /// </summary>
        /// <param name="tRecord">Record to be removen</param>
        private void DataGridViewRemoveRecord(TrainingRecord tRecord)
        {
            // Get the choosen records and add, update dataGridView
            BindingList<TrainingRecord> dataGridViewRecords = DataGridViewTrainingRecord.DataSource as BindingList<TrainingRecord>;

            // If a record already exist in the DataGridView it doesnt add it, it checks by reference number
            TrainingRecord foundRecord = dataGridViewRecords.SingleOrDefault(r => r.Reference == tRecord.Reference);

            // It automatically updates DataGridView when it is added as it is BindingList
            if (foundRecord != null) dataGridViewRecords.Remove(foundRecord);
        }

        /// <summary>
        /// Method will change DataView["Checked"] when items checked in list box
        /// This method provides SYNC with data grid, items will be added and removed from 
        /// data grid when items checked and unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxTask_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // When user checked item from ui, this code will update check state stored in data view
            DataView dv = ListBoxTask.DataSource as DataView;
            DataRowView drv = dv[e.Index];
            drv["Checked"] = (e.NewValue == CheckState.Checked);

            // Sync with data grid view, if item checked add it to data grid, if unchecked then remove
            if ((bool)drv["Checked"])
                DataGridViewAddRecord(drv["Object"] as TrainingRecord);
            else
                DataGridViewRemoveRecord(drv["Object"] as TrainingRecord);
        }

        /// <summary>
        /// Syncs List box ui with the DataView stored inside list box, shows filtered elements in list box depending on the search text
        /// And syncs data grid view with the list box, adds and removes items from data grid view
        /// Syncs DataView["Checked"] with list box's check state
        /// </summary>
        private void UpdateAndSync()
        {
            // Set the DataView.RowFilter to apply/remove the filter. In your case, you need to use the LIKE operator.
            // Restore the checked states from the data source.
            var dv = ListBoxTask.DataSource as DataView;
            dv.RowFilter = GetFilterString();

            // Syncs checked states inside data view with list box ui check boxes
            for (var i = 0; i < ListBoxTask.Items.Count; i++)
            {
                var drv = ListBoxTask.Items[i] as DataRowView;
                var chk = Convert.ToBoolean(drv["Checked"]);
                ListBoxTask.SetItemChecked(i, chk);
            }
        }

        /// <summary>
        /// Triggered when text changed in TaskSearchBox, will trigger list box to update 
        /// based on the text typed into
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxAddTask_TextChanged(object sender, EventArgs e)
        {
            // Update list box to show filtered elements
            UpdateAndSync();
        }

        /// <summary>
        /// Is used by DataView to filter list box by property name
        /// </summary>
        /// <returns>string for filter</returns>
        private string GetFilterString()
        {
            // Creates filter string for data view depending on the selection choosen in the filter combo box
            string input = TextBoxAddTask.Text;
            string filter = input.Trim().Length > 0
                ? $"{(string)ComboBoxFilter.SelectedItem} LIKE '*{input}*'"
                : null;

            return filter;
        }

        /// <summary>
        /// Is called to save file, if there is path in CurrentFilePath, it will save there 
        /// otherwise it will prompt to choose save location and will store it in the CurrentFilePath
        /// </summary>
        /// <param name="resetAll">Whether to call ResetAll() after save</param>
        /// <returns>success as bool</returns>
        public bool Save(bool resetAll)
        {
            // End edit to commit changes
            DataGridViewTrainingRecord.EndEdit();

            // Get member info and training records from DataGridView
            MemberInfo memberInfo = GetMemberInfo();
            List<TrainingRecord> tRecords = GetTrainingRecords();

            // Get path where to save, if current path exists use that if not prompt user to chose
            string currentFilePath = (ParentForm as MainForm).CurrentFilePath ?? Program.PromptSaveExcelFile("Tasks-excel");

            // Set currentfilepath in mainform
            if (currentFilePath != null) (ParentForm as MainForm).CurrentFilePath = currentFilePath;

            TextBoxFilePathInfo.Text = currentFilePath ?? "";

            // Give parameters to save as excel file, if it was successful return true
            if (ExcelOperations.WriteAllToExcel(currentFilePath, memberInfo, tRecords))
            {
                if (resetAll) ResetAll();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Is called when user click Save As in the menu, always asks for location to save
        /// </summary>
        /// <param name="filterIndex">1 for .xlsx, 2 for .json, selection in save dialog</param>
        /// <returns>success as bool</returns>
        public bool SaveAs(int filterIndex)
        {
            // End edit to commit changes
            DataGridViewTrainingRecord.EndEdit();

            // Prompt user to chose location for save, if null then user didnt chose, return
            FileInfo fileInfo = Program.PromptSaveAs("Trainee_tasks-json", filterIndex);
            if (fileInfo == null) return false;

            // If user chose xlsx in save window, then save as .xlsx, otherwise .json
            if (fileInfo.Extension == ".xlsx")
                ExcelOperations.WriteAllToExcel(fileInfo.FullName, GetMemberInfo(), GetTrainingRecords());
            else if (fileInfo.Extension == ".json")
                ExcelOperations.WriteAllToJson(fileInfo.FullName, GetMemberInfo(), GetTrainingRecords());
            else return false;

            return true;
        }

        /// <summary>
        /// Gets Member info from text boxes
        /// </summary>
        /// <returns>Memberinfo instance</returns>
        private MemberInfo GetMemberInfo()
        {
            // Create member info from text box inputs
            return new MemberInfo()
            {
                Trainee = TextBoxTrainee.Text.Trim(),
                Course = TextBoxCourse.Text.Trim(),
                Manager = TextBoxManager.Text.Trim(),
                Position = TextBoxPosition.Text.Trim()
            };
        }

        /// <summary>
        /// Gets training records from data grid view
        /// </summary>
        /// <returns>List of TrainingRecord</returns>
        private List<TrainingRecord> GetTrainingRecords()
        {
            // End edit to make currently editing cell value commit
            DataGridViewTrainingRecord.EndEdit();

            List<TrainingRecord> tRecords = new List<TrainingRecord>();

            // Create list from records in data grid view, using clone to not face with reference problems
            foreach (DataGridViewRow row in DataGridViewTrainingRecord.Rows)
                tRecords.Add((row.DataBoundItem as TrainingRecord).Clone());

            return tRecords;
        }

        /// <summary>
        /// Creates file in the windows temp folder and forwards it to print
        /// </summary>
        public void Print()
        {
            // Creates temporary file in windows' temp folder. prints it, deletes it
            string tempFilePath = Environment.ExpandEnvironmentVariables(@"%Temp%\Temp.xlsx");
            ExcelOperations.WriteAllToExcel(tempFilePath, GetMemberInfo(), GetTrainingRecords(), true);
            ExcelOperations.PrintExcelFile(tempFilePath);
            File.Delete(tempFilePath);
        }

        /// <summary>
        /// Resets list box and the grid that is in sync with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxResetAddTaskTextBox_Click(object sender, EventArgs e)
        {
            // Clear the Trainee info in DataGridView and refresh list box
            TextBoxAddTask.Text = string.Empty;
            DataGridViewTrainingRecord.Rows.Clear();
            InitializeListBoxTask();
        }

        /// <summary>
        /// Resets text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxResetMemberInfo_Click(object sender, EventArgs e)
        {
            // Empty the text boxes used for member info
            TextBoxTrainee.Text = String.Empty;
            TextBoxManager.Text = String.Empty;
            TextBoxCourse.Text = String.Empty;
            TextBoxPosition.Text = String.Empty;
        }

        /// <summary>
        /// Resets text boxes, data grid view and the list
        /// </summary>
        public void ResetAll()
        {
            // Resets everything
            PictureBoxResetAddTaskTextBox_Click(null, null);
            PictureBoxResetMemberInfo_Click(null, null);
            TextBoxFilePathInfo.Text = String.Empty;
        }
    }
}