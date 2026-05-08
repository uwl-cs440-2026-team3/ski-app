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
            nud_time = new NumericUpDown();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            label1 = new Label();
            cb_skier = new ComboBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)nud_time).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 4;
            label2.Text = "Race";
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
            // cb_Race
            // 
            cb_Race.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_Race.FormattingEnabled = true;
            cb_Race.Location = new Point(73, 3);
            cb_Race.Name = "cb_Race";
            cb_Race.Size = new Size(312, 23);
            cb_Race.TabIndex = 19;
            cb_Race.SelectedIndexChanged += ValidateRaces;
            // 
            // nud_time
            // 
            nud_time.Location = new Point(73, 173);
            nud_time.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_time.Name = "nud_time";
            nud_time.Size = new Size(312, 23);
            nud_time.TabIndex = 26;
            nud_time.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.05054F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.9494553F));
            tableLayoutPanel1.Controls.Add(nud_time, 1, 2);
            tableLayoutPanel1.Controls.Add(cb_skier, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(cb_Race, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Location = new Point(8, 7);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(388, 257);
            tableLayoutPanel1.TabIndex = 32;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(btnOk, 1, 0);
            tableLayoutPanel6.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel6.Location = new Point(8, 268);
            tableLayoutPanel6.Margin = new Padding(2);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(388, 48);
            tableLayoutPanel6.TabIndex = 36;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 85);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 4;
            label1.Text = "Skier";
            // 
            // cb_skier
            // 
            cb_skier.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_skier.FormattingEnabled = true;
            cb_skier.Location = new Point(73, 88);
            cb_skier.Name = "cb_skier";
            cb_skier.Size = new Size(312, 23);
            cb_skier.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 170);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 27;
            label3.Text = "Time";
            // 
            // PromptTimes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(405, 326);
            ControlBox = false;
            Controls.Add(tableLayoutPanel6);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PromptTimes";
            Text = "Input";
            ((System.ComponentModel.ISupportInitialize)nud_time).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label label2;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cb_Race;
        private NumericUpDown nud_time;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label1;
        private ComboBox cb_skier;
        private Label label3;
    }
}