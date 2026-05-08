namespace Alpine
{
    partial class PromptCoach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptCoach));
            tb_Email = new TextBox();
            tb_Username = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_Submit = new Button();
            btn_Cancel = new Button();
            mtb_Password = new MaskedTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tb_Email
            // 
            tb_Email.Location = new Point(87, 41);
            tb_Email.Name = "tb_Email";
            tb_Email.Size = new Size(298, 23);
            tb_Email.TabIndex = 0;
            // 
            // tb_Username
            // 
            tb_Username.Location = new Point(87, 3);
            tb_Username.Name = "tb_Username";
            tb_Username.Size = new Size(298, 23);
            tb_Username.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 38);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 3;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 4;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 76);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 5;
            label3.Text = "Password";
            // 
            // btn_Submit
            // 
            btn_Submit.Location = new Point(197, 3);
            btn_Submit.Name = "btn_Submit";
            btn_Submit.Size = new Size(188, 41);
            btn_Submit.TabIndex = 6;
            btn_Submit.Text = "Submit";
            btn_Submit.UseVisualStyleBackColor = true;
            btn_Submit.Click += btnOk_Click;
            // 
            // btn_Cancel
            // 
            btn_Cancel.Location = new Point(3, 3);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(188, 41);
            btn_Cancel.TabIndex = 7;
            btn_Cancel.Text = "Cancel";
            btn_Cancel.UseVisualStyleBackColor = true;
            btn_Cancel.Click += btnCancel_Click;
            // 
            // mtb_Password
            // 
            mtb_Password.Location = new Point(87, 79);
            mtb_Password.Name = "mtb_Password";
            mtb_Password.PasswordChar = '*';
            mtb_Password.Size = new Size(298, 23);
            mtb_Password.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btn_Cancel, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_Submit, 1, 0);
            tableLayoutPanel1.Location = new Point(8, 115);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(388, 47);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.66065F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.33935F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(mtb_Password, 1, 2);
            tableLayoutPanel2.Controls.Add(label3, 0, 2);
            tableLayoutPanel2.Controls.Add(tb_Email, 1, 1);
            tableLayoutPanel2.Controls.Add(tb_Username, 1, 0);
            tableLayoutPanel2.Location = new Point(8, 7);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel2.Size = new Size(388, 104);
            tableLayoutPanel2.TabIndex = 10;
            // 
            // PromptCoach
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(405, 169);
            ControlBox = false;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PromptCoach";
            Text = "Input";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox tb_Email;
        private TextBox tb_Username;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btn_Submit;
        private Button btn_Cancel;
        private MaskedTextBox mtb_Password;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}