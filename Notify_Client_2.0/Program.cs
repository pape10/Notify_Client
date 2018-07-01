using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
namespace Notify_Client_2._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            try
            {
                string path = "c:\\Users\\Public\\last_events.txt";
                if (!File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    string s = "2017-06-28 01:01:53.523";
                    sw.WriteLine(Convert.ToDateTime(s));
                    sw.Flush();
                    sw.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("something went wrong in creating the file");
            }
        }
    }
}
