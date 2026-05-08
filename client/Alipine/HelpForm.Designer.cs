namespace Alpine.Views
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            wv_help = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)wv_help).BeginInit();
            SuspendLayout();
            // 
            // wv_help
            // 
            wv_help.AllowExternalDrop = true;
            wv_help.CreationProperties = null;
            wv_help.DefaultBackgroundColor = Color.White;
            wv_help.Dock = DockStyle.Fill;
            wv_help.Location = new Point(0, 0);
            wv_help.Name = "wv_help";
            wv_help.Size = new Size(984, 961);
            wv_help.TabIndex = 0;
            wv_help.ZoomFactor = 1D;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 961);
            Controls.Add(wv_help);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HelpForm";
            Text = "User Manual";
            ((System.ComponentModel.ISupportInitialize)wv_help).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 wv_help;
    }
}