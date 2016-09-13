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
            this.Settings = new MetroFramework.Controls.MetroTabPage();
            this.metroToggle2 = new MetroFramework.Controls.MetroToggle();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.BaudLbl = new MetroFramework.Controls.MetroLabel();
            this.PortLbl = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.StyleLbl = new MetroFramework.Controls.MetroLabel();
            this.ThemeLbl = new MetroFramework.Controls.MetroLabel();
            this.themeCombobox = new MetroFramework.Controls.MetroComboBox();
            this.styleCombobox = new MetroFramework.Controls.MetroComboBox();
            this.Port = new MetroFramework.Controls.MetroComboBox();
            this.BaudRate = new MetroFramework.Controls.MetroComboBox();
            this.SComTab = new MetroFramework.Controls.MetroTabPage();
            this.UploadCodeButton = new MetroFramework.Controls.MetroButton();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.SendButton = new MetroFramework.Controls.MetroButton();
            this.SketchFilePathButton = new MetroFramework.Controls.MetroButton();
            this.ArduinoIdePathButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.ArduinoIDeLbl = new MetroFramework.Controls.MetroLabel();
            this.ArduinoIdePathTextbox = new MetroFramework.Controls.MetroTextBox();
            this.SketchFilePathTextbox = new MetroFramework.Controls.MetroTextBox();
            this.ClearLogsButton = new MetroFramework.Controls.MetroButton();
            this.SaveLogsButton = new MetroFramework.Controls.MetroButton();
            this.InputTextBox = new MetroFramework.Controls.MetroTextBox();
            this.StopButton = new MetroFramework.Controls.MetroButton();
            this.StartButton = new MetroFramework.Controls.MetroButton();
            this.OutPutTextBox = new MetroFramework.Controls.MetroTextBox();
            this.HumidityLbl = new MetroFramework.Controls.MetroLabel();
            this.TempLbl = new MetroFramework.Controls.MetroLabel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.ColorOrganTab = new MetroFramework.Controls.MetroTabPage();
            this.RedLbl = new MetroFramework.Controls.MetroLabel();
            this.GreenLbl = new MetroFramework.Controls.MetroLabel();
            this.BlueLbl = new MetroFramework.Controls.MetroLabel();
            this.RedTrackBar = new MetroFramework.Controls.MetroTrackBar();
            this.BlueTrackBar = new MetroFramework.Controls.MetroTrackBar();
            this.GreenTrackBar = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ColorOranToggle = new MetroFramework.Controls.MetroToggle();
            this.Settings.SuspendLayout();
            this.SComTab.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.ColorOrganTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // Settings
            // 
            this.Settings.Controls.Add(this.metroToggle2);
            this.Settings.Controls.Add(this.metroLabel2);
            this.Settings.Controls.Add(this.BaudLbl);
            this.Settings.Controls.Add(this.PortLbl);
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
            // metroToggle2
            // 
            this.metroToggle2.AutoSize = true;
            this.metroToggle2.Location = new System.Drawing.Point(111, 91);
            this.metroToggle2.Name = "metroToggle2";
            this.metroToggle2.Size = new System.Drawing.Size(80, 17);
            this.metroToggle2.TabIndex = 10;
            this.metroToggle2.Text = "Off";
            this.metroToggle2.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(7, 89);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(98, 19);
            this.metroLabel2.TabIndex = 9;
            this.metroLabel2.Text = "Try to find port";
            // 
            // BaudLbl
            // 
            this.BaudLbl.AutoSize = true;
            this.BaudLbl.Location = new System.Drawing.Point(7, 57);
            this.BaudLbl.Name = "BaudLbl";
            this.BaudLbl.Size = new System.Drawing.Size(39, 19);
            this.BaudLbl.TabIndex = 8;
            this.BaudLbl.Text = "Baud";
            // 
            // PortLbl
            // 
            this.PortLbl.AutoSize = true;
            this.PortLbl.Location = new System.Drawing.Point(7, 18);
            this.PortLbl.Name = "PortLbl";
            this.PortLbl.Size = new System.Drawing.Size(34, 19);
            this.PortLbl.TabIndex = 8;
            this.PortLbl.Text = "Port";
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
            this.Port.Location = new System.Drawing.Point(47, 16);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(144, 29);
            this.Port.TabIndex = 3;
            this.Port.UseSelectable = true;
            // 
            // BaudRate
            // 
            this.BaudRate.FormattingEnabled = true;
            this.BaudRate.ItemHeight = 23;
            this.BaudRate.Location = new System.Drawing.Point(47, 51);
            this.BaudRate.Name = "BaudRate";
            this.BaudRate.Size = new System.Drawing.Size(144, 29);
            this.BaudRate.TabIndex = 2;
            this.BaudRate.UseSelectable = true;
            // 
            // SComTab
            // 
            this.SComTab.Controls.Add(this.UploadCodeButton);
            this.SComTab.Controls.Add(this.metroButton4);
            this.SComTab.Controls.Add(this.SendButton);
            this.SComTab.Controls.Add(this.SketchFilePathButton);
            this.SComTab.Controls.Add(this.ArduinoIdePathButton);
            this.SComTab.Controls.Add(this.metroLabel4);
            this.SComTab.Controls.Add(this.ArduinoIDeLbl);
            this.SComTab.Controls.Add(this.ArduinoIdePathTextbox);
            this.SComTab.Controls.Add(this.SketchFilePathTextbox);
            this.SComTab.Controls.Add(this.ClearLogsButton);
            this.SComTab.Controls.Add(this.SaveLogsButton);
            this.SComTab.Controls.Add(this.InputTextBox);
            this.SComTab.Controls.Add(this.StopButton);
            this.SComTab.Controls.Add(this.StartButton);
            this.SComTab.Controls.Add(this.OutPutTextBox);
            this.SComTab.HorizontalScrollbarBarColor = true;
            this.SComTab.HorizontalScrollbarHighlightOnWheel = false;
            this.SComTab.HorizontalScrollbarSize = 10;
            this.SComTab.Location = new System.Drawing.Point(4, 38);
            this.SComTab.Name = "SComTab";
            this.SComTab.Size = new System.Drawing.Size(627, 274);
            this.SComTab.TabIndex = 1;
            this.SComTab.Text = "Serial Communication";
            this.SComTab.VerticalScrollbarBarColor = true;
            this.SComTab.VerticalScrollbarHighlightOnWheel = false;
            this.SComTab.VerticalScrollbarSize = 10;
            // 
            // UploadCodeButton
            // 
            this.UploadCodeButton.Location = new System.Drawing.Point(337, 248);
            this.UploadCodeButton.Name = "UploadCodeButton";
            this.UploadCodeButton.Size = new System.Drawing.Size(290, 23);
            this.UploadCodeButton.TabIndex = 14;
            this.UploadCodeButton.Text = "Upload Sketch To Arduino";
            this.UploadCodeButton.UseSelectable = true;
            this.UploadCodeButton.Click += new System.EventHandler(this.UploadCodeButton_Click);
            // 
            // metroButton4
            // 
            this.metroButton4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton4.Location = new System.Drawing.Point(211, 3);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(106, 23);
            this.metroButton4.TabIndex = 6;
            this.metroButton4.Text = "Uzmirsau >:(";
            this.metroButton4.UseSelectable = true;
            // 
            // SendButton
            // 
            this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SendButton.Location = new System.Drawing.Point(228, 251);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(88, 23);
            this.SendButton.TabIndex = 5;
            this.SendButton.Text = "Send";
            this.SendButton.UseSelectable = true;
            // 
            // SketchFilePathButton
            // 
            this.SketchFilePathButton.Location = new System.Drawing.Point(606, 219);
            this.SketchFilePathButton.Name = "SketchFilePathButton";
            this.SketchFilePathButton.Size = new System.Drawing.Size(21, 23);
            this.SketchFilePathButton.TabIndex = 13;
            this.SketchFilePathButton.Text = "...";
            this.SketchFilePathButton.UseSelectable = true;
            this.SketchFilePathButton.Click += new System.EventHandler(this.SketchFilePathButton_Click);
            // 
            // ArduinoIdePathButton
            // 
            this.ArduinoIdePathButton.Location = new System.Drawing.Point(606, 190);
            this.ArduinoIdePathButton.Name = "ArduinoIdePathButton";
            this.ArduinoIdePathButton.Size = new System.Drawing.Size(21, 23);
            this.ArduinoIdePathButton.TabIndex = 13;
            this.ArduinoIdePathButton.Text = "...";
            this.ArduinoIdePathButton.UseSelectable = true;
            this.ArduinoIdePathButton.Click += new System.EventHandler(this.ArduinoIdePathButton_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(337, 219);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(102, 19);
            this.metroLabel4.TabIndex = 12;
            this.metroLabel4.Text = "Sketch File Path:";
            // 
            // ArduinoIDeLbl
            // 
            this.ArduinoIDeLbl.AutoSize = true;
            this.ArduinoIDeLbl.Location = new System.Drawing.Point(337, 190);
            this.ArduinoIDeLbl.Name = "ArduinoIDeLbl";
            this.ArduinoIDeLbl.Size = new System.Drawing.Size(111, 19);
            this.ArduinoIDeLbl.TabIndex = 11;
            this.ArduinoIDeLbl.Text = "Arduino IDE Path:";
            // 
            // ArduinoIdePathTextbox
            // 
            this.ArduinoIdePathTextbox.Lines = new string[0];
            this.ArduinoIdePathTextbox.Location = new System.Drawing.Point(454, 190);
            this.ArduinoIdePathTextbox.MaxLength = 32767;
            this.ArduinoIdePathTextbox.Name = "ArduinoIdePathTextbox";
            this.ArduinoIdePathTextbox.PasswordChar = '\0';
            this.ArduinoIdePathTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ArduinoIdePathTextbox.SelectedText = "";
            this.ArduinoIdePathTextbox.Size = new System.Drawing.Size(156, 23);
            this.ArduinoIdePathTextbox.TabIndex = 10;
            this.ArduinoIdePathTextbox.UseSelectable = true;
            // 
            // SketchFilePathTextbox
            // 
            this.SketchFilePathTextbox.Lines = new string[0];
            this.SketchFilePathTextbox.Location = new System.Drawing.Point(454, 219);
            this.SketchFilePathTextbox.MaxLength = 32767;
            this.SketchFilePathTextbox.Name = "SketchFilePathTextbox";
            this.SketchFilePathTextbox.PasswordChar = '\0';
            this.SketchFilePathTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SketchFilePathTextbox.SelectedText = "";
            this.SketchFilePathTextbox.Size = new System.Drawing.Size(156, 23);
            this.SketchFilePathTextbox.TabIndex = 9;
            this.SketchFilePathTextbox.UseSelectable = true;
            // 
            // ClearLogsButton
            // 
            this.ClearLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearLogsButton.Location = new System.Drawing.Point(102, 3);
            this.ClearLogsButton.Name = "ClearLogsButton";
            this.ClearLogsButton.Size = new System.Drawing.Size(112, 23);
            this.ClearLogsButton.TabIndex = 6;
            this.ClearLogsButton.Text = "Clear Logs";
            this.ClearLogsButton.UseSelectable = true;
            this.ClearLogsButton.Click += new System.EventHandler(this.ClearLogsButtonClicked);
            // 
            // SaveLogsButton
            // 
            this.SaveLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLogsButton.Location = new System.Drawing.Point(0, 3);
            this.SaveLogsButton.Name = "SaveLogsButton";
            this.SaveLogsButton.Size = new System.Drawing.Size(106, 23);
            this.SaveLogsButton.TabIndex = 6;
            this.SaveLogsButton.Text = "Save Logs";
            this.SaveLogsButton.UseSelectable = true;
            this.SaveLogsButton.Click += new System.EventHandler(this.SaveLogsButtonCliecked);
            // 
            // InputTextBox
            // 
            this.InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputTextBox.Lines = new string[0];
            this.InputTextBox.Location = new System.Drawing.Point(0, 251);
            this.InputTextBox.MaxLength = 32767;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.PasswordChar = '\0';
            this.InputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.InputTextBox.SelectedText = "";
            this.InputTextBox.Size = new System.Drawing.Size(254, 23);
            this.InputTextBox.TabIndex = 4;
            this.InputTextBox.UseSelectable = true;
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(475, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(156, 23);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "Stop";
            this.StopButton.UseSelectable = true;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(321, 3);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(156, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseSelectable = true;
            // 
            // OutPutTextBox
            // 
            this.OutPutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutPutTextBox.Lines = new string[] {
        "Waiting for connection..."};
            this.OutPutTextBox.Location = new System.Drawing.Point(0, 25);
            this.OutPutTextBox.MaxLength = 32767;
            this.OutPutTextBox.Multiline = true;
            this.OutPutTextBox.Name = "OutPutTextBox";
            this.OutPutTextBox.PasswordChar = '\0';
            this.OutPutTextBox.ReadOnly = true;
            this.OutPutTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutPutTextBox.SelectedText = "";
            this.OutPutTextBox.Size = new System.Drawing.Size(316, 230);
            this.OutPutTextBox.TabIndex = 2;
            this.OutPutTextBox.Text = "Waiting for connection...";
            this.OutPutTextBox.UseSelectable = true;
            // 
            // HumidityLbl
            // 
            this.HumidityLbl.AutoSize = true;
            this.HumidityLbl.Location = new System.Drawing.Point(159, 35);
            this.HumidityLbl.Name = "HumidityLbl";
            this.HumidityLbl.Size = new System.Drawing.Size(94, 19);
            this.HumidityLbl.TabIndex = 10;
            this.HumidityLbl.Text = "Humidity:  0 %";
            // 
            // TempLbl
            // 
            this.TempLbl.AutoSize = true;
            this.TempLbl.Location = new System.Drawing.Point(159, 16);
            this.TempLbl.Name = "TempLbl";
            this.TempLbl.Size = new System.Drawing.Size(115, 19);
            this.TempLbl.TabIndex = 9;
            this.TempLbl.Text = "Temperature: 0 °C";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.SComTab);
            this.metroTabControl1.Controls.Add(this.ColorOrganTab);
            this.metroTabControl1.Controls.Add(this.Settings);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(635, 316);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // ColorOrganTab
            // 
            this.ColorOrganTab.Controls.Add(this.RedLbl);
            this.ColorOrganTab.Controls.Add(this.GreenLbl);
            this.ColorOrganTab.Controls.Add(this.BlueLbl);
            this.ColorOrganTab.Controls.Add(this.RedTrackBar);
            this.ColorOrganTab.Controls.Add(this.BlueTrackBar);
            this.ColorOrganTab.Controls.Add(this.GreenTrackBar);
            this.ColorOrganTab.Controls.Add(this.metroLabel1);
            this.ColorOrganTab.Controls.Add(this.ColorOranToggle);
            this.ColorOrganTab.HorizontalScrollbarBarColor = true;
            this.ColorOrganTab.HorizontalScrollbarHighlightOnWheel = false;
            this.ColorOrganTab.HorizontalScrollbarSize = 10;
            this.ColorOrganTab.Location = new System.Drawing.Point(4, 38);
            this.ColorOrganTab.Name = "ColorOrganTab";
            this.ColorOrganTab.Size = new System.Drawing.Size(627, 274);
            this.ColorOrganTab.TabIndex = 2;
            this.ColorOrganTab.Text = "Color Organ";
            this.ColorOrganTab.VerticalScrollbarBarColor = true;
            this.ColorOrganTab.VerticalScrollbarHighlightOnWheel = false;
            this.ColorOrganTab.VerticalScrollbarSize = 10;
            // 
            // RedLbl
            // 
            this.RedLbl.AutoSize = true;
            this.RedLbl.Location = new System.Drawing.Point(10, 14);
            this.RedLbl.Name = "RedLbl";
            this.RedLbl.Size = new System.Drawing.Size(35, 19);
            this.RedLbl.TabIndex = 12;
            this.RedLbl.Text = "Red:";
            // 
            // GreenLbl
            // 
            this.GreenLbl.AutoSize = true;
            this.GreenLbl.Location = new System.Drawing.Point(10, 43);
            this.GreenLbl.Name = "GreenLbl";
            this.GreenLbl.Size = new System.Drawing.Size(47, 19);
            this.GreenLbl.TabIndex = 12;
            this.GreenLbl.Text = "Green:";
            // 
            // BlueLbl
            // 
            this.BlueLbl.AutoSize = true;
            this.BlueLbl.Location = new System.Drawing.Point(10, 72);
            this.BlueLbl.Name = "BlueLbl";
            this.BlueLbl.Size = new System.Drawing.Size(37, 19);
            this.BlueLbl.TabIndex = 12;
            this.BlueLbl.Text = "Blue:";
            // 
            // RedTrackBar
            // 
            this.RedTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.RedTrackBar.LargeChange = 1;
            this.RedTrackBar.Location = new System.Drawing.Point(61, 14);
            this.RedTrackBar.Maximum = 40;
            this.RedTrackBar.MouseWheelBarPartitions = 9000;
            this.RedTrackBar.Name = "RedTrackBar";
            this.RedTrackBar.Size = new System.Drawing.Size(146, 23);
            this.RedTrackBar.TabIndex = 11;
            this.RedTrackBar.Text = "metroTrackBar1";
            this.RedTrackBar.Value = 20;
            this.RedTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RedTrackBar_MouseUp);
            // 
            // BlueTrackBar
            // 
            this.BlueTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.BlueTrackBar.LargeChange = 1;
            this.BlueTrackBar.Location = new System.Drawing.Point(61, 72);
            this.BlueTrackBar.Maximum = 40;
            this.BlueTrackBar.MouseWheelBarPartitions = 9000;
            this.BlueTrackBar.Name = "BlueTrackBar";
            this.BlueTrackBar.Size = new System.Drawing.Size(146, 23);
            this.BlueTrackBar.TabIndex = 11;
            this.BlueTrackBar.Text = "metroTrackBar1";
            this.BlueTrackBar.Value = 20;
            this.BlueTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BlueTrackBar_MouseUp);
            // 
            // GreenTrackBar
            // 
            this.GreenTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.GreenTrackBar.LargeChange = 1;
            this.GreenTrackBar.Location = new System.Drawing.Point(61, 43);
            this.GreenTrackBar.Maximum = 40;
            this.GreenTrackBar.MouseWheelBarPartitions = 9000;
            this.GreenTrackBar.Name = "GreenTrackBar";
            this.GreenTrackBar.Size = new System.Drawing.Size(146, 23);
            this.GreenTrackBar.TabIndex = 11;
            this.GreenTrackBar.Text = "metroTrackBar1";
            this.GreenTrackBar.Value = 20;
            this.GreenTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GreenTrackBar_MouseUp);
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(435, 14);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(84, 19);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "Color Organ";
            // 
            // ColorOranToggle
            // 
            this.ColorOranToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorOranToggle.AutoSize = true;
            this.ColorOranToggle.Checked = true;
            this.ColorOranToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ColorOranToggle.Location = new System.Drawing.Point(525, 16);
            this.ColorOranToggle.Name = "ColorOranToggle";
            this.ColorOranToggle.Size = new System.Drawing.Size(80, 17);
            this.ColorOranToggle.TabIndex = 9;
            this.ColorOranToggle.Text = "On";
            this.ColorOranToggle.UseSelectable = true;
            this.ColorOranToggle.CheckedChanged += new System.EventHandler(this.ColorOranToggleChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 396);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.TempLbl);
            this.Controls.Add(this.HumidityLbl);
            this.MaximumSize = new System.Drawing.Size(675, 396);
            this.MinimumSize = new System.Drawing.Size(675, 396);
            this.Name = "MainForm";
            this.Text = "ArduPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Settings.ResumeLayout(false);
            this.Settings.PerformLayout();
            this.SComTab.ResumeLayout(false);
            this.SComTab.PerformLayout();
            this.metroTabControl1.ResumeLayout(false);
            this.ColorOrganTab.ResumeLayout(false);
            this.ColorOrganTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTabPage Settings;
        private MetroFramework.Controls.MetroToggle metroToggle2;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel BaudLbl;
        private MetroFramework.Controls.MetroLabel PortLbl;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroLabel StyleLbl;
        private MetroFramework.Controls.MetroLabel ThemeLbl;
        private MetroFramework.Controls.MetroComboBox themeCombobox;
        private MetroFramework.Controls.MetroComboBox styleCombobox;
        private MetroFramework.Controls.MetroComboBox Port;
        private MetroFramework.Controls.MetroComboBox BaudRate;
        private MetroFramework.Controls.MetroTabPage SComTab;
        private MetroFramework.Controls.MetroButton metroButton4;
        private MetroFramework.Controls.MetroButton ClearLogsButton;
        private MetroFramework.Controls.MetroButton SaveLogsButton;
        private MetroFramework.Controls.MetroButton SendButton;
        private MetroFramework.Controls.MetroTextBox InputTextBox;
        private MetroFramework.Controls.MetroButton StopButton;
        private MetroFramework.Controls.MetroButton StartButton;
        private MetroFramework.Controls.MetroTextBox OutPutTextBox;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroLabel HumidityLbl;
        private MetroFramework.Controls.MetroLabel TempLbl;
        private MetroFramework.Controls.MetroButton SketchFilePathButton;
        private MetroFramework.Controls.MetroButton ArduinoIdePathButton;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel ArduinoIDeLbl;
        private MetroFramework.Controls.MetroTextBox ArduinoIdePathTextbox;
        private MetroFramework.Controls.MetroTextBox SketchFilePathTextbox;
        private MetroFramework.Controls.MetroButton UploadCodeButton;
        private MetroFramework.Controls.MetroTabPage ColorOrganTab;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroToggle ColorOranToggle;
        private MetroFramework.Controls.MetroLabel RedLbl;
        private MetroFramework.Controls.MetroLabel GreenLbl;
        private MetroFramework.Controls.MetroLabel BlueLbl;
        private MetroFramework.Controls.MetroTrackBar RedTrackBar;
        private MetroFramework.Controls.MetroTrackBar BlueTrackBar;
        private MetroFramework.Controls.MetroTrackBar GreenTrackBar;
    }
}

