using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ArduPlayeris
{
    public partial class MainForm : MetroForm
    {
        public SCom serial;
        public SpotifyHelper spotfyH;

        public MainForm()
        {
            InitializeComponent();
            InitThemeSettings();
            metroTabControl1.SelectedTab = SComTab;
            metroTabControl1.SelectedIndexChanged += MetroTabControl1_SelectedIndexChanged;
            serial = new SCom(BaudRate,Port,StartButton,StopButton,SendButton,OutPutTextBox,InputTextBox);
            spotfyH = new SpotifyHelper();
            new CommunictaionManager(this);
            serial.UpdateRecieved += Serial_UpdateRecieved;
            
            //    serial.getInfo(); // move this somewhere else   
           

        }

        private void CompareFiles(bool ShowDialog)
        {
            if (Properties.Settings.Default.SketchPath != "") {
                FileStream stream = new FileStream(Properties.Settings.Default.SketchPath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
                string hash = Convert.ToBase64String(md5Provider.ComputeHash(stream));
                stream.Close();
                if (ShowDialog && hash != Properties.Settings.Default.SketchMd5)
                {
                    DialogResult dialogResult = MetroMessageBox.Show(this, "\nArduino code has been changed. Do you want to upload it?", "Arduino code.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        UploadCodeButton_Click(null, null);
                    }
                }
                Properties.Settings.Default.SketchMd5 = hash;
                Properties.Settings.Default.Save();
                
            }
        }

        private void Serial_UpdateRecieved(string text) // USELESS ATM. REFACTOR.
        {
            string[] updates = text.Split('/');

            if (ColorOranToggle.InvokeRequired)
            {
                if (updates[0] == "1")
                {
                    ColorOranToggle.Invoke(new MethodInvoker(delegate { ColorOranToggle.Checked = true; }));
                }
                else
                    ColorOranToggle.Invoke(new MethodInvoker(delegate { ColorOranToggle.Checked = false; }));
            }
            else {
                if (updates[0] == "1")
                {
                    ColorOranToggle.Checked = true;
                }
                else
                    ColorOranToggle.Checked = false;
            }

            if (TempLbl.InvokeRequired)
                TempLbl.Invoke(new MethodInvoker(delegate { TempLbl.Text = "Temperature: "+updates[1]+" °C"; }));
            else
                TempLbl.Text = "Temperature: " + updates[1] + " °C";

            if (HumidityLbl.InvokeRequired)
                HumidityLbl.Invoke(new MethodInvoker(delegate { HumidityLbl.Text = "Humidity: " + updates[2] + " %"; }));
            else
                HumidityLbl.Text = "Humidity: " + updates[2] + " %";
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            serial.Stop();
        }

        private void ClearLogsButtonClicked(object sender, EventArgs e)
        {
            OutPutTextBox.Clear();
        }

        private void SaveLogsButtonCliecked(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog()
            {
                DefaultExt = ".txt",
                AddExtension = true,
                FileName = "ArduPlayer log",
                SupportMultiDottedExtensions = true,
                Filter = "Text Files (*.txt)|*.txt"
            };

            if (s.ShowDialog() == DialogResult.OK)
            {
                FileStream logFile = File.Open(s.FileName, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(logFile))
                {
                    sw.Write(OutPutTextBox.Text);
                }
                logFile.Close();
            }
        }

    

        private void ArduinoIdePathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FileDialog = new FolderBrowserDialog();
            if (FileDialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo FilePath = new FileInfo(FileDialog.SelectedPath);
            ArduinoIdePathTextbox.Text = FilePath.FullName;
            Properties.Settings.Default.ArduinoIdePath = FilePath.FullName;
            Properties.Settings.Default.Save();
        }

        private void SketchFilePathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            if (FileDialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo FilePath = new FileInfo(FileDialog.FileName);
            SketchFilePathTextbox.Text = FilePath.FullName;
            Properties.Settings.Default.SketchPath = FilePath.FullName;
            Properties.Settings.Default.Save();
        }

        private async void UploadCodeButton_Click(object sender, EventArgs e)
        {
            serial.Stop();
            await Task.Delay(100);
            Thread UploadCode = new Thread(new ThreadStart(UploadArduinoCode));
            UploadCode.Start();
            while (UploadCode.IsAlive)
            {
                await Task.Delay(1000);
            }
            serial.Start();
            await Task.Delay(2000);
          //  serial.getInfo();
            CompareFiles(false);
        }

        private void UploadArduinoCode() {
            Thread.CurrentThread.IsBackground = true;
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            p.StartInfo = info;
            p.Start();
            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("cd " + ArduinoIdePathTextbox.Text);
                    sw.WriteLine("arduino_debug --upload " + SketchFilePathTextbox.Text);
                }
            }        
            p.OutputDataReceived += (s, args) => { try { if (!args.Data.Contains("Windows") && !args.Data.Contains(".ino") && !args.Data.Contains("cd")&& !args.Data.Contains("Microsoft") && args.Data!="") OutPutTextBox.Invoke(new MethodInvoker(delegate { try { OutPutTextBox.AppendText("\r\n\r\n" + args.Data); } catch{ } })); } catch { } };
            p.ErrorDataReceived+= (s, args) => { try { if (args.Data.Contains("ArduinoCodeV2:")) MetroMessageBox.Show(this, "\r\n" + args.Data,"Upload Error",MessageBoxButtons.OK,MessageBoxIcon.Error);  if (!args.Data.Contains("Windows") && !args.Data.Contains(".ino") && !args.Data.Contains("cd")&& !args.Data.Contains("Microsoft") && args.Data!="") OutPutTextBox.Invoke(new MethodInvoker(delegate { try { OutPutTextBox.AppendText("\r\n\r\n" + args.Data); } catch{ } })); } catch { } };            
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();            
            p.Close();
        }
        
        private void MainForm_Shown(object sender, EventArgs e)
        {
            CompareFiles(true);
        }

        private void TestButton(object sender, EventArgs e)
        {

        }
 

        private void AutoStartToggle(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                       ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (metroToggle1.Checked)
                rk.SetValue("ArduPlayer", Application.ExecutablePath.ToString());
            else
                rk.DeleteValue("ArduPlayer", false);
        }
    }
}
