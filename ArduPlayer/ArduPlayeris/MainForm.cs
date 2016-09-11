using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            serial = new SCom(BaudRate,Port,StartButton,StopButton,SendButton,OutPutTextBox,InputTextBox);    
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            serial.Stop();
        }
    }
}
