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
    public partial class DeleteTopUp : Form
    {
        Connect con = new Connect();
        DataTable dt;
        public DeleteTopUp()
        {
            InitializeComponent();
            label3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Tada No harus diisi");
            }
            else {
                dt = con.getTabel("SELECT topupid, TanggalTopUp, JumlahTopUp FROM TopUp WHERE TadaNo = '" + textBox1.Text + "'");
                dataGridView1.DataSource = dt;
            } 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label3.Text == "")
            {
                MessageBox.Show("Pilih Top Up ID yang akan didelete");
            }
            else {
                string jumlahTopUp = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                int jumlahTopUpLength = jumlahTopUp.Length;
                int digitAwal = jumlahTopUpLength % 3;
                int jumlahLoop = (jumlahTopUpLength - digitAwal) / 3;
                string jumlahTopUpHasil = jumlahTopUp.Substring(0, digitAwal);
                for (int i = 0; i < jumlahLoop; i++)
                {
                    if (digitAwal == 0)
                    {
                        jumlahTopUpHasil = jumlahTopUp.Substring(digitAwal, 3);
                    }
                    else
                    {
                        jumlahTopUpHasil = jumlahTopUpHasil + "." + jumlahTopUp.Substring(digitAwal, 3);
                    }
                    digitAwal += 3;
                }
                DialogResult result1 = MessageBox.Show("Are you sure want to Delete ? \n Tada No \t \t:   "+textBox1.Text+"\n Jumlah Top Up \t:   Rp "+jumlahTopUpHasil+"", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result1 == DialogResult.Yes)
                {
                    DataTable dt2 = new DataTable();
                    dt2 = con.getTabel("SELECT NominalTada FROM Tada WHERE TadaNo = '"+textBox1.Text+"'");
                    int temp = Convert.ToInt32(dt2.Rows[0][0].ToString())-Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    con.executeUpdate("UPDATE Tada Set NominalTada='"+temp.ToString()+"' WHERE TadaNo='"+textBox1.Text+"'");
                    con.executeUpdate("DELETE FROM TopUp WHERE TopUpID = '"+label3.Text+"'");
                    MessageBox.Show("Delete Success!");
                    dataGridView1.DataSource = null;
                    textBox1.Text = "";
                    label3.Text = "";
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
