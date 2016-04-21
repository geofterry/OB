using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Offline_Backend_PT_Aksi_Visitama_Project_Application
{
    public partial class Verification_Report : Form
    {
        
        Connect con = new Connect();
        
        
        public Verification_Report()
        {
            InitializeComponent();
            button3.Visible = false;
            comboBox1.Items.Add("Vouchers");
            comboBox1.Items.Add("Cashs");
            comboBox1.Items.Add("Donations");
            comboBox1.Items.Add("Items");
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            //dateTimePicker1.Value = DateTime.Now.AddDays(0).AddHours(0).AddMinutes(0).AddSeconds(0);
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,8,25,0);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 25, 00);
            label5.Text = "";
            DataTable dtVoucher = con.getTabel("SELECT DISTINCT NamaVoucher FROM OrderVoucher");
            for (int i = 0; i < dtVoucher.Rows.Count;i++ )
            {
                comboBox2.Items.Add(dtVoucher.Rows[i][0].ToString());
            }
            comboBox2.Visible = false;
            comboBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "Loading...";
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
            dataGridView5.Rows.Clear();
            dataGridView5.Refresh();
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose the Type");
            }
            else {
                DialogResult result = MessageBox.Show("Do you really want to get report \n from " + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + " \n to " + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + " ?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (comboBox1.SelectedIndex == 0)
                    {

                        dataGridView1.Visible = true;
                        dataGridView2.Visible = false;
                        dataGridView3.Visible = false;
                        dataGridView4.Visible = false;
                        DataTable dtTemp = new DataTable();
                        DataTable dtTemp2 = new DataTable();
                        if (comboBox2.SelectedIndex == -1)
                        {
                            dtTemp = con.getTabel("SELECT dov.NoOrder, dov.TadaNo, g.BrandName , ov.Tanggal, ov.Claimer, ov.NamaVoucher, ov.Jumlah, ov.Pecahan, CAST(CAST(ov.Jumlah*ov.Pecahan as int) as VARCHAR), ov.Telepon, ov.Alamat FROM 	DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success' order by ov.Tanggal desc");
                        }
                        else if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex == -1)
                        {
                            dtTemp = con.getTabel("SELECT dov.NoOrder, dov.TadaNo, g.BrandName , ov.Tanggal, ov.Claimer, ov.NamaVoucher, ov.Jumlah, ov.Pecahan, CAST(CAST(ov.Jumlah*ov.Pecahan as int) as VARCHAR), ov.Telepon, ov.Alamat FROM 	DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success' AND NamaVoucher='" + comboBox2.Text + "' order by ov.Tanggal desc");
                        }
                        else if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1)
                        {
                            dtTemp = con.getTabel("SELECT dov.NoOrder, dov.TadaNo, g.BrandName , ov.Tanggal, ov.Claimer, ov.NamaVoucher, ov.Jumlah, ov.Pecahan, CAST(CAST(ov.Jumlah*ov.Pecahan as int) as VARCHAR), ov.Telepon, ov.Alamat FROM 	DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success' AND NamaVoucher='" + comboBox2.Text + "' AND Pecahan='" + comboBox3.Text + "' order by ov.Tanggal desc");
                        }

                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(dtTemp.Rows[i][0].ToString(), i + 1, dtTemp.Rows[i][1].ToString(), dtTemp.Rows[i][2].ToString(), dtTemp.Rows[i][3].ToString(), dtTemp.Rows[i][4].ToString(), dtTemp.Rows[i][5].ToString(), dtTemp.Rows[i][6].ToString(), dtTemp.Rows[i][7].ToString(), dtTemp.Rows[i][8].ToString(), dtTemp.Rows[i][9].ToString(), dtTemp.Rows[i][10].ToString());
                        }
                        int temp = 0;
                        List<string> voucher = new List<string>();
                        List<string> pecahan = new List<string>();
                        List<int> jumlah = new List<int>();
                        if (comboBox2.SelectedIndex == -1)
                        {
                            dtTemp2 = con.getTabel("SELECT DISTINCT ov.NamaVoucher, ov.Pecahan FROM DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success'");
                        }
                        else if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex == -1)
                        {
                            dtTemp2 = con.getTabel("SELECT DISTINCT ov.NamaVoucher, ov.Pecahan FROM DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success' AND NamaVoucher='" + comboBox2.Text + "'");
                        }
                        else if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1)
                        {
                            dtTemp2 = con.getTabel("SELECT DISTINCT ov.NamaVoucher, ov.Pecahan FROM DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success' AND NamaVoucher='" + comboBox2.Text + "' AND Pecahan='" + comboBox3.Text + "'");
                        }
                        for (int i = 0; i < dtTemp2.Rows.Count; i++)
                        {
                            voucher.Add(dtTemp2.Rows[i][0].ToString());
                            pecahan.Add(dtTemp2.Rows[i][1].ToString());
                        }
                        for (int i = 0; i < voucher.Count; i++)
                        {
                            temp = 0;
                            for (int j = 0; j < dataGridView1.Rows.Count; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[6].Value.ToString() == voucher[i] && dataGridView1.Rows[j].Cells[8].Value.ToString() == pecahan[i])
                                {
                                    temp += Convert.ToInt32(dataGridView1.Rows[j].Cells[7].Value.ToString());
                                }
                            }
                            jumlah.Add(temp);
                        }
                        for (int i = 0; i < voucher.Count; i++)
                        {
                            dataGridView5.Rows.Add(voucher[i], pecahan[i], jumlah[i]);
                        }
                        /*for (int j = 0; j < dataGridView1.Rows.Count;j++ )
                        {
                            //temp = 0;
                            for (int k = 0; k <j; k++)
                            {
                                if(dataGridView1.Rows[j].Cells[6].Value.ToString()==dataGridView1.Rows[k].Cells[6].Value.ToString()){
                                    temp++;
                                }
                            }
                            if(temp!=0){
                                dataGridView5.Rows.Add(dataGridView1.Rows[j].Cells[6].Value.ToString(), j);
                            }
                            //voucher.Add(dataGridView1.Rows[j].Cells[6].Value.ToString());
                        
                        }*/
                        /*for (int j = 0; j < voucher.Count;j++ )
                        {
                            if(j==0){
                                dataGridView5.Rows.Add(voucher[j], j); 
                            }
                            else if (voucher[j] != voucher[j - 1])
                            {
                                dataGridView5.Rows.Add(voucher[j], j);
                            }
                        
                        }*/
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        dataGridView1.Visible = false;
                        dataGridView2.Visible = true;
                        dataGridView3.Visible = false;
                        dataGridView4.Visible = false;
                        DataTable dtTempo = new DataTable();
                        dtTempo = con.getTabel("SELECT rc.RedeemNo, td.TadaNo, gt.BrandName, rc.Tanggal, rc.Claimer, CAST(CAST(0.91* drc.Terpakai AS int) AS Varchar) AS JumlahFee, rc.Telepon, rc.NamaBank, rc.NamaRekening, rc.NoRekening, rc.NamaCabang FROM RedeemCash rc, DetailRedeemCash drc, Tada td, Generation gt WHERE rc.RedeemNo = drc.RedeemNo AND td.TadaNo = drc.TadaNo AND td.GenerationID = gt.GenerationID and (rc.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' )AND rc.StatusRedeemCash='Success' order by rc.Tanggal desc");
                        for (int i = 0; i < dtTempo.Rows.Count; i++)
                        {
                            dataGridView2.Rows.Add(dtTempo.Rows[i][0].ToString(), i + 1, dtTempo.Rows[i][1].ToString(), dtTempo.Rows[i][2].ToString(), dtTempo.Rows[i][3].ToString(), dtTempo.Rows[i][4].ToString(), dtTempo.Rows[i][5].ToString(), dtTempo.Rows[i][6].ToString(), dtTempo.Rows[i][7].ToString(), dtTempo.Rows[i][8].ToString(), dtTempo.Rows[i][9].ToString(), dtTempo.Rows[i][10].ToString());
                        }
                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        dataGridView1.Visible = false;
                        dataGridView2.Visible = false;
                        dataGridView3.Visible = true;
                        dataGridView4.Visible = false;
                        DataTable dtTemp = new DataTable();
                        dtTemp = con.getTabel("Select d.RedeemNo, a.TadaNo, c.BrandName, d.Tanggal, d.Claimer, a.Terpakai, d.Yayasan from DetailDonate a join Tada b  on a.TadaNo = b.TadaNo join Generation c on b.GenerationID = c.GenerationID join Donate d on d.RedeemNo = a.RedeemNo where d.Tanggal >='" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND d.Tanggal <='" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND d.StatusDonate='Success' order by d.Tanggal desc");
                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            dataGridView3.Rows.Add(dtTemp.Rows[i][0].ToString(), i + 1, dtTemp.Rows[i][1].ToString(), dtTemp.Rows[i][2].ToString(), dtTemp.Rows[i][3].ToString(), dtTemp.Rows[i][4].ToString(), dtTemp.Rows[i][5].ToString(), dtTemp.Rows[i][6].ToString());
                        }
                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        dataGridView1.Visible = false;
                        dataGridView2.Visible = false;
                        dataGridView3.Visible = false;
                        dataGridView4.Visible = true;
                        DataTable dtTemp = new DataTable();
                        dtTemp = con.getTabel("select oi.NoOrder, di.TaDaNo, gt.BrandName, oi.Tanggal, oi.Claimer, oi.Kategori, oi.Barang, oi.Jumlah, oi.Harga, CAST(CAST(oi.jumlah*oi.harga as int) as varchar) AS Total, oi.Telepon, oi.Alamat FROM TaDa td, DetailOrderItems di, OrderItem oi, Generation gt WHERE oi.NoOrder = di.NoOrder AND di.TaDaNo = td.TaDaNo AND td.GenerationID = gt.GenerationID AND oi.Tanggal >='" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND oi.Tanggal <='" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND oi.StatusOrderItem='Success' order by oi.Tanggal desc");
                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            dataGridView4.Rows.Add(dtTemp.Rows[i][0], i + 1, dtTemp.Rows[i][1], dtTemp.Rows[i][2], dtTemp.Rows[i][3], dtTemp.Rows[i][4], dtTemp.Rows[i][5], dtTemp.Rows[i][6], dtTemp.Rows[i][7], dtTemp.Rows[i][8], dtTemp.Rows[i][9], dtTemp.Rows[i][10], dtTemp.Rows[i][11]);
                        }
                    }
                }
                //dtTemp = con.getTabel("select oi.NoOrder, di.TaDaNo, gt.BrandName, oi.Tanggal, oi.Claimer, oi.Kategori, oi.Barang, oi.Jumlah, oi.Harga, CAST(CAST(oi.jumlah*oi.harga as int) as varchar) AS Total, oi.Telepon, oi.Alamat FROM TaDa td, DetailOrderItems di, OrderItem oi, Generation gt WHERE oi.NoOrder = di.NoOrder AND di.TaDaNo = td.TaDaNo AND td.GenerationID = gt.GenerationID");
                //dataGridView1.DataSource = dtTemp;
                
            }
            label5.Text = "";
        }

        private void Verification_Report_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataTable dtTemp = new DataTable();
            dtTemp = con.getTabel("SELECT DISTINCT ov.NamaVoucher, ov.Pecahan FROM DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success'");
            //MessageBox.Show(dtTemp.Rows.Count.ToString());
            saveFileDialog1.InitialDirectory = "C:\\Users\\AT\\Desktop\\Print Report\\";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xls|Excel Files(2007)|*.xlsx";
            if(saveFileDialog1.ShowDialog() != DialogResult.Cancel){
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = new Microsoft.Office.Interop.Excel.Worksheet();
                //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets["Sheet1"];
                
                //Change properties of the Workbook
                //ExcelApp.Columns.ColumnWidth = 40;
                //ExcelApp.Columns.RowHeight = 64;
                
                if(comboBox1.SelectedIndex==-1){
                    MessageBox.Show("Please choose the Type");
                }
                else if(comboBox1.SelectedIndex==0){
                    for (int i = 1; i < dtTemp.Rows.Count + 1; i++)
                    {
                        DataTable dt99 = new DataTable();
                        dt99 = con.getTabel("SELECT dov.NoOrder, dov.TadaNo, g.BrandName , ov.Tanggal, ov.Claimer, ov.NamaVoucher, ov.Jumlah, ov.Pecahan, CAST(CAST(ov.Jumlah*ov.Pecahan as int) as VARCHAR), ov.Telepon, ov.Alamat FROM 	DetailOrderVoucher dov, Tada t, Generation g, OrderVoucher ov WHERE dov.TadaNo = t.TadaNo AND t.GenerationID=g.GenerationID AND dov.NoOrder = ov.NoOrder AND (ov.Tanggal BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') AND ov.StatusOrderVoucher='Success' AND ov.NamaVoucher='"+dtTemp.Rows[i-1][0].ToString()+"' AND ov.Pecahan='"+dtTemp.Rows[i-1][1].ToString()+"' order by ov.Tanggal desc");
                        //jika lebih dari 3 jumlah sheet, maka harus add worksheet terlebih dahulu
                        if (i > 3)
                        {
                            ExcelApp.Worksheets.Add();
                            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets["Sheet" + i];
                            xlWorkSheet.Cells[1, 1] = "No Order";
                            //xlWorkSheet.Cells[1, 2] = "No";
                            xlWorkSheet.Cells[1, 2] = "Tada";
                            xlWorkSheet.Cells[1, 3] = "Brand";
                            xlWorkSheet.Cells[1, 4] = "Tanggal";
                            xlWorkSheet.Cells[1, 5] = "Claimer";
                            xlWorkSheet.Cells[1, 6] = "Voucher";
                            xlWorkSheet.Cells[1, 7] = "Jumlah";
                            xlWorkSheet.Cells[1, 8] = "Pecahan";
                            xlWorkSheet.Cells[1, 9] = "Total";
                            xlWorkSheet.Cells[1, 10] = "Telp";
                            xlWorkSheet.Cells[1, 11] = "Alamat";

                            for (int z = 0; z < dt99.Rows.Count; z++)
                            {
                                for (int x = 0; x < dt99.Columns.Count; x++)
                                {
                                    /*if (x == 1)
                                    {
                                        xlWorkSheet.Cells[z + 2, x + 1] = (z+1).ToString();
                                    }
                                    else if(x>1){
                                        xlWorkSheet.Cells[z + 2, x + 1] = dt99.Rows[z][x].ToString();
                                    }*/
                                    //else {
                                    xlWorkSheet.Cells[z + 2, x + 1] = dt99.Rows[z][x].ToString();
                                    //}

                                }
                            }
                            //jika panjang karakter untuk sheet lebih panjang dari 30 karakter, karena sheet excel memiliki batasan 30 karakter
                            if ((dtTemp.Rows[i - 1][0].ToString() + " " + dtTemp.Rows[i - 1][1].ToString()).Length > 30)
                            {
                                //temp untuk mengambil panjang dari value voucher
                                int temp = dtTemp.Rows[i - 1][1].ToString().Length;
                                xlWorkSheet.Name = dtTemp.Rows[i - 1][0].ToString().Substring(0, 30 - temp) + " " + dtTemp.Rows[i - 1][1].ToString();
                            }
                            else
                            {
                                xlWorkSheet.Name = dtTemp.Rows[i - 1][0].ToString() + " " + dtTemp.Rows[i - 1][1].ToString();
                            }
                            xlWorkSheet.Cells[dt99.Rows.Count + 3, 1] = "Voucher";
                            xlWorkSheet.Cells[dt99.Rows.Count + 3, 2] = "Pecahan";
                            xlWorkSheet.Cells[dt99.Rows.Count + 3, 3] = "Jumlah";

                            for (int c = 0; c < dataGridView5.Rows.Count; c++)
                            {
                                for (int d = 0; d < dataGridView5.Columns.Count; d++)
                                {
                                    xlWorkSheet.Cells[dt99.Rows.Count + 4 + c, d + 1] = dataGridView5.Rows[c].Cells[d].Value.ToString(); ;
                                }
                            }

                            xlWorkSheet.Rows.AutoFit();
                            xlWorkSheet.Columns.AutoFit();
                            xlWorkSheet.Range["K1:K1048576"].WrapText = false;
                        }
                        else
                        {
                            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets["Sheet" + i];
                            xlWorkSheet.Cells[1, 1] = "No Order";
                            //xlWorkSheet.Cells[1, 2] = "No";
                            xlWorkSheet.Cells[1, 2] = "Tada";
                            xlWorkSheet.Cells[1, 3] = "Brand";
                            xlWorkSheet.Cells[1, 4] = "Tanggal";
                            xlWorkSheet.Cells[1, 5] = "Claimer";
                            xlWorkSheet.Cells[1, 6] = "Voucher";
                            xlWorkSheet.Cells[1, 7] = "Jumlah";
                            xlWorkSheet.Cells[1, 8] = "Pecahan";
                            xlWorkSheet.Cells[1, 9] = "Total";
                            xlWorkSheet.Cells[1, 10] = "Telp";
                            xlWorkSheet.Cells[1, 11] = "Alamat";

                            for (int z = 0; z < dt99.Rows.Count; z++)
                            {
                                for (int x = 0; x < dt99.Columns.Count; x++)
                                {
                                    /*if (x == 1)
                                    {
                                        xlWorkSheet.Cells[z + 2, x + 1] = (z+1).ToString();
                                    }
                                    else if(x>1){
                                        xlWorkSheet.Cells[z + 2, x + 1] = dt99.Rows[z][x].ToString();
                                    }*/
                                    //else {
                                        xlWorkSheet.Cells[z + 2, x + 1] = dt99.Rows[z][x].ToString();
                                    //}
                                    
                                }
                            }

                            if ((dtTemp.Rows[i - 1][0].ToString() + " " + dtTemp.Rows[i - 1][1].ToString()).Length > 30)
                            {
                                int temp = dtTemp.Rows[i - 1][1].ToString().Length;
                                xlWorkSheet.Name = dtTemp.Rows[i - 1][0].ToString().Substring(0, 30 - temp) + " " + dtTemp.Rows[i - 1][1].ToString();
                            }
                            else
                            {
                                xlWorkSheet.Name = dtTemp.Rows[i - 1][0].ToString() + " " + dtTemp.Rows[i - 1][1].ToString();
                            }
                            xlWorkSheet.Cells[dt99.Rows.Count + 3, 1] = "Voucher";
                            xlWorkSheet.Cells[dt99.Rows.Count + 3, 2] = "Pecahan";
                            xlWorkSheet.Cells[dt99.Rows.Count + 3, 3] = "Jumlah";
                            for (int c = 0; c < dataGridView5.Rows.Count; c++)
                            {
                                for (int d = 0; d < dataGridView5.Columns.Count; d++)
                                {
                                    xlWorkSheet.Cells[dt99.Rows.Count + 4 + c, d + 1] = dataGridView5.Rows[c].Cells[d].Value.ToString(); ;
                                }
                            }
                            xlWorkSheet.Rows.AutoFit();
                            xlWorkSheet.Columns.AutoFit();
                            xlWorkSheet.Range["K1:K1048576"].WrapText = false;
                        }

                    }
                    /*
                    label5.Text = "Loading...";
                    //Storing header part in Excel
                    //xlWorkSheet1.Cells[1, 1] = "No Order";
                    ExcelApp.Cells[1, 2] = "No";
                    ExcelApp.Cells[1, 3] = "Tada";
                    ExcelApp.Cells[1, 4] = "Brand";
                    ExcelApp.Cells[1, 5] = "Tanggal";
                    ExcelApp.Cells[1, 6] = "Claimer";
                    ExcelApp.Cells[1, 7] = "Voucher";
                    ExcelApp.Cells[1, 8] = "Jumlah";
                    ExcelApp.Cells[1, 9] = "Pecahan";
                    ExcelApp.Cells[1, 10] = "Total";
                    ExcelApp.Cells[1, 11] = "Telp";
                    ExcelApp.Cells[1, 12] = "Alamat";

                    //Storing Each row and column value to excel sheet
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }*/

                    /*xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 1] = "Voucher";
                    xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 2] = "Pecahan";
                    xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 3] = "Jumlah";*/

                    /*for (int i = 0; i < dataGridView5.Rows.Count;i++ )
                    {
                        for (int j = 0; j < dataGridView5.Columns.Count;j++ )
                        {
                            ExcelApp.Cells[dataGridView1.Rows.Count + 4 + i, j + 1] = dataGridView5.Rows[i].Cells[j].Value.ToString(); ;
                        }
                    }*/
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    //Storing header part in Excel
                    ExcelApp.Cells[1, 1] = "No Redeem";
                    ExcelApp.Cells[1, 2] = "No";
                    ExcelApp.Cells[1, 3] = "Tada";
                    ExcelApp.Cells[1, 4] = "Brand";
                    ExcelApp.Cells[1, 5] = "Tanggal";
                    ExcelApp.Cells[1, 6] = "Claimer";
                    ExcelApp.Cells[1, 7] = "Jumlah Fee";
                    ExcelApp.Cells[1, 8] = "Telepon";
                    ExcelApp.Cells[1, 9] = "Nama Bank";
                    ExcelApp.Cells[1, 10] = "Nama Rekening";
                    ExcelApp.Cells[1, 11] = "No Rekening";
                    ExcelApp.Cells[1, 12] = "Nama Cabang";

                    //Storing Each row and column value to excel sheet
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView2.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    //Storing header part in Excel
                    ExcelApp.Cells[1, 1] = "No Redeem";
                    ExcelApp.Cells[1, 2] = "No";
                    ExcelApp.Cells[1, 3] = "Tada";
                    ExcelApp.Cells[1, 4] = "Brand";
                    ExcelApp.Cells[1, 5] = "Tanggal";
                    ExcelApp.Cells[1, 6] = "Claimer";
                    ExcelApp.Cells[1, 7] = "Jumlah";
                    ExcelApp.Cells[1, 8] = "Yayasan";

                    //Storing Each row and column value to excel sheet
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView3.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridView3.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    //Storing header part in Excel
                    ExcelApp.Cells[1, 1] = "No Order";
                    ExcelApp.Cells[1, 2] = "No";
                    ExcelApp.Cells[1, 3] = "Tada";
                    ExcelApp.Cells[1, 4] = "Brand";
                    ExcelApp.Cells[1, 5] = "Tanggal";
                    ExcelApp.Cells[1, 6] = "Claimer";
                    ExcelApp.Cells[1, 7] = "Kategori";
                    ExcelApp.Cells[1, 8] = "Barang";
                    ExcelApp.Cells[1, 9] = "Jumlah";
                    ExcelApp.Cells[1, 10] = "Harga";
                    ExcelApp.Cells[1, 11] = "Total";
                    ExcelApp.Cells[1, 12] = "Telp";
                    ExcelApp.Cells[1, 12] = "Alamat";

                    //Storing Each row and column value to excel sheet
                    for (int i = 0; i < dataGridView4.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView4.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridView4.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                ExcelApp.Rows.AutoFit();
                ExcelApp.Columns.AutoFit();
                ExcelApp.Range["K1:K1048576"].WrapText = false;
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                label5.Text = "";
                MessageBox.Show("Export to Excel Success!");
                
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            DataTable dtVoucherValue = con.getTabel("SELECT DISTINCT Pecahan FROM OrderVoucher WHERE NamaVoucher='"+comboBox2.Text+"'");
            for (int i = 0; i < dtVoucherValue.Rows.Count;i++ )
            {
                comboBox3.Items.Add(dtVoucherValue.Rows[i][0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Vouchers")
            {
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
            else {
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "C:\\Users\\AT\\Desktop\\Print Report\\";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xls|Excel Files(2007)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel) {
                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int i = 0;
                int j = 0;

                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 1, j + 1] = cell.Value;
                    }
                }

                xlWorkBook.SaveAs("csharp.net-informations.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                //releaseObject(xlWorkSheet);
                //releaseObject(xlWorkBook);
                //releaseObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file c:\\csharp.net-informations.xls");
            }
        }
    }
}
