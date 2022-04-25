﻿using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace TotalCommanderWinForms
{
    public partial class MainWindow : Form
    {
        private static string prohibitedSymbols;
        private static string dateFormat;
        private WindowSide side;

        static MainWindow()
        {
            prohibitedSymbols = "\\|/*:?\"<>";
            dateFormat = "dd.MM.yyyy HH:mm:ss";
        }

        public MainWindow()
        {
            side = WindowSide.Left;
            InitializeComponent();
            leftDataView.Click += new EventHandler((sender, e) => { side = WindowSide.Left; });
            rightDataView.Click += new EventHandler((sender, e) => { side = WindowSide.Right; });
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
                row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", "<DIR>", info.LastWriteTime.ToString(dateFormat), info.Attributes });
                gridView.Rows.Add(row);
            }

            foreach (string filePath in Directory.GetFiles(currentDrive.Name))
            {
                FileInfo info = new FileInfo(filePath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), Path.GetFileNameWithoutExtension(info.Name), info.Extension, info.Length, info.LastWriteTime.ToString(dateFormat), info.Attributes });
                gridView.Rows.Add(row);
            }
            gridView.Tag = new DirectoryInfo(currentDrive.Name);
        }

        private void LoadFilesFromDirectory(DirectoryInfo currentDirectory, DataGridView gridView)
        {
            if (!currentDirectory.FullName.EndsWith(":\\"))
            {
                //^
                //| Проевка является ли директория диском, так как для диска выписывается ограничение доступа
                try
                {
                    //Добавить Try-Catch блок, так как для некоторых папок невозможно определить права доступа (Такие папки, как Config.MSI) и выдаёт исключение
                    DirectorySecurity securityInfo = Directory.GetAccessControl(currentDirectory.FullName);
                    if (securityInfo.AreAccessRulesProtected)
                    {
                        MessageBox.Show("Программа не имеет доступа к этой папке", "Ошибка доступа");
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
                rowInsert.SetValues(new object[6] { SystemIcons.Exclamation, "[Назад]", null, null, null, null });
                rowInsert.Cells[1].ReadOnly = true;
                gridView.Rows.Add(rowInsert);
            }

            foreach (string directoryPath in Directory.GetDirectories(currentDirectory.FullName))
            {
                DirectoryInfo info = new DirectoryInfo(directoryPath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", "<DIR>", info.LastWriteTime.ToString(dateFormat), info.Attributes });
                gridView.Rows.Add(row);
            }

            foreach (string filePath in Directory.GetFiles(currentDirectory.FullName))
            {
                FileInfo info = new FileInfo(filePath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), Path.GetFileNameWithoutExtension(info.Name), info.Extension, info.Length, info.LastWriteTime.ToString(dateFormat), info.Attributes });
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
                        row.Selected = rowDirectoryInfo != null && rowDirectoryInfo.FullName.Equals(viewInfo.FullName);
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
                    dataView.Rows[e.RowIndex].Tag = new DirectoryInfo($"{info.Parent.FullName}\\{editedText}");
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
                string fileNameNoExt = Path.GetFileNameWithoutExtension(info.Name);
                if (fileNameNoExt.Equals(editedText))
                {
                    return;
                }
                if (File.Exists($"{info.Directory.FullName}\\{editedText}{info.Extension}"))
                {
                    MessageBox.Show("Такой файл уже существует", "Ошибка");
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fileNameNoExt;
                    return;
                }
                try
                {
                    File.Move(info.FullName, $"{info.Directory.FullName}\\{editedText}{info.Extension}");
                    dataView.Rows[e.RowIndex].Tag = new FileInfo($"{info.Directory.FullName}\\{editedText}{info.Extension}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка переименования директории", "Ошибка");
                    dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fileNameNoExt;
                }
            }
        }

        public void CopyToClipboard()
        {
            DataGridViewRowCollection rowCollection = null;
            switch (side)
            {
                case WindowSide.Left:
                    rowCollection = leftDataView.Rows;
                    break;
                case WindowSide.Right:
                    rowCollection = rightDataView.Rows;
                    break;
            }
            if (rowCollection != null)
            {
                StringCollection CopyCollection = new StringCollection();
                foreach (DataGridViewRow row in rowCollection)
                {
                    if (row.Selected && row.Cells[2].Value != null)
                    {
                        FileInfo fileInfo = row.Tag as FileInfo;
                        DirectoryInfo dirInfo = row.Tag as DirectoryInfo;
                        if (fileInfo != null)
                        {
                            CopyCollection.Add(fileInfo.FullName);
                        }
                        else if (dirInfo != null)
                        {
                            CopyCollection.Add(dirInfo.FullName);
                        }
                    }
                }
                Clipboard.SetFileDropList(CopyCollection);
            }
        }

        public void InsertFromClipboard()
        {
            DataGridView dataVeiw = null;
            switch (side)
            {
                case WindowSide.Left:
                    dataVeiw = leftDataView;
                    break;
                case WindowSide.Right:
                    dataVeiw = rightDataView;
                    break;
            }
            if (dataVeiw != null)
            {
                StringCollection CopyCollection = Clipboard.GetFileDropList();
                DirectoryInfo currentDirectory = dataVeiw.Tag as DirectoryInfo;
                foreach (string path in CopyCollection)
                {
                    if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                    {
                        try
                        {
                            CopyDirectory(new DirectoryInfo(path), currentDirectory);
                        }
                        catch
                        {
                            MessageBox.Show($"Путь {path} невозможно скопировать или он был скопирован неполностью");
                            continue;
                        }
                    }
                    else
                    {
                        FileInfo info = new FileInfo(path);
                        info.CopyTo($"{currentDirectory.FullName}\\{info.Name}");
                    }
                }
                if (CopyCollection.Count > 0)
                {
                    LoadFilesFromDirectory(currentDirectory, dataVeiw);
                }
            }
        }

        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyDirectory(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertFromClipboard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridView dataVeiw = null;
            switch (side)
            {
                case WindowSide.Left:
                    dataVeiw = leftDataView;
                    break;
                case WindowSide.Right:
                    dataVeiw = rightDataView;
                    break;
            }
            if (dataVeiw != null)
            {
                foreach (DataGridViewRow row in dataVeiw.Rows)
                {
                    if (row.Selected && row.Cells[2].Value != null)
                    {
                        FileInfo fileInfo = row.Tag as FileInfo;
                        DirectoryInfo dirInfo = row.Tag as DirectoryInfo;
                        if (fileInfo != null)
                        {
                            try
                            {
                                fileInfo.Delete();
                                dataVeiw.Rows.Remove(row);
                            }
                            catch
                            {
                                MessageBox.Show($"Не удалось удалить файл {fileInfo.FullName}", "Ошибка");
                            }
                        }
                        else if (dirInfo != null)
                        {
                            try
                            {
                                dirInfo.Delete();
                                dataVeiw.Rows.Remove(row);
                            }
                            catch
                            {
                                MessageBox.Show($"Не удалось удалить папку {dirInfo.FullName}","Ошибка");
                            }
                        }
                    }
                }
            }
        }
    }

    public enum WindowSide
    {
        Left,
        Right
    }
}
