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
    public partial class TopUpManual : Form
    {
        Connect con = new Connect();
        MDIform mdi = new MDIform();
        string topupid = "";
        int temp2 = 0;
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        public TopUpManual()
        {
            InitializeComponent();
            generate();
            label3.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            
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

        public Boolean cekTadaNo(string a) {
            DataTable dt2 = new DataTable();
            
            dt2 = con.getTabel("SELECT TadaNo FROM Tada");
            int temp = 0;
            for (int i = 0; i < dt2.Rows.Count;i++ )
            {
                if(dt2.Rows[i][0].ToString()==a){
                    temp++;
                }
            }
            if (temp == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text==""){
                MessageBox.Show("Tada No must be filled!");
            }
            else if (cekTadaNo(textBox1.Text) == false)
            {
                MessageBox.Show("Tada No "+textBox1.Text+" tidak ada");
            }
            else if(textBox2.Text==""){
                MessageBox.Show("Jumlah Top Up must be filled!");
            }
            else if(cekAngka(textBox2.Text)==false){
                MessageBox.Show("Jumlah Top Up harus angka");
            }
            else {
                temp2 = 0;
                string jumlahTopUp = textBox2.Text;
                
                int jumlahTopUpLength = jumlahTopUp.Length;
                int digitAwal = jumlahTopUpLength % 3;
                int jumlahLoop = (jumlahTopUpLength - digitAwal) / 3;
                string jumlahTopUpHasil = jumlahTopUp.Substring(0, digitAwal);
                for (int i = 0; i < jumlahLoop;i++ )
                {
                    if(digitAwal==0){
                        jumlahTopUpHasil = jumlahTopUp.Substring(digitAwal, 3);
                    }
                    else {
                        jumlahTopUpHasil = jumlahTopUpHasil + "." + jumlahTopUp.Substring(digitAwal, 3);
                    }
                    digitAwal += 3;
                }

                dt3 = con.getTabel("SELECT NominalTada FROM Tada WHERE TadaNo='"+textBox1.Text+"'");
                dt4 = con.getTabel("SELECT g.BrandName FROM Generation g,Tada t WHERE t.TadaNo='" + textBox1.Text + "' AND t.GenerationID=g.GenerationID");
                temp2 = Convert.ToInt32(textBox2.Text)+Convert.ToInt32(dt3.Rows[0][0].ToString());
                //MessageBox.Show("asd "+ temp);
                //belum selesai untuk tambah '.' pada jumlah top up
                
                DialogResult result1 = MessageBox.Show("Are you sure want to top up ? \n Tada No \t \t:   "+textBox1.Text+"\n Jumlah Top Up \t:   Rp "+jumlahTopUpHasil+"\n Brand \t\t:   "+dt4.Rows[0][0].ToString()+"", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result1 == DialogResult.Yes)
                {

                    con.executeUpdate("INSERT INTO TopUp VALUES('"+topupid+"','"+textBox2.Text+"','"+label3.Text+"','"+textBox1.Text+"','"+MDIform.username+"')");
                    con.executeUpdate("UPDATE Tada SET NominalTada='"+temp2.ToString()+"' WHERE TadaNo='"+textBox1.Text+"'");
                    MessageBox.Show("Top Up Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
        }
    }
}
