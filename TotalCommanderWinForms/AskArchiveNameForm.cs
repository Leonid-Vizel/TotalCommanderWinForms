using System;
using System.Linq;
using System.Windows.Forms;

namespace TotalCommanderWinForms
{
    public partial class AskArchiveNameForm : Form
    {
        public string Result { get; private set; } = null;
        private string prohibited;
        public AskArchiveNameForm(string prohibited)
        {
            this.prohibited = prohibited;
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Название не может быть пустым", "Ошибка");
                DialogResult = DialogResult.Cancel;
                return;
            }
            if (nameTextBox.Text.All(x => !prohibited.Contains(x)))
            {
                Result = nameTextBox.Text;
            }
            else
            {
                MessageBox.Show($"Символы {prohibited} нельзя использоватеть в названиях директорий!", "Ошибка");
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
