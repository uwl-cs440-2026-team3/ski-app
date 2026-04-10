using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Alpine
{
    public partial class PromptSingle : Form
    {
        public string Value1 => textBox1.Text;

        public PromptSingle(string title, string lbl1)
        {
            InitializeComponent();

            Text = title;

            label1.Text = lbl1;

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // fields cannot be empty, why wait for the server to tell us this when we can nip it in the bud right here
            if (Value1 == "")
            {
                MessageBox.Show(
                    "Field cannot be empty!",
                    "Field cannot be empty!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            else if (!(Alpine.Helpers.ValidationHelpers.CheckName(Value1)))
            {
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
