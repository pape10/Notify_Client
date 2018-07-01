using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
namespace Notify_Client_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string path = "c:\\Users\\Public\\last_events.txt";
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(0.2);
            try
            {
                SqlConnection sqlcon = new SqlConnection(@"Data Source=mssql5.gear.host;Initial Catalog=events3;User ID=events3;Password=Ev067I!~zyZy");
                sqlcon.Open();
                var timer = new System.Threading.Timer((e1) =>
                {
                    StreamReader r = new StreamReader(path);
                    DateTime m = new DateTime();
                    while (r.EndOfStream == false)
                    {
                        m = Convert.ToDateTime(r.ReadLine());
                    }
                    r.Close();

                    string query = "select top 1 * from Events2  order by Upload_Time desc";
                    SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);
                    DateTime t2 = Convert.ToDateTime(dtbl.Rows[0][1].ToString());
                    DateTime t1 = Convert.ToDateTime(m);
                    if (dtbl.Rows.Count == 1)
                    {
                    // MessageBox.Show(dtbl.Rows[0][1].ToString()+m.ToString());
                    if (t1 < t2)
                        {
                            notifyIcon1.Visible = true;
                        //notifyIcon1.Icon = 
                        notifyIcon1.ShowBalloonTip(1000, "Important Notice", "Something has come up", ToolTipIcon.Info);
                            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);

                            sw.WriteLine(Convert.ToDateTime(dtbl.Rows[0][1]));
                            sw.Flush();
                            sw.Close();
                            fs.Close();
                        }
                    }
                }, null, startTimeSpan, periodTimeSpan);
                sqlcon.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong and we couldnot contact the database");
            }
        }


        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/");

        }

    }
}

