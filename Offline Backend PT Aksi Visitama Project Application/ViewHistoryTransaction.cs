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
    public partial class ViewHistoryTransaction : Form
    {
        Connect con = new Connect();
        public ViewHistoryTransaction()
        {
            InitializeComponent();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;

            DataTable dtTopUp = new DataTable();
            DataTable dtCekTada = new DataTable();
            DataTable dtSaldoAwal = new DataTable();
            DataTable dtOrderVoucher = new DataTable();
            DataTable dtRedeemCash = new DataTable();
            DataTable dtDonate = new DataTable();
            DataTable dtOrderItem = new DataTable();

            dtSaldoAwal = con.getTabel("SELECT g.NominalGeneration FROM Tada t, Generation g WHERE t.GenerationID=g.GenerationID AND t.TadaNo='"+textBox1.Text+"'");
            dtCekTada = con.getTabel("SELECT * FROM Tada WHERE TadaNo='"+textBox1.Text+"'");
            dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo='"+textBox1.Text+"'");
            dtOrderVoucher = con.getTabel("SELECT dov.*, ov.tanggal FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='"+textBox1.Text+"' AND ov.StatusOrderVoucher='Success'");
            dtRedeemCash = con.getTabel("SELECT drc.*, rc.tanggal FROM DetailRedeemCash drc, RedeemCash rc WHERE drc.RedeemNo = rc.RedeemNo AND drc.TadaNo='"+textBox1.Text+"' AND rc.StatusRedeemCash ='Success'");
            dtDonate = con.getTabel("SELECT dd.*, d.tanggal FROM DetailDonate dd, Donate d WHERE d.RedeemNo = dd.RedeemNo AND dd.TadaNo = '"+textBox1.Text+"' AND d.StatusDonate='Success'");
            dtOrderItem = con.getTabel("SELECT doi.* , oi.tanggal FROM DetailOrderItems doi, OrderItem oi WHERE doi.NoOrder = oi.NoOrder AND doi.TadaNo = '"+textBox1.Text+"' AND oi.StatusOrderItem = 'Success'");

            if(textBox1.Text ==""){
                MessageBox.Show("Tada No must be filled","Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
            }
            else if (dtCekTada.Rows.Count == 0)
            {
                MessageBox.Show("Tada No not found","Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
            }
            else {
                int totalTopUp = 0;
                int totalOrderRedeem = 0;
                textBox2.Text = dtSaldoAwal.Rows[0][0].ToString();
                for (int i = 0; i < dtTopUp.Rows.Count;i++ )
                {
                    dataGridView1.Rows.Add(dtTopUp.Rows[i][0].ToString(),dtTopUp.Rows[i][1].ToString(),dtTopUp.Rows[i][2].ToString());
                    totalTopUp += Convert.ToInt32(dtTopUp.Rows[i][1].ToString());
                }
                textBox3.Text = totalTopUp.ToString();

                for (int i = 0; i < dtOrderVoucher.Rows.Count;i++ )
                {
                    dataGridView2.Rows.Add(dtOrderVoucher.Rows[i][1].ToString(), dtOrderVoucher.Rows[i][2].ToString(), "Voucher", dtOrderVoucher.Rows[i][3].ToString());
                }
                for (int i = 0; i < dtRedeemCash.Rows.Count;i++ )
                {
                    dataGridView2.Rows.Add(dtRedeemCash.Rows[i][1].ToString(), dtRedeemCash.Rows[i][2].ToString(), "Cash", dtRedeemCash.Rows[i][3].ToString());
                }
                for (int i = 0; i < dtDonate.Rows.Count;i++ )
                {
                    dataGridView2.Rows.Add(dtDonate.Rows[i][1].ToString(),dtDonate.Rows[i][2].ToString(),"Donation",dtDonate.Rows[i][3].ToString());
                }
                for (int i = 0; i < dtOrderItem.Rows.Count;i++ )
                {
                    dataGridView2.Rows.Add(dtOrderItem.Rows[i][1].ToString(),dtOrderItem.Rows[i][2].ToString(),"Item",dtOrderItem.Rows[i][3].ToString());
                }
                for (int i = 0; i < dataGridView2.Rows.Count;i++ )
                {
                    totalOrderRedeem += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value.ToString());
                }
                textBox4.Text = totalOrderRedeem.ToString();
                textBox5.Text = (Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox4.Text)).ToString();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView2.CurrentRow.Cells[2].Value.ToString()=="Voucher"){
                dataGridView3.DataSource = con.getTabel("SELECT * FROM OrderVoucher WHERE NoOrder='"+dataGridView2.CurrentRow.Cells[0].Value.ToString()+"'");
                dataGridView4.DataSource = con.getTabel("SELECT * FROM DetailOrderVoucher WHERE NoOrder='"+dataGridView2.CurrentRow.Cells[0].Value.ToString()+"'");
            }
            else if (dataGridView2.CurrentRow.Cells[2].Value.ToString() == "Cash")
            {
                dataGridView3.DataSource = con.getTabel("SELECT * FROM RedeemCash WHERE RedeemNo='"+dataGridView2.CurrentRow.Cells[0].Value.ToString()+"'");
                dataGridView4.DataSource = con.getTabel("SELECT * FROM DetailRedeemCash WHERE RedeemNo='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'");
            }
            else if (dataGridView2.CurrentRow.Cells[2].Value.ToString() == "Donation")
            {
                dataGridView3.DataSource = con.getTabel("SELECT * FROM Donate WHERE RedeemNo='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'");
                dataGridView4.DataSource = con.getTabel("SELECT * FROM DetailDonate WHERE RedeemNo='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'");
            }
            else if (dataGridView2.CurrentRow.Cells[2].Value.ToString() == "Item")
            {
                dataGridView3.DataSource = con.getTabel("SELECT * FROM OrderItem WHERE NoOrder='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'");
                dataGridView4.DataSource = con.getTabel("SELECT * FROM DetailOrderItems WHERE NoOrder='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'");
            }
        }
    }
}
