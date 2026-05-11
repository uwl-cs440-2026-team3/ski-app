namespace Alpine
{
    partial class Landing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Landing));
            btn_Login = new Button();
            btn_Register = new Button();
            tb_Email = new TextBox();
            tb_Username = new TextBox();
            mtb_Password = new MaskedTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Login
            // 
            btn_Login.Location = new Point(274, 2);
            btn_Login.Margin = new Padding(2);
            btn_Login.Name = "btn_Login";
            btn_Login.Size = new Size(268, 70);
            btn_Login.TabIndex = 0;
            btn_Login.Text = "Login";
            btn_Login.UseVisualStyleBackColor = true;
            btn_Login.Click += LoginCLick;
            // 
            // btn_Register
            // 
            btn_Register.Location = new Point(2, 2);
            btn_Register.Margin = new Padding(2);
            btn_Register.Name = "btn_Register";
            btn_Register.Size = new Size(268, 70);
            btn_Register.TabIndex = 1;
            btn_Register.Text = "Register";
            btn_Register.UseVisualStyleBackColor = true;
            btn_Register.Click += RegisterClick;
            // 
            // tb_Email
            // 
            tb_Email.Location = new Point(3, 129);
            tb_Email.Name = "tb_Email";
            tb_Email.Size = new Size(531, 23);
            tb_Email.TabIndex = 2;
            // 
            // tb_Username
            // 
            tb_Username.Location = new Point(3, 45);
            tb_Username.Name = "tb_Username";
            tb_Username.Size = new Size(531, 23);
            tb_Username.TabIndex = 3;
            // 
            // mtb_Password
            // 
            mtb_Password.Location = new Point(3, 213);
            mtb_Password.Name = "mtb_Password";
            mtb_Password.PasswordChar = '*';
            mtb_Password.Size = new Size(531, 23);
            mtb_Password.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Location = new Point(254, 111);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 5;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Location = new Point(243, 195);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom;
            label3.AutoSize = true;
            label3.Location = new Point(242, 27);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 7;
            label3.Text = "Username";
            label3.TextAlign = ContentAlignment.BottomCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(mtb_Password, 0, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 4);
            tableLayoutPanel1.Controls.Add(tb_Username, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 2);
            tableLayoutPanel1.Controls.Add(tb_Email, 0, 3);
            tableLayoutPanel1.Location = new Point(20, 13);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.Size = new Size(544, 255);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btn_Register, 0, 0);
            tableLayoutPanel2.Controls.Add(btn_Login, 1, 0);
            tableLayoutPanel2.Location = new Point(20, 274);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(544, 76);
            tableLayoutPanel2.TabIndex = 9;
            // 
            // Landing
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(584, 361);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "Landing";
            Text = "Log In and Registration";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Login;
        private Button btn_Register;
        private TextBox tb_Email;
        private TextBox tb_Username;
        private MaskedTextBox mtb_Password;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}