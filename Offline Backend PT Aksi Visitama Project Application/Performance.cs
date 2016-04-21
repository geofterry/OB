using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Offline_Backend_PT_Aksi_Visitama_Project_Application
{
    public partial class Performance : Form
    {
        Connect con = new Connect();
        public Performance()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd-MM-yyyy HH:mm:ss";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("File name must be filled");
            }
            else
            {
                chart1.SaveImage("C:\\Users\\AT\\Desktop\\Chart Image from OB\\" + textBox1.Text.ToString() + ".png", ChartImageFormat.Png);
                MessageBox.Show("Save Image Success!");
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add("VOUCHERS \n" + dateTimePicker1.Value.ToString("dd-MM-yyyy HH:mm:ss") + " s/d " + dateTimePicker2.Value.ToString("dd-MM-yyyy HH:mm:ss"));
            DataTable temp = new DataTable();
            DataTable temp2 = new DataTable();
            List<string> namaVoucher = new List<string>();
            //List<string> pecahan = new List<string> ();
            List<int> nomor = new List<int>();
            temp = con.getTabel("SELECT DISTINCT NamaVoucher FROM OrderVoucher WHERE Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-hh HH:mm:ss") + "'");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                namaVoucher.Add(temp.Rows[i][0].ToString());
                //pecahan.Add(temp.Rows[i][1].ToString());
            }

            for (int i = 0; i < namaVoucher.Count; i++)
            {
                temp2 = con.getTabel("SELECT SUM(Jumlah) [jumlah] FROM OrderVoucher WHERE NamaVoucher='" + namaVoucher[i] + "' AND (Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-hh HH:mm:ss") + "')");
                nomor.Add(Convert.ToInt32(temp2.Rows[0][0].ToString()));
                //MessageBox.Show(temp2.Rows[0][0].ToString());
                //Series series = chart1.Series.Add(namaVoucher[i]);
                //series.Points.Add(nomor[i]);
            }
            for (int i = 0; i < namaVoucher.Count; i++)
            {
                dataGridView1.Rows.Add(namaVoucher[i],nomor[i]);
                Series series = chart1.Series.Add(namaVoucher[i]);
                series.ShadowOffset = 1;
                series.Points.Add(nomor[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
