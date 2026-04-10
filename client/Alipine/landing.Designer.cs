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
            btn_Login = new Button();
            btn_Register = new Button();
            tb_Email = new TextBox();
            tb_Username = new TextBox();
            mtb_Password = new MaskedTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btn_Login
            // 
            btn_Login.Location = new Point(12, 228);
            btn_Login.Margin = new Padding(2);
            btn_Login.Name = "btn_Login";
            btn_Login.Size = new Size(481, 82);
            btn_Login.TabIndex = 0;
            btn_Login.Text = "Login";
            btn_Login.UseVisualStyleBackColor = true;
            btn_Login.Click += LoginCLick;
            // 
            // btn_Register
            // 
            btn_Register.Location = new Point(11, 142);
            btn_Register.Margin = new Padding(2);
            btn_Register.Name = "btn_Register";
            btn_Register.Size = new Size(482, 82);
            btn_Register.TabIndex = 1;
            btn_Register.Text = "Register";
            btn_Register.UseVisualStyleBackColor = true;
            btn_Register.Click += RegisterClick;
            // 
            // tb_Email
            // 
            tb_Email.Location = new Point(9, 70);
            tb_Email.Name = "tb_Email";
            tb_Email.Size = new Size(484, 23);
            tb_Email.TabIndex = 2;
            // 
            // tb_Username
            // 
            tb_Username.Location = new Point(9, 26);
            tb_Username.Name = "tb_Username";
            tb_Username.Size = new Size(484, 23);
            tb_Username.TabIndex = 3;
            // 
            // mtb_Password
            // 
            mtb_Password.Location = new Point(12, 114);
            mtb_Password.Name = "mtb_Password";
            mtb_Password.PasswordChar = '*';
            mtb_Password.Size = new Size(481, 23);
            mtb_Password.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 52);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 5;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 96);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 8);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 7;
            label3.Text = "Username";
            // 
            // Landing
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 328);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(mtb_Password);
            Controls.Add(tb_Username);
            Controls.Add(tb_Email);
            Controls.Add(btn_Register);
            Controls.Add(btn_Login);
            Margin = new Padding(2);
            Name = "Landing";
            Text = "landing";
            ResumeLayout(false);
            PerformLayout();
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
    }
}