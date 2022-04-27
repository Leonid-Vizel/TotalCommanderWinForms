using System;
using System.Collections.Generic;
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
        private Button[] lowerButtons;

        static MainWindow()
        {
            prohibitedSymbols = "\\|/*:?\"<>";
            dateFormat = "dd.MM.yyyy HH:mm:ss";
        }

        public MainWindow()
        {
            side = WindowSide.Left;
            InitializeComponent();
            lowerButtons = new Button[5] { copyBtn, transferBtn, pasteBtn, deleteBtn, createDirBtn };
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
            OnLowerPanelSizeChanged(null, null);
        }

        private void LoadFilesFromDisk(DriveInfo currentDrive, DataGridView gridView)
        {
            foreach (string directoryPath in Directory.GetDirectories(currentDrive.Name))
            {
                DirectoryInfo info = new DirectoryInfo(directoryPath);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.Tag = info;
                row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", (long)0, info.LastWriteTime.ToString(dateFormat), info.Attributes });
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
            if (gridView == leftDataView)
            {
                leftPathInfo.Text = $"Путь: {currentDrive.Name}";
            }
            else if (gridView == rightDataView)
            {
                rightPathInfo.Text = $"Путь: {currentDrive.Name}";
            }
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
                row.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", (long)0, info.LastWriteTime.ToString(dateFormat), info.Attributes });
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
            gridView.Tag = currentDirectory;
            if (gridView == leftDataView)
            {
                leftPathInfo.Text = $"Путь: {currentDirectory.FullName}";
            }
            else if (gridView == rightDataView)
            {
                rightPathInfo.Text = $"Путь: {currentDirectory.FullName}";
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
            if (e.RowIndex == -1)
            {
                return;
            }
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
            }
            else
            {
                try
                {
                    Process.Start((rowTag as FileInfo).FullName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Ошибка запуска файла: \n{exception}", "Ошибка запуска");
                }
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
            if (target.FullName.EndsWith("\\"))
            {
                Directory.CreateDirectory($"{target.FullName}{source.Name}");
            }
            else
            {
                Directory.CreateDirectory($"{target.FullName}\\{source.Name}");
            }

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

        private void OnTransferClick(object sender, EventArgs e)
        {
            DataGridView dataVeiwSender = null;
            DataGridView dataVeiwReceiver = null;
            switch (side)
            {
                case WindowSide.Left:
                    dataVeiwSender = leftDataView;
                    dataVeiwReceiver = rightDataView;
                    break;
                case WindowSide.Right:
                    dataVeiwSender = rightDataView;
                    dataVeiwReceiver = leftDataView;
                    break;
            }
            if (dataVeiwSender != null && dataVeiwReceiver != null)
            {
                DirectoryInfo receiverDirectory = dataVeiwReceiver.Tag as DirectoryInfo;
                List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dataVeiwSender.Rows)
                {
                    if (row.Selected && row.Cells[2].Value != null)
                    {
                        FileInfo fileInfo = row.Tag as FileInfo;
                        DirectoryInfo dirInfo = row.Tag as DirectoryInfo;
                        if (fileInfo != null)
                        {
                            FileInfo info = null;
                            try
                            {
                                if (receiverDirectory.FullName.EndsWith("\\"))
                                {
                                    //Заначит это диск
                                    fileInfo.MoveTo($"{receiverDirectory.FullName}{fileInfo.Name}");
                                    info = new FileInfo($"{receiverDirectory.FullName}{fileInfo.Name}");
                                }
                                else
                                {
                                    //Значит это директория
                                    fileInfo.MoveTo($"{receiverDirectory.FullName}\\{fileInfo.Name}");
                                    info = new FileInfo($"{receiverDirectory.FullName}\\{fileInfo.Name}");
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"Ошибка копирования файла {fileInfo.FullName}", "Ошибка");
                                continue;
                            }
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dataVeiwReceiver);
                            newRow.Tag = info;
                            newRow.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), Path.GetFileNameWithoutExtension(info.Name), info.Extension, info.Length, info.LastWriteTime.ToString(dateFormat), info.Attributes });
                            dataVeiwReceiver.Rows.Add(newRow);
                            rowsToDelete.Add(row);
                        }
                        else if (dirInfo != null)
                        {
                            if (dirInfo.FullName.Equals(receiverDirectory.FullName))
                            {
                                MessageBox.Show("Невозможно копировать папку саму в себя", "Ошибка");
                                return;
                            }
                            if (!receiverDirectory.GetDirectories().Select(x => x.FullName).Any(x => x.Equals(dirInfo.FullName)))
                            {
                                try
                                {
                                    dirInfo.MoveTo(receiverDirectory.FullName);
                                }
                                catch
                                {
                                    MessageBox.Show($"Ошибка копирования файла {fileInfo.FullName}", "Ошибка");
                                    continue;
                                }
                                DirectoryInfo info = new DirectoryInfo($"{receiverDirectory}\\{dirInfo.Name}");
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataVeiwReceiver);
                                newRow.Tag = info;
                                newRow.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", (long)0, info.LastWriteTime.ToString(dateFormat), info.Attributes });
                                dataVeiwReceiver.Rows.Add(newRow);
                                rowsToDelete.Add(row);
                            }
                            else
                            {
                                MessageBox.Show($"Папка с именем '{dirInfo.Name}' уже существует в '{receiverDirectory.FullName}'", "Ошибка");
                            }
                        }
                    }
                }
                rowsToDelete.ForEach(x => dataVeiwSender.Rows.Remove(x));
            }
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            DataGridView dataVeiwSender = null;
            DataGridView dataVeiwReceiver = null;
            switch (side)
            {
                case WindowSide.Left:
                    dataVeiwSender = leftDataView;
                    dataVeiwReceiver = rightDataView;
                    break;
                case WindowSide.Right:
                    dataVeiwSender = rightDataView;
                    dataVeiwReceiver = leftDataView;
                    break;
            }
            if (dataVeiwSender != null && dataVeiwReceiver != null)
            {
                DirectoryInfo receiverDirectory = dataVeiwReceiver.Tag as DirectoryInfo;
                foreach (DataGridViewRow row in dataVeiwSender.Rows)
                {
                    if (row.Selected && row.Cells[2].Value != null)
                    {
                        FileInfo fileInfo = row.Tag as FileInfo;
                        DirectoryInfo dirInfo = row.Tag as DirectoryInfo;
                        if (fileInfo != null)
                        {
                            FileInfo info = null;
                            try
                            {
                                if (receiverDirectory.FullName.EndsWith("\\"))
                                {
                                    //Заначит это диск
                                    fileInfo.CopyTo($"{receiverDirectory.FullName}{fileInfo.Name}");
                                    info = new FileInfo($"{receiverDirectory.FullName}{fileInfo.Name}");
                                }
                                else
                                {
                                    //Значит это директория
                                    fileInfo.CopyTo($"{receiverDirectory.FullName}\\{fileInfo.Name}");
                                    info = new FileInfo($"{receiverDirectory.FullName}\\{fileInfo.Name}");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка копирования файла {fileInfo.FullName}\n{ex.Message}", "Ошибка");
                                continue;
                            }
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dataVeiwReceiver);
                            newRow.Tag = info;
                            newRow.SetValues(new object[6] { Icon.ExtractAssociatedIcon(info.FullName), Path.GetFileNameWithoutExtension(info.Name), info.Extension, info.Length, info.LastWriteTime.ToString(dateFormat), info.Attributes });
                            dataVeiwReceiver.Rows.Add(newRow);
                        }
                        else if (dirInfo != null)
                        {
                            if (dirInfo.FullName.Equals(receiverDirectory.FullName))
                            {
                                MessageBox.Show("Невозможно копировать папку саму в себя", "Ошибка");
                                return;
                            }
                            if (!receiverDirectory.GetDirectories().Select(x => x.FullName).Any(x => x.Equals(dirInfo.FullName)))
                            {
                                try
                                {
                                    CopyDirectory(dirInfo, receiverDirectory);
                                }
                                catch
                                {
                                    MessageBox.Show($"Ошибка копирования файла {fileInfo.FullName}", "Ошибка");
                                    continue;
                                }
                                DirectoryInfo info = new DirectoryInfo($"{receiverDirectory}\\{dirInfo.Name}");
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataVeiwReceiver);
                                newRow.Tag = info;
                                newRow.SetValues(new object[6] { DefaultIcons.Folder, info.Name, "<DIR>", (long)0, info.LastWriteTime.ToString(dateFormat), info.Attributes });
                                dataVeiwReceiver.Rows.Add(newRow);
                            }
                            else
                            {
                                MessageBox.Show($"Папка с именем '{dirInfo.Name}' уже существует в '{receiverDirectory.FullName}'", "Ошибка");
                            }
                        }
                    }
                }
            }
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            InsertFromClipboard();
        }

        private void OnDeleteClick(object sender, EventArgs e)
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
                List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();
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
                                rowsToDelete.Add(row);
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
                                rowsToDelete.Add(row);
                            }
                            catch
                            {
                                MessageBox.Show($"Не удалось удалить папку {dirInfo.FullName}", "Ошибка");
                            }
                        }
                    }
                }
                rowsToDelete.ForEach(x => dataVeiw.Rows.Remove(x));
            }
        }

        private void OnCreateDirClick(object sender, EventArgs e)
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
                DirectoryInfo info = dataVeiw.Tag as DirectoryInfo;
                if (info == null)
                {
                    return;
                }
                AskDirectoryNameForm askForm = new AskDirectoryNameForm(prohibitedSymbols);
                if (askForm.ShowDialog() == DialogResult.OK)
                {
                    if (info.FullName.EndsWith("\\"))
                    {
                        //Значит это диск
                        if (Directory.Exists($"{info.FullName}{askForm.Result}"))
                        {
                            MessageBox.Show("Папка с таким названием уже существует!", "Ошибка");
                            return;
                        }
                        DataGridViewRow row = new DataGridViewRow();
                        DirectoryInfo newDirInfo = null;
                        try
                        {
                            newDirInfo = Directory.CreateDirectory($"{info.FullName}{askForm.Result}");
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка создания папки!", "Ошибка");
                            return;
                        }
                        row.Tag = newDirInfo;
                        row.CreateCells(dataVeiw);
                        row.SetValues(new object[6] { DefaultIcons.Folder, newDirInfo.Name, "<DIR>", (long)0, newDirInfo.LastWriteTime.ToString(dateFormat), newDirInfo.Attributes });
                        dataVeiw.Rows.Add(row);
                    }
                    else
                    {
                        //Значит это папка
                        if (Directory.Exists($"{info.FullName}\\{askForm.Result}"))
                        {
                            MessageBox.Show("Папка с таким названием уже существует!", "Ошибка");
                            return;
                        }
                        DataGridViewRow row = new DataGridViewRow();
                        DirectoryInfo newDirInfo = null;
                        try
                        {
                            newDirInfo = Directory.CreateDirectory($"{info.FullName}\\{askForm.Result}");
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка создания папки!", "Ошибка");
                            return;
                        }
                        row.Tag = newDirInfo;
                        row.CreateCells(dataVeiw);
                        row.SetValues(new object[6] { DefaultIcons.Folder, newDirInfo.Name, "<DIR>", (long)0, newDirInfo.LastWriteTime.ToString(dateFormat), newDirInfo.Attributes });
                        dataVeiw.Rows.Add(row);
                    }
                }
            }
        }

        private void OnLowerPanelSizeChanged(object sender, EventArgs e)
        {
            int applyingBtnWidth = lowerPanel.Width / 5;
            for (int i = 0; i < lowerButtons.Length; i++)
            {
                lowerButtons[i].Location = new Point(i * applyingBtnWidth, lowerButtons[i].Location.Y);
                lowerButtons[i].Size = new Size(applyingBtnWidth, lowerButtons[i].Size.Height);
            }
        }

        private void OnSwapBtnClick(object sender, EventArgs e)
        {
            DataGridView dataVeiwSender = null;
            DataGridView dataVeiwReceiver = null;
            switch (side)
            {
                case WindowSide.Left:
                    dataVeiwSender = leftDataView;
                    dataVeiwReceiver = rightDataView;
                    break;
                case WindowSide.Right:
                    dataVeiwSender = rightDataView;
                    dataVeiwReceiver = leftDataView;
                    break;
            }
            if (dataVeiwSender != null && dataVeiwReceiver != null)
            {
                DirectoryInfo senderInfo = dataVeiwSender.Tag as DirectoryInfo;
                DirectoryInfo receiverInfo = dataVeiwReceiver.Tag as DirectoryInfo;
                if (senderInfo != null && receiverInfo != null && !senderInfo.FullName.Equals(receiverInfo.FullName))
                {
                    LoadFilesFromDirectory(senderInfo, dataVeiwReceiver);
                    LoadFilesFromDirectory(receiverInfo, dataVeiwSender);
                }
            }
        }

        private void OnEqualizeBtnClick(object sender, EventArgs e)
        {
            DataGridView dataVeiwSender = null;
            DataGridView dataVeiwReceiver = null;
            switch (side)
            {
                case WindowSide.Left:
                    dataVeiwSender = leftDataView;
                    dataVeiwReceiver = rightDataView;
                    break;
                case WindowSide.Right:
                    dataVeiwSender = rightDataView;
                    dataVeiwReceiver = leftDataView;
                    break;
            }
            if (dataVeiwSender != null && dataVeiwReceiver != null)
            {
                DirectoryInfo senderInfo = dataVeiwSender.Tag as DirectoryInfo;
                DirectoryInfo receiverInfo = dataVeiwReceiver.Tag as DirectoryInfo;
                if (senderInfo != null && receiverInfo != null && !senderInfo.FullName.Equals(receiverInfo.FullName))
                {
                    LoadFilesFromDirectory(senderInfo, dataVeiwReceiver);
                }
            }
        }

        private void OnBackBtnClick(object sender, EventArgs e)
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
                DirectoryInfo info = dataVeiw.Tag as DirectoryInfo;
                if (info == null || info.Parent == null)
                {
                    return;
                }
                LoadFilesFromDirectory(info.Parent, dataVeiw);
            }
        }

        private void OnExitBtnClick(object sender, EventArgs e) => Close();
    }

    public enum WindowSide
    {
        Left,
        Right
    }
}
