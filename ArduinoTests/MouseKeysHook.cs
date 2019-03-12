using EventHook;
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

namespace ArduinoTests
{
    public partial class MouseKeysHook : Form
    {
        private readonly ApplicationWatcher applicationWatcher;
        private readonly ClipboardWatcher clipboardWatcher;
        private readonly EventHookFactory eventHookFactory = new EventHookFactory();
        private readonly KeyboardWatcher keyboardWatcher;
        private readonly MouseWatcher mouseWatcher;
        private readonly PrintWatcher printWatcher;

        public List<int> Positionsx = new List<int>();
        public List<int> Positionsy = new List<int>();

        public MouseKeysHook()
        {
            Application.ApplicationExit += OnApplicationExit;

            InitializeComponent();

            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
            //keyboardWatcher.Start();
            keyboardWatcher.OnKeyInput += (s, e) =>
            {
                Console.WriteLine("Key {0} event of key {1}", e.KeyData.EventType, e.KeyData.Keyname);
            };

            mouseWatcher = eventHookFactory.GetMouseWatcher();
            //mouseWatcher.Start();
            mouseWatcher.OnMouseInput += (s, e) =>
            {
                Console.WriteLine("Mouse event {0} at point {1},{2}", e.Message.ToString(), e.Point.x, e.Point.y);
                CheckMouseClick(e.Message.ToString(), e.Point.x, e.Point.y);
            };

            clipboardWatcher = eventHookFactory.GetClipboardWatcher();
            clipboardWatcher.Start();
            clipboardWatcher.OnClipboardModified += (s, e) =>
            {
                Console.WriteLine("Clipboard updated with data '{0}' of format {1}", e.Data,
                    e.DataFormat.ToString());
            };


            applicationWatcher = eventHookFactory.GetApplicationWatcher();
            applicationWatcher.Start();
            applicationWatcher.OnApplicationWindowChange += (s, e) =>
            {
                Console.WriteLine("Application window of '{0}' with the title '{1}' was {2}",
                    e.ApplicationData.AppName, e.ApplicationData.AppTitle, e.Event);
            };

            printWatcher = eventHookFactory.GetPrintWatcher();
            printWatcher.Start();
            printWatcher.OnPrintEvent += (s, e) =>
            {
                Console.WriteLine("Printer '{0}' currently printing {1} pages.", e.EventData.PrinterName,
                    e.EventData.Pages);
            };
        }

        public void Appon()
        {
            keyboardWatcher.Start();
            mouseWatcher.Start();




        }
             public void Appoff()
        {
            keyboardWatcher.Stop();
            mouseWatcher.Stop();




        }


        public void CheckMouseClick(string Mess, int x, int y)
        {
            string s = Mess;
            if (s.Contains("LBUTTONDOWN"))
            {

                Positionsx.Add(x);
                Positionsy.Add(y);
                Console.WriteLine("GOT BUTTON DOWN!");

            }
           
        }

        public string BigString = "";
        public void SavePoints()
        {
            BigString = "";

            for(int i = 0; i < Positionsx.Count; i ++)
            {

                string fix = Positionsx[i].ToString() + "," + Positionsy[i].ToString();
                string s = BigString + Environment.NewLine + fix;
                BigString = s;
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(@"C:\Users\Justin Jaro\Documents\VLTMedia\Temp", "WriteLines.csv")))
            {

                outputFile.WriteLine(BigString);
            }

        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Appon();
            this.Opacity = 0.75;
            GoFullscreen(true);
        }
        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
        private void MouseKeysHook_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardWatcher.Stop();
            mouseWatcher.Stop();
            clipboardWatcher.Stop();
            applicationWatcher.Stop();
            printWatcher.Stop();

            eventHookFactory.Dispose();
        }
        public void ShowPoints()
        {

            foreach(int i in Positionsx)
            {
                Console.WriteLine(i);
            }

        }

        public void MoveMouse()
        {

            Cursor.Position = new Point(31, 1414);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Appoff();
            this.Opacity = 1;
            GoFullscreen(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SavePoints();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MoveMouse();
        }
    }
}
