namespace Alpine
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            btn_LogOut = new Button();
            lb_name = new Label();
            btn_Cancel = new Button();
            button2 = new Button();
            btn_InsertTimes = new Button();
            btn_ScheduleRace = new Button();
            btn_CreateCoach = new Button();
            btn_CreateCourse = new Button();
            btn_CreateTeam = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            button1 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // btn_LogOut
            // 
            btn_LogOut.Location = new Point(564, 3);
            btn_LogOut.Name = "btn_LogOut";
            btn_LogOut.Size = new Size(221, 68);
            btn_LogOut.TabIndex = 9;
            btn_LogOut.Text = "Logout";
            btn_LogOut.UseVisualStyleBackColor = true;
            btn_LogOut.Click += btn_LogOut_Click;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.Location = new Point(4, 0);
            lb_name.Margin = new Padding(4, 0, 4, 0);
            lb_name.Name = "lb_name";
            lb_name.Size = new Size(53, 25);
            lb_name.TabIndex = 8;
            lb_name.Text = "Hello";
            // 
            // btn_Cancel
            // 
            btn_Cancel.BackColor = SystemColors.ActiveCaption;
            btn_Cancel.FlatAppearance.BorderSize = 0;
            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.Location = new Point(592, 5);
            btn_Cancel.Margin = new Padding(4, 5, 4, 5);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(191, 95);
            btn_Cancel.TabIndex = 24;
            btn_Cancel.Text = "Cancel Race (placehold)";
            btn_Cancel.UseVisualStyleBackColor = false;
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(200, 5);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(187, 95);
            button2.TabIndex = 23;
            button2.Text = "Reschedule Race (placehold)";
            button2.UseVisualStyleBackColor = false;
            // 
            // btn_InsertTimes
            // 
            btn_InsertTimes.BackColor = SystemColors.ActiveCaption;
            btn_InsertTimes.FlatAppearance.BorderSize = 0;
            btn_InsertTimes.FlatStyle = FlatStyle.Flat;
            btn_InsertTimes.Location = new Point(396, 5);
            btn_InsertTimes.Margin = new Padding(4, 5, 4, 5);
            btn_InsertTimes.Name = "btn_InsertTimes";
            btn_InsertTimes.Size = new Size(187, 95);
            btn_InsertTimes.TabIndex = 22;
            btn_InsertTimes.Text = "Insert Race Times (placehold)";
            btn_InsertTimes.UseVisualStyleBackColor = false;
            btn_InsertTimes.Click += btn_InsertTimes_Click;
            // 
            // btn_ScheduleRace
            // 
            btn_ScheduleRace.BackColor = SystemColors.ActiveCaption;
            btn_ScheduleRace.FlatAppearance.BorderSize = 0;
            btn_ScheduleRace.FlatStyle = FlatStyle.Flat;
            btn_ScheduleRace.Location = new Point(4, 5);
            btn_ScheduleRace.Margin = new Padding(4, 5, 4, 5);
            btn_ScheduleRace.Name = "btn_ScheduleRace";
            btn_ScheduleRace.Size = new Size(187, 95);
            btn_ScheduleRace.TabIndex = 21;
            btn_ScheduleRace.Text = "Schedule Race";
            btn_ScheduleRace.UseVisualStyleBackColor = false;
            btn_ScheduleRace.Click += btn_ScheduleRace_Click;
            // 
            // btn_CreateCoach
            // 
            btn_CreateCoach.BackColor = SystemColors.ActiveCaption;
            btn_CreateCoach.FlatAppearance.BorderSize = 0;
            btn_CreateCoach.FlatStyle = FlatStyle.Flat;
            btn_CreateCoach.Location = new Point(4, 5);
            btn_CreateCoach.Margin = new Padding(4, 5, 4, 5);
            btn_CreateCoach.Name = "btn_CreateCoach";
            btn_CreateCoach.Size = new Size(189, 92);
            btn_CreateCoach.TabIndex = 20;
            btn_CreateCoach.Text = "Create Coach";
            btn_CreateCoach.UseVisualStyleBackColor = false;
            btn_CreateCoach.Click += btn_CreateCoach_Click;
            // 
            // btn_CreateCourse
            // 
            btn_CreateCourse.BackColor = SystemColors.ActiveCaption;
            btn_CreateCourse.FlatAppearance.BorderSize = 0;
            btn_CreateCourse.FlatStyle = FlatStyle.Flat;
            btn_CreateCourse.Location = new Point(201, 5);
            btn_CreateCourse.Margin = new Padding(4, 5, 4, 5);
            btn_CreateCourse.Name = "btn_CreateCourse";
            btn_CreateCourse.Size = new Size(189, 92);
            btn_CreateCourse.TabIndex = 18;
            btn_CreateCourse.Text = "Create Course";
            btn_CreateCourse.UseVisualStyleBackColor = false;
            btn_CreateCourse.Click += btn_CreateCourse_Click;
            // 
            // btn_CreateTeam
            // 
            btn_CreateTeam.BackColor = SystemColors.ActiveCaption;
            btn_CreateTeam.FlatAppearance.BorderSize = 0;
            btn_CreateTeam.FlatStyle = FlatStyle.Flat;
            btn_CreateTeam.Location = new Point(398, 5);
            btn_CreateTeam.Margin = new Padding(4, 5, 4, 5);
            btn_CreateTeam.Name = "btn_CreateTeam";
            btn_CreateTeam.Size = new Size(189, 92);
            btn_CreateTeam.TabIndex = 17;
            btn_CreateTeam.Text = "Create Team";
            btn_CreateTeam.UseVisualStyleBackColor = false;
            btn_CreateTeam.Click += btn_CreateTeam_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(button1, 3, 0);
            tableLayoutPanel1.Controls.Add(btn_CreateCoach, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_CreateTeam, 2, 0);
            tableLayoutPanel1.Controls.Add(btn_CreateCourse, 1, 0);
            tableLayoutPanel1.Location = new Point(4, 173);
            tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(791, 102);
            tableLayoutPanel1.TabIndex = 27;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(595, 5);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(189, 92);
            button1.TabIndex = 21;
            button1.Text = "Remove Coach From Team";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btn_RemoveCoach_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(button2, 1, 0);
            tableLayoutPanel2.Controls.Add(btn_InsertTimes, 2, 0);
            tableLayoutPanel2.Controls.Add(btn_Cancel, 3, 0);
            tableLayoutPanel2.Controls.Add(btn_ScheduleRace, 0, 0);
            tableLayoutPanel2.Location = new Point(4, 397);
            tableLayoutPanel2.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(787, 105);
            tableLayoutPanel2.TabIndex = 28;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.6192856F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.38071F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 229F));
            tableLayoutPanel3.Controls.Add(lb_name, 0, 0);
            tableLayoutPanel3.Controls.Add(btn_LogOut, 2, 0);
            tableLayoutPanel3.Location = new Point(4, 5);
            tableLayoutPanel3.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(791, 102);
            tableLayoutPanel3.TabIndex = 29;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 0, 4);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel1, 0, 2);
            tableLayoutPanel4.Controls.Add(label1, 0, 1);
            tableLayoutPanel4.Controls.Add(label2, 0, 3);
            tableLayoutPanel4.Location = new Point(17, 20);
            tableLayoutPanel4.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 5;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel4.Size = new Size(800, 562);
            tableLayoutPanel4.TabIndex = 30;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(4, 143);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(792, 25);
            label1.TabIndex = 30;
            label1.Text = "User and Course Mangement";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(4, 367);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(792, 25);
            label2.TabIndex = 31;
            label2.Text = "Race Managment";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(834, 602);
            ControlBox = false;
            Controls.Add(tableLayoutPanel4);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Admin";
            Text = "Admin";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_LogOut;
        private Label lb_name;
        private Button btn_CreateCourse;
        private Button btn_CreateTeam;
        private Button btn_CreateCoach;
        private Button btn_ScheduleRace;
        private Button button2;
        private Button btn_InsertTimes;
        private Button btn_Cancel;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private Label label2;
        private Button button1;
    }
}