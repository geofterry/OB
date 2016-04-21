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
using System.Collections.Generic;

namespace Offline_Backend_PT_Aksi_Visitama_Project_Application
{
    public partial class Verif : Form
    {
        Connect con = new Connect();
        DataTable dtt = new DataTable();
        DataTable dtTampungNoTadaNoInDb = new DataTable();

        //buat list baru keseluruhan
        List<string> noOrderRedeemNo = new List<string>();
        List<string> jenisOrder = new List<string>();
        List<string> finalTanggal = new List<string>();
        List<DateTime> dtimeFinalTanggal = new List<DateTime>();
        List<string> finalTadaNo = new List<string>();
        List<string> finalClaimer = new List<string>();
        List<int> finalTerpakai = new List<int>();

        

        //buat list baru keseluruhan di table bawah
        List<string> tempFinalNoOrderRedeemNo = new List<string>();
        List<string> tempFinalJenisOrder = new List<string>();
        List<string> tempFinalTanggal = new List<string>();
        List<DateTime> dtimeTempFinalTanggal = new List<DateTime>();
        List<string> tempFinalTadaNo = new List<string>();
        List<string> tempFinalClaimer = new List<string>();
        List<int> tempFinalTerpakai = new List<int>();
        List<string> tempFinalVoucher = new List<string>();
        List<int> tempFinalJumlahVoucher = new List<int>();
        List<int> tempFinalPecahanVoucher = new List<int>();
        List<int> tempFinalDelivery = new List<int>();
        List<string> tempFinalEmail = new List<string>();
        List<string> tempFinalTelepon = new List<string>();
        List<string> tempFinalAlamat = new List<string>();
        List<string> tempFinalNamaBank = new List<string>();
        List<string> tempFinalNamaRekening = new List<string>();
        List<string> tempFinalNoRekening = new List<string>();
        List<string> tempFinalNamaCabang = new List<string>();
        List<string> tempFinalYayasan = new List<string>();
        List<string> tempFinalKategori = new List<string>();
        List<string> tempFinalBarang = new List<string>();
        List<int> tempFinalJumlahItem = new List<int>();
        List<int> tempFinalHargaItem = new List<int>();

        List<int> totalTopUp = new List<int>();
        List<int> totalRedeem = new List<int>();
        List<int> SaldoAwal = new List<int>();
        List<int> SaldoAkhir = new List<int>();
        List<string> status = new List<string>();

        List<int> temptemp = new List<int>();
        List<int> tremp = new List<int>();
        List<int> tempo = new List<int>();
        int flagSubmit = 0;

        public Verif()
        {
            InitializeComponent();
            label7.Text = "";
            button3.Enabled = false;
            button4.Enabled = false;
            dataGridView1.DataSource = null;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            dataGridView2.Visible = false;
            button1.Enabled = true;
            button2.Enabled = true;
            //button3.Enabled = false;
            button4.Enabled = false;
            textBox1.Enabled = false;
            //dataGridView5.DataSource = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '01137-408-1300-1901' AND TanggalTopUp < '2014-08-22 17:13' ");
            //01137-408-1300-1901
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\Users\\Lenovo Admin\\Desktop\\PT AV\\Tarikan data";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            noOrderRedeemNo.Clear();
            jenisOrder.Clear();
            finalTanggal.Clear();
            dtimeFinalTanggal.Clear();
            finalTadaNo.Clear();
            finalClaimer.Clear();
            finalTerpakai.Clear();

            tempFinalNoOrderRedeemNo.Clear();
            tempFinalJenisOrder.Clear();
            tempFinalTanggal.Clear();
            dtimeTempFinalTanggal.Clear();
            tempFinalTadaNo.Clear();
            tempFinalClaimer.Clear();
            tempFinalTerpakai.Clear();
            tempFinalVoucher.Clear();
            tempFinalJumlahVoucher.Clear();
            tempFinalPecahanVoucher.Clear();
            tempFinalDelivery.Clear();
            tempFinalEmail.Clear();
            tempFinalTelepon.Clear();
            tempFinalAlamat.Clear();
            tempFinalNamaBank.Clear();

            tempFinalNoRekening.Clear();
            tempFinalNamaCabang.Clear();
            tempFinalYayasan.Clear();
            tempFinalKategori.Clear();
            tempFinalBarang.Clear();
            tempFinalJumlahItem.Clear();
            tempFinalHargaItem.Clear();
            totalTopUp.Clear();
            totalRedeem.Clear();
            SaldoAwal.Clear();
            SaldoAkhir.Clear();
            status.Clear();
            temptemp.Clear();
            tempo.Clear();
            //Tampung worksheet Vouchers dari excel
            List<string> tadaVoucher = new List<string>();
            List<string> noOrderVoucher = new List<string>();
            List<string> tanggalVoucher = new List<string>();
            List<int> terpakaiVoucher = new List<int>();
            List<string> claimerVoucher = new List<string>();

            List<string> namaVoucher = new List<string>();
            List<int> jumlahVoucher = new List<int>();
            List<int> pecahanVoucher = new List<int>();
            List<int> deliveryVoucher = new List<int>();
            List<string> emailVoucher = new List<string>();
            List<string> teleponVoucher = new List<string>();
            List<string> alamatVoucher = new List<string>();

            //Tampung worksheet Cashs dari excel
            List<string> tadaCash = new List<string>();
            List<string> redeemNoCash = new List<string>();
            List<string> tanggalCash = new List<string>();
            List<int> terpakaiCash = new List<int>();
            List<string> claimerCash = new List<string>();

            List<string> teleponCash = new List<string>();
            List<string> namaBankCash = new List<string>();
            List<string> namaRekeningCash = new List<string>();
            List<string> noRekeningCash = new List<string>();
            List<string> namaCabangCash = new List<string>();

            //Tampung worksheet Donations dari excel
            List<string> tadaDonation = new List<string>();
            List<string> redeemNoDonation = new List<string>();
            List<string> tanggalDonation = new List<string>();
            List<int> terpakaiDonation = new List<int>();
            List<string> claimerDonation = new List<string>();

            List<string> yayasanDonation = new List<string>();

            //Tampung worksheet Items dari excel
            List<string> tadaItem = new List<string>();
            List<string> noOrderItem = new List<string>();
            List<string> tanggalItem = new List<string>();
            List<int> terpakaiItem = new List<int>();
            List<string> claimerItem = new List<string>();


            List<int> deliveryItem = new List<int>();
            List<string> emailItem = new List<string>();
            List<string> teleponItem = new List<string>();
            List<string> alamatItem = new List<string>();
            List<string> kategoriItem = new List<string>();
            List<string> barangItem = new List<string>();
            List<int> jumlahItem = new List<int>();
            List<int> hargaItem = new List<int>();

            int tempSaldoAkhir = 0;
            int tempTotalTopUp = 0;
            int tempTotalRedeem = 0;

            int testVoucher = 0;
            int testCash = 0;
            int testDonation = 0;
            int testItem = 0;

            int VoucherSuccess = 0;
            int CashSuccess = 0;
            int DonationSuccess = 0;
            int ItemSuccess = 0;

            //hapus isi dataGridView1
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            //hapus isi dataGridView2
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

            //hapus list Vouchers
            tadaVoucher.Clear();
            noOrderVoucher.Clear();
            tanggalVoucher.Clear();
            terpakaiVoucher.Clear();
            claimerVoucher.Clear();

            namaVoucher.Clear();
            jumlahVoucher.Clear();
            pecahanVoucher.Clear();
            deliveryVoucher.Clear();
            emailVoucher.Clear();
            teleponVoucher.Clear();
            alamatVoucher.Clear();

            //hapus list Cashs
            tadaCash.Clear();
            redeemNoCash.Clear();
            tanggalCash.Clear();
            terpakaiCash.Clear();
            claimerCash.Clear();

            teleponCash.Clear();
            namaBankCash.Clear();
            namaRekeningCash.Clear();
            namaCabangCash.Clear();
            noRekeningCash.Clear();

            tremp.Clear();

            string pathConn = "";

            //validasi jika file kosong
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please choose the file.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //validasi file harus .xls atau .xlsx
            else if (!textBox1.Text.EndsWith(".xls") == true && !textBox1.Text.EndsWith(".xlsx") == true)
            {
                MessageBox.Show("Wrong file format, The file format must be .xls or .xlsx", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
            //validasi format dalam file harus sesuai
            else {
                try {
                    flagSubmit = 1;
                    if (textBox1.Text.EndsWith(".xls") == true)
                    {
                        pathConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + textBox1.Text + "; Extended Properties=\"Excel 8.0;HDR=YES\"";
                        //OleDbConnection conn = new OleDbConnection(pathConn);
                    }
                    else if (textBox1.Text.EndsWith(".xlsx") == true)
                    {
                        pathConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 12.0;HDR=YES\"";

                    }
                    label7.Text = "Loading...";
                    //string pathConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;\";";
                    OleDbConnection conn = new OleDbConnection(pathConn);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter("Select * from [Vouchers$]", conn);
                    OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter("Select * from [Cashs$]", conn);
                    OleDbDataAdapter myDataAdapter3 = new OleDbDataAdapter("Select * from [Donations$]", conn);
                    OleDbDataAdapter myDataAdapter4 = new OleDbDataAdapter("Select * from [Items$]", conn);

                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();

                    DataTable dtTopUp = new DataTable();
                    DataTable dtOrderVoucher = new DataTable();
                    DataTable dtRedeemCash = new DataTable();
                    DataTable dtRedeemDonation = new DataTable();
                    DataTable dtOrderItem = new DataTable();
                    DataTable dtSaldoAwal = new DataTable();

                    DataTable dtTampungNoOrderAID = new DataTable();
                    DataTable dtTampungNoTadaNoInDb = new DataTable();

                    myDataAdapter1.Fill(dt);
                    myDataAdapter2.Fill(dt2);
                    myDataAdapter3.Fill(dt3);
                    myDataAdapter4.Fill(dt4);

                    //masukin sheet vouchers ke dataGridView
                    for (int i = 0; i < dt.Rows.Count;i++ ) {
                        if(dt.Rows[i][1].ToString()!=""){
                            tadaVoucher.Add(dt.Rows[i][1].ToString());
                            noOrderVoucher.Add(dt.Rows[i][14].ToString());
                            tanggalVoucher.Add(dt.Rows[i][6].ToString());
                            terpakaiVoucher.Add(Convert.ToInt32(dt.Rows[i][5].ToString()));
                            claimerVoucher.Add(dt.Rows[i][7].ToString());

                            if (dt.Rows[i][8].ToString() == "Carl's Jr.")
                            {
                                namaVoucher.Add("Carl s Jr.");
                            }
                            else if (dt.Rows[i][8].ToString() != "Carl's Jr.")
                            {
                                namaVoucher.Add(dt.Rows[i][8].ToString());
                            }
                            //namaVoucher.Add(dt.Rows[i][8].ToString());
                            if (dt.Rows[i][9].ToString() == "")
                            {
                                jumlahVoucher.Add(0);
                            }
                            else if (dt.Rows[i][9].ToString() != "")
                            {
                                jumlahVoucher.Add(Convert.ToInt32(dt.Rows[i][9].ToString()));
                            }
                            if (dt.Rows[i][10].ToString() == "")
                            {
                                pecahanVoucher.Add(0);
                            }
                            else if (dt.Rows[i][10].ToString() != "")
                            {
                                pecahanVoucher.Add(Convert.ToInt32(dt.Rows[i][10].ToString()));
                            }
                            if (dt.Rows[i][11].ToString() == "")
                            {
                                deliveryVoucher.Add(0);
                            }
                            else if (dt.Rows[i][11].ToString() != "")
                            {
                                deliveryVoucher.Add(Convert.ToInt32(dt.Rows[i][11].ToString()));
                            }
                            emailVoucher.Add(dt.Rows[i][16].ToString());
                            teleponVoucher.Add(dt.Rows[i][17].ToString());
                            if (dt.Rows[i][18].ToString().Length > 250)
                            {
                                alamatVoucher.Add(dt.Rows[i][18].ToString().Substring(0, 250));
                            }
                            else {
                                alamatVoucher.Add(dt.Rows[i][18].ToString());
                            }

                            //merge cell
                            if (dt.Rows[i][14].ToString() == "")
                            {
                                noOrderVoucher[i] = noOrderVoucher[i - 1];
                                claimerVoucher[i] = claimerVoucher[i - 1];

                                namaVoucher[i] = namaVoucher[i - 1];
                                jumlahVoucher[i] = jumlahVoucher[i - 1];
                                pecahanVoucher[i] = pecahanVoucher[i - 1];
                                pecahanVoucher[i] = pecahanVoucher[i - 1];
                                deliveryVoucher[i] = deliveryVoucher[i - 1];
                                emailVoucher[i] = emailVoucher[i - 1];
                                teleponVoucher[i] = teleponVoucher[i - 1];
                                alamatVoucher[i] = alamatVoucher[i - 1];
                            }
                            
                            dataGridView1.Rows.Add(noOrderVoucher[i], "Voucher", tanggalVoucher[i], tadaVoucher[i], claimerVoucher[i], terpakaiVoucher[i], "", "");
                            dataGridView2.Rows.Add(noOrderVoucher[i], "Voucher", tanggalVoucher[i], tadaVoucher[i], claimerVoucher[i], terpakaiVoucher[i], namaVoucher[i], jumlahVoucher[i], pecahanVoucher[i], deliveryVoucher[i], emailVoucher[i], teleponVoucher[i], alamatVoucher[i], "", "", "", "", "", "", "", "", "");
                        }
                    }
                    //masukin sheet cashs ke dataGridView
                    for (int i = 0; i < dt2.Rows.Count;i++ )
                    {
                        if (dt2.Rows[i][1].ToString() != "") {
                            tadaCash.Add(dt2.Rows[i][1].ToString());
                            redeemNoCash.Add(dt2.Rows[i][10].ToString());
                            tanggalCash.Add(dt2.Rows[i][6].ToString());
                            terpakaiCash.Add(Convert.ToInt32(dt2.Rows[i][5].ToString()));
                            claimerCash.Add(dt2.Rows[i][7].ToString());

                            teleponCash.Add(dt2.Rows[i][11].ToString());
                            namaBankCash.Add(dt2.Rows[i][12].ToString());
                            namaRekeningCash.Add(dt2.Rows[i][13].ToString());
                            noRekeningCash.Add(dt2.Rows[i][14].ToString());
                            namaCabangCash.Add(dt2.Rows[i][15].ToString());

                            if (dt2.Rows[i][10].ToString() == "")
                            {
                                redeemNoCash[i] = redeemNoCash[i - 1];
                                claimerCash[i] = claimerCash[i - 1];

                                teleponCash[i] = teleponCash[i - 1];
                                namaBankCash[i] = namaBankCash[i - 1];
                                namaRekeningCash[i] = namaRekeningCash[i - 1];
                                noRekeningCash[i] = noRekeningCash[i - 1];
                                namaCabangCash[i] = namaCabangCash[i - 1];
                            }

                            dataGridView1.Rows.Add(redeemNoCash[i], "Cash", tanggalCash[i], tadaCash[i], claimerCash[i], terpakaiCash[i], "", "");
                            dataGridView2.Rows.Add(redeemNoCash[i], "Cash", tanggalCash[i], tadaCash[i], claimerCash[i], terpakaiCash[i], "", "", "", "", "", teleponCash[i], "", namaBankCash[i], namaRekeningCash[i], noRekeningCash[i], namaCabangCash[i], "", "", "", "", "");
                        }
                    }
                    //masukin sheet donation ke dataGridView
                    for (int i = 0; i < dt3.Rows.Count;i++ )
                    {
                        if (dt3.Rows[i][1].ToString() != "") {
                            tadaDonation.Add(dt3.Rows[i][1].ToString());
                            redeemNoDonation.Add(dt3.Rows[i][9].ToString());
                            tanggalDonation.Add(dt3.Rows[i][6].ToString());
                            terpakaiDonation.Add(Convert.ToInt32(dt3.Rows[i][5].ToString()));
                            claimerDonation.Add(dt3.Rows[i][7].ToString());

                            yayasanDonation.Add(dt3.Rows[i][10].ToString());

                            if (dt3.Rows[i][9].ToString() == "")
                            {
                                redeemNoDonation[i] = redeemNoDonation[i - 1];
                                claimerDonation[i] = claimerDonation[i - 1];

                                yayasanDonation[i] = yayasanDonation[i - 1];
                            }
                            dataGridView1.Rows.Add(redeemNoDonation[i], "Donation", tanggalDonation[i], tadaDonation[i], claimerDonation[i], terpakaiDonation[i], "", "");
                            dataGridView2.Rows.Add(redeemNoDonation[i], "Donation", tanggalDonation[i], tadaDonation[i], claimerDonation[i], terpakaiDonation[i], "", "", "", "", "", "", "", "", "", "", "", yayasanDonation[i], "", "", "", "");
                        }
                    }
                    //masukin sheet items ke dataGridView
                    for (int i = 0; i < dt4.Rows.Count;i++ )
                    {
                        if (dt4.Rows[i][1].ToString() != "") {
                            tadaItem.Add(dt4.Rows[i][1].ToString());
                            noOrderItem.Add(dt4.Rows[i][15].ToString());
                            tanggalItem.Add(dt4.Rows[i][6].ToString());
                            terpakaiItem.Add(Convert.ToInt32(dt4.Rows[i][5].ToString()));
                            claimerItem.Add(dt4.Rows[i][7].ToString());

                            if (dt4.Rows[i][12].ToString() == "")
                            {
                                deliveryItem.Add(0);
                            }
                            else if (dt4.Rows[i][12].ToString() != "")
                            {
                                deliveryItem.Add(Convert.ToInt32(dt4.Rows[i][12].ToString()));
                            }
                            emailItem.Add(dt4.Rows[i][17].ToString());
                            teleponItem.Add(dt4.Rows[i][18].ToString());
                            if (dt4.Rows[i][19].ToString().Length > 250)
                            {
                                alamatItem.Add(dt4.Rows[i][19].ToString().Substring(0, 250));
                            }
                            else {
                                alamatItem.Add(dt4.Rows[i][19].ToString());
                            }
                            kategoriItem.Add(dt4.Rows[i][8].ToString());
                            barangItem.Add(dt4.Rows[i][9].ToString());
                            if (dt4.Rows[i][10].ToString() == "")
                            {
                                jumlahItem.Add(0);
                            }
                            else if (dt4.Rows[i][10].ToString() != "")
                            {
                                jumlahItem.Add(Convert.ToInt32(dt4.Rows[i][10].ToString()));
                            }
                            if (dt4.Rows[i][11].ToString() == "")
                            {
                                hargaItem.Add(0);
                            }
                            else if (dt4.Rows[i][11].ToString() != "")
                            {
                                hargaItem.Add(Convert.ToInt32(dt4.Rows[i][11].ToString()));
                            }

                            if (dt4.Rows[i][15].ToString() == "")
                            {
                                noOrderItem[i] = noOrderItem[i - 1];
                                claimerItem[i] = claimerItem[i - 1];

                                deliveryItem[i] = deliveryItem[i - 1];
                                emailItem[i] = emailItem[i - 1];
                                teleponItem[i] = teleponItem[i - 1];
                                alamatItem[i] = alamatItem[i - 1];
                                kategoriItem[i] = kategoriItem[i - 1];
                                barangItem[i] = barangItem[i - 1];
                                jumlahItem[i] = jumlahItem[i - 1];
                                hargaItem[i] = hargaItem[i - 1];
                            }
                            dataGridView1.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "");
                            dataGridView2.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "", "", deliveryItem[i], emailItem[i], teleponItem[i], alamatItem[i], "", "", "", "", "", kategoriItem[i], barangItem[i], jumlahItem[i], hargaItem[i]);
                        }
                    }

                    //masukin semua data ke list baru
                    for (int a = 0; a < dataGridView1.Rows.Count; a++)
                    {
                        noOrderRedeemNo.Add(dataGridView1.Rows[a].Cells[0].Value.ToString());
                        jenisOrder.Add(dataGridView1.Rows[a].Cells[1].Value.ToString());
                        finalTanggal.Add(dataGridView1.Rows[a].Cells[2].Value.ToString());
                        finalTadaNo.Add(dataGridView1.Rows[a].Cells[3].Value.ToString());
                        finalClaimer.Add(dataGridView1.Rows[a].Cells[4].Value.ToString());
                        finalTerpakai.Add(Convert.ToInt32(dataGridView1.Rows[a].Cells[5].Value.ToString()));
                        string[] dateTemp = dataGridView1.Rows[a].Cells[2].Value.ToString().Split(' ');
                        string[] dateTempPart = dateTemp[0].Split('-');
                        string[] hourTempPart = dateTemp[1].Split(':');
                        if (dateTempPart[1] == "January")
                        {
                            dateTempPart[1] = "1";
                        }
                        else if(dateTempPart[1] == "February"){
                            dateTempPart[1] = "2";
                        }
                        else if (dateTempPart[1] == "March")
                        {
                            dateTempPart[1] = "3";
                        }
                        else if (dateTempPart[1] == "April")
                        {
                            dateTempPart[1] = "4";
                        }
                        else if (dateTempPart[1] == "May")
                        {
                            dateTempPart[1] = "5";
                        }
                        else if (dateTempPart[1] == "June")
                        {
                            dateTempPart[1] = "6";
                        }
                        else if (dateTempPart[1] == "July")
                        {
                            dateTempPart[1] = "7";
                        }
                        else if (dateTempPart[1] == "August")
                        {
                            dateTempPart[1] = "8";
                        }
                        else if (dateTempPart[1] == "September")
                        {
                            dateTempPart[1] = "9";
                        }
                        else if (dateTempPart[1] == "October")
                        {
                            dateTempPart[1] = "10";
                        }
                        else if (dateTempPart[1] == "November")
                        {
                            dateTempPart[1] = "11";
                        }
                        else if (dateTempPart[1] == "December")
                        {
                            dateTempPart[1] = "12";
                        }
                        DateTime dtime = new DateTime(Convert.ToInt32(dateTempPart[2]), Convert.ToInt32(dateTempPart[1]), Convert.ToInt32(dateTempPart[0]), Convert.ToInt32(hourTempPart[0]), Convert.ToInt32(hourTempPart[1]), 0);
                        dtimeFinalTanggal.Add(dtime);
                    }

                    for (int b = 0; b < dataGridView2.Rows.Count;b++ )
                    {
                        tempFinalNoOrderRedeemNo.Add(dataGridView2.Rows[b].Cells[0].Value.ToString());
                        tempFinalJenisOrder.Add(dataGridView2.Rows[b].Cells[1].Value.ToString());
                        tempFinalTanggal.Add(dataGridView2.Rows[b].Cells[2].Value.ToString());
                        tempFinalTadaNo.Add(dataGridView2.Rows[b].Cells[3].Value.ToString());
                        tempFinalClaimer.Add(dataGridView2.Rows[b].Cells[4].Value.ToString());
                        tempFinalTerpakai.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[5].Value.ToString()));
                        tempFinalVoucher.Add(dataGridView2.Rows[b].Cells[6].Value.ToString());
                        if (dataGridView2.Rows[b].Cells[7].Value.ToString() != "")
                        {
                            tempFinalJumlahVoucher.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[7].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[7].Value.ToString() == "")
                        {
                            tempFinalJumlahVoucher.Add(0);
                        }
                        if (dataGridView2.Rows[b].Cells[8].Value.ToString() != "")
                        {
                            tempFinalPecahanVoucher.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[8].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[8].Value.ToString() == "")
                        {
                            tempFinalPecahanVoucher.Add(0);
                        }
                        if (dataGridView2.Rows[b].Cells[9].Value.ToString() != "")
                        {
                            tempFinalDelivery.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[9].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[9].Value.ToString() == "")
                        {
                            tempFinalDelivery.Add(0);
                        }

                        tempFinalEmail.Add(dataGridView2.Rows[b].Cells[10].Value.ToString());
                        tempFinalTelepon.Add(dataGridView2.Rows[b].Cells[11].Value.ToString());
                        if (dataGridView2.Rows[b].Cells[12].Value.ToString().Length > 250)
                        {
                            tempFinalAlamat.Add(dataGridView2.Rows[b].Cells[12].Value.ToString().Substring(0,250));
                        }
                        else {
                            tempFinalAlamat.Add(dataGridView2.Rows[b].Cells[12].Value.ToString());
                        }
                        
                        tempFinalNamaBank.Add(dataGridView2.Rows[b].Cells[13].Value.ToString());
                        tempFinalNamaRekening.Add(dataGridView2.Rows[b].Cells[14].Value.ToString());
                        tempFinalNoRekening.Add(dataGridView2.Rows[b].Cells[15].Value.ToString());
                        tempFinalNamaCabang.Add(dataGridView2.Rows[b].Cells[16].Value.ToString());
                        tempFinalYayasan.Add(dataGridView2.Rows[b].Cells[17].Value.ToString());
                        tempFinalKategori.Add(dataGridView2.Rows[b].Cells[18].Value.ToString());
                        tempFinalBarang.Add(dataGridView2.Rows[b].Cells[19].Value.ToString());
                        //tempFinalJumlahItem.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[20].Value.ToString()));
                        if (dataGridView2.Rows[b].Cells[20].Value.ToString() != "")
                        {
                            tempFinalJumlahItem.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[20].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[20].Value.ToString() == "")
                        {
                            tempFinalJumlahItem.Add(0);
                        }
                        if (dataGridView2.Rows[b].Cells[21].Value.ToString() != "")
                        {
                            tempFinalHargaItem.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[21].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[21].Value.ToString() == "")
                        {
                            tempFinalHargaItem.Add(0);
                        }

                        string[] dateTemp = dataGridView2.Rows[b].Cells[2].Value.ToString().Split(' ');
                        string[] dateTempPart = dateTemp[0].Split('-');
                        string[] hourTempPart = dateTemp[1].Split(':');
                        if (dateTempPart[1] == "January")
                        {
                            dateTempPart[1] = "1";
                        }
                        else if (dateTempPart[1] == "February")
                        {
                            dateTempPart[1] = "2";
                        }
                        else if (dateTempPart[1] == "March")
                        {
                            dateTempPart[1] = "3";
                        }
                        else if (dateTempPart[1] == "April")
                        {
                            dateTempPart[1] = "4";
                        }
                        else if (dateTempPart[1] == "May")
                        {
                            dateTempPart[1] = "5";
                        }
                        else if (dateTempPart[1] == "June")
                        {
                            dateTempPart[1] = "6";
                        }
                        else if (dateTempPart[1] == "July")
                        {
                            dateTempPart[1] = "7";
                        }
                        else if (dateTempPart[1] == "August")
                        {
                            dateTempPart[1] = "8";
                        }
                        else if (dateTempPart[1] == "September")
                        {
                            dateTempPart[1] = "9";
                        }
                        else if (dateTempPart[1] == "October")
                        {
                            dateTempPart[1] = "10";
                        }
                        else if (dateTempPart[1] == "November")
                        {
                            dateTempPart[1] = "11";
                        }
                        else if (dateTempPart[1] == "December")
                        {
                            dateTempPart[1] = "12";
                        }
                        DateTime dtime = new DateTime(Convert.ToInt32(dateTempPart[2]), Convert.ToInt32(dateTempPart[1]), Convert.ToInt32(dateTempPart[0]), Convert.ToInt32(hourTempPart[0]), Convert.ToInt32(hourTempPart[1]), 0);
                        dtimeTempFinalTanggal.Add(dtime);
                    }

                    //sorting based on tanggal
                    for (int x = 0; x < noOrderRedeemNo.Count;x++)
                    {
                        for (int y = noOrderRedeemNo.Count - 1; y > x;y-- )
                        {
                            int result = DateTime.Compare(dtimeFinalTanggal[y - 1], dtimeFinalTanggal[y]);
                            if(result>0){
                                DateTime dateswap = dtimeFinalTanggal[y - 1];
                                dtimeFinalTanggal[y - 1] = dtimeFinalTanggal[y];
                                dtimeFinalTanggal[y] = dateswap;

                                string swapNoOrderRedeemNo = noOrderRedeemNo[y - 1];
                                noOrderRedeemNo[y - 1] = noOrderRedeemNo[y];
                                noOrderRedeemNo[y] = swapNoOrderRedeemNo;

                                string swapJenisOrder = jenisOrder[y - 1];
                                jenisOrder[y - 1] = jenisOrder[y];
                                jenisOrder[y] = swapJenisOrder;

                                string swapFinalTanggal = finalTanggal[y - 1];
                                finalTanggal[y - 1] = finalTanggal[y];
                                finalTanggal[y] = swapFinalTanggal;

                                string swapFinalTadaNo = finalTadaNo[y - 1];
                                finalTadaNo[y - 1] = finalTadaNo[y];
                                finalTadaNo[y] = swapFinalTadaNo;

                                string swapFinalClaimer = finalClaimer[y - 1];
                                finalClaimer[y - 1] = finalClaimer[y];
                                finalClaimer[y] = swapFinalClaimer;

                                int swapFinalTerpakai = finalTerpakai[y - 1];
                                finalTerpakai[y - 1] = finalTerpakai[y];
                                finalTerpakai[y] = swapFinalTerpakai;
                            }
                        }
                    }

                    for (int x = 0; x < tempFinalNoOrderRedeemNo.Count; x++)
                    {
                        for (int y = tempFinalNoOrderRedeemNo.Count - 1; y > x; y--)
                        {
                            int result = DateTime.Compare(dtimeTempFinalTanggal[y - 1], dtimeTempFinalTanggal[y]);
                            if(result>0){
                                DateTime dateswap = dtimeTempFinalTanggal[y - 1];
                                dtimeTempFinalTanggal[y - 1] = dtimeTempFinalTanggal[y];
                                dtimeTempFinalTanggal[y] = dateswap;

                                string swapTempFinalNoOrderRedeemNo = tempFinalNoOrderRedeemNo[y-1];
                                tempFinalNoOrderRedeemNo[y-1] = tempFinalNoOrderRedeemNo[y];
                                tempFinalNoOrderRedeemNo[y] = swapTempFinalNoOrderRedeemNo;

                                string swapTempFinalJenisOrder = tempFinalJenisOrder[y-1];
                                tempFinalJenisOrder[y - 1] = tempFinalJenisOrder[y];
                                tempFinalJenisOrder[y] = swapTempFinalJenisOrder;

                                string swapTempFinalTanggal = tempFinalTanggal[y-1];
                                tempFinalTanggal[y - 1] = tempFinalTanggal[y];
                                tempFinalTanggal[y] = swapTempFinalTanggal;

                                string swapTempFinalTadaNo = tempFinalTadaNo[y-1];
                                tempFinalTadaNo[y - 1] = tempFinalTadaNo[y];
                                tempFinalTadaNo[y] = swapTempFinalTadaNo;

                                string swapTempFinalClaimer = tempFinalClaimer[y-1];
                                tempFinalClaimer[y - 1] = tempFinalClaimer[y];
                                tempFinalClaimer[y] = swapTempFinalClaimer;

                                int swapTempFinalTerpakai = tempFinalTerpakai[y-1];
                                tempFinalTerpakai[y - 1] = tempFinalTerpakai[y];
                                tempFinalTerpakai[y] = swapTempFinalTerpakai;

                                string swapTempFinalVoucher = tempFinalVoucher[y-1];
                                tempFinalVoucher[y - 1] = tempFinalVoucher[y];
                                tempFinalVoucher[y] = swapTempFinalVoucher;

                                int swapTempFinalJumlahVoucher = tempFinalJumlahVoucher[y-1];
                                tempFinalJumlahVoucher[y - 1] = tempFinalJumlahVoucher[y];
                                tempFinalJumlahVoucher[y] = swapTempFinalJumlahVoucher;

                                int swapTempFinalPecahanVoucher = tempFinalPecahanVoucher[y-1];
                                tempFinalPecahanVoucher[y - 1] = tempFinalPecahanVoucher[y];
                                tempFinalPecahanVoucher[y] = swapTempFinalPecahanVoucher;

                                int swapTempFinalDelivery = tempFinalDelivery[y-1];
                                tempFinalDelivery[y - 1] = tempFinalDelivery[y];
                                tempFinalDelivery[y] = swapTempFinalDelivery;

                                string swapTempFinalEmail = tempFinalEmail[y-1];
                                tempFinalEmail[y - 1] = tempFinalEmail[y];
                                tempFinalEmail[y] = swapTempFinalEmail;

                                string swapTempFinalTelepon = tempFinalTelepon[y-1];
                                tempFinalTelepon[y - 1] = tempFinalTelepon[y];
                                tempFinalTelepon[y] = swapTempFinalTelepon;

                                string swapTempFinalAlamat = tempFinalAlamat[y-1];
                                tempFinalAlamat[y - 1] = tempFinalAlamat[y];
                                tempFinalAlamat[y] = swapTempFinalAlamat;

                                string swapTempFinalNamaBank = tempFinalNamaBank[y-1];
                                tempFinalNamaBank[y - 1] = tempFinalNamaBank[y];
                                tempFinalNamaBank[y] = swapTempFinalNamaBank;

                                string swapTempFinalNamaRekening = tempFinalNamaRekening[y-1];
                                tempFinalNamaRekening[y - 1] = tempFinalNamaRekening[y];
                                tempFinalNamaRekening[y] = swapTempFinalNamaRekening;

                                string swapTempFinalNoRekenig = tempFinalNoRekening[y-1];
                                tempFinalNoRekening[y - 1] = tempFinalNoRekening[y];
                                tempFinalNoRekening[y] = swapTempFinalNoRekenig;

                                string swapTempFinalNamaCabang = tempFinalNamaCabang[y-1];
                                tempFinalNamaCabang[y - 1] = tempFinalNamaCabang[y];
                                tempFinalNamaCabang[y] = swapTempFinalNamaCabang;

                                string swapTempFinalYayasan = tempFinalYayasan[y-1];
                                tempFinalYayasan[y - 1] = tempFinalYayasan[y];
                                tempFinalYayasan[y] = swapTempFinalYayasan;

                                string swapTempFinalKategori = tempFinalKategori[y-1];
                                tempFinalKategori[y - 1] = tempFinalKategori[y];
                                tempFinalKategori[y] = swapTempFinalKategori;

                                string swapTempFinalBarang = tempFinalBarang[y-1];
                                tempFinalBarang[y - 1] = tempFinalBarang[y];
                                tempFinalBarang[y] = swapTempFinalBarang;

                                int swapTempFinalJumlahItem = tempFinalJumlahItem[y-1];
                                tempFinalJumlahItem[y - 1] = tempFinalJumlahItem[y];
                                tempFinalJumlahItem[y] = swapTempFinalJumlahItem;

                                int swapTempFinalHargaItem = tempFinalHargaItem[y-1];
                                tempFinalHargaItem[y - 1] = tempFinalHargaItem[y];
                                tempFinalHargaItem[y] = swapTempFinalHargaItem;
                            }
                        }
                    }
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();

                    dataGridView2.Rows.Clear();
                    dataGridView2.Refresh();
                    //PUT HERE
                    for (int yy = 0; yy < noOrderRedeemNo.Count;yy++ )
                    {
                        dtTampungNoTadaNoInDb = con.getTabel("SELECT TadaNo FROM Tada WHERE TadaNo='"+finalTadaNo[yy]+"'");
                        if (dtTampungNoTadaNoInDb.Rows.Count == 0)
                        {
                            tempo.Add(1);
                        }
                        else {
                            tempo.Add(0);
                        }
                    }

                    for (int zz = 0; zz < noOrderRedeemNo.Count;zz++ )
                    {
                        if(jenisOrder[zz]=="Voucher"){
                            dtTampungNoOrderAID = con.getTabel("SELECT NoOrder FROM OrderVoucher WHERE NoOrder='"+noOrderRedeemNo[zz]+"' AND StatusOrderVoucher='Success'");
                        }
                        else if(jenisOrder[zz]=="Cash"){
                            dtTampungNoOrderAID = con.getTabel("SELECT RedeemNo FROM RedeemCash WHERE RedeemNo='" + noOrderRedeemNo[zz] + "' AND StatusRedeemCash='Success'");
                        }
                        else if (jenisOrder[zz] == "Item")
                        {
                            dtTampungNoOrderAID = con.getTabel("SELECT NoOrder FROM OrderItem WHERE NoOrder='" + noOrderRedeemNo[zz] + "' AND StatusOrderItem='Success'");
                        }
                        else {
                            dtTampungNoOrderAID = con.getTabel("SELECT RedeemNo FROM Donate WHERE RedeemNo='" + noOrderRedeemNo[zz] + "' AND StatusDonate='Success'");
                        }
                        if (dtTampungNoOrderAID.Rows.Count > 0)
                        {
                            temptemp.Add(1);
                        }

                        else {
                            temptemp.Add(0);
                        }
                    }

                    for (int jj = 0; jj < noOrderRedeemNo.Count;jj++ )
                    {
                        int wowo = 0;
                        for (int yx = 0; yx < jj;yx++ )
                        {
                            if (jenisOrder[jj] == jenisOrder[yx] && noOrderRedeemNo[jj] == noOrderRedeemNo[yx] && finalClaimer[jj] != finalClaimer[yx])
                            {
                                wowo++;
                            }
                        }
                        if (wowo > 0)
                        {
                            tremp.Add(1);
                        }
                        else {
                            tremp.Add(0);
                        }
                    }


                    for (int t = 0; t < noOrderRedeemNo.Count;t++ )
                    {
                        dtSaldoAwal = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='" + finalTadaNo[t] + "'");
                        //MessageBox.Show(dtSaldoAwal.Rows[0][0].ToString() + t );
                        //dtSaldoAwal = con.getTabel("SELECT TadaNo FROM Tada WHERE TadaNo='"+finalTadaNo[t]+"'");
                        /*if (dtSaldoAwal.Rows.Count== 0)
                        {
                            MessageBox.Show(t + " " + finalTadaNo[t] + tempFinalJenisOrder[t]);
                        }*/
                        /*else {
                            SaldoAwal.Add(Convert.ToInt32(dtSaldoAwal.Rows[0][0].ToString()));
                        }*/

                        if (tempo[t] == 0)
                        {
                            SaldoAwal.Add(Convert.ToInt32(dtSaldoAwal.Rows[0][0].ToString()));
                        }
                        else if(tempo[t]==1){
                            SaldoAwal.Add(0);
                        }
                        //string[] test = finalTanggal[t].Split(' ');
                        //dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + finalTadaNo[t] + "' AND TanggalTopUp < '" + dtimeTempFinalTanggal[t].ToString("yyyy-MM-dd") + "' ");
                        dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo ='"+finalTadaNo[t]+"' ");
                        tempTotalTopUp = 0;
                        for (int k = 0; k < dtTopUp.Rows.Count; k++)
                        {
                            tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString());
                        }
                        if (tempo[t] == 1)
                        {
                            totalTopUp.Add(0);
                        }
                        else if(tempo[t]==0){
                            totalTopUp.Add(tempTotalTopUp);
                        }

                        dtOrderVoucher = con.getTabel("SELECT dov.TadaNo,dov.NoOrder,dov.Terpakai FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='" + finalTadaNo[t] + "' AND ov.StatusOrderVoucher='Success'");
                        tempTotalRedeem = 0;

                        for (int m = 0; m < dtOrderVoucher.Rows.Count; m++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtOrderVoucher.Rows[m][2].ToString());
                        }
                        //totalRedeem.Add(tempTotalRedeem);


                        dtRedeemCash = con.getTabel("SELECT drc.TadaNo, drc.RedeemNo, drc.Terpakai FROM DetailRedeemCash drc, RedeemCash rc WHERE drc.RedeemNo = rc.RedeemNo AND drc.TadaNo = '" + finalTadaNo[t] + "' AND rc.StatusRedeemCash= 'Success'");
                        //tempTotalRedeem = 0;
                        for (int j = 0; j < dtRedeemCash.Rows.Count; j++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtRedeemCash.Rows[j][2].ToString());
                        }
                        //totalRedeem.Add(tempTotalRedeem);


                        dtRedeemDonation = con.getTabel("SELECT dd.TadaNo, dd.RedeemNo, dd.Terpakai FROM DetailDonate dd, Donate d WHERE dd.RedeemNo = d.RedeemNo AND dd.TadaNo = '" + finalTadaNo[t] + "' AND d.StatusDonate = 'Success'");
                        //tempTotalRedeem = 0;
                        for (int j = 0; j < dtRedeemDonation.Rows.Count; j++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtRedeemDonation.Rows[j][2].ToString());
                        }
                        //totalRedeem.Add(tempTotalRedeem);


                        dtOrderItem = con.getTabel("SELECT doi.TadaNo,doi.NoOrder,doi.Terpakai FROM DetailOrderItems doi, OrderItem oi WHERE doi.NoOrder=oi.NoOrder AND doi.TadaNo = '" + finalTadaNo[t] + "' AND oi.StatusOrderItem = 'Success'");
                        //tempTotalRedeem = 0;
                        for (int j = 0; j < dtOrderItem.Rows.Count; j++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtOrderItem.Rows[j][2].ToString());
                        }
                        if(tempo[t]==1){
                            totalRedeem.Add(0);
                        }
                        else if (tempo[t] == 0)
                        {
                            totalRedeem.Add(tempTotalRedeem);
                        }
                    }
                    
                    for (int f = 0; f < noOrderRedeemNo.Count; f++) {
                        tempTotalRedeem = totalRedeem[f];
                        for (int l = 0; l < f; l++)
                        {
                            if (finalTadaNo[f] == finalTadaNo[l] && status[l] == "Success")
                            {
                                //if(status[f]=="Success"){
                                tempTotalRedeem += finalTerpakai[l];
                                
                            }
                        }
                        totalRedeem[f] = tempTotalRedeem;
                        tempSaldoAkhir = SaldoAwal[f] + totalTopUp[f] - totalRedeem[f];
                        if(tempo[f]==1){
                            SaldoAkhir.Add(0);
                        }
                        else if (tempo[f] == 0)
                        {
                            SaldoAkhir.Add(tempSaldoAkhir);
                        }

                        if(tempo[f]==1){
                            status.Add("TadaNo Not Found");
                        }
                        else if(tempo[f]==0){

                            if (SaldoAkhir[f] >= finalTerpakai[f])
                            {
                                    status.Add("Success");
                                    
                            }
                            else if (SaldoAkhir[f] < finalTerpakai[f])
                            {
                                status.Add("Failed");
                            }
                        }

                        //if (tempFinalTadaNo[f]=="0320-1551-3161-1652-593")
                        //{
                        if(tempo[f]==1){
                            dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                            
                        }
                        else if (tempo[f] == 0)
                        {
                            if (temptemp[f] == 1)
                            {
                                status[f] = "Already in DB";
                                if(tempFinalJenisOrder[f]=="Voucher"){
                                    testVoucher++;
                                }
                                else if(tempFinalJenisOrder[f] == "Cash"){
                                    testCash++;
                                }
                                else if (tempFinalJenisOrder[f] == "Item")
                                {
                                    testItem++;
                                }
                                else
                                {
                                    testDonation++;
                                }
                                dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                            }
                            else if(tremp[f]==1){
                                status[f] = "HAYA";
                                dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                            }
                            else if (temptemp[f] == 0)
                            {
                                if (status[f] == "Failed")
                                {
                                    dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                                }

                                if (status[f] == "Success")
                                {
                                    if (tempFinalJenisOrder[f] == "Voucher")
                                    {
                                        VoucherSuccess++;
                                        //MessageBox.Show(""+finalTadaNo[f]);
                                    }
                                    else if (tempFinalJenisOrder[f] == "Cash")
                                    {
                                        CashSuccess++;
                                    }
                                    else if (tempFinalJenisOrder[f] == "Item")
                                    {
                                        ItemSuccess++;
                                    }
                                    else
                                    {
                                        DonationSuccess++;
                                    }
                                }
                            }
                        }
                        //}
                        
                    }
                    for (int g = 0; g < tempFinalNoOrderRedeemNo.Count;g++ )
                    {
                        dataGridView2.Rows.Add(tempFinalNoOrderRedeemNo[g], tempFinalJenisOrder[g], tempFinalTanggal[g], tempFinalTadaNo[g], tempFinalClaimer[g], tempFinalTerpakai[g], tempFinalVoucher[g], tempFinalJumlahVoucher[g], tempFinalPecahanVoucher[g], tempFinalDelivery[g], tempFinalEmail[g], tempFinalTelepon[g], tempFinalAlamat[g], tempFinalNamaBank[g], tempFinalNamaRekening[g], tempFinalNoRekening[g], tempFinalNamaCabang[g], tempFinalYayasan[g], tempFinalKategori[g], tempFinalBarang[g], tempFinalJumlahItem[g], tempFinalHargaItem[g]);
                    }
                    label7.Text = "";
                    MessageBox.Show("Load completed");
                    textBox7.Text = ""+testVoucher;
                    textBox8.Text = "" + testCash;
                    textBox9.Text = "" + testDonation;
                    textBox10.Text = "" + testItem;
                    //--------------------------------
                    textBox11.Text = "" + VoucherSuccess;
                    textBox12.Text = "" + CashSuccess;
                    textBox13.Text = "" + DonationSuccess;
                    textBox14.Text = "" + ItemSuccess;
                    //--------------------------------
                }
                catch(Exception ex){
                    MessageBox.Show("File must be a Giftcard Report", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //masukin tada No yang di klik ke variable tempTadaNo
            string tempTadaNo = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //menampilkan tempTadaNo di textbox2
            textBox2.Text = tempTadaNo;

            //simpan jenis order dari datagridview ke variable
            string tempJenisOrder = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            //bersihkan datagridview3 & datagridview4 terlebih dahulu setiap kali cell click
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();

            //dt5 untuk get nominal generation berdasarkan tadaNo pada row yg d klik
            DataTable dt55 = new DataTable();
            dt55 = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='" + tempTadaNo + "'");
            //memasukan nominal generation yang telah di get dari dt5 untuk ditampilkan di textbox2
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "TadaNo Not Found")
            {
                textBox3.Text = "0";
            }
            else
            {
                textBox3.Text = dt55.Rows[0][0].ToString();
            }
            //List<DateTime> dtimeFinalTanggal = new List<DateTime>();
            //dtimeFinalTanggal.Clear();
            string[] dateTemp = dataGridView1.CurrentRow.Cells[2].Value.ToString().Split(' ');
            string[] dateTempPart = dateTemp[0].Split('-');
            string[] hourTempPart = dateTemp[1].Split(':');
            if (dateTempPart[1] == "January")
            {
                dateTempPart[1] = "1";
            }
            else if (dateTempPart[1] == "February")
            {
                dateTempPart[1] = "2";
            }
            else if (dateTempPart[1] == "March")
            {
                dateTempPart[1] = "3";
            }
            else if (dateTempPart[1] == "April")
            {
                dateTempPart[1] = "4";
            }
            else if (dateTempPart[1] == "May")
            {
                dateTempPart[1] = "5";
            }
            else if (dateTempPart[1] == "June")
            {
                dateTempPart[1] = "6";
            }
            else if (dateTempPart[1] == "July")
            {
                dateTempPart[1] = "7";
            }
            else if (dateTempPart[1] == "August")
            {
                dateTempPart[1] = "8";
            }
            else if (dateTempPart[1] == "September")
            {
                dateTempPart[1] = "9";
            }
            else if (dateTempPart[1] == "October")
            {
                dateTempPart[1] = "10";
            }
            else if (dateTempPart[1] == "November")
            {
                dateTempPart[1] = "11";
            }
            else if (dateTempPart[1] == "December")
            {
                dateTempPart[1] = "12";
            }
            DateTime dtime = new DateTime(Convert.ToInt32(dateTempPart[2]), Convert.ToInt32(dateTempPart[1]), Convert.ToInt32(dateTempPart[0]), Convert.ToInt32(hourTempPart[0]), Convert.ToInt32(hourTempPart[1]), 0);
            //dtimeFinalTanggal.Add(dtime);
            DataTable dt6 = new DataTable();
            //dt6 = con.getTabel("SELECT * FROM TopUp WHERE TadaNo='" + tempTadaNo + "' AND TanggalTopUp < '" + dtime.ToString("yyyy-MM-dd hh:mm") + "'");
            dt6 = con.getTabel("SELECT * FROM TopUp WHERE TadaNo='"+tempTadaNo+"' ");
            if(dataGridView1.CurrentRow.Cells[7].Value.ToString()!="TadaNo Not Found"){
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add(dt6.Rows[i][0], dt6.Rows[i][1], dt6.Rows[i][2], dt6.Rows[i][3]);
                }
            }
            
            /*if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != "TadaNo Not Found") {
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add(dt6.Rows[i][0], dt6.Rows[i][1], dt6.Rows[i][2], dt6.Rows[i][3]);
                }
            }*/
            

            //masukin angka total top up ke dalam textbox4
            int tempTotalTopUp = 0;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                tempTotalTopUp += Convert.ToInt32(dataGridView3.Rows[i].Cells[1].Value.ToString());
            }
            textBox4.Text = tempTotalTopUp.ToString();

            //masukin data redeem ke dt7
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt10 = new DataTable();
            
            dt7 = con.getTabel("SELECT dov.TadaNo,dov.NoOrder,dov.Terpakai FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='" + tempTadaNo + "' AND ov.StatusOrderVoucher='Success'");
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != "TadaNo Not Found")
            {
                for (int i = 0; i < dt7.Rows.Count; i++)
                {
                    dataGridView4.Rows.Add(dt7.Rows[i][0].ToString(), dt7.Rows[i][1].ToString(), dt7.Rows[i][2].ToString(), "Voucher");
                }
            }
            
            dt8 = con.getTabel("SELECT doi.TadaNo,doi.NoOrder,doi.Terpakai FROM DetailOrderItems doi, OrderItem oi WHERE doi.NoOrder=oi.NoOrder AND doi.TadaNo = '"+tempTadaNo+"' AND oi.StatusOrderItem = 'Success'");
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != "TadaNo Not Found")
            {
                for (int i = 0; i < dt8.Rows.Count; i++)
                {
                    dataGridView4.Rows.Add(dt8.Rows[i][0].ToString(), dt8.Rows[i][1].ToString(), dt8.Rows[i][2].ToString(), "Item");
                }
            }

            dt9 = con.getTabel("SELECT drc.TadaNo, drc.RedeemNo, drc.Terpakai FROM DetailRedeemCash drc, RedeemCash rc WHERE drc.RedeemNo = rc.RedeemNo AND drc.TadaNo = '"+tempTadaNo+"' AND rc.StatusRedeemCash= 'Success'");
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != "TadaNo Not Found")
            {
                for (int i = 0; i < dt9.Rows.Count; i++)
                {
                    dataGridView4.Rows.Add(dt9.Rows[i][0].ToString(), dt9.Rows[i][1].ToString(), dt9.Rows[i][2].ToString(), "Cash");
                }

                dt10 = con.getTabel("SELECT dd.TadaNo, dd.RedeemNo, dd.Terpakai FROM DetailDonate dd, Donate d WHERE dd.RedeemNo = d.RedeemNo AND dd.TadaNo = '" + tempTadaNo + "' AND d.StatusDonate = 'Success'");
                for (int i = 0; i < dt10.Rows.Count; i++)
                {
                    dataGridView4.Rows.Add(dt10.Rows[i][0].ToString(), dt10.Rows[i][1].ToString(), dt10.Rows[i][2].ToString(), "Donation");
                }
            }

            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != "TadaNo Not Found")
            {
                //jika diatasnya ada tada no yg sama, masukin k datagridview redeem
                for (int i = 0; i < dataGridView1.CurrentCell.RowIndex; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == dataGridView1.CurrentRow.Cells[3].Value.ToString() && dataGridView1.Rows[i].Cells[7].Value.ToString() == "Success")
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Voucher")
                        {
                            dataGridView4.Rows.Add(dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString(), "Voucher");
                        }
                        else if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Cash")
                        {
                            dataGridView4.Rows.Add(dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString(), "Cash");
                        }
                        else if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Donation")
                        {
                            dataGridView4.Rows.Add(dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString(), "Donation");
                        }
                        else
                        {
                            dataGridView4.Rows.Add(dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString(), "Item");
                        }
                    }
                }
            }

            int tempTotalRedeem = 0;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                tempTotalRedeem += Convert.ToInt32(dataGridView4.Rows[i].Cells[2].Value.ToString());
            }


            textBox5.Text = tempTotalRedeem.ToString();
            textBox6.Text = (Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text) - Convert.ToInt32(textBox5.Text)).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            /*int a = 0;
            try {
                dtt = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='110'");
                //dtt = con.getTabel("SELECT TadaNo From Tada WHERE TadaNo='010'");
                a = Convert.ToInt32( dtt.Rows[0][0].ToString());
            }
            catch(Exception ex){
                MessageBox.Show("alalala long");
            }*/
            /*for (int i = 0; i < dtt.Rows.Count;i++ )
            {
                if(dtt.Rows[i][0].ToString()=="0110"){
                    a++;
                }
            }
            if (a == 0)
            {
                MessageBox.Show("ga ada di database");
            }
            else {
                MessageBox.Show("ada di database");
            }*/
            //MessageBox.Show(""+noOrderRedeemNo.Count);
            DialogResult result1 = MessageBox.Show("Are you sure want to submit this file into database?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes) {
                label7.Text = "Loading...";
                for (int i = 0; i < noOrderRedeemNo.Count;i++ )
                {
                    if(tempFinalJenisOrder[i]=="Voucher"){
                        if(status[i]=="Success"){
                            
                            if (i == 0)
                            {
                                con.executeUpdate("INSERT INTO OrderVoucher VALUES('" + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalVoucher[i].Replace("'", "''") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalJumlahVoucher[i] + "','" + tempFinalPecahanVoucher[i] + "','" + tempFinalDelivery[i] + "','" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO OrderVoucher VALUES('" + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalVoucher[i].Replace("'", "''") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalJumlahVoucher[i] + "','" + tempFinalPecahanVoucher[i] + "','" + tempFinalDelivery[i] + "','" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailOrderVoucher VALUES('" + finalTadaNo[i] + "','" + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                        }
                        else if(status[i]=="Failed"){
                            
                            if (i == 0)
                            {
                                con.executeUpdate("INSERT INTO OrderVoucher VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalVoucher[i].Replace("'", "''") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalJumlahVoucher[i] + "','" + tempFinalPecahanVoucher[i] + "','" + tempFinalDelivery[i] + "','" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO OrderVoucher VALUES('F" +DateTime.Now.ToString("MMddyyyyHHmmss")+ noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalVoucher[i].Replace("'", "''") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalJumlahVoucher[i] + "','" + tempFinalPecahanVoucher[i] + "','" + tempFinalDelivery[i] + "','" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailOrderVoucher VALUES('" + finalTadaNo[i] + "','F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                        }
                    }
                    else if(tempFinalJenisOrder[i]=="Cash"){
                        if (status[i] == "Success" )
                        {
                            
                            if(i==0){
                                con.executeUpdate("INSERT INTO RedeemCash VALUES('" + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalNamaBank[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalNamaRekening[i].Replace("'", "''") + "','" + tempFinalNoRekening[i].Replace("'", "''") + "','" + tempFinalNamaCabang[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO RedeemCash VALUES('" + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalNamaBank[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalNamaRekening[i].Replace("'", "''") + "','" + tempFinalNoRekening[i].Replace("'", "''") + "','" + tempFinalNamaCabang[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailRedeemCash VALUES('" + finalTadaNo[i] + "','" + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                        }
                        else if(status[i] == "Failed"){
                            
                            if (i == 0)
                            {
                                con.executeUpdate("INSERT INTO RedeemCash VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalNamaBank[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalNamaRekening[i].Replace("'", "''") + "','" + tempFinalNoRekening[i].Replace("'", "''") + "','" + tempFinalNamaCabang[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO RedeemCash VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + tempFinalNamaBank[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalNamaRekening[i].Replace("'", "''") + "','" + tempFinalNoRekening[i].Replace("'", "''") + "','" + tempFinalNamaCabang[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailRedeemCash VALUES('" + finalTadaNo[i] + "','F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                        }
                    }
                    else if(tempFinalJenisOrder[i] == "Donation"){
                        if (status[i] == "Success" )
                        {
                            if(i==0){
                                con.executeUpdate("INSERT INTO Donate VALUES('" + noOrderRedeemNo[i] + "','" + tempFinalYayasan[i].Replace("'", "''") + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO Donate VALUES('" + noOrderRedeemNo[i] + "','" + tempFinalYayasan[i].Replace("'", "''") + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailDonate VALUES('" + finalTadaNo[i] + "','" + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                            
                        }
                        else if(status[i] == "Failed"){
                            if(i==0){
                                con.executeUpdate("INSERT INTO Donate VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + tempFinalYayasan[i].Replace("'", "''") + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO Donate VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + tempFinalYayasan[i].Replace("'", "''") + "','" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "','" + tempFinalClaimer[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailDonate VALUES('" + finalTadaNo[i] + "','F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                            
                        }
                    }
                    else if(tempFinalJenisOrder[i] == "Item"){
                        if (status[i] == "Success" )
                        {
                            if(i==0){
                                con.executeUpdate("INSERT INTO OrderItem VALUES('" + noOrderRedeemNo[i] + "','" + tempFinalClaimer[i].Replace("'", "''") + "', '" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "', '" + tempFinalKategori[i].Replace("'", "''") + "','" + tempFinalBarang[i].Replace("'", "''") + "','" + tempFinalJumlahItem[i] + "','" + tempFinalHargaItem[i] + "','" + tempFinalDelivery[i] + "', '" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO OrderItem VALUES('" + noOrderRedeemNo[i] + "','" + tempFinalClaimer[i].Replace("'", "''") + "', '" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "', '" + tempFinalKategori[i].Replace("'", "''") + "','" + tempFinalBarang[i].Replace("'", "''") + "','" + tempFinalJumlahItem[i] + "','" + tempFinalHargaItem[i] + "','" + tempFinalDelivery[i] + "', '" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailOrderItems VALUES('" + finalTadaNo[i] + "','" + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                            
                        }
                        else if( status[i] == "Failed"){
                            if (i == 0)
                            {
                                con.executeUpdate("INSERT INTO OrderItem VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + tempFinalClaimer[i].Replace("'", "''") + "', '" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "', '" + tempFinalKategori[i].Replace("'", "''") + "','" + tempFinalBarang[i].Replace("'", "''") + "','" + tempFinalJumlahItem[i] + "','" + tempFinalHargaItem[i] + "','" + tempFinalDelivery[i] + "', '" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            else if (noOrderRedeemNo[i] != noOrderRedeemNo[i - 1])
                            {
                                con.executeUpdate("INSERT INTO OrderItem VALUES('F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + tempFinalClaimer[i].Replace("'", "''") + "', '" + dtimeTempFinalTanggal[i].ToString("yyyy-MM-dd HH:mm") + "', '" + tempFinalKategori[i].Replace("'", "''") + "','" + tempFinalBarang[i].Replace("'", "''") + "','" + tempFinalJumlahItem[i] + "','" + tempFinalHargaItem[i] + "','" + tempFinalDelivery[i] + "', '" + tempFinalEmail[i].Replace("'", "''") + "','" + tempFinalTelepon[i].Replace("'", "''") + "','" + tempFinalAlamat[i].Replace("'", "''") + "','" + status[i] + "','" + MDIform.username + "')");
                            }
                            con.executeUpdate("INSERT INTO DetailOrderItems VALUES('" + finalTadaNo[i] + "','F" + DateTime.Now.ToString("MMddyyyyHHmmss") + noOrderRedeemNo[i] + "','" + finalTerpakai[i] + "')");
                            
                        }
                    }
                    
                }
                
            }
            flagSubmit = 2;
            label7.Text = "";
            MessageBox.Show("Submit Success!");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            noOrderRedeemNo.Clear();
            jenisOrder.Clear();
            finalTanggal.Clear();
            dtimeFinalTanggal.Clear();
            finalTadaNo.Clear();
            finalClaimer.Clear();
            finalTerpakai.Clear();

            tempFinalNoOrderRedeemNo.Clear();
            tempFinalJenisOrder.Clear();
            tempFinalTanggal.Clear();
            dtimeTempFinalTanggal.Clear();
            tempFinalTadaNo.Clear();
            tempFinalClaimer.Clear();
            tempFinalTerpakai.Clear();
            tempFinalVoucher.Clear();
            tempFinalJumlahVoucher.Clear();
            tempFinalPecahanVoucher.Clear();
            tempFinalDelivery.Clear();
            tempFinalEmail.Clear();
            tempFinalTelepon.Clear();
            tempFinalAlamat.Clear();
            tempFinalNamaBank.Clear();

            tempFinalNoRekening.Clear();
            tempFinalNamaCabang.Clear();
            tempFinalYayasan.Clear();
            tempFinalKategori.Clear();
            tempFinalBarang.Clear();
            tempFinalJumlahItem.Clear();
            tempFinalHargaItem.Clear();
            totalTopUp.Clear();
            totalRedeem.Clear();
            SaldoAwal.Clear();
            SaldoAkhir.Clear();
            status.Clear();
            temptemp.Clear();
            tempo.Clear();

            //Tampung worksheet Vouchers dari excel
            List<string> tadaVoucher = new List<string>();
            List<string> noOrderVoucher = new List<string>();
            List<string> tanggalVoucher = new List<string>();
            List<int> terpakaiVoucher = new List<int>();
            List<string> claimerVoucher = new List<string>();

            List<string> namaVoucher = new List<string>();
            List<int> jumlahVoucher = new List<int>();
            List<int> pecahanVoucher = new List<int>();
            List<int> deliveryVoucher = new List<int>();
            List<string> emailVoucher = new List<string>();
            List<string> teleponVoucher = new List<string>();
            List<string> alamatVoucher = new List<string>();

            //Tampung worksheet Cashs dari excel
            List<string> tadaCash = new List<string>();
            List<string> redeemNoCash = new List<string>();
            List<string> tanggalCash = new List<string>();
            List<int> terpakaiCash = new List<int>();
            List<string> claimerCash = new List<string>();

            List<string> teleponCash = new List<string>();
            List<string> namaBankCash = new List<string>();
            List<string> namaRekeningCash = new List<string>();
            List<string> noRekeningCash = new List<string>();
            List<string> namaCabangCash = new List<string>();

            //Tampung worksheet Donations dari excel
            List<string> tadaDonation = new List<string>();
            List<string> redeemNoDonation = new List<string>();
            List<string> tanggalDonation = new List<string>();
            List<int> terpakaiDonation = new List<int>();
            List<string> claimerDonation = new List<string>();

            List<string> yayasanDonation = new List<string>();

            //Tampung worksheet Items dari excel
            List<string> tadaItem = new List<string>();
            List<string> noOrderItem = new List<string>();
            List<string> tanggalItem = new List<string>();
            List<int> terpakaiItem = new List<int>();
            List<string> claimerItem = new List<string>();


            List<int> deliveryItem = new List<int>();
            List<string> emailItem = new List<string>();
            List<string> teleponItem = new List<string>();
            List<string> alamatItem = new List<string>();
            List<string> kategoriItem = new List<string>();
            List<string> barangItem = new List<string>();
            List<int> jumlahItem = new List<int>();
            List<int> hargaItem = new List<int>();

            int tempSaldoAkhir = 0;
            int tempTotalTopUp = 0;
            int tempTotalRedeem = 0;

            //hapus isi dataGridView1
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            //hapus isi dataGridView2
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

            //hapus list Vouchers
            tadaVoucher.Clear();
            noOrderVoucher.Clear();
            tanggalVoucher.Clear();
            terpakaiVoucher.Clear();
            claimerVoucher.Clear();

            namaVoucher.Clear();
            jumlahVoucher.Clear();
            pecahanVoucher.Clear();
            deliveryVoucher.Clear();
            emailVoucher.Clear();
            teleponVoucher.Clear();
            alamatVoucher.Clear();

            //hapus list Cashs
            tadaCash.Clear();
            redeemNoCash.Clear();
            tanggalCash.Clear();
            terpakaiCash.Clear();
            claimerCash.Clear();

            teleponCash.Clear();
            namaBankCash.Clear();
            namaRekeningCash.Clear();
            namaCabangCash.Clear();
            noRekeningCash.Clear();

            tremp.Clear();

            string pathConn = "";

            //validasi jika file kosong
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please choose the file.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //validasi file harus .xls atau .xlsx
            else if (!textBox1.Text.EndsWith(".xls") == true && !textBox1.Text.EndsWith(".xlsx") == true)
            {
                MessageBox.Show("Wrong file format, The file format must be .xls or .xlsx", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
            //validasi format dalam file harus sesuai
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
                    //string pathConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;\";";
                    OleDbConnection conn = new OleDbConnection(pathConn);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    OleDbDataAdapter myDataAdapter1 = new OleDbDataAdapter("Select * from [Vouchers$]", conn);
                    OleDbDataAdapter myDataAdapter2 = new OleDbDataAdapter("Select * from [Cashs$]", conn);
                    OleDbDataAdapter myDataAdapter3 = new OleDbDataAdapter("Select * from [Donations$]", conn);
                    OleDbDataAdapter myDataAdapter4 = new OleDbDataAdapter("Select * from [Items$]", conn);

                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();

                    DataTable dtTopUp = new DataTable();
                    DataTable dtOrderVoucher = new DataTable();
                    DataTable dtRedeemCash = new DataTable();
                    DataTable dtRedeemDonation = new DataTable();
                    DataTable dtOrderItem = new DataTable();
                    DataTable dtSaldoAwal = new DataTable();

                    DataTable dtTampungNoOrderAID = new DataTable();
                    DataTable dtTampungNoTadaNoInDb = new DataTable();

                    myDataAdapter1.Fill(dt);
                    myDataAdapter2.Fill(dt2);
                    myDataAdapter3.Fill(dt3);
                    myDataAdapter4.Fill(dt4);
                    label7.Text = "Loading...";
                    //masukin sheet vouchers ke dataGridView
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][1].ToString() != "")
                        {
                            tadaVoucher.Add(dt.Rows[i][1].ToString());
                            noOrderVoucher.Add(dt.Rows[i][14].ToString());
                            tanggalVoucher.Add(dt.Rows[i][6].ToString());
                            terpakaiVoucher.Add(Convert.ToInt32(dt.Rows[i][5].ToString()));
                            claimerVoucher.Add(dt.Rows[i][7].ToString());
                            //int index = 0;
                            //int tempAdaPetik = 0;
                            /*for (int k = 0; k < dt.Rows[i][8].ToString().Length; i++)
                            {
                                if (dt.Rows[i][8].ToString()[k]=='\'')
                                {
                                    index = k;
                                    tempAdaPetik++;
                                }
                            }
                            if(tempAdaPetik!=0){
                                namaVoucher.Add(dt.Rows[i][8].ToString().Substring(0, index) + " " + dt.Rows[i][8].ToString().Substring(index+1));
                            }
                            else if(tempAdaPetik==0){
                                namaVoucher.Add(dt.Rows[i][8].ToString());
                            }*/
                            if (dt.Rows[i][8].ToString() == "Carl's Jr.")
                            {
                                namaVoucher.Add("Carl s Jr.");
                            }
                            else if (dt.Rows[i][8].ToString() != "Carl's Jr.")
                            {
                                namaVoucher.Add(dt.Rows[i][8].ToString());
                            }
                            
                            if (dt.Rows[i][9].ToString() == "")
                            {
                                jumlahVoucher.Add(0);
                            }
                            else if (dt.Rows[i][9].ToString() != "")
                            {
                                jumlahVoucher.Add(Convert.ToInt32(dt.Rows[i][9].ToString()));
                            }
                            if (dt.Rows[i][10].ToString() == "")
                            {
                                pecahanVoucher.Add(0);
                            }
                            else if (dt.Rows[i][10].ToString() != "")
                            {
                                pecahanVoucher.Add(Convert.ToInt32(dt.Rows[i][10].ToString()));
                            }
                            if (dt.Rows[i][11].ToString() == "")
                            {
                                deliveryVoucher.Add(0);
                            }
                            else if (dt.Rows[i][11].ToString() != "")
                            {
                                deliveryVoucher.Add(Convert.ToInt32(dt.Rows[i][11].ToString()));
                            }
                            emailVoucher.Add(dt.Rows[i][16].ToString());
                            teleponVoucher.Add(dt.Rows[i][17].ToString());
                            if (dt.Rows[i][18].ToString().Length>250)
                            {
                                alamatVoucher.Add(dt.Rows[i][18].ToString().Substring(0,250));
                            }
                            else{
                                alamatVoucher.Add(dt.Rows[i][18].ToString());
                            }
                            

                            //merge cell
                            if (dt.Rows[i][14].ToString() == "")
                            {
                                noOrderVoucher[i] = noOrderVoucher[i - 1];
                                claimerVoucher[i] = claimerVoucher[i - 1];

                                namaVoucher[i] = namaVoucher[i - 1];
                                jumlahVoucher[i] = jumlahVoucher[i - 1];
                                pecahanVoucher[i] = pecahanVoucher[i - 1];
                                pecahanVoucher[i] = pecahanVoucher[i - 1];
                                deliveryVoucher[i] = deliveryVoucher[i - 1];
                                emailVoucher[i] = emailVoucher[i - 1];
                                teleponVoucher[i] = teleponVoucher[i - 1];
                                alamatVoucher[i] = alamatVoucher[i - 1];
                            }
                            dataGridView1.Rows.Add(noOrderVoucher[i], "Voucher", tanggalVoucher[i], tadaVoucher[i], claimerVoucher[i], terpakaiVoucher[i], "", "");
                            dataGridView2.Rows.Add(noOrderVoucher[i], "Voucher", tanggalVoucher[i], tadaVoucher[i], claimerVoucher[i], terpakaiVoucher[i], namaVoucher[i], jumlahVoucher[i], pecahanVoucher[i], deliveryVoucher[i], emailVoucher[i], teleponVoucher[i], alamatVoucher[i], "", "", "", "", "", "", "", "", "");
                        }
                    }
                    //masukin sheet cashs ke dataGridView
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i][1].ToString() != "")
                        {
                            tadaCash.Add(dt2.Rows[i][1].ToString());
                            redeemNoCash.Add(dt2.Rows[i][10].ToString());
                            tanggalCash.Add(dt2.Rows[i][6].ToString());
                            terpakaiCash.Add(Convert.ToInt32(dt2.Rows[i][5].ToString()));
                            claimerCash.Add(dt2.Rows[i][7].ToString());

                            teleponCash.Add(dt2.Rows[i][11].ToString());
                            namaBankCash.Add(dt2.Rows[i][12].ToString());
                            namaRekeningCash.Add(dt2.Rows[i][13].ToString());
                            noRekeningCash.Add(dt2.Rows[i][14].ToString());
                            namaCabangCash.Add(dt2.Rows[i][15].ToString());

                            if (dt2.Rows[i][10].ToString() == "")
                            {
                                redeemNoCash[i] = redeemNoCash[i - 1];
                                claimerCash[i] = claimerCash[i - 1];

                                teleponCash[i] = teleponCash[i - 1];
                                namaBankCash[i] = namaBankCash[i - 1];
                                namaRekeningCash[i] = namaRekeningCash[i - 1];
                                noRekeningCash[i] = noRekeningCash[i - 1];
                                namaCabangCash[i] = namaCabangCash[i - 1];
                            }

                            dataGridView1.Rows.Add(redeemNoCash[i], "Cash", tanggalCash[i], tadaCash[i], claimerCash[i], terpakaiCash[i], "", "");
                            dataGridView2.Rows.Add(redeemNoCash[i], "Cash", tanggalCash[i], tadaCash[i], claimerCash[i], terpakaiCash[i], "", "", "", "", "", teleponCash[i], "", namaBankCash[i], namaRekeningCash[i], noRekeningCash[i], namaCabangCash[i], "", "", "", "", "");
                        }
                    }
                    //masukin sheet donation ke dataGridView
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        if (dt3.Rows[i][1].ToString() != "")
                        {
                            tadaDonation.Add(dt3.Rows[i][1].ToString());
                            redeemNoDonation.Add(dt3.Rows[i][9].ToString());
                            tanggalDonation.Add(dt3.Rows[i][6].ToString());
                            terpakaiDonation.Add(Convert.ToInt32(dt3.Rows[i][5].ToString()));
                            claimerDonation.Add(dt3.Rows[i][7].ToString());

                            yayasanDonation.Add(dt3.Rows[i][10].ToString());

                            if (dt3.Rows[i][9].ToString() == "")
                            {
                                redeemNoDonation[i] = redeemNoDonation[i - 1];
                                claimerDonation[i] = claimerDonation[i - 1];

                                yayasanDonation[i] = yayasanDonation[i - 1];
                            }
                            dataGridView1.Rows.Add(redeemNoDonation[i], "Donation", tanggalDonation[i], tadaDonation[i], claimerDonation[i], terpakaiDonation[i], "", "");
                            dataGridView2.Rows.Add(redeemNoDonation[i], "Donation", tanggalDonation[i], tadaDonation[i], claimerDonation[i], terpakaiDonation[i], "", "", "", "", "", "", "", "", "", "", "", yayasanDonation[i], "", "", "", "");
                        }
                    }
                    //masukin sheet items ke dataGridView
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        if (dt4.Rows[i][1].ToString() != "")
                        {
                            tadaItem.Add(dt4.Rows[i][1].ToString());
                            noOrderItem.Add(dt4.Rows[i][15].ToString());
                            tanggalItem.Add(dt4.Rows[i][6].ToString());
                            terpakaiItem.Add(Convert.ToInt32(dt4.Rows[i][5].ToString()));
                            claimerItem.Add(dt4.Rows[i][7].ToString());

                            if (dt4.Rows[i][12].ToString() == "")
                            {
                                deliveryItem.Add(0);
                            }
                            else if (dt4.Rows[i][12].ToString() != "")
                            {
                                deliveryItem.Add(Convert.ToInt32(dt4.Rows[i][12].ToString()));
                            }
                            emailItem.Add(dt4.Rows[i][17].ToString());
                            teleponItem.Add(dt4.Rows[i][18].ToString());
                            if (dt4.Rows[i][19].ToString().Length > 250)
                            {
                                alamatItem.Add(dt4.Rows[i][19].ToString().Substring(0,250));
                            }
                            else {
                                alamatItem.Add(dt4.Rows[i][19].ToString());
                            }
                            
                            kategoriItem.Add(dt4.Rows[i][8].ToString());
                            barangItem.Add(dt4.Rows[i][9].ToString());
                            if (dt4.Rows[i][10].ToString() == "")
                            {
                                jumlahItem.Add(0);
                            }
                            else if (dt4.Rows[i][10].ToString() != "")
                            {
                                jumlahItem.Add(Convert.ToInt32(dt4.Rows[i][10].ToString()));
                            }
                            if (dt4.Rows[i][11].ToString() == "")
                            {
                                hargaItem.Add(0);
                            }
                            else if (dt4.Rows[i][11].ToString() != "")
                            {
                                hargaItem.Add(Convert.ToInt32(dt4.Rows[i][11].ToString()));
                            }

                            if (dt4.Rows[i][15].ToString() == "")
                            {
                                noOrderItem[i] = noOrderItem[i - 1];
                                claimerItem[i] = claimerItem[i - 1];

                                deliveryItem[i] = deliveryItem[i - 1];
                                emailItem[i] = emailItem[i - 1];
                                teleponItem[i] = teleponItem[i - 1];
                                alamatItem[i] = alamatItem[i - 1];
                                kategoriItem[i] = kategoriItem[i - 1];
                                barangItem[i] = barangItem[i - 1];
                                jumlahItem[i] = jumlahItem[i - 1];
                                hargaItem[i] = hargaItem[i - 1];
                            }
                            dataGridView1.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "");
                            dataGridView2.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "", "", deliveryItem[i], emailItem[i], teleponItem[i], alamatItem[i], "", "", "", "", "", kategoriItem[i], barangItem[i], jumlahItem[i], hargaItem[i]);
                        }
                    }
                    /*
                    //buat list baru keseluruhan
                    List<string> noOrderRedeemNo = new List<string>();
                    List<string> jenisOrder = new List<string>();
                    List<string> finalTanggal = new List<string>();
                    List<DateTime> dtimeFinalTanggal = new List<DateTime>();
                    List<string> finalTadaNo = new List<string>();
                    List<string> finalClaimer = new List<string>();
                    List<int> finalTerpakai = new List<int>();

                    //buat list baru keseluruhan di table bawah
                    List<string> tempFinalNoOrderRedeemNo = new List<string>();
                    List<string> tempFinalJenisOrder = new List<string>();
                    List<string> tempFinalTanggal = new List<string>();
                    List<DateTime> dtimeTempFinalTanggal = new List<DateTime>();
                    List<string> tempFinalTadaNo = new List<string>();
                    List<string> tempFinalClaimer = new List<string>();
                    List<int> tempFinalTerpakai = new List<int>();
                    List<string> tempFinalVoucher = new List<string>();
                    List<int> tempFinalJumlahVoucher = new List<int>();
                    List<int> tempFinalPecahanVoucher = new List<int>();
                    List<int> tempFinalDelivery = new List<int>();
                    List<string> tempFinalEmail = new List<string>();
                    List<string> tempFinalTelepon = new List<string>();
                    List<string> tempFinalAlamat = new List<string>();
                    List<string> tempFinalNamaBank = new List<string>();
                    List<string> tempFinalNamaRekening = new List<string>();
                    List<string> tempFinalNoRekening = new List<string>();
                    List<string> tempFinalNamaCabang = new List<string>();
                    List<string> tempFinalYayasan = new List<string>();
                    List<string> tempFinalKategori = new List<string>();
                    List<string> tempFinalBarang = new List<string>();
                    List<int> tempFinalJumlahItem = new List<int>();
                    List<int> tempFinalHargaItem = new List<int>();*/

                    //masukin semua data ke list baru
                    for (int a = 0; a < dataGridView1.Rows.Count; a++)
                    {
                        noOrderRedeemNo.Add(dataGridView1.Rows[a].Cells[0].Value.ToString());
                        jenisOrder.Add(dataGridView1.Rows[a].Cells[1].Value.ToString());
                        finalTanggal.Add(dataGridView1.Rows[a].Cells[2].Value.ToString());
                        finalTadaNo.Add(dataGridView1.Rows[a].Cells[3].Value.ToString());
                        finalClaimer.Add(dataGridView1.Rows[a].Cells[4].Value.ToString());
                        finalTerpakai.Add(Convert.ToInt32(dataGridView1.Rows[a].Cells[5].Value.ToString()));
                        string[] dateTemp = dataGridView1.Rows[a].Cells[2].Value.ToString().Split(' ');
                        string[] dateTempPart = dateTemp[0].Split('-');
                        string[] hourTempPart = dateTemp[1].Split(':');
                        if (dateTempPart[1] == "January")
                        {
                            dateTempPart[1] = "1";
                        }
                        else if (dateTempPart[1] == "February")
                        {
                            dateTempPart[1] = "2";
                        }
                        else if (dateTempPart[1] == "March")
                        {
                            dateTempPart[1] = "3";
                        }
                        else if (dateTempPart[1] == "April")
                        {
                            dateTempPart[1] = "4";
                        }
                        else if (dateTempPart[1] == "May")
                        {
                            dateTempPart[1] = "5";
                        }
                        else if (dateTempPart[1] == "June")
                        {
                            dateTempPart[1] = "6";
                        }
                        else if (dateTempPart[1] == "July")
                        {
                            dateTempPart[1] = "7";
                        }
                        else if (dateTempPart[1] == "August")
                        {
                            dateTempPart[1] = "8";
                        }
                        else if (dateTempPart[1] == "September")
                        {
                            dateTempPart[1] = "9";
                        }
                        else if (dateTempPart[1] == "October")
                        {
                            dateTempPart[1] = "10";
                        }
                        else if (dateTempPart[1] == "November")
                        {
                            dateTempPart[1] = "11";
                        }
                        else if (dateTempPart[1] == "December")
                        {
                            dateTempPart[1] = "12";
                        }
                        DateTime dtime = new DateTime(Convert.ToInt32(dateTempPart[2]), Convert.ToInt32(dateTempPart[1]), Convert.ToInt32(dateTempPart[0]), Convert.ToInt32(hourTempPart[0]), Convert.ToInt32(hourTempPart[1]), 0);
                        dtimeFinalTanggal.Add(dtime);
                    }

                    for (int b = 0; b < dataGridView2.Rows.Count; b++)
                    {
                        tempFinalNoOrderRedeemNo.Add(dataGridView2.Rows[b].Cells[0].Value.ToString());
                        tempFinalJenisOrder.Add(dataGridView2.Rows[b].Cells[1].Value.ToString());
                        tempFinalTanggal.Add(dataGridView2.Rows[b].Cells[2].Value.ToString());
                        tempFinalTadaNo.Add(dataGridView2.Rows[b].Cells[3].Value.ToString());
                        tempFinalClaimer.Add(dataGridView2.Rows[b].Cells[4].Value.ToString());
                        tempFinalTerpakai.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[5].Value.ToString()));
                        tempFinalVoucher.Add(dataGridView2.Rows[b].Cells[6].Value.ToString());
                        if (dataGridView2.Rows[b].Cells[7].Value.ToString() != "")
                        {
                            tempFinalJumlahVoucher.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[7].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[7].Value.ToString() == "")
                        {
                            tempFinalJumlahVoucher.Add(0);
                        }
                        if (dataGridView2.Rows[b].Cells[8].Value.ToString() != "")
                        {
                            tempFinalPecahanVoucher.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[8].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[8].Value.ToString() == "")
                        {
                            tempFinalPecahanVoucher.Add(0);
                        }
                        if (dataGridView2.Rows[b].Cells[9].Value.ToString() != "")
                        {
                            tempFinalDelivery.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[9].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[9].Value.ToString() == "")
                        {
                            tempFinalDelivery.Add(0);
                        }

                        tempFinalEmail.Add(dataGridView2.Rows[b].Cells[10].Value.ToString());
                        tempFinalTelepon.Add(dataGridView2.Rows[b].Cells[11].Value.ToString());
                        if (dataGridView2.Rows[b].Cells[12].Value.ToString().Length > 250)
                        {
                            tempFinalAlamat.Add(dataGridView2.Rows[b].Cells[12].Value.ToString().Substring(0,250));
                        }
                        else {
                            tempFinalAlamat.Add(dataGridView2.Rows[b].Cells[12].Value.ToString());
                        }
                        
                        tempFinalNamaBank.Add(dataGridView2.Rows[b].Cells[13].Value.ToString());
                        tempFinalNamaRekening.Add(dataGridView2.Rows[b].Cells[14].Value.ToString());
                        tempFinalNoRekening.Add(dataGridView2.Rows[b].Cells[15].Value.ToString());
                        tempFinalNamaCabang.Add(dataGridView2.Rows[b].Cells[16].Value.ToString());
                        tempFinalYayasan.Add(dataGridView2.Rows[b].Cells[17].Value.ToString());
                        tempFinalKategori.Add(dataGridView2.Rows[b].Cells[18].Value.ToString());
                        tempFinalBarang.Add(dataGridView2.Rows[b].Cells[19].Value.ToString());
                        //tempFinalJumlahItem.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[20].Value.ToString()));
                        if (dataGridView2.Rows[b].Cells[20].Value.ToString() != "")
                        {
                            tempFinalJumlahItem.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[20].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[20].Value.ToString() == "")
                        {
                            tempFinalJumlahItem.Add(0);
                        }
                        if (dataGridView2.Rows[b].Cells[21].Value.ToString() != "")
                        {
                            tempFinalHargaItem.Add(Convert.ToInt32(dataGridView2.Rows[b].Cells[21].Value.ToString()));
                        }
                        else if (dataGridView2.Rows[b].Cells[21].Value.ToString() == "")
                        {
                            tempFinalHargaItem.Add(0);
                        }

                        string[] dateTemp = dataGridView2.Rows[b].Cells[2].Value.ToString().Split(' ');
                        string[] dateTempPart = dateTemp[0].Split('-');
                        string[] hourTempPart = dateTemp[1].Split(':');
                        if (dateTempPart[1] == "January")
                        {
                            dateTempPart[1] = "1";
                        }
                        else if (dateTempPart[1] == "February")
                        {
                            dateTempPart[1] = "2";
                        }
                        else if (dateTempPart[1] == "March")
                        {
                            dateTempPart[1] = "3";
                        }
                        else if (dateTempPart[1] == "April")
                        {
                            dateTempPart[1] = "4";
                        }
                        else if (dateTempPart[1] == "May")
                        {
                            dateTempPart[1] = "5";
                        }
                        else if (dateTempPart[1] == "June")
                        {
                            dateTempPart[1] = "6";
                        }
                        else if (dateTempPart[1] == "July")
                        {
                            dateTempPart[1] = "7";
                        }
                        else if (dateTempPart[1] == "August")
                        {
                            dateTempPart[1] = "8";
                        }
                        else if (dateTempPart[1] == "September")
                        {
                            dateTempPart[1] = "9";
                        }
                        else if (dateTempPart[1] == "October")
                        {
                            dateTempPart[1] = "10";
                        }
                        else if (dateTempPart[1] == "November")
                        {
                            dateTempPart[1] = "11";
                        }
                        else if (dateTempPart[1] == "December")
                        {
                            dateTempPart[1] = "12";
                        }
                        DateTime dtime = new DateTime(Convert.ToInt32(dateTempPart[2]), Convert.ToInt32(dateTempPart[1]), Convert.ToInt32(dateTempPart[0]), Convert.ToInt32(hourTempPart[0]), Convert.ToInt32(hourTempPart[1]), 0);
                        dtimeTempFinalTanggal.Add(dtime);
                    }

                    //sorting based on tanggal
                    for (int x = 0; x < noOrderRedeemNo.Count; x++)
                    {
                        for (int y = noOrderRedeemNo.Count - 1; y > x; y--)
                        {
                            int result = DateTime.Compare(dtimeFinalTanggal[y - 1], dtimeFinalTanggal[y]);
                            if (result > 0)
                            {
                                DateTime dateswap = dtimeFinalTanggal[y - 1];
                                dtimeFinalTanggal[y - 1] = dtimeFinalTanggal[y];
                                dtimeFinalTanggal[y] = dateswap;

                                string swapNoOrderRedeemNo = noOrderRedeemNo[y - 1];
                                noOrderRedeemNo[y - 1] = noOrderRedeemNo[y];
                                noOrderRedeemNo[y] = swapNoOrderRedeemNo;

                                string swapJenisOrder = jenisOrder[y - 1];
                                jenisOrder[y - 1] = jenisOrder[y];
                                jenisOrder[y] = swapJenisOrder;

                                string swapFinalTanggal = finalTanggal[y - 1];
                                finalTanggal[y - 1] = finalTanggal[y];
                                finalTanggal[y] = swapFinalTanggal;

                                string swapFinalTadaNo = finalTadaNo[y - 1];
                                finalTadaNo[y - 1] = finalTadaNo[y];
                                finalTadaNo[y] = swapFinalTadaNo;

                                string swapFinalClaimer = finalClaimer[y - 1];
                                finalClaimer[y - 1] = finalClaimer[y];
                                finalClaimer[y] = swapFinalClaimer;

                                int swapFinalTerpakai = finalTerpakai[y - 1];
                                finalTerpakai[y - 1] = finalTerpakai[y];
                                finalTerpakai[y] = swapFinalTerpakai;
                            }
                        }
                    }

                    for (int x = 0; x < tempFinalNoOrderRedeemNo.Count; x++)
                    {
                        for (int y = tempFinalNoOrderRedeemNo.Count - 1; y > x; y--)
                        {
                            int result = DateTime.Compare(dtimeTempFinalTanggal[y - 1], dtimeTempFinalTanggal[y]);
                            if (result > 0)
                            {
                                DateTime dateswap = dtimeTempFinalTanggal[y - 1];
                                dtimeTempFinalTanggal[y - 1] = dtimeTempFinalTanggal[y];
                                dtimeTempFinalTanggal[y] = dateswap;

                                string swapTempFinalNoOrderRedeemNo = tempFinalNoOrderRedeemNo[y - 1];
                                tempFinalNoOrderRedeemNo[y - 1] = tempFinalNoOrderRedeemNo[y];
                                tempFinalNoOrderRedeemNo[y] = swapTempFinalNoOrderRedeemNo;

                                string swapTempFinalJenisOrder = tempFinalJenisOrder[y - 1];
                                tempFinalJenisOrder[y - 1] = tempFinalJenisOrder[y];
                                tempFinalJenisOrder[y] = swapTempFinalJenisOrder;

                                string swapTempFinalTanggal = tempFinalTanggal[y - 1];
                                tempFinalTanggal[y - 1] = tempFinalTanggal[y];
                                tempFinalTanggal[y] = swapTempFinalTanggal;

                                string swapTempFinalTadaNo = tempFinalTadaNo[y - 1];
                                tempFinalTadaNo[y - 1] = tempFinalTadaNo[y];
                                tempFinalTadaNo[y] = swapTempFinalTadaNo;

                                string swapTempFinalClaimer = tempFinalClaimer[y - 1];
                                tempFinalClaimer[y - 1] = tempFinalClaimer[y];
                                tempFinalClaimer[y] = swapTempFinalClaimer;

                                int swapTempFinalTerpakai = tempFinalTerpakai[y - 1];
                                tempFinalTerpakai[y - 1] = tempFinalTerpakai[y];
                                tempFinalTerpakai[y] = swapTempFinalTerpakai;

                                string swapTempFinalVoucher = tempFinalVoucher[y - 1];
                                tempFinalVoucher[y - 1] = tempFinalVoucher[y];
                                tempFinalVoucher[y] = swapTempFinalVoucher;

                                int swapTempFinalJumlahVoucher = tempFinalJumlahVoucher[y - 1];
                                tempFinalJumlahVoucher[y - 1] = tempFinalJumlahVoucher[y];
                                tempFinalJumlahVoucher[y] = swapTempFinalJumlahVoucher;

                                int swapTempFinalPecahanVoucher = tempFinalPecahanVoucher[y - 1];
                                tempFinalPecahanVoucher[y - 1] = tempFinalPecahanVoucher[y];
                                tempFinalPecahanVoucher[y] = swapTempFinalPecahanVoucher;

                                int swapTempFinalDelivery = tempFinalDelivery[y - 1];
                                tempFinalDelivery[y - 1] = tempFinalDelivery[y];
                                tempFinalDelivery[y] = swapTempFinalDelivery;

                                string swapTempFinalEmail = tempFinalEmail[y - 1];
                                tempFinalEmail[y - 1] = tempFinalEmail[y];
                                tempFinalEmail[y] = swapTempFinalEmail;

                                string swapTempFinalTelepon = tempFinalTelepon[y - 1];
                                tempFinalTelepon[y - 1] = tempFinalTelepon[y];
                                tempFinalTelepon[y] = swapTempFinalTelepon;

                                string swapTempFinalAlamat = tempFinalAlamat[y - 1];
                                tempFinalAlamat[y - 1] = tempFinalAlamat[y];
                                tempFinalAlamat[y] = swapTempFinalAlamat;

                                string swapTempFinalNamaBank = tempFinalNamaBank[y - 1];
                                tempFinalNamaBank[y - 1] = tempFinalNamaBank[y];
                                tempFinalNamaBank[y] = swapTempFinalNamaBank;

                                string swapTempFinalNamaRekening = tempFinalNamaRekening[y - 1];
                                tempFinalNamaRekening[y - 1] = tempFinalNamaRekening[y];
                                tempFinalNamaRekening[y] = swapTempFinalNamaRekening;

                                string swapTempFinalNoRekenig = tempFinalNoRekening[y - 1];
                                tempFinalNoRekening[y - 1] = tempFinalNoRekening[y];
                                tempFinalNoRekening[y] = swapTempFinalNoRekenig;

                                string swapTempFinalNamaCabang = tempFinalNamaCabang[y - 1];
                                tempFinalNamaCabang[y - 1] = tempFinalNamaCabang[y];
                                tempFinalNamaCabang[y] = swapTempFinalNamaCabang;

                                string swapTempFinalYayasan = tempFinalYayasan[y - 1];
                                tempFinalYayasan[y - 1] = tempFinalYayasan[y];
                                tempFinalYayasan[y] = swapTempFinalYayasan;

                                string swapTempFinalKategori = tempFinalKategori[y - 1];
                                tempFinalKategori[y - 1] = tempFinalKategori[y];
                                tempFinalKategori[y] = swapTempFinalKategori;

                                string swapTempFinalBarang = tempFinalBarang[y - 1];
                                tempFinalBarang[y - 1] = tempFinalBarang[y];
                                tempFinalBarang[y] = swapTempFinalBarang;

                                int swapTempFinalJumlahItem = tempFinalJumlahItem[y - 1];
                                tempFinalJumlahItem[y - 1] = tempFinalJumlahItem[y];
                                tempFinalJumlahItem[y] = swapTempFinalJumlahItem;

                                int swapTempFinalHargaItem = tempFinalHargaItem[y - 1];
                                tempFinalHargaItem[y - 1] = tempFinalHargaItem[y];
                                tempFinalHargaItem[y] = swapTempFinalHargaItem;
                            }
                        }
                    }
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();

                    dataGridView2.Rows.Clear();
                    dataGridView2.Refresh();

                    for (int yy = 0; yy < noOrderRedeemNo.Count; yy++)
                    {
                        dtTampungNoTadaNoInDb = con.getTabel("SELECT TadaNo FROM Tada WHERE TadaNo='" + finalTadaNo[yy] + "'");
                        if (dtTampungNoTadaNoInDb.Rows.Count == 0)
                        {
                            tempo.Add(1);
                        }
                        else
                        {
                            tempo.Add(0);
                        }
                    }

                    for (int zz = 0; zz < noOrderRedeemNo.Count; zz++)
                    {
                        if (jenisOrder[zz] == "Voucher")
                        {
                            dtTampungNoOrderAID = con.getTabel("SELECT NoOrder FROM OrderVoucher WHERE NoOrder='" + noOrderRedeemNo[zz] + "' AND StatusOrderVoucher='Success'");
                        }
                        else if (jenisOrder[zz] == "Cash")
                        {
                            dtTampungNoOrderAID = con.getTabel("SELECT RedeemNo FROM RedeemCash WHERE RedeemNo='" + noOrderRedeemNo[zz] + "' AND StatusRedeemCash='Success'");
                        }
                        else if (jenisOrder[zz] == "Item")
                        {
                            dtTampungNoOrderAID = con.getTabel("SELECT NoOrder FROM OrderItem WHERE NoOrder='" + noOrderRedeemNo[zz] + "' AND StatusOrderItem='Success'");
                        }
                        else
                        {
                            dtTampungNoOrderAID = con.getTabel("SELECT RedeemNo FROM Donate WHERE RedeemNo='" + noOrderRedeemNo[zz] + "' AND StatusDonate='Success'");
                        }
                        if (dtTampungNoOrderAID.Rows.Count > 0)
                        {
                            temptemp.Add(1);
                        }

                        else
                        {
                            temptemp.Add(0);
                        }
                    }

                    for (int jj = 0; jj < noOrderRedeemNo.Count; jj++)
                    {
                        int wowo = 0;
                        for (int yx = 0; yx < jj; yx++)
                        {
                            if (jenisOrder[jj] == jenisOrder[yx] && noOrderRedeemNo[jj] == noOrderRedeemNo[yx]&&finalClaimer[jj]!=finalClaimer[yx])
                            {
                                wowo++;
                            }
                        }
                        if (wowo > 0)
                        {
                            tremp.Add(1);
                        }
                        else
                        {
                            tremp.Add(0);
                        }
                    }

                    for (int t = 0; t < noOrderRedeemNo.Count; t++)
                    {
                        dtSaldoAwal = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='" + finalTadaNo[t] + "'");
                        //MessageBox.Show(dtSaldoAwal.Rows[0][0].ToString() + t );
                        //dtSaldoAwal = con.getTabel("SELECT TadaNo FROM Tada WHERE TadaNo='"+finalTadaNo[t]+"'");
                        /*if (dtSaldoAwal.Rows.Count== 0)
                        {
                            MessageBox.Show(t + " " + finalTadaNo[t] + tempFinalJenisOrder[t]);
                        }*/
                        /*else {
                            SaldoAwal.Add(Convert.ToInt32(dtSaldoAwal.Rows[0][0].ToString()));
                        }*/

                        if (tempo[t] == 0)
                        {
                            SaldoAwal.Add(Convert.ToInt32(dtSaldoAwal.Rows[0][0].ToString()));
                        }
                        else if (tempo[t] == 1)
                        {
                            SaldoAwal.Add(0);
                        }
                        //string[] test = finalTanggal[t].Split(' ');
                        dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + finalTadaNo[t] + "' ");
                        //AND TanggalTopUp < '" + dtimeTempFinalTanggal[t].ToString("yyyy-MM-dd hh:mm") + "'
                        tempTotalTopUp = 0;
                        for (int k = 0; k < dtTopUp.Rows.Count; k++)
                        {
                            tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString());
                        }
                        if (tempo[t] == 1)
                        {
                            totalTopUp.Add(0);
                        }
                        else if (tempo[t] == 0)
                        {
                            totalTopUp.Add(tempTotalTopUp);
                        }

                        dtOrderVoucher = con.getTabel("SELECT dov.TadaNo,dov.NoOrder,dov.Terpakai FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='" + finalTadaNo[t] + "' AND ov.StatusOrderVoucher='Success'");
                        tempTotalRedeem = 0;

                        for (int m = 0; m < dtOrderVoucher.Rows.Count; m++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtOrderVoucher.Rows[m][2].ToString());
                        }
                        //totalRedeem.Add(tempTotalRedeem);


                        dtRedeemCash = con.getTabel("SELECT drc.TadaNo, drc.RedeemNo, drc.Terpakai FROM DetailRedeemCash drc, RedeemCash rc WHERE drc.RedeemNo = rc.RedeemNo AND drc.TadaNo = '" + finalTadaNo[t] + "' AND rc.StatusRedeemCash= 'Success'");
                        //tempTotalRedeem = 0;
                        for (int j = 0; j < dtRedeemCash.Rows.Count; j++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtRedeemCash.Rows[j][2].ToString());
                        }
                        //totalRedeem.Add(tempTotalRedeem);


                        dtRedeemDonation = con.getTabel("SELECT dd.TadaNo, dd.RedeemNo, dd.Terpakai FROM DetailDonate dd, Donate d WHERE dd.RedeemNo = d.RedeemNo AND dd.TadaNo = '" + finalTadaNo[t] + "' AND d.StatusDonate = 'Success'");
                        //tempTotalRedeem = 0;
                        for (int j = 0; j < dtRedeemDonation.Rows.Count; j++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtRedeemDonation.Rows[j][2].ToString());
                        }
                        //totalRedeem.Add(tempTotalRedeem);


                        dtOrderItem = con.getTabel("SELECT doi.TadaNo,doi.NoOrder,doi.Terpakai FROM DetailOrderItems doi, OrderItem oi WHERE doi.NoOrder=oi.NoOrder AND doi.TadaNo = '" + finalTadaNo[t] + "' AND oi.StatusOrderItem = 'Success'");
                        //tempTotalRedeem = 0;
                        for (int j = 0; j < dtOrderItem.Rows.Count; j++)
                        {
                            tempTotalRedeem += Convert.ToInt32(dtOrderItem.Rows[j][2].ToString());
                        }
                        if (tempo[t] == 1)
                        {
                            totalRedeem.Add(0);
                        }
                        else if (tempo[t] == 0)
                        {
                            totalRedeem.Add(tempTotalRedeem);
                        }
                    }

                    for (int f = 0; f < noOrderRedeemNo.Count; f++)
                    {
                        tempTotalRedeem = totalRedeem[f];
                        for (int l = 0; l < f; l++)
                        {
                            if (finalTadaNo[f] == finalTadaNo[l] && status[l] == "Success")
                            {
                                //if(status[f]=="Success"){
                                tempTotalRedeem += finalTerpakai[l];
                            }
                        }
                        totalRedeem[f] = tempTotalRedeem;
                        tempSaldoAkhir = SaldoAwal[f] + totalTopUp[f] - totalRedeem[f];
                        if (tempo[f] == 1)
                        {
                            SaldoAkhir.Add(0);
                        }
                        else if (tempo[f] == 0)
                        {
                            SaldoAkhir.Add(tempSaldoAkhir);
                        }

                        if (tempo[f] == 1)
                        {
                            status.Add("TadaNo Not Found");
                        }
                        else if (tempo[f] == 0)
                        {

                            if (SaldoAkhir[f] >= finalTerpakai[f])
                            {
                                status.Add("Success");
                            }
                            else
                            {
                                status.Add("Failed");
                            }
                        }

                        //if (tempFinalTadaNo[f]=="0320-1551-3161-1652-593")
                        //{
                        if (tempo[f] == 1)
                        {
                            dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                        }
                        else if (tempo[f] == 0)
                        {
                            if (temptemp[f] == 1)
                            {
                                status[f] = "Already in DB";
                                //dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                            }
                            else if (temptemp[f] == 0)
                            {
                                if (status[f] == "Failed")
                                {
                                    dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);
                                }
                            }
                        }
                        //}

                    }
                    for (int g = 0; g < tempFinalNoOrderRedeemNo.Count; g++)
                    {
                        dataGridView2.Rows.Add(tempFinalNoOrderRedeemNo[g], tempFinalJenisOrder[g], tempFinalTanggal[g], tempFinalTadaNo[g], tempFinalClaimer[g], tempFinalTerpakai[g], tempFinalVoucher[g], tempFinalJumlahVoucher[g], tempFinalPecahanVoucher[g], tempFinalDelivery[g], tempFinalEmail[g], tempFinalTelepon[g], tempFinalAlamat[g], tempFinalNamaBank[g], tempFinalNamaRekening[g], tempFinalNoRekening[g], tempFinalNamaCabang[g], tempFinalYayasan[g], tempFinalKategori[g], tempFinalBarang[g], tempFinalJumlahItem[g], tempFinalHargaItem[g]);
                    }
                    flagSubmit = 1;
                    label7.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File must be a Giftcard Report", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*if (dataGridView1.Rows.Count > 0)
            {
                button3.Enabled = true;
            }
            else {
                button3.Enabled = false;
            }*/
            if(flagSubmit==1){
                button3.Enabled = true;
                button4.Enabled = false;
            }
            else if(flagSubmit==2){
                button3.Enabled = false;
                button4.Enabled = true;
            }
        }

        private void Verif_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to close this form?", "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Dispose();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else {
                e.Cancel = true;
            }
        }
    }
}
