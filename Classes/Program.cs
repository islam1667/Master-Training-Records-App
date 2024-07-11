using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MasterTrainingRecordsApp
{
    internal static class Program
    {
        // The main entry point for the application.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Prompts window to select location to save
        /// </summary>
        /// <param name="defaultFileName">Default file name to show in window</param>
        /// <returns>File path of choosen location</returns>
        public static string PromptSaveExcelFile(string defaultFileName)
        {
            // Prompt user to choose location and name for Excel file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.FileName = $"{defaultFileName}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    return saveFileDialog.FileName;

                return null;
            }
        }

        /// <summary>
        /// Prompt to save as
        /// </summary>
        /// <param name="defaultFileName">Default file name to show in window</param>
        /// <param name="filterIndex">xlsx=1, json=2</param>
        /// <returns></returns>
        public static FileInfo PromptSaveAs(string defaultFileName, int filterIndex)
        {
            // Prompt user to choose location and name for Excel file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx|Json File(*.json)| *.json";
                saveFileDialog.FilterIndex = filterIndex;
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.FileName = $"{defaultFileName}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return new FileInfo(saveFileDialog.FileName);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Prompt to select file to open
        /// </summary>
        /// <returns></returns>
        public static string PromptOpenExcelFile()
        {
            // Prompt user to choose location and name for Excel file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                openFileDialog.DefaultExt = "xlsx";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Open Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;

                return null;
            }
        }

        /// <summary>
        /// Prompt to select multiple files
        /// </summary>
        /// <returns>List of file paths</returns>
        public static List<string> PromptOpenMultipleExcelFile()
        {
            // Prompt user to choose location and name for Excel file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                openFileDialog.DefaultExt = "xlsx";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Open Excel File";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileNames.ToList();

                return null;
            }
        }

        /// <summary>
        /// Prompts that there is unsaved changes
        /// </summary>
        /// <returns>Dialog result - Yes, No, Cancel</returns>
        public static DialogResult PromptUnsavedChanges()
        {
            return MessageBox.Show(
                text: "Do you want to save your changes?",
                caption: "Unsaved file",
                buttons: MessageBoxButtons.YesNoCancel,
                icon: MessageBoxIcon.Exclamation,
                defaultButton: MessageBoxDefaultButton.Button1);
        }
    }
}