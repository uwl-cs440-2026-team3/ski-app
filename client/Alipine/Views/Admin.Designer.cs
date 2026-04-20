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
            btn_LogOut = new Button();
            lb_name = new Label();
            rtb_log = new RichTextBox();
            label1 = new Label();
            panel1 = new Panel();
            button5 = new Button();
            button4 = new Button();
            btn_Cancel = new Button();
            button2 = new Button();
            btn_InsertTimes = new Button();
            btn_ScheduleRace = new Button();
            btn_CreateCoach = new Button();
            btn_CreateCourse = new Button();
            btn_CreateTeam = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_LogOut
            // 
            btn_LogOut.Location = new Point(441, 5);
            btn_LogOut.Margin = new Padding(2);
            btn_LogOut.Name = "btn_LogOut";
            btn_LogOut.Size = new Size(157, 27);
            btn_LogOut.TabIndex = 9;
            btn_LogOut.Text = "Logout";
            btn_LogOut.UseVisualStyleBackColor = true;
            btn_LogOut.Click += btn_LogOut_Click;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.Location = new Point(12, 17);
            lb_name.Name = "lb_name";
            lb_name.Size = new Size(35, 15);
            lb_name.TabIndex = 8;
            lb_name.Text = "Hello";
            // 
            // rtb_log
            // 
            rtb_log.BackColor = Color.LightSteelBlue;
            rtb_log.BorderStyle = BorderStyle.None;
            rtb_log.Location = new Point(14, 35);
            rtb_log.Name = "rtb_log";
            rtb_log.Size = new Size(224, 334);
            rtb_log.TabIndex = 11;
            rtb_log.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(14, 17);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 10;
            label1.Text = "Server Log";
            // 
            // panel1
            // 
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(btn_Cancel);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(btn_InsertTimes);
            panel1.Controls.Add(btn_ScheduleRace);
            panel1.Controls.Add(btn_CreateCoach);
            panel1.Controls.Add(btn_CreateCourse);
            panel1.Controls.Add(btn_CreateTeam);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(rtb_log);
            panel1.Location = new Point(12, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(609, 393);
            panel1.TabIndex = 12;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveCaption;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(244, 307);
            button5.Name = "button5";
            button5.Size = new Size(342, 62);
            button5.TabIndex = 26;
            button5.Text = "(placehold)";
            button5.UseVisualStyleBackColor = false;
            button5.Visible = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(429, 239);
            button4.Name = "button4";
            button4.Size = new Size(157, 62);
            button4.TabIndex = 25;
            button4.Text = "(placehold)";
            button4.UseVisualStyleBackColor = false;
            button4.Visible = false;
            // 
            // btn_Cancel
            // 
            btn_Cancel.BackColor = SystemColors.ActiveCaption;
            btn_Cancel.FlatAppearance.BorderSize = 0;
            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.Location = new Point(429, 103);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(157, 62);
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
            button2.Location = new Point(429, 171);
            button2.Name = "button2";
            button2.Size = new Size(157, 62);
            button2.TabIndex = 23;
            button2.Text = "Reschedule Race (placehold)";
            button2.UseVisualStyleBackColor = false;
            button2.Visible = false;
            // 
            // btn_InsertTimes
            // 
            btn_InsertTimes.BackColor = SystemColors.ActiveCaption;
            btn_InsertTimes.FlatAppearance.BorderSize = 0;
            btn_InsertTimes.FlatStyle = FlatStyle.Flat;
            btn_InsertTimes.Location = new Point(429, 35);
            btn_InsertTimes.Name = "btn_InsertTimes";
            btn_InsertTimes.Size = new Size(157, 62);
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
            btn_ScheduleRace.Location = new Point(244, 239);
            btn_ScheduleRace.Name = "btn_ScheduleRace";
            btn_ScheduleRace.Size = new Size(157, 62);
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
            btn_CreateCoach.Location = new Point(244, 35);
            btn_CreateCoach.Name = "btn_CreateCoach";
            btn_CreateCoach.Size = new Size(157, 62);
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
            btn_CreateCourse.Location = new Point(244, 171);
            btn_CreateCourse.Name = "btn_CreateCourse";
            btn_CreateCourse.Size = new Size(157, 62);
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
            btn_CreateTeam.Location = new Point(244, 103);
            btn_CreateTeam.Name = "btn_CreateTeam";
            btn_CreateTeam.Size = new Size(157, 62);
            btn_CreateTeam.TabIndex = 17;
            btn_CreateTeam.Text = "Create Team";
            btn_CreateTeam.UseVisualStyleBackColor = false;
            btn_CreateTeam.Click += btn_CreateTeam_Click;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(635, 447);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(btn_LogOut);
            Controls.Add(lb_name);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "Admin";
            Text = "Admin";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_LogOut;
        private Label lb_name;
        private RichTextBox rtb_log;
        private Label label1;
        private Panel panel1;
        private Button btn_CreateCourse;
        private Button btn_CreateTeam;
        private Button btn_CreateCoach;
        private Button btn_ScheduleRace;
        private Button button2;
        private Button btn_InsertTimes;
        private Button button4;
        private Button btn_Cancel;
        private Button button5;
    }
}