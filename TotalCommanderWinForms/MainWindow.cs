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
            if (!currentDirectory.FullName.EndsWith("\\"))
            {
                //А
                //| Проевка является ли директория диском, так как для диска выписывается ограничение доступа
                try
                {
                    //Добавить Tty-Catch блок, так как для некоторых папок невозможно определить права доступа (Такие папки, как Config.MSI) и выдаёт исключение
                    DirectorySecurity securityInfo = Directory.GetAccessControl(currentDirectory.FullName);
                    if (securityInfo.AreAccessRulesProtected)
                    {
                        MessageBox.Show("Программа не имеет дсотупа к этой папке", "Ошибка доступа");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Программа не может получить права доступа к этой папке", "Ошибка");
                    return;
                }
            }
            gridView.Rows.Clear();
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

        private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataView = sender as DataGridView;
            if (dataView == null)
            {
                return;
            }
            DataGridViewRow selectedRow = dataView.Rows[e.RowIndex];
            object rowTag = selectedRow.Tag;
            if (rowTag is DirectoryInfo)
            {
                DirectoryInfo selectedRowInfo = rowTag as DirectoryInfo;
                LoadFilesFromDirectory(selectedRowInfo, dataView);
                DirectoryInfo viewInfo = dataView.Tag as DirectoryInfo;
                //|     Проверка, чтобы лишний раз код нк исполнялся
                //V
                if (viewInfo != null && viewInfo.Parent != null && viewInfo.Parent.FullName.Equals(selectedRowInfo.FullName))
                {
                    foreach (DataGridViewRow row in dataView.Rows)
                    {
                        DirectoryInfo rowDirectoryInfo = row.Tag as DirectoryInfo;
                        if (rowDirectoryInfo != null && rowDirectoryInfo.FullName.Equals(viewInfo.FullName))
                        {
                            row.Selected = true;
                        }
                    }
                }
                dataView.Tag = rowTag;
            }
            else
            {
                Process.Start((rowTag as FileInfo).FullName);
            }
        }

        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataView = sender as DataGridView;
            if (dataView == null)
            {
                return;
            }
            DataGridViewRow selectedRow = dataView.Rows[e.RowIndex];
            object rowTag = selectedRow.Tag;
            if (rowTag == null)
            {
                return;
            }
            string editedText = dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (editedText.Any(x => prohibitedSymbols.Contains(x)))
            {
                MessageBox.Show($"Символы {prohibitedSymbols} нельзя использоватеть в названиях директорий и файлов!", "Ошибка");
                if (rowTag is DirectoryInfo)
                {
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (rowTag as DirectoryInfo).Name;
                }
                else
                {
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (rowTag as FileInfo).Name;
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
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = info.Name;
                    return;
                }
                try
                {
                    Directory.Move(info.FullName, $"{info.Parent.FullName}\\{editedText}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка переименования директории", "Ошибка");
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = info.Name;
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
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Path.GetFileNameWithoutExtension(info.Name);
                    return;
                }
                try
                {
                    File.Move(info.FullName, $"{info.Directory.FullName}\\{editedText}{info.Extension}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка переименования директории", "Ошибка");
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Path.GetFileNameWithoutExtension(info.Name);
                }
            }
        }
    }
}
