
namespace TotalCommanderWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftView = new System.Windows.Forms.TreeView();
            this.rightView = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.leftView);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.rightView);
            this.MainSplitContainer.Size = new System.Drawing.Size(640, 434);
            this.MainSplitContainer.SplitterDistance = 284;
            this.MainSplitContainer.TabIndex = 0;
            // 
            // leftView
            // 
            this.leftView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftView.Location = new System.Drawing.Point(0, 0);
            this.leftView.MinimumSize = new System.Drawing.Size(167, 434);
            this.leftView.Name = "leftView";
            this.leftView.Size = new System.Drawing.Size(284, 434);
            this.leftView.TabIndex = 0;
            this.leftView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.leftView_AfterSelect);
            // 
            // rightView
            // 
            this.rightView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightView.Location = new System.Drawing.Point(0, 0);
            this.rightView.MinimumSize = new System.Drawing.Size(167, 434);
            this.rightView.Name = "rightView";
            this.rightView.Size = new System.Drawing.Size(352, 434);
            this.rightView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 434);
            this.Controls.Add(this.MainSplitContainer);
            this.MinimumSize = new System.Drawing.Size(656, 473);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проводник";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.TreeView leftView;
        private System.Windows.Forms.TreeView rightView;
    }
}

