using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommanderWinForms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                leftDiskDropDown.Items.Add(info);
                rightDiskDropDown.Items.Add(info);
            }
            leftDiskDropDown.SelectedIndex = rightDiskDropDown.SelectedIndex = 0;
        }

        private void leftDiskDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox senderBox = sender as ComboBox;
            if (senderBox == null)
            {
                return;
            }
            DriveInfo currentDrive = senderBox.SelectedItem as DriveInfo;
            if (currentDrive == null)
            {
                return;
            }
            switch (senderBox.Name)
            {
                case "leftDiskDropDown":
                    leftDataView.Rows.Clear();
                    leftDiskSpaceInfo.Text = $"{currentDrive.AvailableFreeSpace} из {currentDrive.TotalSize} совбодно";
                    foreach (string directoryPath in Directory.GetDirectories(currentDrive.Name))
                    {
                        DirectoryInfo info = new DirectoryInfo(directoryPath);
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(leftDataView);
                        row.Tag = info;
                        row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", "<DIR>", info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                        leftDataView.Rows.Add(row);
                    }

                    foreach (string filePath in Directory.GetFiles(currentDrive.Name))
                    {
                        FileInfo info = new FileInfo(filePath);
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(leftDataView);
                        row.Tag = info;
                        row.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), info.Name, info.Extension, info.Length, info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                        leftDataView.Rows.Add(row);
                    }
                    break;
                case "rightDiskDropDown":
                    rightDataView.Rows.Clear();
                    rightDiskSpaceInfo.Text = $"{currentDrive.AvailableFreeSpace} из {currentDrive.TotalSize} совбодно";
                    foreach (string directoryPath in Directory.GetDirectories(currentDrive.Name))
                    {
                        DirectoryInfo info = new DirectoryInfo(directoryPath);
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(rightDataView);
                        row.Tag = info;
                        row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", "<DIR>", info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                        rightDataView.Rows.Add(row);
                    }

                    foreach (string filePath in Directory.GetFiles(currentDrive.Name))
                    {
                        FileInfo info = new FileInfo(filePath);
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(rightDataView);
                        row.Tag = info;
                        row.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), info.Name, info.Extension, info.Length, info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                        rightDataView.Rows.Add(row);
                    }
                    break;
            }
        }
    }
}
