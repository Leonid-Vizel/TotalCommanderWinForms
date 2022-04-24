using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommanderWinForms
{
    public partial class MainWindow : Form
    {
        private static string prohibitedSymbols = "\\|/*:?\"<>";

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

        private void LoadFilesFromDisk(DriveInfo currentDrive, DataGridView gridView)
        {

            foreach (string directoryPath in Directory.GetDirectories(currentDrive.Name))
            {
                DirectoryInfo info = new DirectoryInfo(directoryPath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", "<DIR>", info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                gridView.Rows.Add(row);
            }

            foreach (string filePath in Directory.GetFiles(currentDrive.Name))
            {
                FileInfo info = new FileInfo(filePath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), Path.GetFileNameWithoutExtension(info.Name), info.Extension, info.Length, info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                gridView.Rows.Add(row);
            }
        }

        private void LoadFilesFromDirectory(DirectoryInfo currentDirectory, DataGridView gridView)
        {
            if (currentDirectory.Parent != null)
            {
                DataGridViewRow rowInsert = new DataGridViewRow();
                rowInsert.CreateCells(gridView);
                rowInsert.Tag = currentDirectory.Parent;
                rowInsert.SetValues(new object[6] { SystemIcons.Exclamation, "[Назад]", "", "", "", "" });
                rowInsert.Cells[1].ReadOnly = true;
                gridView.Rows.Add(rowInsert);
            }

            foreach (string directoryPath in Directory.GetDirectories(currentDirectory.FullName))
            {
                DirectoryInfo info = new DirectoryInfo(directoryPath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", "<DIR>", info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                gridView.Rows.Add(row);
            }

            foreach (string filePath in Directory.GetFiles(currentDirectory.FullName))
            {
                FileInfo info = new FileInfo(filePath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), Path.GetFileNameWithoutExtension(info.Name), info.Extension, info.Length, info.LastWriteTime.ToString("dd.MM.yyyy"), info.Attributes });
                gridView.Rows.Add(row);
            }
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
                    LoadFilesFromDisk(currentDrive, leftDataView);
                    break;
                case "rightDiskDropDown":
                    rightDataView.Rows.Clear();
                    rightDiskSpaceInfo.Text = $"{currentDrive.AvailableFreeSpace} из {currentDrive.TotalSize} совбодно";
                    LoadFilesFromDisk(currentDrive, rightDataView);
                    break;
            }
        }

        private void leftDataView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = leftDataView.Rows[e.RowIndex];
            object rowTag = selectedRow.Tag;
            if (rowTag is DirectoryInfo)
            {
                leftDataView.Rows.Clear();
                LoadFilesFromDirectory(rowTag as DirectoryInfo, leftDataView);
            }
            else
            {
                Process.Start((rowTag as FileInfo).FullName);
            }
        }

        private void leftDataView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = leftDataView.Rows[e.RowIndex];
            object rowTag = selectedRow.Tag;
            if (rowTag == null)
            {
                return;
            }
            string editedText = leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (editedText.Any(x=>prohibitedSymbols.Contains(x)))
            {
                MessageBox.Show($"Символы {prohibitedSymbols} нельзя использоватеть в названиях директорий и файлов!","Ошибка");
                if (rowTag is DirectoryInfo)
                {
                    leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (rowTag as DirectoryInfo).Name;
                }
                else
                {
                    leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (rowTag as FileInfo).Name;
                }
                return;
            }
            if (rowTag is DirectoryInfo)
            {
                DirectoryInfo info = rowTag as DirectoryInfo;
                if (info.Name.Equals(editedText))
                {
                    return;
                }
                if (File.Exists($"{info.Parent.FullName}\\{editedText}"))
                {
                    MessageBox.Show("Такая папка уже существует", "Ошибка");
                    leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = info.Name;
                    return;
                }    
                try
                {
                    Directory.Move(info.FullName, $"{info.Parent.FullName}\\{editedText}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка переименования директории","Ошибка");
                    leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = info.Name;
                }
            }
            else
            {
                FileInfo info = rowTag as FileInfo;
                if (info.Name.Equals(editedText))
                {
                    return;
                }
                if (File.Exists($"{info.Directory.FullName}\\{editedText}{info.Extension}"))
                {
                    MessageBox.Show("Такой файл уже существует", "Ошибка");
                    leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Path.GetFileNameWithoutExtension(info.Name);
                    return;
                }
                try
                {
                    File.Move(info.FullName, $"{info.Directory.FullName}\\{editedText}{info.Extension}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка переименования директории", "Ошибка");
                    leftDataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Path.GetFileNameWithoutExtension(info.Name);
                }
            }
        }
    }
}
