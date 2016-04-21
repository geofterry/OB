using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Offline_Backend_PT_Aksi_Visitama_Project_Application
{
    public partial class LoginForm : Form
    {
        Connect con = new Connect();
        DataTable dt = new DataTable();
        DataTable dtpass = new DataTable();
        MDIform mdi;
        //string today = DateTime.Today.ToString("dd-MM-yyyy");
        //DateTime time = DateTime.Now;              // Use current time
	    //string format = "dd-MM-yyyy hh:mm";    // Use this format
        //string stringToday = "";
	    //Console.WriteLine(time.ToString(format));

        //string testDate = "07-August-2014 10:59";
        //List<string> datedate = new List<string>();
        //List<string> redeem = new List<string>();
        //List<DateTime> date = new List<DateTime>();
        //List<int> angka = new List<int>();

        public LoginForm()
        {
            InitializeComponent();
            
            //datedate.Add("11-August-2014 15:30");
            //datedate.Add("11-August-2014 11:22");
            //datedate.Add("29-July-2014 23:05");
            //datedate.Add("24-July-2014 15:51");
            
            /*for (int i = 0; i < datedate.Count; i++)
            {
                string[] dateTemp = datedate[i].Split(' ');
                string[] dateTempPart = dateTemp[0].Split('-');
                string[] hourTempPart = dateTemp[1].Split(':');
                if(dateTempPart[1]=="August"){
                    dateTempPart[1] = "8";
                }
                else if(dateTempPart[1]=="July"){
                    dateTempPart[1] = "7";
                }
                DateTime dtime = new DateTime(Convert.ToInt32(dateTempPart[2]),Convert.ToInt32(dateTempPart[1]),Convert.ToInt32(dateTempPart[0]),Convert.ToInt32(hourTempPart[0]),Convert.ToInt32(hourTempPart[1]),0);
                date.Add(dtime);
                //MessageBox.Show(dateTemp.ToString());
            }*/

            /*angka.Add(4);
            angka.Add(2);
            angka.Add(1);
            angka.Add(3);
            
            redeem.Add("Voucher");
            redeem.Add("Cash");
            redeem.Add("Donation");
            redeem.Add("Item");
            

            for (int i = 0; i < redeem.Count;i++ )
            {
                for (int j = redeem.Count - 1; j > i; j-- )
                {
                    int result = DateTime.Compare(date[j-1],date[j]);
                    //MessageBox.Show(date[j-1].ToString());
                    if(result>0){
                        //DateTime swap = date[j - 1];
                        DateTime dateswap = date[j - 1];
                        date[j - 1] = date[j];
                        date[j] = dateswap;

                        string swapp = redeem[j - 1];
                        redeem[j - 1] = redeem[j];
                        redeem[j] = swapp;

                        string swap = datedate[j - 1];
                        datedate[j - 1] = datedate[j];
                        datedate[j] = swap;
                    }
                }
            }
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            for (int i = 0; i < datedate.Count;i++ )
            {
                dataGridView1.Rows.Add(datedate[i],redeem[i]);
            }

            stringToday = time.ToString(format);*/
            //string [] dateTemp = testDate.Split(' ');
            
            //string[] dateTempPart = dateTemp[0].Split('-');
            //string[] hourTempPart = dateTemp[1].Split(':');
            //if(dateTempPart[1] == "August"){
            //    dateTempPart[1] = "8";
            //}
            //DateTime date1 = new DateTime(Convert.ToInt32(dateTempPart[2]), Convert.ToInt32(dateTempPart[1]), Convert.ToInt32(dateTempPart[0]));
            //int result = DateTime.Compare(time,date1);
            //string relationship;

            //if (result < 0)
            //    relationship = "is earlier than";
            //else if (result == 0)
            //    relationship = "is the same time as";
            //else
            //    relationship = "is later than";
            
            //textBox1.Text = relationship;

            textBox2.Text = "";
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox1.Text==""){
                MessageBox.Show("Username harus diisi", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Password harus diisi", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                dt = con.getTabel("select * from employee where username = '"+textBox1.Text+"'");
                if(dt.Rows.Count==0){
                    MessageBox.Show("Username tidak tersedia", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    dtpass = con.getTabel("SELECT * FROM Employee WHERE Username='" + textBox1.Text + "' AND EmployeePassword = '" + textBox2.Text + "'");
                    if (dtpass.Rows.Count != 0)
                    {
                        MessageBox.Show("Login Success\nWelcome " + dtpass.Rows[0][1].ToString() + "!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MDIform.role = dtpass.Rows[0][3].ToString();
                        MDIform.nameLogin = dtpass.Rows[0][1].ToString();
                        MDIform.username = textBox1.Text;
                        this.Visible = false;
                        mdi = new MDIform();
                        mdi.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Password Salah", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            
        }
    }
}
