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
            tb_Email = new TextBox();
            tb_Username = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_Submit = new Button();
            btn_Cancel = new Button();
            mtb_Password = new MaskedTextBox();
            SuspendLayout();
            // 
            // tb_Email
            // 
            tb_Email.Location = new Point(104, 41);
            tb_Email.Name = "tb_Email";
            tb_Email.Size = new Size(250, 23);
            tb_Email.TabIndex = 0;
            // 
            // tb_Username
            // 
            tb_Username.Location = new Point(104, 12);
            tb_Username.Name = "tb_Username";
            tb_Username.Size = new Size(250, 23);
            tb_Username.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 3;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 20);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 5;
            label3.Text = "Password";
            // 
            // btn_Submit
            // 
            btn_Submit.Location = new Point(187, 108);
            btn_Submit.Name = "btn_Submit";
            btn_Submit.Size = new Size(167, 49);
            btn_Submit.TabIndex = 6;
            btn_Submit.Text = "Submit";
            btn_Submit.UseVisualStyleBackColor = true;
            btn_Submit.Click += btnOk_Click;
            // 
            // btn_Cancel
            // 
            btn_Cancel.Location = new Point(14, 108);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(167, 49);
            btn_Cancel.TabIndex = 7;
            btn_Cancel.Text = "Cancel";
            btn_Cancel.UseVisualStyleBackColor = true;
            btn_Cancel.Click += btnCancel_Click;
            // 
            // mtb_Password
            // 
            mtb_Password.Location = new Point(104, 70);
            mtb_Password.Name = "mtb_Password";
            mtb_Password.PasswordChar = '*';
            mtb_Password.Size = new Size(250, 23);
            mtb_Password.TabIndex = 8;
            // 
            // PromptCoach
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 169);
            ControlBox = false;
            Controls.Add(mtb_Password);
            Controls.Add(btn_Cancel);
            Controls.Add(btn_Submit);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tb_Username);
            Controls.Add(tb_Email);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PromptCoach";
            Text = "Input";
            ResumeLayout(false);
            PerformLayout();
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
    }
}