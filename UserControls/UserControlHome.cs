using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MasterTrainingRecordsApp
{
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            InitializeComponent();

            UpdateTreeView();
            TreeViewFileHistory.NodeMouseDoubleClick += NodeClickOpenFile;
        }

        /// <summary>
        /// Opens file when node clicked, if choosen file does not exist, removes from xml and list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeClickOpenFile(object sender, TreeNodeMouseClickEventArgs e)
        {
            // If the node is 'files' node it will return, if the node is 'file' which contains
            // the path of file it will proceed
            if ((string)e.Node.Tag != "file") return;

            // If file does not exist, remove it from the list
            string filePath = e.Node.Text;
            if (!File.Exists(filePath))
            {
                FileHistory.RemoveFromHistoryXml(filePath);
                MessageBox.Show("File Does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else (this.ParentForm as MainForm).OpenFileFrom(filePath);

            UpdateTreeView();
        }

        /// <summary>
        /// Opens new file, calls method in MainForm.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewFile_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainForm).MenuItemNewFile_Click(sender, e);
        }

        /// <summary>
        /// Opens file, calls method in MainForm.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainForm).MenuItemOpenFile_Click(sender, e);
        }

        /// <summary>
        /// Reads file history and loads it into tree
        /// </summary>
        public void UpdateTreeView()
        {
            // Clear nodes beforehead to avoid repetetion
            TreeViewFileHistory.Nodes.Clear();
            List<TreeNode> nodes = FileHistory.ReadXmlFile();
            foreach (TreeNode node in nodes) TreeViewFileHistory.Nodes.Add(node);

            TreeViewFileHistory.ExpandAll();
        }

        /// <summary>
        /// Opens help page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonHelp_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainForm).MenuItemHelp_Click(sender, e);
        }
    }
}
