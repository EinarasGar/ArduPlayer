namespace ArduPlayeris
{
    partial class MainForm
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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.Settings = new MetroFramework.Controls.MetroTabPage();
            this.themeCombobox = new MetroFramework.Controls.MetroComboBox();
            this.styleCombobox = new MetroFramework.Controls.MetroComboBox();
            this.Port = new MetroFramework.Controls.MetroComboBox();
            this.BaudRate = new MetroFramework.Controls.MetroComboBox();
            this.ThemeLbl = new MetroFramework.Controls.MetroLabel();
            this.StyleLbl = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroTabControl1.SuspendLayout();
            this.Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.Settings);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(635, 316);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(621, 268);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "metroTabPage2";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // Settings
            // 
            this.Settings.Controls.Add(this.metroButton1);
            this.Settings.Controls.Add(this.StyleLbl);
            this.Settings.Controls.Add(this.ThemeLbl);
            this.Settings.Controls.Add(this.themeCombobox);
            this.Settings.Controls.Add(this.styleCombobox);
            this.Settings.Controls.Add(this.Port);
            this.Settings.Controls.Add(this.BaudRate);
            this.Settings.HorizontalScrollbarBarColor = true;
            this.Settings.HorizontalScrollbarHighlightOnWheel = false;
            this.Settings.HorizontalScrollbarSize = 10;
            this.Settings.Location = new System.Drawing.Point(4, 38);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(627, 274);
            this.Settings.TabIndex = 0;
            this.Settings.Text = "Settings";
            this.Settings.VerticalScrollbarBarColor = true;
            this.Settings.VerticalScrollbarHighlightOnWheel = false;
            this.Settings.VerticalScrollbarSize = 10;
            // 
            // themeCombobox
            // 
            this.themeCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.themeCombobox.FormattingEnabled = true;
            this.themeCombobox.ItemHeight = 23;
            this.themeCombobox.Location = new System.Drawing.Point(443, 16);
            this.themeCombobox.Name = "themeCombobox";
            this.themeCombobox.Size = new System.Drawing.Size(178, 29);
            this.themeCombobox.TabIndex = 5;
            this.themeCombobox.UseSelectable = true;
            // 
            // styleCombobox
            // 
            this.styleCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.styleCombobox.FormattingEnabled = true;
            this.styleCombobox.ItemHeight = 23;
            this.styleCombobox.Location = new System.Drawing.Point(443, 51);
            this.styleCombobox.Name = "styleCombobox";
            this.styleCombobox.Size = new System.Drawing.Size(178, 29);
            this.styleCombobox.TabIndex = 4;
            this.styleCombobox.UseSelectable = true;
            // 
            // Port
            // 
            this.Port.FormattingEnabled = true;
            this.Port.ItemHeight = 23;
            this.Port.Location = new System.Drawing.Point(3, 18);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(121, 29);
            this.Port.TabIndex = 3;
            this.Port.UseSelectable = true;
            // 
            // BaudRate
            // 
            this.BaudRate.FormattingEnabled = true;
            this.BaudRate.ItemHeight = 23;
            this.BaudRate.Location = new System.Drawing.Point(130, 18);
            this.BaudRate.Name = "BaudRate";
            this.BaudRate.Size = new System.Drawing.Size(121, 29);
            this.BaudRate.TabIndex = 2;
            this.BaudRate.UseSelectable = true;
            // 
            // ThemeLbl
            // 
            this.ThemeLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ThemeLbl.AutoSize = true;
            this.ThemeLbl.Location = new System.Drawing.Point(388, 18);
            this.ThemeLbl.Name = "ThemeLbl";
            this.ThemeLbl.Size = new System.Drawing.Size(49, 19);
            this.ThemeLbl.TabIndex = 6;
            this.ThemeLbl.Text = "Theme";
            // 
            // StyleLbl
            // 
            this.StyleLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StyleLbl.AutoSize = true;
            this.StyleLbl.Location = new System.Drawing.Point(388, 57);
            this.StyleLbl.Name = "StyleLbl";
            this.StyleLbl.Size = new System.Drawing.Size(36, 19);
            this.StyleLbl.TabIndex = 6;
            this.StyleLbl.Text = "Style";
            // 
            // metroButton1
            // 
            this.metroButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroButton1.Location = new System.Drawing.Point(0, 236);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(627, 38);
            this.metroButton1.TabIndex = 7;
            this.metroButton1.Text = "Save Settings";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.SaveSettingsButton);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 396);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.metroTabControl1.ResumeLayout(false);
            this.Settings.ResumeLayout(false);
            this.Settings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage Settings;
        private MetroFramework.Controls.MetroComboBox BaudRate;
        private MetroFramework.Controls.MetroComboBox Port;
        private MetroFramework.Controls.MetroComboBox themeCombobox;
        private MetroFramework.Controls.MetroComboBox styleCombobox;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroLabel ThemeLbl;
        private MetroFramework.Controls.MetroLabel StyleLbl;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}

