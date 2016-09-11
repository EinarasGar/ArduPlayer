using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.IO.Ports;
using MetroFramework.Components;

namespace ArduPlayeris
{
    class SCom
    {

        public SCom(MetroComboBox BaudRate, MetroComboBox Port)
        {
            string[] serialPorts = System.IO.Ports.SerialPort.GetPortNames();
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

            SerialPort port = new SerialPort();
            port.PortName = Port.SelectedItem.ToString();
            port.BaudRate = Convert.ToInt32(BaudRate.SelectedItem.ToString());

       
        }
    }
}
