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
        public List<string> MainMenuu = new List<string>()
        {
           "one",
       "two",
"three",
"four",
"five",
"six",
"seven",
"eight",
"nine",
"ten",



        };
        //public void Update()
        //{


        //}
        public float lastnum = 0;
        public float currectnum = 0;
        public float currectnumMainmenu = 0;
        public float currectMath = 0;
        public bool Runningis = false;
        //public SerialPort SPMain;

            public void Pageup()
        {
            //string l= lastnum.ToString();

           
            float t = currectnumMainmenu + 1;
            if (t > 9)
            {
                t = 0;

            }
                currectnumMainmenu = t;
            int ff = (int)t;
            textBox1.Invoke(new Action(() => textBox1.Text = MainMenuu[ff]));
            Thread.Sleep(8);
            //label1.Text = MainMenuu[ff];
            //label1.Refresh();



            //textBox1.Invoke(new Action(() => textBox1.Text = "PAGE UP: " + l));

            //label1.Text = "PAGE UP: " + l;

        }
        public void Pageup2()
        {
            //string l= lastnum.ToString();
            Point p = Cursor.Position;
            int ss = p.X + 3;
            Cursor.Position = new Point(ss, p.Y);
            string sss = "B+";
            //int ff = (int)t;
            textBox1.Invoke(new Action(() => textBox1.Text = sss));
            //Thread.Sleep(8);
            //label1.Text = MainMenuu[ff];
            //label1.Refresh();



            //textBox1.Invoke(new Action(() => textBox1.Text = "PAGE UP: " + l));

            //label1.Text = "PAGE UP: " + l;

        }
        public void PageDown()
        {

            float t = currectnumMainmenu - 1;
            if (t < 0)
            {
                t = 9;

            }
            currectnumMainmenu = t;
            int ff = (int)t;
            textBox1.Invoke(new Action(() => textBox1.Text = MainMenuu[ff]));
            Thread.Sleep(8);
        }

        public void PageDown2()
        {

            //string l= lastnum.ToString();
            Point p = Cursor.Position;
            int ss = p.X - 1;
            Cursor.Position = new Point(ss, p.Y);
            string sss = "B-";
            //int ff = (int)t;
            textBox1.Invoke(new Action(() => textBox1.Text = sss));
            Thread.Sleep(8);
            //label1.Text = MainMenuu[ff];
            //label1.Refresh();



            //textBox1.Invoke(new Action(() => textBox1.Text = "PAGE UP: " + l));

            //label1.Text = "PAGE UP: " + l;

        }
        public void SetLastnum(float num)
        {
            Console.WriteLine("This is Lastnum : " + num);
            if (num < lastnum)
            {
                lastnum = num;
                PageDown();

            }

            if (num > lastnum)
            {
                lastnum = num;
                Pageup();

            }

        }


        public void StartServer()
        {
            Console.WriteLine("0");

            //string cb = comboBox1.SelectedItem.ToString();
            //Console.WriteLine(comboBox1.SelectedItem);

            _serialPort = new SerialPort();
            //Console.WriteLine("1");

            _serialPort.PortName = "COM6";//Set your board COM
            //Console.WriteLine("2");

            _serialPort.BaudRate = 9600;
            //Console.WriteLine("3");


            _serialPort.Open();
            //Console.WriteLine("4");
            Runningis = true;
            while (Runningis == true)
            {
                string a = _serialPort.ReadExisting();

                //string f = textBox1.Text + a + "\n";
                string f = a;
                Console.WriteLine(f);
                try
                {

                    float n = float.Parse(f);
                    SetLastnum(n);

                    //textBox1.Invoke(new Action(() => textBox1.Text = f));



                }
                catch
                {

                    Console.WriteLine("Couldn't parse float");


                }
                Console.WriteLine("f is : " + f);
                

                if (f.Contains("PRESSED A"))
                {
                    MoveMouse();
                    textBox1.Invoke(new Action(() => textBox1.Text = f));


                }
                if (f.Contains("B +"))
                {
                    Pageup2();


                }
                if (f.Contains("B -"))
                {
                    PageDown2();


                }
                //Console.WriteLine("5aa");
                //textBox1.Text = f;
                //Console.WriteLine("5b");

                //textBox1.Refresh();
                //Console.WriteLine("6");

                Thread.Sleep(20);
                //Console.WriteLine("7");

            }



        }

      

        List<string> Loadedpos = new List<string>();

        List<string> xpos = new List<string>();

        List<string> ypos = new List<string>();

        public void Convertcsv()
        {

            CSVRead cs = new CSVRead();
            Loadedpos = cs.CSVConvert(@"C:\Users\Justin Jaro\Documents\VLTMedia\Temp\WriteLines.csv");
foreach(string s in Loadedpos)
            {
                Console.WriteLine(s);
            }

        }

        public void MoveMouse()
        {
            int t = (int)currectnumMainmenu + 1;

            string p = Loadedpos[t];

            string[] parts = p.Split(',');
            float XX = float.Parse(parts[0]);
            float YY = float.Parse(parts[0]);
            int xxx = (int)XX;
            int yyy = (int)YY;

            Cursor.Position = new Point(xxx, yyy);


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

        private void button3_Click(object sender, EventArgs e)
        {
            _serialPort.Write("Message");
            Console.WriteLine("Message Sent");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MouseKeysHook mh = new MouseKeysHook();
            mh.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Convertcsv();
            Console.WriteLine(Loadedpos.Count());

            
        }
    }
}
