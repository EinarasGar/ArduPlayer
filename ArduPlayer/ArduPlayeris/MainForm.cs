using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduPlayeris
{
    public partial class MainForm : MetroForm
    {
        SCom serial;
        public MainForm()
        {
            InitializeComponent();
            InitThemeSettings();
            metroTabControl1.SelectedTab = metroTabPage2;
            serial = new SCom(BaudRate,Port,StartButton,StopButton,SendButton,OutPutTextBox,InputTextBox);
            serial.UpdateRecieved += Serial_UpdateRecieved;
            serial.getInfo(); // move this somewhere else
        }

        private void Serial_UpdateRecieved(string text)
        {
            if (ColorOranToggle.InvokeRequired)
            {
                if (text == "1")
                {
                    ColorOranToggle.Invoke(new MethodInvoker(delegate { ColorOranToggle.Checked = true; }));
                }
                else
                    ColorOranToggle.Invoke(new MethodInvoker(delegate { ColorOranToggle.Checked = false; }));
            }
            else {
                if (text == "1")
                {
                    ColorOranToggle.Checked = true;
                }
                else
                    ColorOranToggle.Checked = false;
            }
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

        private void ColorOranToggleChanged(object sender, EventArgs e)
        {
            if (ColorOranToggle.Checked)
                serial.Send("colorson");
            else
                serial.Send("colorsoff");
        }
    }
}
