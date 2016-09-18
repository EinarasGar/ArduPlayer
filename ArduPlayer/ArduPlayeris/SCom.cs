using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.IO.Ports;
using MetroFramework.Components;
using System.Windows.Forms;
using System.Threading;

namespace ArduPlayeris
{
    public delegate void UpdateListener(string text);
    class SCom
    {
        delegate void SetTextCallback(string text);

        private MetroButton StartButton;
        private MetroButton StopButton;
        private SerialPort port;
        private MetroTextBox textBox;
        private MetroTextBox InputTextBox;
        private MetroComboBox BaudRate;
        private MetroComboBox Port;
        public event UpdateListener UpdateRecieved;
        private bool canRead = true;

        public SCom(MetroComboBox BaudRate, MetroComboBox Port, MetroButton StartButton, MetroButton StopButton,MetroButton SendButton, MetroTextBox textBox, MetroTextBox input)
        {
            this.StartButton = StartButton;
            this.StopButton = StopButton;
            this.textBox = textBox;
            this.InputTextBox = input;
            this.BaudRate = BaudRate;
            this.Port = Port;
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
            Port.SelectedIndex = 0;
            BaudRate.SelectedIndex = 2;

            port = new SerialPort();
            port.BaudRate = Convert.ToInt32(BaudRate.SelectedItem.ToString());

            StartButton.Click += StartButton_Click;
            StopButton.Click += StopButton_Click;
            SendButton.Click += SendButtonClicked;
            InputTextBox.KeyDown += InputTextBox_KeyDown;
            Port.SelectedIndexChanged += Port_SelectedIndexChanged;
            BaudRate.SelectedIndexChanged += BaudRate_SelectedIndexChanged;

            if (Properties.Settings.Default.Try == true) {
                foreach (string ports in SerialPort.GetPortNames())
                {
                    if (port.IsOpen) port.Close();
                    port.PortName = ports;
                    port.Open();
                    port.Write( "hey!\n");
                    Thread.Sleep(150);                    
                    if (port.ReadExisting().Contains("Hello!")) {
                        port.Close();
                        Port.Text = ports;
                        Start();
                        port.DataReceived += Port_DataReceived;
                        return;
                    }
                        port.Close();
                }
                textBox.AppendText("\r\n");                
                textBox.AppendText("Couldn't start communication with any available ports");
            }
            port.PortName = Port.SelectedItem.ToString();

            port.DataReceived += Port_DataReceived;
        }

        private void BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            port.BaudRate = Convert.ToInt32(BaudRate.SelectedItem.ToString());
        }

        private void Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!port.IsOpen)
            port.PortName = Port.SelectedItem.ToString();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendButtonClicked(null, null);
               
            }
        }
                
        private void SetText(string text)
        {
            if (this.textBox.InvokeRequired)
            {
                if (text == "!\r\n")
                {
                    getInfo();
                    return;
                }
                if (text.Contains("+")) {
                    VolumeHelper.IncrementVolume("Spotify");
                    
                    Send("volume" + Math.Round((double)VolumeHelper.GetApplicationVolume("Spotify")).ToString());
                    return;
                }
                if (text.Contains("-"))
                {
                    VolumeHelper.DecrementVolume("Spotify");
                    Send("volume" + Math.Round((double)VolumeHelper.GetApplicationVolume("Spotify")).ToString());
                    return;
                }
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
            if (!canRead) return;
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

                Port.Enabled = true;
                BaudRate.Enabled = true;
                StartButton.Enabled = true;
                StopButton.Enabled = false;
                port.Close();
                textBox.AppendText("\n");
                textBox.AppendText("Disconnected.\r\n");
            }
        }

        public void Start() {
            if (!port.IsOpen)
            {
                Port.Enabled = false;
                BaudRate.Enabled = false;
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                port.Open();
                textBox.Clear();
                textBox.AppendText("Connected to Arduino!\n");
                   
            }
        }
        
        public void getInfo()
        {
            try
            {
                if (port.IsOpen)
                {
                    canRead = false;
                    Thread.Sleep(200);
                    Send("giveInfo");
                    Thread.Sleep(200);
                    string split1 = port.ReadExisting().Split('{')[1];
                    string split2 = split1.Split('}')[0];
                    UpdateRecieved?.Invoke(split2);
                    canRead = true;

                }
            }
            catch
            {
                SetText("Couldnt Recieve information");
            }

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Start();
            getInfo();
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
