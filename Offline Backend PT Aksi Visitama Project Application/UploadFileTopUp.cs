using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Offline_Backend_PT_Aksi_Visitama_Project_Application
{
    public partial class UploadFileTopUp : Form
    {
        Connect con = new Connect();

        string topupid = "";
        public UploadFileTopUp()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            init();
        }

        public void init()
        {
            dataGridView1.DataSource = null;
            dataGridView2.Rows.Clear();

            textBox1.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pathConn = "";
            //validasi jika belum ada file yg d pilih
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please choose the file.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //validasi jika format file yang dimasukan salah
            else if (!textBox1.Text.EndsWith(".xls") == true && !textBox1.Text.EndsWith(".xlsx") == true)
            {
                MessageBox.Show("Wrong file format, The file format must be .xls or .xlsx", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
            else
            {
                try
                {
                    if (textBox1.Text.EndsWith(".xls") == true)
                    {
                        pathConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + textBox1.Text + "; Extended Properties=\"Excel 8.0;HDR=YES\"";
                        //OleDbConnection conn = new OleDbConnection(pathConn);
                    }
                    else if (textBox1.Text.EndsWith(".xlsx") == true)
                    {
                        pathConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                    }
                    OleDbConnection conn = new OleDbConnection(pathConn);
                    OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [Worksheet$C3:D1003] where [No Kartu] IS NOT NULL OR [Rp# Top Up] IS NOT NULL", conn);
                    DataTable dt = new DataTable();
                    myDataAdapter.Fill(dt);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (dt.Rows[i][2].ToString() != "")
                    //    {
                    //        dataGridView1.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString());
                    //    }
                    //}
                    dataGridView1.DataSource = dt;

                    //pindahin ke datagridview2 data merge kosong diisi
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (i != 0 && dataGridView1.Rows[i].Cells[0].Value.ToString() == "")
                        {
                            //dataGridView2.Rows.Add((DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString())).ToShortDateString(), dataGridView1.Rows[i - 1].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString());
                            dataGridView2.Rows.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString());
                        }
                        else
                        {
                            //dataGridView2.Rows.Add((DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString())).ToShortDateString(), dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString());
                            dataGridView2.Rows.Add(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString());
                        }
                    }
                    //Cek No tada Ada atau tidak di database jika ada masuk datagridview3, jika tidak ada masuk datagridview4
                    DataTable dtCekTada = new DataTable();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        dtCekTada = con.getTabel("SELECT * FROM Tada WHERE TadaNo = '" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "'");
                        if (dtCekTada.Rows.Count == 0)
                        {
                            dataGridView4.Rows.Add(dataGridView2.Rows[i].Cells[0].Value.ToString(), dataGridView2.Rows[i].Cells[1].Value.ToString());
                        }
                        else
                        {
                            dataGridView3.Rows.Add(dataGridView2.Rows[i].Cells[0].Value.ToString(), dataGridView2.Rows[i].Cells[1].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void generate()
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = con.getTabel("SELECT TopUpID FROM TopUp order by TopUpID desc");
                string code = dt1.Rows[0][0].ToString();
                int numericCode = Int32.Parse(code.Substring(2)) + 1;
                if (numericCode < 10) topupid = "TU0000" + numericCode;
                else if (numericCode < 100) topupid = "TU000" + numericCode;
                else if (numericCode < 1000) topupid = "TU00" + numericCode;
                else if (numericCode < 10000) topupid = "TU0" + numericCode;
                else topupid = "TU" + numericCode;
            }
            catch (Exception ee)
            {
                topupid = "TU00001";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int tempNominal = 0;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                generate();
                DataTable dtTemp = con.getTabel("SELECT NominalTada From TADA WHERE TadaNo='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'");
                tempNominal = int.Parse(dtTemp.Rows[0][0].ToString()) + int.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString());
                con.executeUpdate("INSERT INTO TOPUP VALUES('" + topupid + "','" + dataGridView3.Rows[i].Cells[1].Value.ToString() + "','" + DateTime.Now + "','" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "','" + MDIform.username + "')");
                con.executeUpdate("UPDATE Tada SET NominalTada = '" + tempNominal + "' WHERE TadaNo='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'");
            }
            dataGridView3.Rows.Clear();
            MessageBox.Show("Upload Success");
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }

            if (dataGridView4.Rows.Count > 0)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int flag = 0;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                DataTable dtCekRepair = con.getTabel("SELECT * FROM Tada WHERE TadaNo='" + dataGridView4.Rows[i].Cells[1].Value.ToString() + "'");
                if (dtCekRepair.Rows.Count == 0)
                {
                    flag++;
                }
            }
            if (flag != 0)
            {
                MessageBox.Show("Masih terdapat Tada yang belum terdaftar!");
            }
            else
            {
                int tempNominal = 0;
                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    generate();
                    DataTable dtTemp = con.getTabel("SELECT NominalTada From TADA WHERE TadaNo='" + dataGridView4.Rows[i].Cells[0].Value.ToString() + "'");
                    tempNominal = int.Parse(dtTemp.Rows[0][0].ToString()) + int.Parse(dataGridView4.Rows[i].Cells[1].Value.ToString());
                    con.executeUpdate("INSERT INTO TOPUP VALUES('" + topupid + "','" + dataGridView4.Rows[i].Cells[1].Value.ToString() + "','" + DateTime.Now + "','" + dataGridView4.Rows[i].Cells[0].Value.ToString() + "','" + MDIform.username + "')");
                    con.executeUpdate("UPDATE Tada SET NominalTada = '" + tempNominal + "' WHERE TadaNo='" + dataGridView4.Rows[i].Cells[0].Value.ToString() + "'");
                }
                dataGridView4.Rows.Clear();
                MessageBox.Show("Repair Success");
                init();
            }
        }
    }
}
