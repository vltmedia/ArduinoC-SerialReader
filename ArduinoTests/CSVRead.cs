using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ArduinoTests
{
    public class CSVRead
    {


        public List<string> CSVConvert(string path)
        {
            using (StreamReader sr = new StreamReader(@path))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                List<string> ss = new List<string>();
                string[] parts = line.Split('\n');
                foreach (string value in parts)
                {
                    ss.Add(value);
                }




                return ss;
            }
           

        }

        public void Splitcom(string strings)
        {
            List<string> ss = new List<string>();


            // Split the cvs on a comma.
            string[] parts = strings.Split(',');
            foreach (string value in parts)
            {
                ss.Add(value);
            }
        }
        public List<string> Splitcom2(string strings)
        {
            List<string> ss = new List<string>();

            // Split the cvs on a comma.
            string[] parts = strings.Split(',');
            foreach (string value in parts)
            {
                ss.Add(value);
            }
            return ss;
        }
    }
}
