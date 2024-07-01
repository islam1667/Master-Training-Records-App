using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;

/*
 * CONTENT OF DATABASE PATH XML IS SUPPOSED TO BE LIKE THIS
 * 
 * <databasePath>
 *   <file path="C:\Users\Downloads\Database.xlsx" />
 * </databasePath>
*/


namespace MasterTrainingRecordsApp
{
    internal static class ExcelOperations
    {
        /// <summary>
        /// Stores xml file path that stores database file path
        /// </summary>
        private static string _dataBaseExcelFilePathXml = Environment.ExpandEnvironmentVariables("%Appdata%/MasterTrainingRecordsApp/DataBase_Path.xml");

        /// <summary>
        /// Gets the file path of database from xml
        /// </summary>
        public static string DataBaseExcelFilePath
        {
            get
            {
                // if xml file or xml element or path inside xml does not exist, return
                if (!File.Exists(_dataBaseExcelFilePathXml)) return null;
                XDocument xmlDoc = XDocument.Load(_dataBaseExcelFilePathXml);
                if (xmlDoc.Root.Element("file") == null || xmlDoc.Root.Element("file").Attribute("path").Value == string.Empty) return null;

                // If file which have path in 'file' element does not exist, remove it from xml
                if (!File.Exists(xmlDoc.Root.Element("file").Attribute("path").Value))
                {
                    xmlDoc.Root.Element("file").Remove();
                    xmlDoc.Save(_dataBaseExcelFilePathXml);
                    return null;
                }
                return xmlDoc.Root.Element("file").Attribute("path").Value;
            }
            set
            {
                // it is set for first time and file does not exist, create appropriate folders and xml
                if (!File.Exists(_dataBaseExcelFilePathXml))
                {
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string folderPath = Path.Combine(appDataFolder, "MasterTrainingRecordsApp");

                    Directory.CreateDirectory(folderPath);

                    _dataBaseExcelFilePathXml = Path.Combine(folderPath, "DataBase_Path.xml");

                    File.Create(_dataBaseExcelFilePathXml).Close(); // Close the file stream immediately
                    // Create root node just by writing into file
                    File.WriteAllText(_dataBaseExcelFilePathXml, "<databasePath>\n</databasePath>");
                }
                // Remove previously added database and add new one
                XDocument xmlDoc = XDocument.Load(_dataBaseExcelFilePathXml);
                xmlDoc.Root.RemoveAll();
                xmlDoc.Root.Add(new XElement("file", new XAttribute("path", value ?? "")));
                xmlDoc.Save(_dataBaseExcelFilePathXml);
            }
        }

        /// <summary>
        /// Stores records of database in a list
        /// </summary>
        public static List<TrainingRecord> RecordsDatabase { get; private set; } = new List<TrainingRecord>();

        /// <summary>
        /// Print file, creates temp.xlsx in windows temp folder, prints and deletes, wrote by gpt
        /// </summary>
        /// <param name="filePath">File Path of file to be printed</param>
        public static void PrintExcelFile(string filePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            try
            {
                // Open the Excel file
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = workbook.Sheets[1]; // Assuming you want to print the first sheet

                // Set the print area to the used range of the worksheet
                Range usedRange = worksheet.UsedRange;
                worksheet.PageSetup.PrintArea = usedRange.Address;

                // Set orientation to landscape
                worksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;

                // Set paper size to A4
                worksheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;

                // Set page setup options
                worksheet.PageSetup.Zoom = false; // Disable zoom to use FitToPages settings
                worksheet.PageSetup.FitToPagesWide = 1; // Fit to one page wide
                worksheet.PageSetup.FitToPagesTall = false; // Fit all rows, set to 1 to fit to one page tall

                // Adjust margins
                worksheet.PageSetup.LeftMargin = excelApp.InchesToPoints(0.2); // Set left margin to 0.5 inches
                worksheet.PageSetup.RightMargin = excelApp.InchesToPoints(0.2); // Set right margin to 0.5 inches
                worksheet.PageSetup.TopMargin = excelApp.InchesToPoints(0.2); // Set top margin to 0.5 inches
                worksheet.PageSetup.BottomMargin = excelApp.InchesToPoints(0.2); // Set bottom margin to 0.5 inches

                // Show print preview
                excelApp.Visible = true; // Excel must be visible to show the print preview
                worksheet.PrintPreview();

                // Print the worksheet
                //worksheet.PrintOut();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured during print.\n+{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the workbook without saving
                workbook?.Close(false);

                // Quit the Excel application
                excelApp.Quit();

                // Release COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Writes file into json
        /// </summary>
        /// <param name="filePath">File Path of file to be written</param>
        /// <param name="memberInfo">Member info to be written</param>
        /// <param name="tRecords">Training Records to be written</param>
        /// <returns>Success, if was successful to write</returns>
        public static bool WriteAllToJson(string filePath, MemberInfo memberInfo, List<TrainingRecord> tRecords)
        {
            // Prevent errors occur with null check
            if (filePath == null)
            {
                MessageBox.Show("File not saved.", "FilePath null", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Serialize data into json string
            string jsonString = JsonConvert.SerializeObject(memberInfo, Formatting.Indented);
            string jsonString2 = JsonConvert.SerializeObject(tRecords, Formatting.Indented);

            // Join strings and write into file
            File.WriteAllText(filePath, jsonString + Environment.NewLine + jsonString2);

            MessageBox.Show("Json saved.", "Save dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        /// <summary>
        /// Initializes excel file to be saved, colors, merges etc
        /// </summary>
        /// <returns>ExcelPackage that stores initialized file</returns>
        private static ExcelPackage InitializeExcelTemplate()
        {
            ExcelPackage package = new ExcelPackage();

            // Add a worksheet to the Excel package
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Records of Training");

            // Write headers for Trainee, Course, Position, and Manager
            worksheet.Cells["A1"].Value = "Trainee:";
            worksheet.Cells["A2"].Value = "Course:";
            worksheet.Cells["C1"].Value = "Position:";
            worksheet.Cells["C2"].Value = "Manager:";

            // Merge cells
            worksheet.Cells["E1:J1"].Merge = true;
            worksheet.Cells["E2:J2"].Merge = true;
            worksheet.Cells["C1:D1"].Merge = true;
            worksheet.Cells["C2:D2"].Merge = true;

            // Write default headers starting from row 3
            string[] defaultHeaders = { "\tReference\t", "Tasks, Knowledge and Technical References", "Training Category", "Type", "A\nTraining Started", "B\nTraining Completed", "C\nTrainer Initials", "D\nCertifier Initials", "E\nCertifying Score", "F\nRequired Score" };
            for (int i = 0; i < defaultHeaders.Length; i++)
                worksheet.Cells[3, i + 1].Value = defaultHeaders[i];

            // Some Style
            worksheet.DefaultColWidth = 9;
            worksheet.Column(1).Width = 32;
            worksheet.Column(2).Width = 32;
            worksheet.DefaultRowHeight = 25;
            worksheet.DefaultColWidth = 10;
            worksheet.Columns.Style.WrapText = true;
            worksheet.Columns.Style.Font.Size = 9;
            worksheet.Columns.Style.Font.Name = "Arial";
            worksheet.Columns.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Columns.BestFit = true;
            worksheet.Row(3).Style.Font.Size = 10;
            worksheet.Row(1).Height = 20;
            worksheet.Row(2).Height = 20;
            worksheet.Row(3).Height = 38;
            worksheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Row(3).Style.Font.Bold = true;

            // Apply styles to specific cells and ranges
            worksheet.Cells["A1:A2"].Style.Font.Size = 11;
            worksheet.Cells["A1:A2"].Style.Font.Bold = true;
            worksheet.Cells["C1:C2"].Style.Font.Size = 11;
            worksheet.Cells["C1:C2"].Style.Font.Bold = true;

            // Set background color for specific cells and ranges
            worksheet.Cells["A1:A2"].Style.Fill.SetBackground(System.Drawing.ColorTranslator.FromHtml("#538dd5"));
            worksheet.Cells["C1:C2"].Style.Fill.SetBackground(System.Drawing.ColorTranslator.FromHtml("#538dd5"));
            worksheet.Cells["A3:J3"].Style.Fill.SetBackground(System.Drawing.ColorTranslator.FromHtml("#538dd5"));
            worksheet.Cells["B1:B2"].Style.Fill.SetBackground(System.Drawing.ColorTranslator.FromHtml("#00b050"));
            worksheet.Cells["E1:J2"].Style.Fill.SetBackground(System.Drawing.ColorTranslator.FromHtml("#00b050"));

            // Set border styles
            worksheet.Cells["A1:J3"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells["A1:J3"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Cells["A1:J3"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells["A1:J3"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            return package;
        }

        /// <summary>
        /// Reads training records from source database file
        /// </summary>
        /// <param name="filePath">File path of source file</param>
        /// <returns>List of records that read</returns>
        public static List<TrainingRecord> ReadDBExcelFile(string filePath)
        {
            // Open the Excel file using ExcelPackage
            using (ExcelPackage package = new ExcelPackage(filePath))
            {
                // Get the first page of excel file
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                // Prevent errors occur with null check
                if (worksheet == null)
                {
                    MessageBox.Show("Error reading Excel file, worksheet null or file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Clear previously added records, because new db is read and records from this db will be added
                RecordsDatabase.Clear();

                // Iterate through rows in the Excel file and create TrainingRecord objects
                int totalRows = worksheet.Dimension.Rows;
                // Start from row 2
                for (int rowIndex = 2; rowIndex <= totalRows; rowIndex++)
                {
                    // Read record from file
                    RecordsDatabase.Add(new TrainingRecord
                    {
                        Reference = worksheet.Cells[rowIndex, 1].Text,
                        Task = worksheet.Cells[rowIndex, 2].Text,
                        Category = worksheet.Cells[rowIndex, 3].Text,
                        Type = worksheet.Cells[rowIndex, 4].Text,
                        StartTime = worksheet.Cells[rowIndex, 5].Text,
                        EndTime = worksheet.Cells[rowIndex, 6].Text,
                        TrainerInitials = worksheet.Cells[rowIndex, 7].Text,
                        CertifierInitials = worksheet.Cells[rowIndex, 8].Text,
                        CertifierScore = int.TryParse(worksheet.Cells[rowIndex, 9].Value?.ToString(), out int res) ? res : default,
                        RequiredScore = int.TryParse(worksheet.Cells[rowIndex, 10].Value?.ToString(), out res) ? res : default,
                    });
                }
            }
            return RecordsDatabase;
        }

        /// <summary>
        /// Reads member info from file that is saved using this app
        /// </summary>
        /// <param name="filePath">File path of file</param>
        /// <returns>Member info that read</returns>
        public static MemberInfo ReadMemberInfoFromExcel(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage(filePath))
            {
                // Get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                // Prevent errors occur with null check
                if (worksheet == null)
                {
                    MessageBox.Show("Error reading Excel file, worksheet null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Read member info from appropriate cells
                MemberInfo memberInfo = new MemberInfo
                {
                    Trainee = worksheet.Cells["B1"].Text,
                    Course = worksheet.Cells["B2"].Text,
                    Position = worksheet.Cells["E1"].Text,
                    Manager = worksheet.Cells["E2"].Text
                };
                return memberInfo;
            }
        }

        /// <summary>
        /// Reads training records from file that is saved using this app
        /// </summary>
        /// <param name="filePath">File path of file</param>
        /// <returns>Binding list to be used in data grid view</returns>
        public static BindingList<TrainingRecord> ReadTrainingRecordsFromExcel(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage(filePath))
            {
                // Get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                // Prevent errors occur with null check
                if (worksheet == null)
                {
                    MessageBox.Show("Error reading Excel file, worksheet null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                int totalRows = worksheet.Dimension.Rows;
                BindingList<TrainingRecord> tRecords = new BindingList<TrainingRecord>();

                // Start reading from row 4 to skip the header
                for (int rowIndex = 4; rowIndex <= totalRows; rowIndex++)
                {
                    // Populate properties of TrainingRecord from Excel cells
                    TrainingRecord record = new TrainingRecord
                    {
                        Reference = worksheet.Cells[rowIndex, 1].Text,
                        Task = worksheet.Cells[rowIndex, 2].Text,
                        Category = worksheet.Cells[rowIndex, 3].Text,
                        Type = worksheet.Cells[rowIndex, 4].Text,
                        StartTime = worksheet.Cells[rowIndex, 5].Text,
                        EndTime = worksheet.Cells[rowIndex, 6].Text,
                        TrainerInitials = worksheet.Cells[rowIndex, 7].Text,
                        CertifierInitials = worksheet.Cells[rowIndex, 8].Text,
                        CertifierScore = int.TryParse(worksheet.Cells[rowIndex, 9].Value?.ToString(), out int res) ? res : default,
                        RequiredScore = int.TryParse(worksheet.Cells[rowIndex, 10].Value?.ToString(), out res) ? res : default,
                    };

                    // Add record to the list only if any data is present
                    if (!record.IsEmpty())
                    {
                        tRecords.Add(record);
                    }
                }
                return tRecords;
            }
        }

        /// <summary>
        /// Writes all to excel, saves
        /// </summary>
        /// <param name="filePath">File path of file to be saved</param>
        /// <param name="memberInfo">Member Info to be written</param>
        /// <param name="records">Training Records to be written</param>
        /// <param name="temp">If file is temporarily saved for printing</param>
        /// <returns>Success, if was successful</returns>
        public static bool WriteAllToExcel(string filePath, MemberInfo memberInfo, List<TrainingRecord> records, bool temp = false)
        {
            // Prevent errors occur with null checkiiiii
            if (filePath == null)
            {
                MessageBox.Show("File not saved.", "FilePath null", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (ExcelPackage package = InitializeExcelTemplate())
            {
                // Add a worksheet to the Excel package
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                // Write member(trainee) info to file
                worksheet.Cells["B1"].Value = memberInfo.Trainee;
                worksheet.Cells["B2"].Value = memberInfo.Course;
                worksheet.Cells["E1"].Value = memberInfo.Position;
                worksheet.Cells["E2"].Value = memberInfo.Manager;

                // Write training records into file starting from row 4
                // Iterating through properties to get column count and property value
                for (int row = 4; row < records.LongCount() + 4; row++)
                {
                    int column = 1;
                    foreach (var property in typeof(TrainingRecord).GetProperties())
                    {
                        worksheet.Cells[row, column].Value = property.GetValue(records[row - 4]);
                        worksheet.Cells[row, column++].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }

                package.SaveAs(filePath);

                // If file is created temporarily for printing, it will not add it into history
                if (!temp)
                {
                    MessageBox.Show("Saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FileHistory.AddToHistoryXml(filePath);
                }
                return true;
            }
        }
    }
}
