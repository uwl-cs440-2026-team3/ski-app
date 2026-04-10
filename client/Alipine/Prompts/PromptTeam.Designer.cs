namespace Alpine
{
    partial class PromptTeam
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
            label1 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            cb_SkierTwo = new ComboBox();
            cb_SkierOne = new ComboBox();
            cb_Coach = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            tb_name = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 74);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 3;
            label1.Text = "First Skier";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(190, 129);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(167, 49);
            btnOk.TabIndex = 6;
            btnOk.Text = "Submit";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(17, 129);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(167, 49);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cb_SkierTwo
            // 
            cb_SkierTwo.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_SkierTwo.FormattingEnabled = true;
            cb_SkierTwo.Location = new Point(107, 100);
            cb_SkierTwo.Name = "cb_SkierTwo";
            cb_SkierTwo.Size = new Size(250, 23);
            cb_SkierTwo.TabIndex = 8;
            cb_SkierTwo.DropDown += cb_SkierTwo_SelectedIndexChanged;
            cb_SkierTwo.SelectedIndexChanged += cb_SkierTwo_SelectedIndexChanged;
            // 
            // cb_SkierOne
            // 
            cb_SkierOne.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_SkierOne.FormattingEnabled = true;
            cb_SkierOne.Location = new Point(107, 71);
            cb_SkierOne.Name = "cb_SkierOne";
            cb_SkierOne.Size = new Size(250, 23);
            cb_SkierOne.TabIndex = 9;
            cb_SkierOne.DropDown += cb_SkierOne_SelectedIndexChanged;
            cb_SkierOne.SelectedIndexChanged += cb_SkierOne_SelectedIndexChanged;
            // 
            // cb_Coach
            // 
            cb_Coach.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_Coach.FormattingEnabled = true;
            cb_Coach.Location = new Point(107, 42);
            cb_Coach.Name = "cb_Coach";
            cb_Coach.Size = new Size(250, 23);
            cb_Coach.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 45);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 11;
            label2.Text = "Coach";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 103);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 12;
            label3.Text = "Second Skier";
            // 
            // tb_name
            // 
            tb_name.Location = new Point(107, 13);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(250, 23);
            tb_name.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 21);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 14;
            label4.Text = "Team Name";
            // 
            // PromptTeam
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 186);
            ControlBox = false;
            Controls.Add(label4);
            Controls.Add(tb_name);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cb_Coach);
            Controls.Add(cb_SkierOne);
            Controls.Add(cb_SkierTwo);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PromptTeam";
            Text = "Input";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cb_SkierTwo;
        private ComboBox cb_SkierOne;
        private ComboBox cb_Coach;
        private Label label2;
        private Label label3;
        private TextBox tb_name;
        private Label label4;
    }
}