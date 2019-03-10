using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace ArduinoTests
{
    public partial class Form1 : Form
    {
        static SerialPort _serialPort;
        public string[] ports;
        public Form1()
        {
            
            InitializeComponent();
            GetPorts();
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //this.backgroundWorker1.CancelAsync += new DoWorkEventHandler(backgroundWorker1_DoWork);

            //this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //this.backgroundWorker1.WorkerReportsProgress = true;
        }

        //public void Update()
        //{


        //}

        public bool Runningis = false;
        public void StartServer()
        {
            Console.WriteLine("0");

            //string cb = comboBox1.SelectedItem.ToString();
            //Console.WriteLine(comboBox1.SelectedItem);

            _serialPort = new SerialPort();
            Console.WriteLine("1");

            _serialPort.PortName = "COM7";//Set your board COM
            Console.WriteLine("2");

            _serialPort.BaudRate = 9600;
            Console.WriteLine("3");


            _serialPort.Open();
            Console.WriteLine("4");
            Runningis = true;
            while (Runningis == true)
            {
                string a = _serialPort.ReadExisting();
                Console.WriteLine(Runningis);

                //string f = textBox1.Text + a + "\n";
                string f = a;
                Console.WriteLine("5aa");
                textBox1.Invoke(new Action(() => textBox1.Text = f));
                //textBox1.Text = f;
                Console.WriteLine("5b");

                //textBox1.Refresh();
                //Console.WriteLine("6");

                Thread.Sleep(100);
                //Console.WriteLine("7");

            }



        }

        private void input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.X)
            {
                // Your logic here....
                e.Handled = true; //Handle the Keypress event (suppress the Beep)
                Console.WriteLine("POOP");
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((this.ActiveControl == this) && (keyData == Keys.C))
            {
                Console.WriteLine("POOP");

                //do something
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void GetPorts()
        {

            ports = SerialPort.GetPortNames();

            foreach(string p in ports)
            {
                comboBox1.Items.Add(p);
                //Console.WriteLine(p);
                if (ports[0] != null)
                {

                    comboBox1.SelectedItem = ports[0];
                }


            }

        }

        public void ClosePorts()
        {

            _serialPort.Close();

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending)
            {
                Runningis = false;
                e.Cancel = true;
                //e.Cancel = true;
            }
            else
            {
                Console.WriteLine("BG Worker started");
                StartServer();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Start BackgroundWorker
            backgroundWorker1.RunWorkerAsync();
            //StartServer();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Runningis = false;
            ClosePorts();
            backgroundWorker1.CancelAsync();
            MessageBox.Show("DISCONNECTED!");

        }
    }
}
