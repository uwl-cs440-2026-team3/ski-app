namespace Alpine
{
    partial class PromptCancel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptCancel));
            btnOk = new Button();
            btnCancel = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            cb_Race = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnOk.Location = new Point(197, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(192, 42);
            btnOk.TabIndex = 6;
            btnOk.Text = "Submit";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCancel.Location = new Point(3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(188, 42);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.63899F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.36101F));
            tableLayoutPanel1.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel1.Controls.Add(btnOk, 1, 0);
            tableLayoutPanel1.Location = new Point(8, 61);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 72.22222F));
            tableLayoutPanel1.Size = new Size(392, 48);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 3;
            label1.Text = "Race";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cb_Race
            // 
            cb_Race.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cb_Race.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_Race.FormattingEnabled = true;
            cb_Race.Location = new Point(75, 3);
            cb_Race.Name = "cb_Race";
            cb_Race.Size = new Size(314, 23);
            cb_Race.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.4482765F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.55173F));
            tableLayoutPanel2.Controls.Add(cb_Race, 1, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Location = new Point(8, 17);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(392, 41);
            tableLayoutPanel2.TabIndex = 9;
            // 
            // PromptCancel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(405, 116);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tableLayoutPanel2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PromptCancel";
            Text = "Input";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btnOk;
        private Button btnCancel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ComboBox cb_Race;
        private TableLayoutPanel tableLayoutPanel2;
    }
}