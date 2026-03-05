namespace Alipine
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabcntrl_Main = new TabControl();
            tab_Landing = new TabPage();
            btn_logout = new Button();
            label5 = new Label();
            label6 = new Label();
            btn_login = new Button();
            rtb_loginPassword = new RichTextBox();
            rtb_loginEmail = new RichTextBox();
            label4 = new Label();
            rtb_RegisterEmail = new RichTextBox();
            label3 = new Label();
            label2 = new Label();
            rtb_RegisterPassword = new RichTextBox();
            rtb_RegisterUsername = new RichTextBox();
            btn_Register = new Button();
            tabcntrl_Admin = new TabPage();
            label22 = new Label();
            rtb_CoachEmail = new RichTextBox();
            label23 = new Label();
            label24 = new Label();
            rtb_CoachPassword = new RichTextBox();
            rtb_CoachUser = new RichTextBox();
            btn_CreateCoach = new Button();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            rtb_Date = new RichTextBox();
            rtb_CourseName = new RichTextBox();
            rtb_TimeEnd = new RichTextBox();
            rtb_TimeStart = new RichTextBox();
            rtb_TeamTwo = new RichTextBox();
            rtb_TeamOne = new RichTextBox();
            rtb_RaceName = new RichTextBox();
            btn_ScheduleRace = new Button();
            btn_AssignToTeam = new Button();
            rtb_AssignTeamName = new RichTextBox();
            rtb_AssignPersonName = new RichTextBox();
            btn_CreateCourse = new Button();
            label7 = new Label();
            label8 = new Label();
            btn_CreateTeam = new Button();
            rtb_Course = new RichTextBox();
            rtb_Team = new RichTextBox();
            tab_Coach = new TabPage();
            label19 = new Label();
            label18 = new Label();
            dataGridView2 = new DataGridView();
            tab_Skier = new TabPage();
            label20 = new Label();
            label21 = new Label();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            rtb_log = new RichTextBox();
            lb_role = new Label();
            tabcntrl_Main.SuspendLayout();
            tab_Landing.SuspendLayout();
            tabcntrl_Admin.SuspendLayout();
            tab_Coach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tab_Skier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tabcntrl_Main
            // 
            tabcntrl_Main.Appearance = TabAppearance.FlatButtons;
            tabcntrl_Main.Controls.Add(tab_Landing);
            tabcntrl_Main.Controls.Add(tabcntrl_Admin);
            tabcntrl_Main.Controls.Add(tab_Coach);
            tabcntrl_Main.Controls.Add(tab_Skier);
            tabcntrl_Main.Location = new Point(191, 20);
            tabcntrl_Main.Margin = new Padding(4, 5, 4, 5);
            tabcntrl_Main.Name = "tabcntrl_Main";
            tabcntrl_Main.SelectedIndex = 0;
            tabcntrl_Main.Size = new Size(1020, 557);
            tabcntrl_Main.TabIndex = 0;
            tabcntrl_Main.TabStop = false;
            // 
            // tab_Landing
            // 
            tab_Landing.BackColor = Color.LightSteelBlue;
            tab_Landing.Controls.Add(btn_logout);
            tab_Landing.Controls.Add(label5);
            tab_Landing.Controls.Add(label6);
            tab_Landing.Controls.Add(btn_login);
            tab_Landing.Controls.Add(rtb_loginPassword);
            tab_Landing.Controls.Add(rtb_loginEmail);
            tab_Landing.Controls.Add(label4);
            tab_Landing.Controls.Add(rtb_RegisterEmail);
            tab_Landing.Controls.Add(label3);
            tab_Landing.Controls.Add(label2);
            tab_Landing.Controls.Add(rtb_RegisterPassword);
            tab_Landing.Controls.Add(rtb_RegisterUsername);
            tab_Landing.Controls.Add(btn_Register);
            tab_Landing.Location = new Point(4, 37);
            tab_Landing.Margin = new Padding(4, 5, 4, 5);
            tab_Landing.Name = "tab_Landing";
            tab_Landing.Padding = new Padding(4, 5, 4, 5);
            tab_Landing.Size = new Size(1012, 516);
            tab_Landing.TabIndex = 0;
            tab_Landing.Text = "Landing";
            // 
            // btn_logout
            // 
            btn_logout.BackColor = SystemColors.Control;
            btn_logout.Enabled = false;
            btn_logout.FlatAppearance.BorderSize = 0;
            btn_logout.FlatStyle = FlatStyle.Flat;
            btn_logout.Location = new Point(731, 167);
            btn_logout.Margin = new Padding(4, 5, 4, 5);
            btn_logout.Name = "btn_logout";
            btn_logout.Size = new Size(224, 38);
            btn_logout.TabIndex = 14;
            btn_logout.Text = "Logout";
            btn_logout.UseVisualStyleBackColor = false;
            btn_logout.Click += btn_logout_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 138);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(54, 25);
            label5.TabIndex = 13;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(266, 138);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(87, 25);
            label6.TabIndex = 12;
            label6.Text = "Password";
            // 
            // btn_login
            // 
            btn_login.BackColor = SystemColors.Control;
            btn_login.FlatAppearance.BorderSize = 0;
            btn_login.FlatStyle = FlatStyle.Flat;
            btn_login.Location = new Point(499, 167);
            btn_login.Margin = new Padding(4, 5, 4, 5);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(224, 38);
            btn_login.TabIndex = 11;
            btn_login.Text = "Login";
            btn_login.UseVisualStyleBackColor = false;
            btn_login.Click += btn_login_Click;
            // 
            // rtb_loginPassword
            // 
            rtb_loginPassword.BorderStyle = BorderStyle.None;
            rtb_loginPassword.Location = new Point(266, 168);
            rtb_loginPassword.Margin = new Padding(4, 5, 4, 5);
            rtb_loginPassword.Name = "rtb_loginPassword";
            rtb_loginPassword.Size = new Size(224, 38);
            rtb_loginPassword.TabIndex = 8;
            rtb_loginPassword.Text = "";
            // 
            // rtb_loginEmail
            // 
            rtb_loginEmail.BorderStyle = BorderStyle.None;
            rtb_loginEmail.Location = new Point(33, 168);
            rtb_loginEmail.Margin = new Padding(4, 5, 4, 5);
            rtb_loginEmail.Name = "rtb_loginEmail";
            rtb_loginEmail.Size = new Size(224, 38);
            rtb_loginEmail.TabIndex = 7;
            rtb_loginEmail.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(499, 33);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(54, 25);
            label4.TabIndex = 6;
            label4.Text = "Email";
            // 
            // rtb_RegisterEmail
            // 
            rtb_RegisterEmail.BorderStyle = BorderStyle.None;
            rtb_RegisterEmail.HideSelection = false;
            rtb_RegisterEmail.Location = new Point(499, 63);
            rtb_RegisterEmail.Margin = new Padding(4, 5, 4, 5);
            rtb_RegisterEmail.Name = "rtb_RegisterEmail";
            rtb_RegisterEmail.Size = new Size(224, 38);
            rtb_RegisterEmail.TabIndex = 5;
            rtb_RegisterEmail.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(266, 33);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 33);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 3;
            label2.Text = "Username";
            // 
            // rtb_RegisterPassword
            // 
            rtb_RegisterPassword.BorderStyle = BorderStyle.None;
            rtb_RegisterPassword.Location = new Point(266, 63);
            rtb_RegisterPassword.Margin = new Padding(4, 5, 4, 5);
            rtb_RegisterPassword.Name = "rtb_RegisterPassword";
            rtb_RegisterPassword.Size = new Size(224, 38);
            rtb_RegisterPassword.TabIndex = 2;
            rtb_RegisterPassword.Text = "";
            // 
            // rtb_RegisterUsername
            // 
            rtb_RegisterUsername.BorderStyle = BorderStyle.None;
            rtb_RegisterUsername.Location = new Point(33, 63);
            rtb_RegisterUsername.Margin = new Padding(4, 5, 4, 5);
            rtb_RegisterUsername.Name = "rtb_RegisterUsername";
            rtb_RegisterUsername.Size = new Size(224, 38);
            rtb_RegisterUsername.TabIndex = 1;
            rtb_RegisterUsername.Text = "";
            // 
            // btn_Register
            // 
            btn_Register.BackColor = SystemColors.Control;
            btn_Register.FlatAppearance.BorderSize = 0;
            btn_Register.FlatStyle = FlatStyle.Flat;
            btn_Register.Location = new Point(731, 62);
            btn_Register.Margin = new Padding(4, 5, 4, 5);
            btn_Register.Name = "btn_Register";
            btn_Register.Size = new Size(224, 38);
            btn_Register.TabIndex = 0;
            btn_Register.Text = "Attempt Registration";
            btn_Register.UseVisualStyleBackColor = false;
            btn_Register.Click += btn_SkierRegistration_Click;
            // 
            // tabcntrl_Admin
            // 
            tabcntrl_Admin.BackColor = Color.LightSteelBlue;
            tabcntrl_Admin.Controls.Add(label22);
            tabcntrl_Admin.Controls.Add(rtb_CoachEmail);
            tabcntrl_Admin.Controls.Add(label23);
            tabcntrl_Admin.Controls.Add(label24);
            tabcntrl_Admin.Controls.Add(rtb_CoachPassword);
            tabcntrl_Admin.Controls.Add(rtb_CoachUser);
            tabcntrl_Admin.Controls.Add(btn_CreateCoach);
            tabcntrl_Admin.Controls.Add(label17);
            tabcntrl_Admin.Controls.Add(label16);
            tabcntrl_Admin.Controls.Add(label15);
            tabcntrl_Admin.Controls.Add(label14);
            tabcntrl_Admin.Controls.Add(label13);
            tabcntrl_Admin.Controls.Add(label12);
            tabcntrl_Admin.Controls.Add(label11);
            tabcntrl_Admin.Controls.Add(label10);
            tabcntrl_Admin.Controls.Add(label9);
            tabcntrl_Admin.Controls.Add(rtb_Date);
            tabcntrl_Admin.Controls.Add(rtb_CourseName);
            tabcntrl_Admin.Controls.Add(rtb_TimeEnd);
            tabcntrl_Admin.Controls.Add(rtb_TimeStart);
            tabcntrl_Admin.Controls.Add(rtb_TeamTwo);
            tabcntrl_Admin.Controls.Add(rtb_TeamOne);
            tabcntrl_Admin.Controls.Add(rtb_RaceName);
            tabcntrl_Admin.Controls.Add(btn_ScheduleRace);
            tabcntrl_Admin.Controls.Add(btn_AssignToTeam);
            tabcntrl_Admin.Controls.Add(rtb_AssignTeamName);
            tabcntrl_Admin.Controls.Add(rtb_AssignPersonName);
            tabcntrl_Admin.Controls.Add(btn_CreateCourse);
            tabcntrl_Admin.Controls.Add(label7);
            tabcntrl_Admin.Controls.Add(label8);
            tabcntrl_Admin.Controls.Add(btn_CreateTeam);
            tabcntrl_Admin.Controls.Add(rtb_Course);
            tabcntrl_Admin.Controls.Add(rtb_Team);
            tabcntrl_Admin.Location = new Point(4, 37);
            tabcntrl_Admin.Margin = new Padding(4, 5, 4, 5);
            tabcntrl_Admin.Name = "tabcntrl_Admin";
            tabcntrl_Admin.Padding = new Padding(4, 5, 4, 5);
            tabcntrl_Admin.Size = new Size(1012, 516);
            tabcntrl_Admin.TabIndex = 1;
            tabcntrl_Admin.Text = "Admin";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(493, 195);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(54, 25);
            label22.TabIndex = 49;
            label22.Text = "Email";
            // 
            // rtb_CoachEmail
            // 
            rtb_CoachEmail.BorderStyle = BorderStyle.None;
            rtb_CoachEmail.Enabled = false;
            rtb_CoachEmail.HideSelection = false;
            rtb_CoachEmail.Location = new Point(493, 225);
            rtb_CoachEmail.Margin = new Padding(4, 5, 4, 5);
            rtb_CoachEmail.Name = "rtb_CoachEmail";
            rtb_CoachEmail.Size = new Size(224, 38);
            rtb_CoachEmail.TabIndex = 48;
            rtb_CoachEmail.Text = "";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(260, 195);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(87, 25);
            label23.TabIndex = 47;
            label23.Text = "Password";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(27, 195);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(91, 25);
            label24.TabIndex = 46;
            label24.Text = "Username";
            // 
            // rtb_CoachPassword
            // 
            rtb_CoachPassword.BorderStyle = BorderStyle.None;
            rtb_CoachPassword.Enabled = false;
            rtb_CoachPassword.Location = new Point(260, 225);
            rtb_CoachPassword.Margin = new Padding(4, 5, 4, 5);
            rtb_CoachPassword.Name = "rtb_CoachPassword";
            rtb_CoachPassword.Size = new Size(224, 38);
            rtb_CoachPassword.TabIndex = 45;
            rtb_CoachPassword.Text = "";
            // 
            // rtb_CoachUser
            // 
            rtb_CoachUser.BorderStyle = BorderStyle.None;
            rtb_CoachUser.Enabled = false;
            rtb_CoachUser.Location = new Point(27, 225);
            rtb_CoachUser.Margin = new Padding(4, 5, 4, 5);
            rtb_CoachUser.Name = "rtb_CoachUser";
            rtb_CoachUser.Size = new Size(224, 38);
            rtb_CoachUser.TabIndex = 44;
            rtb_CoachUser.Text = "";
            // 
            // btn_CreateCoach
            // 
            btn_CreateCoach.BackColor = SystemColors.Control;
            btn_CreateCoach.Enabled = false;
            btn_CreateCoach.FlatAppearance.BorderSize = 0;
            btn_CreateCoach.FlatStyle = FlatStyle.Flat;
            btn_CreateCoach.Location = new Point(726, 223);
            btn_CreateCoach.Margin = new Padding(4, 5, 4, 5);
            btn_CreateCoach.Name = "btn_CreateCoach";
            btn_CreateCoach.Size = new Size(224, 38);
            btn_CreateCoach.TabIndex = 43;
            btn_CreateCoach.Text = "Create Coach";
            btn_CreateCoach.UseVisualStyleBackColor = false;
            btn_CreateCoach.Click += btn_CreateCoach_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(493, 417);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(119, 25);
            label17.TabIndex = 42;
            label17.Text = "Course Name";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(260, 417);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(85, 25);
            label16.TabIndex = 41;
            label16.Text = "End Time";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(27, 417);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(91, 25);
            label15.TabIndex = 40;
            label15.Text = "Start Time";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(726, 315);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(49, 25);
            label14.TabIndex = 39;
            label14.Text = "Date";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(493, 315);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(90, 25);
            label13.TabIndex = 38;
            label13.Text = "Team Two";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(260, 315);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(91, 25);
            label12.TabIndex = 37;
            label12.Text = "Team One";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(27, 315);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(101, 25);
            label11.TabIndex = 36;
            label11.Text = "Race Name";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(726, 28);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(105, 25);
            label10.TabIndex = 35;
            label10.Text = "Team Name";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(493, 28);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(99, 25);
            label9.TabIndex = 34;
            label9.Text = "User Name";
            // 
            // rtb_Date
            // 
            rtb_Date.BorderStyle = BorderStyle.None;
            rtb_Date.Enabled = false;
            rtb_Date.Location = new Point(726, 345);
            rtb_Date.Margin = new Padding(4, 5, 4, 5);
            rtb_Date.Name = "rtb_Date";
            rtb_Date.Size = new Size(224, 38);
            rtb_Date.TabIndex = 33;
            rtb_Date.Text = "";
            // 
            // rtb_CourseName
            // 
            rtb_CourseName.BorderStyle = BorderStyle.None;
            rtb_CourseName.Enabled = false;
            rtb_CourseName.Location = new Point(493, 447);
            rtb_CourseName.Margin = new Padding(4, 5, 4, 5);
            rtb_CourseName.Name = "rtb_CourseName";
            rtb_CourseName.Size = new Size(224, 38);
            rtb_CourseName.TabIndex = 32;
            rtb_CourseName.Text = "";
            // 
            // rtb_TimeEnd
            // 
            rtb_TimeEnd.BorderStyle = BorderStyle.None;
            rtb_TimeEnd.Enabled = false;
            rtb_TimeEnd.Location = new Point(260, 447);
            rtb_TimeEnd.Margin = new Padding(4, 5, 4, 5);
            rtb_TimeEnd.Name = "rtb_TimeEnd";
            rtb_TimeEnd.Size = new Size(224, 38);
            rtb_TimeEnd.TabIndex = 31;
            rtb_TimeEnd.Text = "";
            // 
            // rtb_TimeStart
            // 
            rtb_TimeStart.BorderStyle = BorderStyle.None;
            rtb_TimeStart.Enabled = false;
            rtb_TimeStart.Location = new Point(27, 447);
            rtb_TimeStart.Margin = new Padding(4, 5, 4, 5);
            rtb_TimeStart.Name = "rtb_TimeStart";
            rtb_TimeStart.Size = new Size(224, 38);
            rtb_TimeStart.TabIndex = 30;
            rtb_TimeStart.Text = "";
            // 
            // rtb_TeamTwo
            // 
            rtb_TeamTwo.BorderStyle = BorderStyle.None;
            rtb_TeamTwo.Enabled = false;
            rtb_TeamTwo.Location = new Point(493, 345);
            rtb_TeamTwo.Margin = new Padding(4, 5, 4, 5);
            rtb_TeamTwo.Name = "rtb_TeamTwo";
            rtb_TeamTwo.Size = new Size(224, 38);
            rtb_TeamTwo.TabIndex = 29;
            rtb_TeamTwo.Text = "";
            // 
            // rtb_TeamOne
            // 
            rtb_TeamOne.BorderStyle = BorderStyle.None;
            rtb_TeamOne.Enabled = false;
            rtb_TeamOne.Location = new Point(260, 345);
            rtb_TeamOne.Margin = new Padding(4, 5, 4, 5);
            rtb_TeamOne.Name = "rtb_TeamOne";
            rtb_TeamOne.Size = new Size(224, 38);
            rtb_TeamOne.TabIndex = 28;
            rtb_TeamOne.Text = "";
            // 
            // rtb_RaceName
            // 
            rtb_RaceName.BorderStyle = BorderStyle.None;
            rtb_RaceName.Enabled = false;
            rtb_RaceName.Location = new Point(27, 345);
            rtb_RaceName.Margin = new Padding(4, 5, 4, 5);
            rtb_RaceName.Name = "rtb_RaceName";
            rtb_RaceName.Size = new Size(224, 38);
            rtb_RaceName.TabIndex = 27;
            rtb_RaceName.Text = "";
            // 
            // btn_ScheduleRace
            // 
            btn_ScheduleRace.BackColor = SystemColors.Control;
            btn_ScheduleRace.Enabled = false;
            btn_ScheduleRace.FlatAppearance.BorderSize = 0;
            btn_ScheduleRace.FlatStyle = FlatStyle.Flat;
            btn_ScheduleRace.Location = new Point(726, 445);
            btn_ScheduleRace.Margin = new Padding(4, 5, 4, 5);
            btn_ScheduleRace.Name = "btn_ScheduleRace";
            btn_ScheduleRace.Size = new Size(224, 38);
            btn_ScheduleRace.TabIndex = 26;
            btn_ScheduleRace.Text = "Schedule Race";
            btn_ScheduleRace.UseVisualStyleBackColor = false;
            btn_ScheduleRace.Click += btn_ScheduleRace_Click;
            // 
            // btn_AssignToTeam
            // 
            btn_AssignToTeam.BackColor = SystemColors.Control;
            btn_AssignToTeam.Enabled = false;
            btn_AssignToTeam.FlatAppearance.BorderSize = 0;
            btn_AssignToTeam.FlatStyle = FlatStyle.Flat;
            btn_AssignToTeam.Location = new Point(493, 110);
            btn_AssignToTeam.Margin = new Padding(4, 5, 4, 5);
            btn_AssignToTeam.Name = "btn_AssignToTeam";
            btn_AssignToTeam.Size = new Size(457, 38);
            btn_AssignToTeam.TabIndex = 22;
            btn_AssignToTeam.Text = "Assign To Team";
            btn_AssignToTeam.UseVisualStyleBackColor = false;
            btn_AssignToTeam.Click += btn_AssignToTeam_Click;
            // 
            // rtb_AssignTeamName
            // 
            rtb_AssignTeamName.BorderStyle = BorderStyle.None;
            rtb_AssignTeamName.Enabled = false;
            rtb_AssignTeamName.Location = new Point(726, 62);
            rtb_AssignTeamName.Margin = new Padding(4, 5, 4, 5);
            rtb_AssignTeamName.Name = "rtb_AssignTeamName";
            rtb_AssignTeamName.Size = new Size(224, 38);
            rtb_AssignTeamName.TabIndex = 21;
            rtb_AssignTeamName.Text = "";
            // 
            // rtb_AssignPersonName
            // 
            rtb_AssignPersonName.BorderStyle = BorderStyle.None;
            rtb_AssignPersonName.Enabled = false;
            rtb_AssignPersonName.Location = new Point(493, 62);
            rtb_AssignPersonName.Margin = new Padding(4, 5, 4, 5);
            rtb_AssignPersonName.Name = "rtb_AssignPersonName";
            rtb_AssignPersonName.Size = new Size(224, 38);
            rtb_AssignPersonName.TabIndex = 20;
            rtb_AssignPersonName.Text = "";
            // 
            // btn_CreateCourse
            // 
            btn_CreateCourse.BackColor = SystemColors.Control;
            btn_CreateCourse.Enabled = false;
            btn_CreateCourse.FlatAppearance.BorderSize = 0;
            btn_CreateCourse.FlatStyle = FlatStyle.Flat;
            btn_CreateCourse.Location = new Point(260, 110);
            btn_CreateCourse.Margin = new Padding(4, 5, 4, 5);
            btn_CreateCourse.Name = "btn_CreateCourse";
            btn_CreateCourse.Size = new Size(224, 38);
            btn_CreateCourse.TabIndex = 19;
            btn_CreateCourse.Text = "Create Course";
            btn_CreateCourse.UseVisualStyleBackColor = false;
            btn_CreateCourse.Click += btn_CreateCourse_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(27, 32);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(105, 25);
            label7.TabIndex = 18;
            label7.Text = "Team Name";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(260, 28);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(119, 25);
            label8.TabIndex = 17;
            label8.Text = "Course Name";
            // 
            // btn_CreateTeam
            // 
            btn_CreateTeam.BackColor = SystemColors.Control;
            btn_CreateTeam.Enabled = false;
            btn_CreateTeam.FlatAppearance.BorderSize = 0;
            btn_CreateTeam.FlatStyle = FlatStyle.Flat;
            btn_CreateTeam.Location = new Point(27, 110);
            btn_CreateTeam.Margin = new Padding(4, 5, 4, 5);
            btn_CreateTeam.Name = "btn_CreateTeam";
            btn_CreateTeam.Size = new Size(224, 38);
            btn_CreateTeam.TabIndex = 16;
            btn_CreateTeam.Text = "Create Team";
            btn_CreateTeam.UseVisualStyleBackColor = false;
            btn_CreateTeam.Click += btn_CreateTeam_Click;
            // 
            // rtb_Course
            // 
            rtb_Course.BorderStyle = BorderStyle.None;
            rtb_Course.Enabled = false;
            rtb_Course.Location = new Point(260, 62);
            rtb_Course.Margin = new Padding(4, 5, 4, 5);
            rtb_Course.Name = "rtb_Course";
            rtb_Course.Size = new Size(224, 38);
            rtb_Course.TabIndex = 15;
            rtb_Course.Text = "";
            // 
            // rtb_Team
            // 
            rtb_Team.BorderStyle = BorderStyle.None;
            rtb_Team.Enabled = false;
            rtb_Team.Location = new Point(27, 62);
            rtb_Team.Margin = new Padding(4, 5, 4, 5);
            rtb_Team.Name = "rtb_Team";
            rtb_Team.Size = new Size(224, 38);
            rtb_Team.TabIndex = 14;
            rtb_Team.Text = "";
            // 
            // tab_Coach
            // 
            tab_Coach.BackColor = Color.LightSteelBlue;
            tab_Coach.Controls.Add(label19);
            tab_Coach.Controls.Add(label18);
            tab_Coach.Controls.Add(dataGridView2);
            tab_Coach.Location = new Point(4, 37);
            tab_Coach.Margin = new Padding(4, 5, 4, 5);
            tab_Coach.Name = "tab_Coach";
            tab_Coach.Padding = new Padding(4, 5, 4, 5);
            tab_Coach.Size = new Size(1012, 516);
            tab_Coach.TabIndex = 2;
            tab_Coach.Text = "Coach";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(4, 40);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(164, 25);
            label19.TabIndex = 3;
            label19.Text = "Here are your races";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(4, 7);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(141, 25);
            label18.TabIndex = 2;
            label18.Text = "You are on team";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(4, 70);
            dataGridView2.Margin = new Padding(4, 5, 4, 5);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 62;
            dataGridView2.Size = new Size(996, 425);
            dataGridView2.TabIndex = 1;
            // 
            // tab_Skier
            // 
            tab_Skier.BackColor = Color.LightSteelBlue;
            tab_Skier.Controls.Add(label20);
            tab_Skier.Controls.Add(label21);
            tab_Skier.Controls.Add(dataGridView1);
            tab_Skier.Location = new Point(4, 37);
            tab_Skier.Margin = new Padding(4, 5, 4, 5);
            tab_Skier.Name = "tab_Skier";
            tab_Skier.Padding = new Padding(4, 5, 4, 5);
            tab_Skier.Size = new Size(1012, 516);
            tab_Skier.TabIndex = 3;
            tab_Skier.Text = "Skier";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(4, 40);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(164, 25);
            label20.TabIndex = 6;
            label20.Text = "Here are your races";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(4, 7);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(141, 25);
            label21.TabIndex = 5;
            label21.Text = "You are on team";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(4, 70);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(996, 425);
            dataGridView1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(17, 20);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(96, 25);
            label1.TabIndex = 1;
            label1.Text = "Server Log";
            // 
            // rtb_log
            // 
            rtb_log.BackColor = Color.LightSteelBlue;
            rtb_log.BorderStyle = BorderStyle.None;
            rtb_log.Location = new Point(17, 60);
            rtb_log.Margin = new Padding(4, 5, 4, 5);
            rtb_log.Name = "rtb_log";
            rtb_log.Size = new Size(166, 510);
            rtb_log.TabIndex = 3;
            rtb_log.Text = "";
            // 
            // lb_role
            // 
            lb_role.AutoSize = true;
            lb_role.Location = new Point(1066, 15);
            lb_role.Margin = new Padding(4, 0, 4, 0);
            lb_role.Name = "lb_role";
            lb_role.Size = new Size(0, 25);
            lb_role.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1229, 597);
            Controls.Add(lb_role);
            Controls.Add(rtb_log);
            Controls.Add(label1);
            Controls.Add(tabcntrl_Main);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Ski App (WIP)";
            tabcntrl_Main.ResumeLayout(false);
            tab_Landing.ResumeLayout(false);
            tab_Landing.PerformLayout();
            tabcntrl_Admin.ResumeLayout(false);
            tabcntrl_Admin.PerformLayout();
            tab_Coach.ResumeLayout(false);
            tab_Coach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tab_Skier.ResumeLayout(false);
            tab_Skier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabcntrl_Main;
        private TabPage tab_Landing;
        private Label label2;
        private RichTextBox rtb_RegisterPassword;
        private RichTextBox rtb_RegisterUsername;
        private Button btn_Register;
        private TabPage tabcntrl_Admin;
        private Label label1;
        private RichTextBox rtb_log;
        private Label label3;
        private Label label4;
        private RichTextBox rtb_RegisterEmail;
        private Button btn_login;
        private RichTextBox rtb_loginPassword;
        private RichTextBox rtb_loginEmail;
        private Label label6;
        private Label label5;
        private Button btn_logout;
        private TabPage tab_Coach;
        private TabPage tab_Skier;
        private Label lb_role;
        private RichTextBox rtb_CourseName;
        private RichTextBox rtb_TimeEnd;
        private RichTextBox rtb_TimeStart;
        private RichTextBox rtb_TeamTwo;
        private RichTextBox rtb_TeamOne;
        private RichTextBox rtb_RaceName;
        private Button btn_ScheduleRace;
        private Button btn_AssignToTeam;
        private RichTextBox rtb_AssignTeamName;
        private RichTextBox rtb_AssignPersonName;
        private Button btn_CreateCourse;
        private Label label7;
        private Label label8;
        private Button btn_CreateTeam;
        private RichTextBox rtb_Course;
        private RichTextBox rtb_Team;
        private RichTextBox rtb_Date;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label19;
        private Label label18;
        private DataGridView dataGridView2;
        private Label label22;
        private RichTextBox rtb_CoachEmail;
        private Label label23;
        private Label label24;
        private RichTextBox rtb_CoachPassword;
        private RichTextBox rtb_CoachUser;
        private Button btn_CreateCoach;
        private Label label20;
        private Label label21;
        private DataGridView dataGridView1;
    }
}
