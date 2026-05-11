namespace Alpine
{
    partial class PromptSchedule
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
            label2 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            cb_TeamOne = new ComboBox();
            cb_TeamTwo = new ComboBox();
            label3 = new Label();
            dtp_Date = new DateTimePicker();
            label4 = new Label();
            dtp_Start = new DateTimePicker();
            label5 = new Label();
            label6 = new Label();
            cb_Course = new ComboBox();
            label7 = new Label();
            dtp_End = new DateTimePicker();
            tb_Name = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 3;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 38);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 4;
            label2.Text = "Team One?";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(197, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(188, 42);
            btnOk.TabIndex = 6;
            btnOk.Text = "Submit";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(188, 42);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cb_TeamOne
            // 
            cb_TeamOne.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_TeamOne.FormattingEnabled = true;
            cb_TeamOne.Location = new Point(98, 41);
            cb_TeamOne.Name = "cb_TeamOne";
            cb_TeamOne.Size = new Size(287, 23);
            cb_TeamOne.TabIndex = 8;
            cb_TeamOne.DropDown += cb_TeamOne_SelectedIndexChanged;
            cb_TeamOne.SelectedIndexChanged += cb_TeamOne_SelectedIndexChanged;
            // 
            // cb_TeamTwo
            // 
            cb_TeamTwo.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_TeamTwo.FormattingEnabled = true;
            cb_TeamTwo.Location = new Point(98, 79);
            cb_TeamTwo.Name = "cb_TeamTwo";
            cb_TeamTwo.Size = new Size(287, 23);
            cb_TeamTwo.TabIndex = 11;
            cb_TeamTwo.DropDown += cb_TeamTwo_SelectedIndexChanged;
            cb_TeamTwo.SelectedIndexChanged += cb_TeamTwo_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 76);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 10;
            label3.Text = "Team Two?";
            // 
            // dtp_Date
            // 
            dtp_Date.Format = DateTimePickerFormat.Short;
            dtp_Date.Location = new Point(98, 155);
            dtp_Date.Name = "dtp_Date";
            dtp_Date.Size = new Size(287, 23);
            dtp_Date.TabIndex = 12;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(3, 152);
            label4.Name = "label4";
            label4.Size = new Size(89, 15);
            label4.TabIndex = 13;
            label4.Text = "Date";
            // 
            // dtp_Start
            // 
            dtp_Start.Format = DateTimePickerFormat.Time;
            dtp_Start.Location = new Point(98, 193);
            dtp_Start.Name = "dtp_Start";
            dtp_Start.ShowUpDown = true;
            dtp_Start.Size = new Size(287, 23);
            dtp_Start.TabIndex = 14;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(3, 190);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 15;
            label5.Text = "Starts At";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(3, 228);
            label6.Name = "label6";
            label6.Size = new Size(89, 15);
            label6.TabIndex = 17;
            label6.Text = "Ends At";
            // 
            // cb_Course
            // 
            cb_Course.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_Course.FormattingEnabled = true;
            cb_Course.Location = new Point(98, 117);
            cb_Course.Name = "cb_Course";
            cb_Course.Size = new Size(287, 23);
            cb_Course.TabIndex = 19;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(3, 114);
            label7.Name = "label7";
            label7.Size = new Size(89, 15);
            label7.TabIndex = 18;
            label7.Text = "Course?";
            // 
            // dtp_End
            // 
            dtp_End.Format = DateTimePickerFormat.Time;
            dtp_End.Location = new Point(98, 231);
            dtp_End.Name = "dtp_End";
            dtp_End.ShowUpDown = true;
            dtp_End.Size = new Size(287, 23);
            dtp_End.TabIndex = 20;
            // 
            // tb_Name
            // 
            tb_Name.Location = new Point(98, 3);
            tb_Name.Name = "tb_Name";
            tb_Name.Size = new Size(287, 23);
            tb_Name.TabIndex = 21;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.7292423F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.27076F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(dtp_End, 1, 6);
            tableLayoutPanel1.Controls.Add(tb_Name, 1, 0);
            tableLayoutPanel1.Controls.Add(label6, 0, 6);
            tableLayoutPanel1.Controls.Add(cb_Course, 1, 3);
            tableLayoutPanel1.Controls.Add(dtp_Start, 1, 5);
            tableLayoutPanel1.Controls.Add(label5, 0, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label7, 0, 3);
            tableLayoutPanel1.Controls.Add(dtp_Date, 1, 4);
            tableLayoutPanel1.Controls.Add(label4, 0, 4);
            tableLayoutPanel1.Controls.Add(cb_TeamOne, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(cb_TeamTwo, 1, 2);
            tableLayoutPanel1.Location = new Point(8, 7);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.Size = new Size(388, 268);
            tableLayoutPanel1.TabIndex = 22;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel2.Controls.Add(btnOk, 1, 0);
            tableLayoutPanel2.Location = new Point(8, 279);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(388, 49);
            tableLayoutPanel2.TabIndex = 23;
            // 
            // PromptSchedule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(405, 338);
            ControlBox = false;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PromptSchedule";
            Text = "Input";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cb_TeamOne;
        private ComboBox cb_TeamTwo;
        private Label label3;
        private DateTimePicker dtp_Date;
        private Label label4;
        private DateTimePicker dtp_Start;
        private Label label5;
        private Label label6;
        private ComboBox cb_Course;
        private Label label7;
        private DateTimePicker dtp_End;
        private TextBox tb_Name;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}