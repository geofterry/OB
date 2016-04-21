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
    public partial class UpdateTransactionForm : Form
    {
        Connect con = new Connect();
        int flag = 0;
        string topupid = "";
        public UpdateTransactionForm()
        {
            InitializeComponent();
            textBox1.Text = "";
            dataGridView1.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;

            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            dataGridView1.Enabled = true;
            flag = 0;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Tada No. must be filled");
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            }
            else {
                DataTable dtOrderVoucher = new DataTable();
                DataTable dtRedeemCash = new DataTable();
                DataTable dtDonate = new DataTable();
                DataTable dtOrderItem = new DataTable();
                dtOrderVoucher = con.getTabel("SELECT dov.*, ov.tanggal FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='" + textBox1.Text + "' AND ov.StatusOrderVoucher='Success'");
                dtRedeemCash = con.getTabel("SELECT drc.*, rc.tanggal FROM DetailRedeemCash drc, RedeemCash rc WHERE drc.RedeemNo = rc.RedeemNo AND drc.TadaNo='" + textBox1.Text + "' AND rc.StatusRedeemCash ='Success'");
                dtDonate = con.getTabel("SELECT dd.*, d.tanggal FROM DetailDonate dd, Donate d WHERE d.RedeemNo = dd.RedeemNo AND dd.TadaNo = '" + textBox1.Text + "' AND d.StatusDonate='Success'");
                dtOrderItem = con.getTabel("SELECT doi.* , oi.tanggal FROM DetailOrderItems doi, OrderItem oi WHERE doi.NoOrder = oi.NoOrder AND doi.TadaNo = '" + textBox1.Text + "' AND oi.StatusOrderItem = 'Success'");

                for (int i = 0; i < dtOrderVoucher.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dtOrderVoucher.Rows[i][1].ToString(), dtOrderVoucher.Rows[i][2].ToString(), "Voucher", dtOrderVoucher.Rows[i][3].ToString());
                }
                for (int i = 0; i < dtRedeemCash.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dtRedeemCash.Rows[i][1].ToString(), dtRedeemCash.Rows[i][2].ToString(), "Cash", dtRedeemCash.Rows[i][3].ToString());
                }
                for (int i = 0; i < dtDonate.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dtDonate.Rows[i][1].ToString(), dtDonate.Rows[i][2].ToString(), "Donation", dtDonate.Rows[i][3].ToString());
                }
                for (int i = 0; i < dtOrderItem.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dtOrderItem.Rows[i][1].ToString(), dtOrderItem.Rows[i][2].ToString(), "Item", dtOrderItem.Rows[i][3].ToString());
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Voucher")
            {
                dataGridView2.DataSource = con.getTabel("SELECT * FROM OrderVoucher WHERE NoOrder='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and statusordervoucher='Success'");
                dataGridView3.DataSource = con.getTabel("SELECT * FROM DetailOrderVoucher WHERE NoOrder='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");

                textBox2.Text = dataGridView2.Rows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView2.Rows[0].Cells[5].Value.ToString();
                textBox4.Text = dataGridView2.Rows[0].Cells[4].Value.ToString();
                //textBox5.Text = (Convert.ToInt32(dataGridView2.Rows[0].Cells[5].Value.ToString()) * Convert.ToInt32(dataGridView2.Rows[0].Cells[4].Value.ToString())).ToString();
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";

                flag = 1;

                groupBox3.Visible = true;
                groupBox4.Visible = false;
                groupBox5.Visible = false;

                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            else if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Cash")
            {
                dataGridView2.DataSource = con.getTabel("SELECT * FROM RedeemCash WHERE RedeemNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and statusredeemcash = 'success'");
                dataGridView3.DataSource = con.getTabel("SELECT * FROM DetailRedeemCash WHERE RedeemNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = dataGridView2.Rows[0].Cells[6].Value.ToString();
                textBox7.Text = dataGridView2.Rows[0].Cells[5].Value.ToString();
                textBox8.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";

                flag = 2;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
                groupBox5.Visible = false;
                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            else if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Donation")
            {
                dataGridView2.DataSource = con.getTabel("SELECT * FROM Donate WHERE RedeemNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and statusdonate='Success'");
                dataGridView3.DataSource = con.getTabel("SELECT * FROM DetailDonate WHERE RedeemNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";

                flag = 3;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            else if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "Item")
            {
                dataGridView2.DataSource = con.getTabel("SELECT * FROM OrderItem WHERE NoOrder='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and statusorderitem='Success'");
                dataGridView3.DataSource = con.getTabel("SELECT * FROM DetailOrderItems WHERE NoOrder='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
                textBox10.Text = dataGridView2.Rows[0].Cells[4].Value.ToString();
                textBox11.Text = dataGridView2.Rows[0].Cells[5].Value.ToString();
                textBox12.Text = "";
                textBox13.Text = "";
                flag = 4;

                groupBox3.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = true;

                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(flag==0){
                MessageBox.Show("You must choose the redeem");
            }
            else if(flag==1){
                dataGridView1.Enabled = false;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = true;
                textBox13.Enabled = true;

                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = false;
            }
            else if(flag==2){
                dataGridView1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;

                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = false;
            }
            else if (flag == 3)
            {
                dataGridView1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;

                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = false;
            }
            else if(flag==4){
                dataGridView1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = false;
                textBox13.Enabled = false;

                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flag = 0;
            dataGridView1.Enabled = true;
            
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;

            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";

            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
        }

        private Boolean cekAngka(string a)
        {
            try
            {
                int.Parse(a);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(flag==1){
                if(textBox2.Text == ""){
                    MessageBox.Show("Voucher must be filled");
                }
                else if(textBox3.Text ==""){
                    MessageBox.Show("Nominal harus diisi");
                }
                else if(cekAngka(textBox3.Text)==false){
                    MessageBox.Show("Nominal harus angka");
                }
                else if(textBox4.Text==""){
                    MessageBox.Show("Jumlah harus diisi");
                }
                else if (cekAngka(textBox4.Text) == false)
                {
                    MessageBox.Show("Jumlah harus angka");
                }
                else if ((Convert.ToInt32(dataGridView2.Rows[0].Cells[5].Value.ToString()) * Convert.ToInt32(dataGridView2.Rows[0].Cells[4].Value.ToString())).ToString() != (Convert.ToInt32(textBox3.Text) * Convert.ToInt32(textBox4.Text)).ToString())
                {
                    MessageBox.Show("Total tidak boleh berbeda dengan total sebelumnya");
                }
                else {
                    con.executeUpdate("UPDATE OrderVoucher SET NamaVoucher='"+textBox2.Text+"', Pecahan='"+textBox3.Text+"', Jumlah='"+textBox4.Text+"' WHERE NoOrder='"+dataGridView2.Rows[0].Cells[0].Value.ToString()+"'");
                    MessageBox.Show("Update Success!!");
                    flag = 0;
                    dataGridView1.Enabled = true;
                    dataGridView1.DataSource = null;
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    textBox11.Enabled = false;

                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";

                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                    groupBox5.Visible = false;
                }
                
            }
            else if(flag==2){
                if(textBox6.Text == ""){
                    MessageBox.Show("No Rekening harus diisi");
                }
                else if(textBox7.Text ==""){
                    MessageBox.Show("Nama Rekening harus diisi");
                }
                else if (textBox8.Text == "")
                {
                    MessageBox.Show("Nama Bank harus diisi");
                }
                else {
                    con.executeUpdate("UPDATE RedeemCash SET NoRekening='"+textBox6.Text+"',NamaRekening='"+textBox7.Text+"', NamaBank='"+textBox8.Text+"' WHERE RedeemNo='"+dataGridView2.Rows[0].Cells[0].Value.ToString()+"'");
                    MessageBox.Show("Update Success!!");
                    flag = 0;
                    dataGridView1.Enabled = true;
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    textBox11.Enabled = false;

                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";

                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                    groupBox5.Visible = false;
                }
            }
            else if(flag==4){
                if(textBox9.Text==""){
                    MessageBox.Show("Kategori harus diisi");
                }
                else if(textBox10.Text==""){
                    MessageBox.Show("Barang harus diisi");
                }
                else if (textBox11.Text == "")
                {
                    MessageBox.Show("Jumlah harus diisi");
                }
                else if(cekAngka(textBox11.Text)==false){
                    MessageBox.Show("Jumlah harus angka");
                }
                else {
                    con.executeUpdate("UPDATE OrderItem SET Kategori='"+textBox9.Text+"',Barang='"+textBox10.Text+"', Jumlah='"+textBox11.Text+"' WHERE NoOrder='"+dataGridView2.Rows[0].Cells[0].Value.ToString()+"'");
                    MessageBox.Show("Update Success!!");
                    flag = 0;
                    dataGridView1.Enabled = true;
                    dataGridView1.DataSource = null;
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    textBox11.Enabled = false;

                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";

                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                    groupBox5.Visible = false;
                }
            }
            else if(flag==5){
                List<int> TotalNominalPlusTopUp = new List<int>();
                int nominalTerpakai= 0;
                int totalSisaNominal = 0;
                List<string> tada = new List<string>();
                List<int> nominalTada = new List<int>();
                List<int> listRedeem = new List<int>();

                
                for(int i=0;i<dataGridView3.Rows.Count;i++){
                    //hitung total NominalPlusTopUp per Tada
                    DataTable dt66 = new DataTable();
                    dt66 = con.getTabel("SELECT NominalTada FROM Tada WHERE TadaNo='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'");
                    TotalNominalPlusTopUp.Add(Convert.ToInt32(dt66.Rows[0][0].ToString()));

                    //simpen no tada 
                    tada.Add(""+dataGridView3.Rows[i].Cells[0].Value.ToString());

                    //hitung total redeem
                    DataTable dtVoucher = new DataTable();
                    DataTable dtCash = new DataTable();
                    DataTable dtDonate = new DataTable();
                    DataTable dtItem = new DataTable();
                    nominalTerpakai = 0;
                    dtVoucher = con.getTabel("SELECT dov.Terpakai FROM DetailOrderVoucher dov, Tada t, OrderVoucher ov WHERE t.TadaNo=dov.TadaNo and ov.NoOrder=dov.NoOrder and ov.StatusOrderVoucher='Success' and dov.TadaNo='"+ dataGridView3.Rows[i].Cells[0].Value.ToString()+"'");
                    for (int j = 0; j < dtVoucher.Rows.Count;j++ )
                    {
                        nominalTerpakai += Convert.ToInt32(dtVoucher.Rows[0][j].ToString());
                    }
                    dtCash = con.getTabel("SELECT drc.Terpakai FROM DetailRedeemCash drc, Tada t, RedeemCash rc WHERE t.TadaNo = drc.TadaNo and rc.RedeemNo=drc.RedeemNo and rc.StatusRedeemCash='Success' and drc.TadaNo='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'");
                    for (int j = 0; j < dtCash.Rows.Count;j++ )
                    {
                        nominalTerpakai += Convert.ToInt32(dtCash.Rows[0][j].ToString());
                    }
                    dtDonate = con.getTabel("SELECT Terpakai FROM DetailDonate dd, Donate d, Tada t WHERE t.TadaNo = dd.TadaNo and d.RedeemNo=dd.RedeemNo and d.StatusDonate='Success' and dd.TadaNo='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'");
                    for (int j = 0; j < dtDonate.Rows.Count; j++)
                    {
                        nominalTerpakai += Convert.ToInt32(dtDonate.Rows[0][j].ToString());
                    }
                    dtItem = con.getTabel("SELECT Terpakai FROM DetailOrderItems doi, OrderItem oi, Tada t WHERE t.TadaNo=doi.TadaNo and oi.NoOrder = doi.NoOrder and oi.StatusOrderItem='Success' and doi.TadaNo='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'");
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        nominalTerpakai += Convert.ToInt32(dtItem.Rows[0][j].ToString());
                    }
                    listRedeem.Add(nominalTerpakai);

                    nominalTada.Add((Convert.ToInt32(dt66.Rows[0][0].ToString())) - nominalTerpakai);
                    //nominalTada = nominal real dari setiap tada di datagridview3
                    //nominalTada.Add(Convert.ToInt32(dt66.Rows[0][0].ToString()));
                }
                for (int i = 0; i < tada.Count; i++)
                {
                    totalSisaNominal += nominalTada[i];
                //    MessageBox.Show("Tada " + tada[i] + " \n memiliki Nominal plus TopUp " + TotalNominalPlusTopUp[i] + "\n Total Terpakai" + listRedeem[i]);
                }
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Voucher must be filled");
                }
                else if (textBox12.Text == "")
                {
                    MessageBox.Show("Nominal harus diisi");
                }
                else if (cekAngka(textBox12.Text) == false)
                {
                    MessageBox.Show("Nominal harus angka");
                }
                else if(Convert.ToInt32(textBox12.Text)<0){
                    MessageBox.Show("Nominal tidak boleh kurang dari sama dengan nol!");
                }
                else if (textBox13.Text == "")
                {
                    MessageBox.Show("Jumlah harus diisi");
                }
                else if (cekAngka(textBox13.Text) == false)
                {
                    MessageBox.Show("Jumlah harus angka");
                }
                else if(Convert.ToInt32(textBox13.Text)<0){
                    MessageBox.Show("Jumlah tidak boleh kurang dari sama dengan nol!");
                }
                else if (Convert.ToInt32(textBox12.Text) * Convert.ToInt32(textBox13.Text) > (Convert.ToInt32(dataGridView2.Rows[0].Cells[5].Value.ToString()) * Convert.ToInt32(dataGridView2.Rows[0].Cells[6].Value.ToString())) + totalSisaNominal)
                {
                    MessageBox.Show("Nominal Saldo Tidak Mencukupi");
                }
                else {
                    con.executeUpdate("INSERT INTO  OrderVoucher VALUES('CI" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + textBox5.Text + "','" + dataGridView2.Rows[0].Cells[1].Value.ToString() + "','" + textBox13.Text + "','" + textBox12.Text + "','" + dataGridView2.Rows[0].Cells[7].Value.ToString() + "','" + dataGridView2.Rows[0].Cells[8].Value.ToString() + "','" + dataGridView2.Rows[0].Cells[9].Value.ToString() + "','" + dataGridView2.Rows[0].Cells[10].Value.ToString() + "','Success','" + MDIform.username + "')");
                    con.executeUpdate("UPDATE OrderItem SET StatusOrderItem='Change' WHERE NoOrder='" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'");
                    if (Convert.ToInt32(textBox12.Text) * Convert.ToInt32(textBox13.Text) >= Convert.ToInt32(dataGridView2.Rows[0].Cells[5].Value.ToString()) * Convert.ToInt32(dataGridView2.Rows[0].Cells[6].Value.ToString()))
                    {
                        int lebih = (Convert.ToInt32(textBox12.Text) * Convert.ToInt32(textBox13.Text)) - (Convert.ToInt32(dataGridView2.Rows[0].Cells[5].Value.ToString()) * Convert.ToInt32(dataGridView2.Rows[0].Cells[6].Value.ToString()));
                        int pengurangan = lebih;
                        for (int i = 0; i < tada.Count; i++)
                        {
                            if (nominalTada[i] >= pengurangan)
                            {
                                if(pengurangan!=0){
                                    MessageBox.Show("Tada " + tada[i] + " dikurang " + pengurangan);
                                }
                                //con.executeUpdate("UPDATE Tada SET NominalTada='"+(nominalTada[i]-pengurangan).ToString()+"' WHERE TadaNo='"+tada[i]+"'");
                                con.executeUpdate("INSERT INTO DetailOrderVoucher VALUES('" + tada[i] + "','CI" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "','"+(pengurangan+Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString()))+"')");
                                pengurangan = 0;
                            }
                            else if (nominalTada[i] < pengurangan)
                            {
                                MessageBox.Show("Tada " + tada[i] + " dikurang " + nominalTada[i]);
                                //con.executeUpdate("UPDATE Tada SET NominalTada='0' WHERE TadaNo='" + tada[i] + "'");
                                con.executeUpdate("INSERT INTO DetailOrderVoucher VALUES('" + tada[i] + "','CI" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "','" + (nominalTada[i]+Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString())) + "')");
                                pengurangan = pengurangan - nominalTada[i];
                            }
                        }
                        //MessageBox.Show("Lebih "+lebih);
                    }
                    else {
                        int kurang = (Convert.ToInt32(dataGridView2.Rows[0].Cells[5].Value.ToString()) * Convert.ToInt32(dataGridView2.Rows[0].Cells[6].Value.ToString())) - (Convert.ToInt32(textBox12.Text) * Convert.ToInt32(textBox13.Text));
                        int penambahan = kurang;
                        int tempPlus = 1;
                        for (int i = 0; i < tada.Count;i++ )
                        {
                            
                            if (Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString()) >= penambahan)
                            {
                                
                                if (penambahan != 0)
                                {
                                    //MessageBox.Show("Tada " + tada[i] + " ditambah " + penambahan);
                                    DataTable dt1 = new DataTable();
                                    dt1 = con.getTabel("SELECT TopUpID FROM TopUp order by TopUpID desc");
                                    string code = dt1.Rows[0][0].ToString();
                                    int numericCode = Int32.Parse(code.Substring(2)) + tempPlus;
                                    if (numericCode < 10) topupid = "TU0000" + numericCode;
                                    else if (numericCode < 100) topupid = "TU000" + numericCode;
                                    else if (numericCode < 1000) topupid = "TU00" + numericCode;
                                    else if (numericCode < 10000) topupid = "TU0" + numericCode;
                                    else topupid = "TU" + numericCode;
                                    tempPlus++;
                                    //MessageBox.Show("Top Up ID " + topupid);
                                    con.executeUpdate("INSERT INTO TopUp VALUES('C" + topupid + "','" + penambahan + "','" + DateTime.Now.ToShortDateString() + "','" + tada[i] + "','" + MDIform.username + "')");
                                }
                                //MessageBox.Show("Tada "+tada[i] + " saldonya "+(nominalTada[i] + penambahan).ToString());
                                //MessageBox.Show("Tada "+tada[i] + " terpakai "+( Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString())-penambahan));
                                //con.executeUpdate("UPDATE Tada SET NominalTada='" + (nominalTada[i] + penambahan).ToString() + "' WHERE TadaNo='" + tada[i] + "'");

                                
                                con.executeUpdate("UPDATE Tada SET NominalTada='" + (TotalNominalPlusTopUp[i] + penambahan) + "' WHERE TadaNo='" + tada[i] + "'");
                                con.executeUpdate("INSERT INTO DetailOrderVoucher VALUES('" + tada[i] + "','CI" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "','" + ( Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString())-penambahan) + "' )");
                                penambahan = 0;
                            }
                            else if (Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString()) < penambahan)
                            {

                                //MessageBox.Show("Tada " + tada[i] + " ditambah " + dataGridView3.Rows[i].Cells[2].Value.ToString());
                                //MessageBox.Show("Tada "+tada[i]+" saldo nya "+(nominalTada[i] + Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString())).ToString());
                                //MessageBox.Show("Tada " + tada[i] + " terpakai 0");
                                //con.executeUpdate("UPDATE Tada SET NominalTada='" + (nominalTada[i] + Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString())).ToString() + "' WHERE TadaNo='" + tada[i] + "'");
                                DataTable dt1 = new DataTable();
                                dt1 = con.getTabel("SELECT TopUpID FROM TopUp order by TopUpID desc");
                                string code = dt1.Rows[0][0].ToString();
                                int numericCode = Int32.Parse(code.Substring(2)) + tempPlus;
                                if (numericCode < 10) topupid = "TU0000" + numericCode;
                                else if (numericCode < 100) topupid = "TU000" + numericCode;
                                else if (numericCode < 1000) topupid = "TU00" + numericCode;
                                else if (numericCode < 10000) topupid = "TU0" + numericCode;
                                else topupid = "TU" + numericCode;

                                //MessageBox.Show("Top Up ID " + topupid);
                                con.executeUpdate("INSERT INTO TopUp VALUES('C" + topupid + "','" + nominalTada[i] + "','" + DateTime.Now.ToShortDateString() + "','" + tada[i] + "','" + MDIform.username + "')");
                                con.executeUpdate("UPDATE Tada SET NominalTada='" + (TotalNominalPlusTopUp[i] + nominalTada[i]) + "' WHERE TadaNo='" + tada[i] + "'");
                                con.executeUpdate("INSERT INTO DetailOrderVoucher VALUES('"+tada[i]+"','CI"+dataGridView2.Rows[0].Cells[0].Value.ToString()+"','0')");
                                penambahan = penambahan - Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString());
                                tempPlus++;
                            }
                        }
                        //MessageBox.Show("Kurang "+kurang);
                    }
                    MessageBox.Show("Change from Item to Voucher Success!");
                    flag = 0;
                    dataGridView1.Enabled = true;

                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    textBox11.Enabled = false;
                    textBox12.Enabled = false;
                    textBox13.Enabled = false;

                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";

                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                    groupBox5.Visible = false;
                    dataGridView1.DataSource = null;
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            textBox5.Enabled = true;
            textBox12.Enabled = true;
            textBox13.Enabled = true;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            flag = 5;
        }
    }
}
