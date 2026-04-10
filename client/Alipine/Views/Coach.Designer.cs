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
            dataGridView2 = new DataGridView();
            button1 = new Button();
            lb_name = new Label();
            lb_team = new Label();
            lb_coach = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(9, 119);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 62;
            dataGridView2.Size = new Size(533, 142);
            dataGridView2.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(379, 6);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(163, 27);
            button1.TabIndex = 7;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = true;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.Location = new Point(9, 12);
            lb_name.Name = "lb_name";
            lb_name.Size = new Size(35, 15);
            lb_name.TabIndex = 9;
            lb_name.Text = "Hello";
            // 
            // lb_team
            // 
            lb_team.AutoSize = true;
            lb_team.Location = new Point(12, 41);
            lb_team.Name = "lb_team";
            lb_team.Size = new Size(93, 15);
            lb_team.TabIndex = 10;
            lb_team.Text = "You are on team";
            // 
            // lb_coach
            // 
            lb_coach.AutoSize = true;
            lb_coach.Location = new Point(12, 65);
            lb_coach.Name = "lb_coach";
            lb_coach.Size = new Size(68, 15);
            lb_coach.TabIndex = 11;
            lb_coach.Text = "Your Coach";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 92);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 12;
            label1.Text = "Your teammates:";
            // 
            // Coach
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(label1);
            Controls.Add(lb_coach);
            Controls.Add(lb_team);
            Controls.Add(lb_name);
            Controls.Add(button1);
            Controls.Add(dataGridView2);
            Margin = new Padding(2);
            Name = "Coach";
            Text = "Coach";
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView2;
        private Button button1;
        private Label lb_name;
        private Label lb_team;
        private Label lb_coach;
        private Label label1;
    }
}