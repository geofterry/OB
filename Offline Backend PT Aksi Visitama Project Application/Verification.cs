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
    public partial class Verification : Form
    {
        Connect con = new Connect();
        public Verification()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            Connect con = new Connect();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

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

            List<int> totalTopUp = new List<int>();

            int tempSaldoAkhir = 0;
            int tempTotalTopUp = 0;
            int tempTotalRedeem = 0;

            List<int> totalRedeem = new List<int>();
            /*List<int> totalOrderVoucher = new List<int>();
            List<int> totalRedeemCash = new List<int>();
            List<int> totalDonation = new List<int>();
            List<int> totalOrderItem = new List<int>();*/
            List<int> SaldoAwal = new List<int>();
            List<int> SaldoAkhir = new List<int>();
            List<string> status = new List<string>();

            //hapus isi dataGridView1
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            //hapus isi dataGridView4
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();

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

            //hapus list Donations
            tadaDonation.Clear();
            redeemNoDonation.Clear();
            tanggalDonation.Clear();
            terpakaiDonation.Clear();
            claimerDonation.Clear();

            yayasanDonation.Clear();

            //hapus list Items
            tadaItem.Clear();
            noOrderItem.Clear();
            tanggalItem.Clear();
            terpakaiItem.Clear();
            claimerItem.Clear();

            deliveryItem.Clear();
            emailItem.Clear();
            teleponItem.Clear();
            alamatItem.Clear();
            kategoriItem.Clear();
            barangItem.Clear();
            jumlahItem.Clear();
            hargaItem.Clear();

            /*totalTopUpVoucher.Clear();
            totalTopUpCash.Clear();
            totalTopUpDonation.Clear();
            totalTopUpItem.Clear();

            totalOrderVoucher.Clear();
            totalRedeemCash.Clear();
            totalDonation.Clear();
            totalOrderItem.Clear();*/

            SaldoAwal.Clear();
            SaldoAkhir.Clear();

            string pathConn = "";
            //validasi jika belum ada file yg d pilih
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please choose the file.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //validasi jika format file yang dimasukan salah/bukan excel
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
                    //string pathConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;\";";
                    OleDbConnection conn = new OleDbConnection(pathConn);

                    //OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [Vouchers$A3:S65536]", conn);
                    OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [Vouchers$]", conn);
                    //Select Tanggal, [No#] as [Number], [Gen ID], [Tada No#] as [Tada No], Nominal, Brand, [Invoice No] from [Worksheet$]
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


                    myDataAdapter.Fill(dt);
                    myDataAdapter2.Fill(dt2);
                    myDataAdapter3.Fill(dt3);
                    myDataAdapter4.Fill(dt4);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][1].ToString() != "")
                        {
                            tadaVoucher.Add(dt.Rows[i][1].ToString());
                            noOrderVoucher.Add(dt.Rows[i][14].ToString());
                            tanggalVoucher.Add(dt.Rows[i][6].ToString());
                            terpakaiVoucher.Add(Convert.ToInt32(dt.Rows[i][5].ToString()));
                            claimerVoucher.Add(dt.Rows[i][7].ToString());

                            namaVoucher.Add(dt.Rows[i][8].ToString());
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
                            alamatVoucher.Add(dt.Rows[i][18].ToString());

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

                            //get tada total top up
                            /*dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + dt.Rows[i][1].ToString() + "'");
                            tempTotalTopUp = 0;
                            for (int k = 0; k < dtTopUp.Rows.Count; k++)
                            {
                                tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString()); 
                            }
                            totalTopUpVoucher.Add(tempTotalTopUp);

                            //get tada total redeem from database
                            dtRedeemVocuher = con.getTabel("SELECT * FROM DetailOrderVoucher WHERE TadaNo='" + dt.Rows[i][1].ToString() + "'");
                            tempTotalRedeem = 0;
                            for (int j = 0; j < dtRedeemVocuher.Rows.Count; j++)
                            {
                                tempTotalRedeem += Convert.ToInt32(dtRedeemVocuher.Rows[j][2].ToString());
                            }
                            totalOrderVoucher.Add(tempTotalRedeem);

                            //get saldo awal tada
                            dtSaldoAwal = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='" + dt.Rows[i][1].ToString() + "'");
                            SaldoAwal.Add(Convert.ToInt32(dtSaldoAwal.Rows[0][0].ToString()));

                            //count saldo akhir tada
                            tempSaldoAkhir = SaldoAwal[i] + totalTopUpVoucher[i] - totalOrderVoucher[i];
                            SaldoAkhir.Add(tempSaldoAkhir);*/

                            //input ke datagridview1 & datagridview4
                            dataGridView1.Rows.Add(noOrderVoucher[i], "Voucher", tanggalVoucher[i], tadaVoucher[i], claimerVoucher[i], terpakaiVoucher[i], "", "");
                            dataGridView4.Rows.Add(noOrderVoucher[i], "Voucher", tanggalVoucher[i], tadaVoucher[i], claimerVoucher[i], terpakaiVoucher[i], namaVoucher[i], jumlahVoucher[i], pecahanVoucher[i], deliveryVoucher[i], emailVoucher[i], teleponVoucher[i], alamatVoucher[i], "", "", "", "", "", "", "", "", "");

                        }
                    }

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

                            /*dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + dt2.Rows[i][1].ToString() + "'");
                            tempTotalTopUp = 0;
                            for (int k = 0; k < dtTopUp.Rows.Count; k++)
                            {
                                tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString());
                            }
                            totalTopUpCash.Add(tempTotalTopUp);*/
                            dataGridView1.Rows.Add(redeemNoCash[i], "Cash", tanggalCash[i], tadaCash[i], claimerCash[i], terpakaiCash[i], "", "");
                            dataGridView4.Rows.Add(redeemNoCash[i], "Cash", tanggalCash[i], tadaCash[i], claimerCash[i], terpakaiCash[i], "", "", "", "", "", teleponCash[i], "", namaBankCash[i], namaRekeningCash[i], noRekeningCash[i], namaCabangCash[i], "", "", "", "", "");
                        }
                    }
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
                            /*dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + dt3.Rows[i][1].ToString() + "'");
                            tempTotalTopUp = 0;
                            for (int k = 0; k < dtTopUp.Rows.Count; k++)
                            {
                                tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString());
                            }
                            totalTopUpDonation.Add(tempTotalTopUp);*/
                            dataGridView1.Rows.Add(redeemNoDonation[i], "Donation", tanggalDonation[i], tadaDonation[i], claimerDonation[i], terpakaiDonation[i], "", "");
                            dataGridView4.Rows.Add(redeemNoDonation[i], "Donation", tanggalDonation[i], tadaDonation[i], claimerDonation[i], terpakaiDonation[i], "", "", "", "", "", "", "", "", "", "", "", yayasanDonation[i], "", "", "", "");
                        }
                    }
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
                            //deliveryItem.Add(dt4.Rows[i][12].ToString());
                            //deliveryItem.Add(Convert.ToInt32(dt4.Rows[i][12].ToString()));
                            emailItem.Add(dt4.Rows[i][17].ToString());
                            teleponItem.Add(dt4.Rows[i][18].ToString());
                            alamatItem.Add(dt4.Rows[i][19].ToString());
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
                            //jumlahItem.Add(dt4.Rows[i][10].ToString());
                            if (dt4.Rows[i][11].ToString() == "")
                            {
                                hargaItem.Add(0);
                            }
                            else if (dt4.Rows[i][11].ToString() != "")
                            {
                                hargaItem.Add(Convert.ToInt32(dt4.Rows[i][11].ToString()));
                            }
                            //hargaItem.Add(dt4.Rows[i][11].ToString());

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
                            /*dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + dt2.Rows[i][1].ToString() + "'");
                            tempTotalTopUp = 0;
                            for (int k = 0; k < dtTopUp.Rows.Count; k++)
                            {
                                tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString());
                            }
                            totalTopUpItem.Add(tempTotalTopUp);*/
                            dataGridView1.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "");
                            //dataGridView4.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "", "", deliveryItem[i], emailItem[i], teleponItem[i], alamatItem[i], "", "", "", "", "", kategoriItem[i], barangItem[i], "","");
                            dataGridView4.Rows.Add(noOrderItem[i], "Item", tanggalItem[i], tadaItem[i], claimerItem[i], terpakaiItem[i], "", "", "", deliveryItem[i], emailItem[i], teleponItem[i], alamatItem[i], "", "", "", "", "", kategoriItem[i], barangItem[i], jumlahItem[i], hargaItem[i]);
                        }

                    }


                    //this.dataGridView1.Sort(this.Column2, ListSortDirection.Ascending);
                    //this.dataGridView1.Columns["Tanggal"].SortMode = DataGridViewColumnSortMode.Automatic;
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
                    dataGridView4.Sort(dataGridView4.Columns[2], ListSortDirection.Ascending);


                    /*for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        for (int y = 0; y < z; y++)
                        {
                            if (Convert.ToDateTime(dataGridView1.Rows[y].Cells[2].Value.ToString()) > Convert.ToDateTime(dataGridView1.Rows[z].Cells[2].Value.ToString()))
                            {
                                if (dataGridView1.Rows[y].Cells[3].Value.ToString() == dataGridView1.Rows[z].Cells[3].Value.ToString())
                                {
                                    MessageBox.Show("" + z);
                                }
                            }
                        }

                        if (dataGridView1.Rows[z].Cells[3].Value.ToString() == dataGridView1.CurrentRow.Cells[3].Value.ToString())
                        {
                            dataGridView3.Rows.Add(dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString());
                        }
                    }*/
                    List<string> noOrderRedeemNo = new List<string>();
                    List<string> jenisOrder = new List<string>();
                    List<string> finalTanggal = new List<string>();
                    List<string> finalTadaNo = new List<string>();
                    List<string> finalClaimer = new List<string>();
                    List<int> finalTerpakai = new List<int>();

                    List<string> tempFinalNoOrderRedeemNo = new List<string>();
                    List<string> tempFinalJenisOrder = new List<string>();
                    List<string> tempFinalTanggal = new List<string>();
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

                    for (int c = 0; c < dataGridView4.Rows.Count; c++)
                    {
                        tempFinalNoOrderRedeemNo.Add(dataGridView4.Rows[c].Cells[0].Value.ToString());
                        tempFinalJenisOrder.Add(dataGridView4.Rows[c].Cells[1].Value.ToString());
                        tempFinalTanggal.Add(dataGridView4.Rows[c].Cells[2].Value.ToString());
                        tempFinalTadaNo.Add(dataGridView4.Rows[c].Cells[3].Value.ToString());
                        tempFinalClaimer.Add(dataGridView4.Rows[c].Cells[4].Value.ToString());
                        tempFinalTerpakai.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[5].Value.ToString()));
                        tempFinalVoucher.Add(dataGridView4.Rows[c].Cells[6].Value.ToString());
                        if (dataGridView4.Rows[c].Cells[7].Value.ToString() != "")
                        {
                            tempFinalJumlahVoucher.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[7].Value.ToString()));
                        }
                        else if (dataGridView4.Rows[c].Cells[7].Value.ToString() == "")
                        {
                            tempFinalJumlahVoucher.Add(0);
                        }
                        if (dataGridView4.Rows[c].Cells[8].Value.ToString() != "")
                        {
                            tempFinalPecahanVoucher.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[8].Value.ToString()));
                        }
                        else if (dataGridView4.Rows[c].Cells[8].Value.ToString() == "")
                        {
                            tempFinalPecahanVoucher.Add(0);
                        }
                        if (dataGridView4.Rows[c].Cells[9].Value.ToString() != "")
                        {
                            tempFinalDelivery.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[9].Value.ToString()));
                        }
                        else if (dataGridView4.Rows[c].Cells[9].Value.ToString() == "")
                        {
                            tempFinalDelivery.Add(0);
                        }

                        tempFinalEmail.Add(dataGridView4.Rows[c].Cells[10].Value.ToString());
                        tempFinalTelepon.Add(dataGridView4.Rows[c].Cells[11].Value.ToString());
                        tempFinalAlamat.Add(dataGridView4.Rows[c].Cells[12].Value.ToString());
                        tempFinalNamaBank.Add(dataGridView4.Rows[c].Cells[13].Value.ToString());
                        tempFinalNamaRekening.Add(dataGridView4.Rows[c].Cells[14].Value.ToString());
                        tempFinalNoRekening.Add(dataGridView4.Rows[c].Cells[15].Value.ToString());
                        tempFinalNamaCabang.Add(dataGridView4.Rows[c].Cells[16].Value.ToString());
                        tempFinalYayasan.Add(dataGridView4.Rows[c].Cells[17].Value.ToString());
                        tempFinalKategori.Add(dataGridView4.Rows[c].Cells[18].Value.ToString());
                        tempFinalBarang.Add(dataGridView4.Rows[c].Cells[19].Value.ToString());
                        //tempFinalJumlahItem.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[20].Value.ToString()));
                        if (dataGridView4.Rows[c].Cells[20].Value.ToString() != "")
                        {
                            tempFinalJumlahItem.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[20].Value.ToString()));
                        }
                        else if (dataGridView4.Rows[c].Cells[20].Value.ToString() == "")
                        {
                            tempFinalJumlahItem.Add(0);
                        }
                        if (dataGridView4.Rows[c].Cells[21].Value.ToString() != "")
                        {
                            tempFinalHargaItem.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[21].Value.ToString()));
                        }
                        else if (dataGridView4.Rows[c].Cells[21].Value.ToString() == "")
                        {
                            tempFinalHargaItem.Add(0);
                        }
                        //tempFinalHargaItem.Add(Convert.ToInt32(dataGridView4.Rows[c].Cells[21].Value.ToString()));
                    }
                    for (int a = 0; a < dataGridView1.Rows.Count; a++)
                    {
                        noOrderRedeemNo.Add(dataGridView1.Rows[a].Cells[0].Value.ToString());
                        jenisOrder.Add(dataGridView1.Rows[a].Cells[1].Value.ToString());
                        finalTanggal.Add(dataGridView1.Rows[a].Cells[2].Value.ToString());
                        finalTadaNo.Add(dataGridView1.Rows[a].Cells[3].Value.ToString());
                        finalClaimer.Add(dataGridView1.Rows[a].Cells[4].Value.ToString());
                        finalTerpakai.Add(Convert.ToInt32(dataGridView1.Rows[a].Cells[5].Value.ToString()));
                    }
                    //hapus isi dataGridView1
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();

                    //hapus isi dataGridView4
                    dataGridView4.Rows.Clear();
                    dataGridView4.Refresh();



                    for (int b = 0; b < noOrderRedeemNo.Count; b++)
                    {
                        dtSaldoAwal = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='" + finalTadaNo[b] + "'");
                        SaldoAwal.Add(Convert.ToInt32(dtSaldoAwal.Rows[0][0].ToString()));
                        dtTopUp = con.getTabel("SELECT * FROM TopUp WHERE TadaNo = '" + finalTadaNo[b] + "'");
                        tempTotalTopUp = 0;
                        for (int k = 0; k < dtTopUp.Rows.Count; k++)
                        {
                            tempTotalTopUp += Convert.ToInt32(dtTopUp.Rows[k][1].ToString());
                        }
                        totalTopUp.Add(tempTotalTopUp);

                        if (jenisOrder[b] == "Voucher")
                        {
                            dtOrderVoucher = con.getTabel("SELECT * FROM DetailOrderVoucher WHERE TadaNo='" + finalTadaNo[b] + "'");
                            tempTotalRedeem = 0;
                            for (int j = 0; j < dtOrderVoucher.Rows.Count; j++)
                            {
                                tempTotalRedeem += Convert.ToInt32(dtOrderVoucher.Rows[j][2].ToString());
                            }
                            totalRedeem.Add(tempTotalRedeem);
                        }
                        else if (jenisOrder[b] == "Cash")
                        {
                            dtRedeemCash = con.getTabel("SELECT * FROM DetailRedeemCash WHERE TadaNo='" + finalTadaNo[b] + "'");
                            tempTotalRedeem = 0;
                            for (int j = 0; j < dtRedeemCash.Rows.Count; j++)
                            {
                                tempTotalRedeem += Convert.ToInt32(dtRedeemCash.Rows[j][2].ToString());
                            }
                            totalRedeem.Add(tempTotalRedeem);
                        }
                        else if (jenisOrder[b] == "Donation")
                        {
                            dtRedeemDonation = con.getTabel("SELECT * FROM DetailDonate WHERE TadaNo='" + finalTadaNo[b] + "'");
                            tempTotalRedeem = 0;
                            for (int j = 0; j < dtRedeemDonation.Rows.Count; j++)
                            {
                                tempTotalRedeem += Convert.ToInt32(dtRedeemDonation.Rows[j][2].ToString());
                            }
                            totalRedeem.Add(tempTotalRedeem);
                        }
                        else if (jenisOrder[b] == "Item")
                        {
                            dtOrderItem = con.getTabel("SELECT * FROM DetailOrderItems WHERE TadaNo='" + finalTadaNo[b] + "'");
                            tempTotalRedeem = 0;
                            for (int j = 0; j < dtOrderItem.Rows.Count; j++)
                            {
                                tempTotalRedeem += Convert.ToInt32(dtOrderItem.Rows[j][2].ToString());
                            }
                            totalRedeem.Add(tempTotalRedeem);
                        }
                    }
                    for (int f = 0; f < noOrderRedeemNo.Count; f++)
                    {
                        //if (jenisOrder[f] == "Voucher")

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
                        SaldoAkhir.Add(tempSaldoAkhir);
                        if (SaldoAkhir[f] >= finalTerpakai[f])
                        {
                            status.Add("Success");
                        }
                        else
                        {
                            status.Add("Failed");
                        }
                        dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], SaldoAkhir[f], status[f]);

                        /*for (int l = 0; l < b;l++ )
                        {
                            if (finalTadaNo[b] == finalTadaNo[l])
                            {
                                tempTotalRedeem += Convert.ToInt32(dtOrderVoucher.Rows[j][2].ToString());
                            }
                        }*/
                        //else
                        //dataGridView1.Rows.Add(noOrderRedeemNo[f], jenisOrder[f], finalTanggal[f], finalTadaNo[f], finalClaimer[f], finalTerpakai[f], "asd", "ahh");


                    }
                    //hapus isi dataGridView4
                    for (int d = 0; d < tempFinalNoOrderRedeemNo.Count; d++)
                    {
                        dataGridView4.Rows.Add(tempFinalNoOrderRedeemNo[d], tempFinalJenisOrder[d], tempFinalTanggal[d], tempFinalTadaNo[d], tempFinalClaimer[d], tempFinalTerpakai[d], tempFinalVoucher[d], tempFinalJumlahVoucher[d], tempFinalPecahanVoucher[d], tempFinalDelivery[d], tempFinalEmail[d], tempFinalTelepon[d], tempFinalAlamat[d], tempFinalNamaBank[d], tempFinalNamaRekening[d], tempFinalNoRekening[d], tempFinalNamaCabang[d], tempFinalYayasan[d], tempFinalKategori[d], tempFinalBarang[d], tempFinalJumlahItem[d], tempFinalHargaItem[d]);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
            dataGridView4.Sort(dataGridView4.Columns[2], ListSortDirection.Ascending);
        }

        //CLICK ON dataGridView1
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string tempTadaNo = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = tempTadaNo;

            string tempJenisOrder = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();

            //dt5 untuk get nominalgeneration berdasarkan tadaNo pada row yg d klik
            DataTable dt5 = new DataTable();
            dt5 = con.getTabel("SELECT g.NominalGeneration FROM Generation g, Tada t WHERE t.GenerationID=g.GenerationID AND t.TadaNo='" + tempTadaNo + "'");
            textBox2.Text = dt5.Rows[0][0].ToString();

            DataTable dt6 = new DataTable();
            dt6 = con.getTabel("SELECT * FROM TopUp WHERE TadaNo='" + tempTadaNo + "'");



            //dataGridView2.DataSource = dt6;
            for (int i = 0; i < dt6.Rows.Count; i++)
            {
                dataGridView2.Rows.Add(dt6.Rows[i][0], dt6.Rows[i][1], dt6.Rows[i][2], dt6.Rows[i][3]);
            }

            int tempTotalTopUp = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                tempTotalTopUp += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value.ToString());
            }
            textBox3.Text = tempTotalTopUp.ToString();

            //isi data table untuk detail redeem
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            if (tempJenisOrder == "Voucher")
            {
                dt7 = con.getTabel("SELECT dov.TadaNo,dov.NoOrder,dov.Terpakai FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='" + tempTadaNo + "' AND ov.StatusOrderVoucher='Success'");
            }
            else if (tempJenisOrder == "Cash")
            {
                dt7 = con.getTabel("SELECT dov.TadaNo,dov.NoOrder,dov.Terpakai FROM DetailOrderVoucher dov, OrderVoucher ov WHERE dov.NoOrder=ov.NoOrder AND dov.TadaNo='" + tempTadaNo + "' AND ov.StatusOrderVoucher='Success'");
                //dt7 = con.getTabel("SELECT * FROM DetailRedeemCash WHERE TadaNo='" + tempTadaNo + "'");
            }
            else if (tempJenisOrder == "Donation")
            {
                dt7 = con.getTabel("SELECT * FROM DetailDonate WHERE TadaNo='" + tempTadaNo + "'");
            }
            else if (tempJenisOrder == "Item")
            {
                dt7 = con.getTabel("SELECT * FROM DetailOrderItems WHERE TadaNo='" + tempTadaNo + "'");
            }

            for (int i = 0; i < dt7.Rows.Count; i++)
            {
                
                dataGridView3.Rows.Add(dt7.Rows[i][0].ToString(), dt7.Rows[i][1].ToString(), dt7.Rows[i][2].ToString());
            }



            //jika diatasnya ada tada no yg sama, masukin k datagridview redeem
            for (int i = 0; i < dataGridView1.CurrentCell.RowIndex; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == dataGridView1.CurrentRow.Cells[3].Value.ToString() && dataGridView1.Rows[i].Cells[7].Value.ToString() == "Success")
                {
                    dataGridView3.Rows.Add(dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[5].Value.ToString());
                }
            }


            int tempTotalRedeem = 0;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                tempTotalRedeem += Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value.ToString());
            }
            textBox4.Text = tempTotalRedeem.ToString();
            textBox6.Text = (Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox4.Text)).ToString();
        }
    }
}
