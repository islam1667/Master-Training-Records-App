using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;


/*
 * CONTENT OF HISTORY XML MEANT TO BE LIKE THIS
 * 
 * <fileHistory>
 *   <files date="01-08-2024">
 *     <file path="C:\Users\Downloads\Tasks-excel.xlsx" />
 *     <file path="C:\Users\Downloads\Tasks-excel.xlsx" />
 *   </files>
 *   <files date="01-07-2024">
 *     <file path="C:\Users\Downloads\Tasks-excel.xlsx" />
 *   </files>
 * </fileHistory>
 */


namespace MasterTrainingRecordsApp
{
    internal class FileHistory
    {
        /// <summary>
        /// File Path of xml, located in appdata folder
        /// </summary>
        private static string _xmlFilePath = Environment.ExpandEnvironmentVariables("%Appdata%/MasterTrainingRecordsApp/File_History.xml");

        /// <summary>
        /// Gets and Sets xml file path, if xml does not exist it creates appropriate folders and xml
        /// </summary>
        public static string XmlFilePath
        {
            get
            {
                // If xml file does not exist, it will create appropriate folders, xml file and its root element
                if (!File.Exists(_xmlFilePath))
                {
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string folderPath = Path.Combine(appDataFolder, "MasterTrainingRecordsApp");

                    Directory.CreateDirectory(folderPath);

                    _xmlFilePath = Path.Combine(folderPath, "File_History.xml");

                    File.Create(_xmlFilePath).Close(); // Close the file stream immediately
                    File.WriteAllText(_xmlFilePath, "<fileHistory>\n</fileHistory>");
                }
                return _xmlFilePath;
            }
        }

        /// <summary>
        /// Reads file pathes from xml and returns as tree node to be used in home page
        /// </summary>
        /// <returns>List of tree nodes to be used in home page</returns>
        public static List<TreeNode> ReadXmlFile()
        {
            List<TreeNode> result = new List<TreeNode>();

            // Prevent errors occur with null check
            XDocument xmlDoc = XDocument.Load(XmlFilePath);
            if (xmlDoc == null || xmlDoc.Root == null) return result;

            // Iterate through each <files> element and iterate every <file> element inside <files>
            foreach (XElement filesElement in xmlDoc.Root.Elements("files"))
            {
                TreeNode filesNode = new TreeNode($"Edited: {(string)filesElement.Attribute("date")}")
                {
                    Tag = "files"
                };

                foreach (XElement fileElement in filesElement.Elements("file"))
                {
                    TreeNode fileNode = new TreeNode((string)fileElement.Attribute("path"))
                    {
                        Tag = "file"
                    };
                    filesNode.Nodes.Add(fileNode);
                }
                result.Add(filesNode);
            }

            return result;
        }

        /// <summary>
        /// Adds file path to history xml, if there is tag for today add it inside, if not creates tag for todays history
        /// </summary>
        /// <param name="filePath">File path to be added</param>
        public static void AddToHistoryXml(string filePath)
        {
            // Prevent errors with null check
            if (!File.Exists(XmlFilePath) || XDocument.Load(XmlFilePath).Root == null) return;

            XDocument xmlDoc = XDocument.Load(XmlFilePath);

            // Get the first <files> element
            XElement firstFilesElement = xmlDoc.Root.Elements("files").FirstOrDefault();

            // if last <files> element have todays date then <file> element will be added inside of that
            if (firstFilesElement != null && DateTime.Today.ToString("dd-MM-yyyy") == firstFilesElement.Attribute("date").Value)
            {
                // Check if it already exist, delete if exist not to make copy
                foreach (XElement fileEl in firstFilesElement.Elements("file"))
                {
                    if (fileEl.Attribute("path").Value == filePath) fileEl.Remove();
                }
                firstFilesElement.Add(new XElement("file", new XAttribute("path", filePath)));
            }
            else
            {
                // If <files> element with todays date does not exist, create one with todays date
                // Add <file> inside <files> with path attribute
                XElement newFilesElement = new XElement("files",
                new XAttribute("date", DateTime.Today.ToString("dd-MM-yyyy")),
                new XElement("file", new XAttribute("path", filePath)));

                xmlDoc.Root.AddFirst(newFilesElement);
            }

            xmlDoc.Save(XmlFilePath);
        }

        /// <summary>
        /// Removes file path from xml, is called when file does not exist so to remove from history
        /// </summary>
        /// <param name="filePath">File path to be removed</param>
        public static void RemoveFromHistoryXml(string filePath)
        {
            // Prevent errors with null check
            if (!File.Exists(XmlFilePath) || XDocument.Load(XmlFilePath).Root != null) return;

            XDocument xmlDoc = XDocument.Load(XmlFilePath);

            // Iterate through every element to find and remove
            foreach (XElement filesEl in xmlDoc.Root.Elements("files"))
            {
                foreach (XElement fileEl in filesEl.Elements("file"))
                {
                    if (fileEl.Attribute("path").Value == filePath) fileEl.Remove();
                }
                // If <files> element is empty after <file> removed, also delete <files>
                if (filesEl.IsEmpty) filesEl.Remove();
            }

            xmlDoc.Save(XmlFilePath);
        }
    }
}