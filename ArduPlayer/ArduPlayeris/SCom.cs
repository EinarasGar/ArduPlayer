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
using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using MetroFramework;

namespace ArduPlayeris
{
    public delegate void UpdateListener(string text);
    public delegate void UsbListener();
    public class SCom
    {
        //http://stackoverflow.com/questions/271238/how-do-i-detect-when-a-removable-disk-is-inserted-using-c
        ManagementEventWatcher w = null;
        void AddRemoveUSBHandler()
        {

            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;

            try
            {

                q = new WqlEventQuery();
                q.EventClassName = "__InstanceDeletionEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = "TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += USBRemoved;

                w.Start();
            }
            catch (Exception e)
            {


                Console.WriteLine(e.Message);
                if (w != null)
                {
                    w.Stop();

                }
            }

        }

        void AddInsertUSBHandler()
        {

            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;

            try
            {

                q = new WqlEventQuery();
                q.EventClassName = "__InstanceCreationEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = "TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += USBInserted;

                w.Start();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                if (w != null)
                {
                    w.Stop();

                }
            }

        }

        delegate void SetTextCallback(string text);

        private MainForm mainForma;
        private MetroButton StartButton;
        private MetroButton StopButton;
        private MetroButton SendButton;
        private SerialPort port;
        private MetroTextBox textBox;
        private MetroTextBox InputTextBox;
        private MetroComboBox BaudRate;
        private MetroComboBox Port;
        public event UpdateListener UpdateRecieved;
        public event UpdateListener CommandRecieved;
        public event UsbListener USBconnected;
        public event UsbListener USBdisconnected;
        private bool canRead = true;

        private string[] portBuffer;
        

        public SCom(MetroComboBox BaudRate, MetroComboBox Port, MetroButton StartButton, MetroButton StopButton,MetroButton SendButton, MetroTextBox textBox, MetroTextBox input)
        {

            AddInsertUSBHandler();
            AddRemoveUSBHandler();
            this.StartButton = StartButton;
            this.StopButton = StopButton;
            this.textBox = textBox;
            this.InputTextBox = input;
            this.BaudRate = BaudRate;
            this.Port = Port;
            string[] serialPorts = SerialPort.GetPortNames();
            portBuffer = serialPorts;
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
            

         /*   if (Properties.Settings.Default.Try == true) {
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
            }*/

            port.PortName = Port.SelectedItem.ToString();

            port.DataReceived += Port_DataReceived;

           // Start();
        }

        public SCom(MainForm mainForm)
        {
            AddInsertUSBHandler();
            AddRemoveUSBHandler();
            this.StartButton = mainForm.StartButton;
            this.StopButton = mainForm.StopButton;
            this.textBox = mainForm.OutPutTextBox;
            this.InputTextBox = mainForm.InputTextBox;
            this.BaudRate = mainForm.BaudRate;
            this.SendButton = mainForm.SendButton;
            this.mainForma = mainForm;
            this.Port = mainForm.Port;
            string[] serialPorts = SerialPort.GetPortNames();
            portBuffer = serialPorts;
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


            /*   if (Properties.Settings.Default.Try == true) {
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
               }*/

            port.PortName = Port.SelectedItem.ToString();

            port.DataReceived += Port_DataReceived;

            // Start();
        }

        void USBInserted(object sender, EventArgs e)
        {
            string newPort = "";
            foreach (string portName in SerialPort.GetPortNames())
            {
                if (!portBuffer.Contains(portName))
                {
                    newPort = portName;
                }
            }
            if (newPort != "")
            {
                string[] serialPorts = SerialPort.GetPortNames();
                portBuffer = serialPorts;
                Port.Items.Clear();
                Port.Items.AddRange(serialPorts);
                Port.SelectedIndex = 0;

                DialogResult dialogResult = MetroMessageBox.Show(this.mainForma, "\nNew serial port " + newPort + " has been detected\nDo you want to start communication?", "Serial Port", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    port.PortName = newPort;
                    Start();
                }

            }
            Console.WriteLine("A USB device inserted");
            USBconnected?.Invoke();

        }

        void USBRemoved(object sender, EventArgs e)
        {
            Console.WriteLine("A USB device removed");
            if (port.IsOpen)
            {
                Console.WriteLine("IsOpen"); // netinka
            }
            if (!port.IsOpen)
            {
                Stop();
            }
            USBdisconnected?.Invoke();

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
                string text = port.ReadLine().Replace("\r",string.Empty);
                if (text[0] == '!')
                {
                    CommandRecieved?.Invoke(text);
                    SetText(text + "\n");

                }
                else
                {
                    SetText(text + "\n");
                }
                
            }
            catch (Exception ex)
            {
                SetText(ex.ToString());
            }
        }

        public void Stop()
        {
            Port.Enabled = true;
            BaudRate.Enabled = true;
            StartButton.Enabled = true;
            StopButton.Enabled = false;
            string[] serialPorts = SerialPort.GetPortNames();
            portBuffer = serialPorts;
            Port.Items.Clear();
            Port.Items.AddRange(serialPorts);
            Port.SelectedIndex = 0;
            port.Close();
            textBox.AppendText("\n");
            textBox.AppendText("Disconnected.\r\n");
            
          
        }

        public void Start() {
            if (!port.IsOpen)
            {
                Port.Enabled = false;
                BaudRate.Enabled = false;
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                port.DtrEnable = true;
                port.RtsEnable = true;
                port.Open();
                textBox.Clear();
                textBox.AppendText("Connected to Port: " + port.PortName + " and Baud: " + port.BaudRate+ "\n" );
                   
            }
        }
        
        public void getInfo() // TODO: REWORK THIS
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
            //getInfo();
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
