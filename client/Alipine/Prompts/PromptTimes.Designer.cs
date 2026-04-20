namespace Alpine
{
    partial class PromptTimes
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
            label2 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            cb_Race = new ComboBox();
            lb_TeamOne = new Label();
            nud_SkierThree = new NumericUpDown();
            nud_SkierFour = new NumericUpDown();
            lb_SkierThree = new Label();
            lb_SkierFour = new Label();
            lb_SkierTwo = new Label();
            lb_SkierOne = new Label();
            nud_SkierTwo = new NumericUpDown();
            nud_SkierOne = new NumericUpDown();
            lb_TeamTwo = new Label();
            ((System.ComponentModel.ISupportInitialize)nud_SkierThree).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_SkierFour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_SkierTwo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_SkierOne).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 29);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 4;
            label2.Text = "Race?";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(187, 226);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(167, 49);
            btnOk.TabIndex = 6;
            btnOk.Text = "Submit";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(14, 226);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(167, 49);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cb_Race
            // 
            cb_Race.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_Race.FormattingEnabled = true;
            cb_Race.Location = new Point(59, 26);
            cb_Race.Name = "cb_Race";
            cb_Race.Size = new Size(295, 23);
            cb_Race.TabIndex = 19;
            cb_Race.SelectedIndexChanged += validate;
            // 
            // lb_TeamOne
            // 
            lb_TeamOne.AutoSize = true;
            lb_TeamOne.Location = new Point(142, 66);
            lb_TeamOne.Name = "lb_TeamOne";
            lb_TeamOne.Size = new Size(95, 15);
            lb_TeamOne.TabIndex = 21;
            lb_TeamOne.Text = "{teamnamehere}";
            lb_TeamOne.TextAlign = ContentAlignment.TopCenter;
            // 
            // nud_SkierThree
            // 
            nud_SkierThree.Location = new Point(16, 197);
            nud_SkierThree.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_SkierThree.Name = "nud_SkierThree";
            nud_SkierThree.Size = new Size(167, 23);
            nud_SkierThree.TabIndex = 22;
            // 
            // nud_SkierFour
            // 
            nud_SkierFour.Location = new Point(187, 197);
            nud_SkierFour.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_SkierFour.Name = "nud_SkierFour";
            nud_SkierFour.Size = new Size(167, 23);
            nud_SkierFour.TabIndex = 23;
            // 
            // lb_SkierThree
            // 
            lb_SkierThree.AutoSize = true;
            lb_SkierThree.Location = new Point(16, 179);
            lb_SkierThree.Name = "lb_SkierThree";
            lb_SkierThree.Size = new Size(138, 15);
            lb_SkierThree.TabIndex = 24;
            lb_SkierThree.Text = "{subjectnamehere}s time";
            // 
            // lb_SkierFour
            // 
            lb_SkierFour.AutoSize = true;
            lb_SkierFour.Location = new Point(187, 179);
            lb_SkierFour.Name = "lb_SkierFour";
            lb_SkierFour.Size = new Size(138, 15);
            lb_SkierFour.TabIndex = 25;
            lb_SkierFour.Text = "{subjectnamehere}s time";
            // 
            // lb_SkierTwo
            // 
            lb_SkierTwo.AutoSize = true;
            lb_SkierTwo.Location = new Point(189, 93);
            lb_SkierTwo.Name = "lb_SkierTwo";
            lb_SkierTwo.Size = new Size(138, 15);
            lb_SkierTwo.TabIndex = 29;
            lb_SkierTwo.Text = "{subjectnamehere}s time";
            // 
            // lb_SkierOne
            // 
            lb_SkierOne.AutoSize = true;
            lb_SkierOne.Location = new Point(16, 93);
            lb_SkierOne.Name = "lb_SkierOne";
            lb_SkierOne.Size = new Size(138, 15);
            lb_SkierOne.TabIndex = 28;
            lb_SkierOne.Text = "{subjectnamehere}s time";
            // 
            // nud_SkierTwo
            // 
            nud_SkierTwo.Location = new Point(187, 111);
            nud_SkierTwo.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_SkierTwo.Name = "nud_SkierTwo";
            nud_SkierTwo.Size = new Size(167, 23);
            nud_SkierTwo.TabIndex = 27;
            // 
            // nud_SkierOne
            // 
            nud_SkierOne.Location = new Point(16, 111);
            nud_SkierOne.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_SkierOne.Name = "nud_SkierOne";
            nud_SkierOne.Size = new Size(167, 23);
            nud_SkierOne.TabIndex = 26;
            nud_SkierOne.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lb_TeamTwo
            // 
            lb_TeamTwo.AutoSize = true;
            lb_TeamTwo.Location = new Point(142, 147);
            lb_TeamTwo.Name = "lb_TeamTwo";
            lb_TeamTwo.Size = new Size(95, 15);
            lb_TeamTwo.TabIndex = 30;
            lb_TeamTwo.Text = "{teamnamehere}";
            lb_TeamTwo.TextAlign = ContentAlignment.TopCenter;
            // 
            // PromptTimes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 292);
            ControlBox = false;
            Controls.Add(lb_TeamTwo);
            Controls.Add(lb_SkierTwo);
            Controls.Add(lb_SkierOne);
            Controls.Add(nud_SkierTwo);
            Controls.Add(nud_SkierOne);
            Controls.Add(lb_SkierFour);
            Controls.Add(lb_SkierThree);
            Controls.Add(nud_SkierFour);
            Controls.Add(nud_SkierThree);
            Controls.Add(lb_TeamOne);
            Controls.Add(cb_Race);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PromptTimes";
            Text = "Input";
            Load += PromptTimes_Load;
            ((System.ComponentModel.ISupportInitialize)nud_SkierThree).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_SkierFour).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_SkierTwo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_SkierOne).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cb_Race;
        private Label lb_TeamOne;
        private NumericUpDown nud_SkierThree;
        private NumericUpDown nud_SkierFour;
        private Label lb_SkierThree;
        private Label lb_SkierFour;
        private Label lb_SkierTwo;
        private Label lb_SkierOne;
        private NumericUpDown nud_SkierTwo;
        private NumericUpDown nud_SkierOne;
        private Label lb_TeamTwo;
    }
}