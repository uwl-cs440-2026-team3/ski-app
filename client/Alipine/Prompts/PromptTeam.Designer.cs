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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 70);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 3;
            label1.Text = "First Skier";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(197, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(188, 41);
            btnOk.TabIndex = 6;
            btnOk.Text = "Submit";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(188, 41);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cb_SkierTwo
            // 
            cb_SkierTwo.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_SkierTwo.FormattingEnabled = true;
            cb_SkierTwo.Location = new Point(85, 108);
            cb_SkierTwo.Name = "cb_SkierTwo";
            cb_SkierTwo.Size = new Size(300, 23);
            cb_SkierTwo.TabIndex = 8;
            cb_SkierTwo.DropDown += cb_SkierTwo_SelectedIndexChanged;
            cb_SkierTwo.SelectedIndexChanged += cb_SkierTwo_SelectedIndexChanged;
            // 
            // cb_SkierOne
            // 
            cb_SkierOne.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_SkierOne.FormattingEnabled = true;
            cb_SkierOne.Location = new Point(85, 73);
            cb_SkierOne.Name = "cb_SkierOne";
            cb_SkierOne.Size = new Size(300, 23);
            cb_SkierOne.TabIndex = 9;
            cb_SkierOne.DropDown += cb_SkierOne_SelectedIndexChanged;
            cb_SkierOne.SelectedIndexChanged += cb_SkierOne_SelectedIndexChanged;
            // 
            // cb_Coach
            // 
            cb_Coach.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_Coach.FormattingEnabled = true;
            cb_Coach.Location = new Point(85, 38);
            cb_Coach.Name = "cb_Coach";
            cb_Coach.Size = new Size(300, 23);
            cb_Coach.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 35);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 11;
            label2.Text = "Coach";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 105);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 12;
            label3.Text = "Second Skier";
            // 
            // tb_name
            // 
            tb_name.Location = new Point(85, 3);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(300, 23);
            tb_name.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 14;
            label4.Text = "Team Name";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.2996387F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.70036F));
            tableLayoutPanel1.Controls.Add(label4, 0, 0);
            tableLayoutPanel1.Controls.Add(cb_SkierTwo, 1, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Controls.Add(tb_name, 1, 0);
            tableLayoutPanel1.Controls.Add(cb_SkierOne, 1, 2);
            tableLayoutPanel1.Controls.Add(cb_Coach, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 2);
            tableLayoutPanel1.Location = new Point(8, 7);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(388, 143);
            tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel2.Controls.Add(btnOk, 1, 0);
            tableLayoutPanel2.Location = new Point(8, 152);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(388, 47);
            tableLayoutPanel2.TabIndex = 16;
            // 
            // PromptTeam
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(405, 206);
            ControlBox = false;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PromptTeam";
            Text = "Input";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
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
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}