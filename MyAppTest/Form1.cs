 using System;
using System.Windows.Forms;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using System.IO.Ports;

namespace MyAppTest
{
    public partial class Form1 : Form
    {
        Boolean isConnected;
        private SerialPort port;
        
        ArduinoSketchUploader uploader = new ArduinoSketchUploader(
            new ArduinoSketchUploaderOptions()
            {
                FileName = @"C:\Arduino\123\123.ino.eightanaloginputs.hex",
                PortName = "COM3",
                ArduinoModel = ArduinoModel.NanoR3
            });
        
        ArduinoSketchUploader uploader2 = new ArduinoSketchUploader(
            new ArduinoSketchUploaderOptions()
            {
                FileName = @"C:\Arduino\12\12.ino.eightanaloginputs.hex",
                PortName = "COM3",
                ArduinoModel = ArduinoModel.NanoR3
            });
        
        public Form1()
        {
            port = new SerialPort("com3", 9600);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uploader.UploadSketch();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uploader2.UploadSketch();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0)
            {
                MessageBox.Show("com Port not found");
            }

            foreach (string x in ports)
            {
                comboBox1.Items.Add(x);
                Console.WriteLine(ports.Length);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }

        private void ConnectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port.PortName = selectedPort;
            port.Open();
            ConnectButton.Text = "disconnect";
        }

        private void disconnectToArduino()
        {
            isConnected = false;
            port.Close();
            ConnectButton.Text = "connect";
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                ConnectToArduino();
            }
            else
            {
                disconnectToArduino();
            }
        }
    }
}