using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TotalCommanderWinForms
{
    public partial class MainForm : Form
    {
        private ImageList imageList;
        public MainForm()
        {
            InitializeComponent();
            imageList = new ImageList();
            imageList.Images.Add("Disk",SystemIcons.Information);
            imageList.Images.Add("Directory", SystemIcons.Shield);
            imageList.Images.Add("File", SystemIcons.Warning);
            imageList.Images.Add("Selected", SystemIcons.Question);
            leftView.ImageList = imageList;
            leftView.SelectedImageKey = "Selected";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                TreeNode diskNode = leftView.Nodes.Add(info.Name);
                diskNode.ImageKey = "Disk";
                diskNode.Tag = info;
            }
        }

        private void leftView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object NodeTag = e.Node.Tag;
            if (NodeTag is DriveInfo)
            {
                DriveInfo driveInfo = (DriveInfo)NodeTag;
                
                foreach (string directoryPath in Directory.GetDirectories(driveInfo.Name))
                {
                    DirectoryInfo info = new DirectoryInfo(directoryPath);
                    TreeNode direcoryNode = e.Node.Nodes.Add(info.Name);
                    direcoryNode.ImageKey = "Directory";
                    direcoryNode.Tag = info;
                }

                foreach (string filePath in Directory.GetFiles(driveInfo.Name))
                {
                    FileInfo info = new FileInfo(filePath);
                    TreeNode fileNode = e.Node.Nodes.Add(info.Name);
                    fileNode.ImageKey = "File";
                    fileNode.Tag = info;
                }
            }
            else if (NodeTag is DirectoryInfo)
            {
                DirectoryInfo dirInfo = (DirectoryInfo)NodeTag;
                //Добавить проверку на доступность
                foreach (string directoryPath in Directory.GetDirectories(dirInfo.FullName))
                {
                    DirectoryInfo info = new DirectoryInfo(directoryPath);
                    TreeNode direcoryNode = e.Node.Nodes.Add(info.Name);
                    direcoryNode.ImageKey = "Directory";
                    direcoryNode.Tag = info;
                }

                foreach (string filePath in Directory.GetFiles(dirInfo.FullName))
                {
                    FileInfo info = new FileInfo(filePath);
                    TreeNode fileNode = e.Node.Nodes.Add(info.Name);
                    fileNode.ImageKey = "File";
                    fileNode.Tag = info;
                }
            }
            else
            {
                FileInfo fileInfo = (FileInfo)NodeTag;
                Process.Start(fileInfo.FullName);
            }
        }
    }
}
