using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.IO.Ports;
using MetroFramework.Components;
using System.Windows.Forms;

namespace ArduPlayeris
{
    class SCom
    {

        private MetroButton StartButton;
        private MetroButton StopButton;
        private SerialPort port;
        private MetroTextBox textBox;
        private MetroTextBox InputTextBox;

        public SCom(MetroComboBox BaudRate, MetroComboBox Port, MetroButton StartButton, MetroButton StopButton,MetroButton SendButton, MetroTextBox textBox, MetroTextBox input)
        {
            this.StartButton = StartButton;
            this.StopButton = StopButton;
            this.textBox = textBox;
            this.InputTextBox = input;
            string[] serialPorts = SerialPort.GetPortNames();
            Port.Items.AddRange(serialPorts);
            BaudRate.Items.Add(2400);
            BaudRate.Items.Add(4800);
            BaudRate.Items.Add(9600);
            BaudRate.Items.Add(14400);
            BaudRate.Items.Add(19200);
            BaudRate.Items.Add(28800);
            BaudRate.Items.Add(38400);
            BaudRate.Items.Add(57600);
            BaudRate.Items.Add(115200);
            Port.SelectedIndex = 1;
            BaudRate.SelectedIndex = 2;

            port = new SerialPort();
            port.PortName = Port.SelectedItem.ToString();
            port.BaudRate = Convert.ToInt32(BaudRate.SelectedItem.ToString());

            port.DataReceived += Port_DataReceived;
            StartButton.Click += StartButton_Click;
            StopButton.Click += StopButton_Click;
            SendButton.Click += SendButtonClicked;
            InputTextBox.KeyDown += InputTextBox_KeyDown;
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendButtonClicked(null, null);
               
            }
        }

 
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            if (this.textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                textBox.BeginInvoke(d, new object[] { text });
            }
            else
            {
                textBox.AppendText(text);
            }
        }


        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SetText(port.ReadExisting());
            }
            catch (Exception ex)
            {
                SetText(ex.ToString());
            }
        }

        public void Stop()
        {
            if (port.IsOpen)
            {
                StartButton.Enabled = true;
                StopButton.Enabled = false;
                port.Close();
                textBox.AppendText("\n");
                textBox.AppendText("\nDisconnected.");
            }
        }

        public void Start() {
            if (!port.IsOpen)
            {
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                port.Open();
                textBox.Clear();
                textBox.AppendText("Connected to Arduino!\n");
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        public void Send(string Text) {
            if (!port.IsOpen) return;
            if (Text == "") return;
            port.Write(Text + "\n");
        }
        private void SendButtonClicked(object sender, EventArgs e)
        {
            Send(InputTextBox.Text);
            InputTextBox.Clear();
        }

    }
}
