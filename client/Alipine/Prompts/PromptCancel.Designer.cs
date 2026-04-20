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
            label1 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            cb_Race = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 15);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 3;
            label1.Text = "Race";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(187, 41);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(167, 49);
            btnOk.TabIndex = 6;
            btnOk.Text = "Submit";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(14, 41);
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
            cb_Race.Location = new Point(104, 12);
            cb_Race.Name = "cb_Race";
            cb_Race.Size = new Size(250, 23);
            cb_Race.TabIndex = 8;
            // 
            // PromptCancel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 113);
            ControlBox = false;
            Controls.Add(cb_Race);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(label1);
            Name = "PromptCancel";
            Text = "Input";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cb_Race;
    }
}