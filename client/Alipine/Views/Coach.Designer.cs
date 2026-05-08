namespace Alpine
{
    partial class Coach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Coach));
            dgv_races = new DataGridView();
            btn_logout = new Button();
            lb_name = new Label();
            lb_team = new Label();
            lb_coach = new Label();
            lb_teammates = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgv_races).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // dgv_races
            // 
            dgv_races.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_races.Location = new Point(3, 169);
            dgv_races.Name = "dgv_races";
            dgv_races.ReadOnly = true;
            dgv_races.RowHeadersVisible = false;
            dgv_races.RowHeadersWidth = 62;
            dgv_races.ShowEditingIcon = false;
            dgv_races.Size = new Size(754, 265);
            dgv_races.TabIndex = 4;
            // 
            // btn_logout
            // 
            btn_logout.Location = new Point(597, 2);
            btn_logout.Margin = new Padding(2);
            btn_logout.Name = "btn_logout";
            btn_logout.Size = new Size(155, 41);
            btn_logout.TabIndex = 7;
            btn_logout.Text = "Logout";
            btn_logout.UseVisualStyleBackColor = true;
            btn_logout.Click += btn_LogOut_Click;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.Location = new Point(3, 0);
            lb_name.Name = "lb_name";
            lb_name.Size = new Size(35, 15);
            lb_name.TabIndex = 9;
            lb_name.Text = "Hello";
            // 
            // lb_team
            // 
            lb_team.AutoSize = true;
            lb_team.Location = new Point(3, 45);
            lb_team.Name = "lb_team";
            lb_team.Size = new Size(93, 15);
            lb_team.TabIndex = 10;
            lb_team.Text = "You are on team";
            // 
            // lb_coach
            // 
            lb_coach.AutoSize = true;
            lb_coach.Location = new Point(3, 67);
            lb_coach.Name = "lb_coach";
            lb_coach.Size = new Size(68, 15);
            lb_coach.TabIndex = 11;
            lb_coach.Text = "Your Coach";
            // 
            // lb_teammates
            // 
            lb_teammates.AutoSize = true;
            lb_teammates.Location = new Point(3, 89);
            lb_teammates.Name = "lb_teammates";
            lb_teammates.Size = new Size(99, 15);
            lb_teammates.TabIndex = 12;
            lb_teammates.Text = "Your teammates: ";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.6139F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.3861F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 158F));
            tableLayoutPanel1.Controls.Add(lb_name, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_logout, 2, 0);
            tableLayoutPanel1.Controls.Add(lb_teammates, 0, 3);
            tableLayoutPanel1.Controls.Add(lb_team, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_coach, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 5);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 28.5714283F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.Size = new Size(754, 160);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 133);
            label2.Name = "label2";
            label2.Size = new Size(122, 15);
            label2.TabIndex = 13;
            label2.Text = "Your upcoming races:";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(dgv_races, 0, 1);
            tableLayoutPanel2.Location = new Point(12, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 37.98627F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 62.01373F));
            tableLayoutPanel2.Size = new Size(760, 437);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // Coach
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(784, 461);
            Controls.Add(tableLayoutPanel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "Coach";
            Text = "Coach ";
            ((System.ComponentModel.ISupportInitialize)dgv_races).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgv_races;
        private Button btn_logout;
        private Label lb_name;
        private Label lb_team;
        private Label lb_coach;
        private Label lb_teammates;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
    }
}