namespace TotalCommanderWinForms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftDataView = new System.Windows.Forms.DataGridView();
            this.leftImgColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.leftNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftExtColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftAttrColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftPathInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.leftDiskSpaceInfo = new System.Windows.Forms.Label();
            this.leftDiskDropDown = new System.Windows.Forms.ComboBox();
            this.rightDataView = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightPathInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rightDiskSpaceInfo = new System.Windows.Forms.Label();
            this.rightDiskDropDown = new System.Windows.Forms.ComboBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выделениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.командыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftDataView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightDataView)).BeginInit();
            this.panel2.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.leftDataView);
            this.MainSplitContainer.Panel1.Controls.Add(this.leftPathInfo);
            this.MainSplitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.rightDataView);
            this.MainSplitContainer.Panel2.Controls.Add(this.rightPathInfo);
            this.MainSplitContainer.Panel2.Controls.Add(this.panel2);
            this.MainSplitContainer.Size = new System.Drawing.Size(800, 403);
            this.MainSplitContainer.SplitterDistance = 358;
            this.MainSplitContainer.TabIndex = 1;
            // 
            // leftDataView
            // 
            this.leftDataView.AllowUserToAddRows = false;
            this.leftDataView.AllowUserToDeleteRows = false;
            this.leftDataView.AllowUserToResizeRows = false;
            this.leftDataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.leftDataView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.leftDataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.leftDataView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.leftDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leftDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.leftImgColumn,
            this.leftNameColumn,
            this.leftExtColumn,
            this.leftSizeColumn,
            this.leftDateColumn,
            this.leftAttrColumn});
            this.leftDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftDataView.Location = new System.Drawing.Point(0, 42);
            this.leftDataView.Name = "leftDataView";
            this.leftDataView.RowHeadersVisible = false;
            this.leftDataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.leftDataView.Size = new System.Drawing.Size(354, 357);
            this.leftDataView.TabIndex = 3;
            this.leftDataView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDoubleClick);
            this.leftDataView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellEndEdit);
            // 
            // leftImgColumn
            // 
            this.leftImgColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            this.leftImgColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.leftImgColumn.FillWeight = 60.9137F;
            this.leftImgColumn.HeaderText = "Img";
            this.leftImgColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.leftImgColumn.MinimumWidth = 35;
            this.leftImgColumn.Name = "leftImgColumn";
            this.leftImgColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.leftImgColumn.Width = 35;
            // 
            // leftNameColumn
            // 
            this.leftNameColumn.FillWeight = 107.8173F;
            this.leftNameColumn.HeaderText = "Name";
            this.leftNameColumn.Name = "leftNameColumn";
            // 
            // leftExtColumn
            // 
            this.leftExtColumn.FillWeight = 107.8173F;
            this.leftExtColumn.HeaderText = "Ext";
            this.leftExtColumn.Name = "leftExtColumn";
            this.leftExtColumn.ReadOnly = true;
            // 
            // leftSizeColumn
            // 
            this.leftSizeColumn.FillWeight = 107.8173F;
            this.leftSizeColumn.HeaderText = "Size";
            this.leftSizeColumn.Name = "leftSizeColumn";
            this.leftSizeColumn.ReadOnly = true;
            // 
            // leftDateColumn
            // 
            this.leftDateColumn.FillWeight = 107.8173F;
            this.leftDateColumn.HeaderText = "Date";
            this.leftDateColumn.Name = "leftDateColumn";
            this.leftDateColumn.ReadOnly = true;
            // 
            // leftAttrColumn
            // 
            this.leftAttrColumn.FillWeight = 107.8173F;
            this.leftAttrColumn.HeaderText = "Attr";
            this.leftAttrColumn.Name = "leftAttrColumn";
            this.leftAttrColumn.ReadOnly = true;
            // 
            // leftPathInfo
            // 
            this.leftPathInfo.BackColor = System.Drawing.Color.LightSkyBlue;
            this.leftPathInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftPathInfo.Location = new System.Drawing.Point(0, 21);
            this.leftPathInfo.Name = "leftPathInfo";
            this.leftPathInfo.Size = new System.Drawing.Size(354, 21);
            this.leftPathInfo.TabIndex = 2;
            this.leftPathInfo.Text = "Path";
            this.leftPathInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.leftDiskSpaceInfo);
            this.panel1.Controls.Add(this.leftDiskDropDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 21);
            this.panel1.TabIndex = 1;
            // 
            // leftDiskSpaceInfo
            // 
            this.leftDiskSpaceInfo.BackColor = System.Drawing.SystemColors.Control;
            this.leftDiskSpaceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftDiskSpaceInfo.Location = new System.Drawing.Point(43, 0);
            this.leftDiskSpaceInfo.Name = "leftDiskSpaceInfo";
            this.leftDiskSpaceInfo.Size = new System.Drawing.Size(311, 21);
            this.leftDiskSpaceInfo.TabIndex = 5;
            this.leftDiskSpaceInfo.Text = "0 из 0 свободно";
            this.leftDiskSpaceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // leftDiskDropDown
            // 
            this.leftDiskDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftDiskDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftDiskDropDown.FormattingEnabled = true;
            this.leftDiskDropDown.Location = new System.Drawing.Point(0, 0);
            this.leftDiskDropDown.Name = "leftDiskDropDown";
            this.leftDiskDropDown.Size = new System.Drawing.Size(43, 21);
            this.leftDiskDropDown.TabIndex = 4;
            this.leftDiskDropDown.SelectedIndexChanged += new System.EventHandler(this.leftDiskDropDown_SelectedIndexChanged);
            // 
            // rightDataView
            // 
            this.rightDataView.AllowUserToAddRows = false;
            this.rightDataView.AllowUserToDeleteRows = false;
            this.rightDataView.AllowUserToResizeRows = false;
            this.rightDataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rightDataView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.rightDataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rightDataView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.rightDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rightDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.rightDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightDataView.Location = new System.Drawing.Point(0, 42);
            this.rightDataView.Name = "rightDataView";
            this.rightDataView.RowHeadersVisible = false;
            this.rightDataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rightDataView.Size = new System.Drawing.Size(434, 357);
            this.rightDataView.TabIndex = 6;
            this.rightDataView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDoubleClick);
            this.rightDataView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellEndEdit);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewImageColumn1.FillWeight = 60.9137F;
            this.dataGridViewImageColumn1.HeaderText = "Img";
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.MinimumWidth = 35;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 35;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 107.8173F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 107.8173F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Ext";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 107.8173F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Size";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 107.8173F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 107.8173F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Attr";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // rightPathInfo
            // 
            this.rightPathInfo.BackColor = System.Drawing.Color.LightSkyBlue;
            this.rightPathInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightPathInfo.Location = new System.Drawing.Point(0, 21);
            this.rightPathInfo.Name = "rightPathInfo";
            this.rightPathInfo.Size = new System.Drawing.Size(434, 21);
            this.rightPathInfo.TabIndex = 5;
            this.rightPathInfo.Text = "Path";
            this.rightPathInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rightDiskSpaceInfo);
            this.panel2.Controls.Add(this.rightDiskDropDown);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 21);
            this.panel2.TabIndex = 4;
            // 
            // rightDiskSpaceInfo
            // 
            this.rightDiskSpaceInfo.BackColor = System.Drawing.SystemColors.Control;
            this.rightDiskSpaceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightDiskSpaceInfo.Location = new System.Drawing.Point(43, 0);
            this.rightDiskSpaceInfo.Name = "rightDiskSpaceInfo";
            this.rightDiskSpaceInfo.Size = new System.Drawing.Size(391, 21);
            this.rightDiskSpaceInfo.TabIndex = 3;
            this.rightDiskSpaceInfo.Text = "0 из 0 свободно";
            this.rightDiskSpaceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rightDiskDropDown
            // 
            this.rightDiskDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.rightDiskDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightDiskDropDown.FormattingEnabled = true;
            this.rightDiskDropDown.Location = new System.Drawing.Point(0, 0);
            this.rightDiskDropDown.Name = "rightDiskDropDown";
            this.rightDiskDropDown.Size = new System.Drawing.Size(43, 21);
            this.rightDiskDropDown.TabIndex = 0;
            this.rightDiskDropDown.SelectedIndexChanged += new System.EventHandler(this.leftDiskDropDown_SelectedIndexChanged);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлыToolStripMenuItem,
            this.выделениеToolStripMenuItem,
            this.командыToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.mainMenuStrip.TabIndex = 2;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // файлыToolStripMenuItem
            // 
            this.файлыToolStripMenuItem.Name = "файлыToolStripMenuItem";
            this.файлыToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.файлыToolStripMenuItem.Text = "Файлы";
            // 
            // выделениеToolStripMenuItem
            // 
            this.выделениеToolStripMenuItem.Name = "выделениеToolStripMenuItem";
            this.выделениеToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.выделениеToolStripMenuItem.Text = "Выделение";
            // 
            // командыToolStripMenuItem
            // 
            this.командыToolStripMenuItem.Name = "командыToolStripMenuItem";
            this.командыToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.командыToolStripMenuItem.Text = "Команды";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 427);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 23);
            this.panel3.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(243, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(162, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(81, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.panel3);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(529, 229);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тотальный коммандир";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftDataView)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightDataView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выделениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem командыToolStripMenuItem;
        private System.Windows.Forms.DataGridView leftDataView;
        private System.Windows.Forms.Label leftPathInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label leftDiskSpaceInfo;
        private System.Windows.Forms.ComboBox leftDiskDropDown;
        private System.Windows.Forms.Label rightPathInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label rightDiskSpaceInfo;
        private System.Windows.Forms.ComboBox rightDiskDropDown;
        private System.Windows.Forms.DataGridViewImageColumn leftImgColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftExtColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftSizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftAttrColumn;
        private System.Windows.Forms.DataGridView rightDataView;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}