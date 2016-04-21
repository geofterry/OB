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
    public partial class RedimptionReportPerBrand : Form
    {
        Connect con = new Connect();
        public RedimptionReportPerBrand()
        {
            InitializeComponent();
            DataTable dt1 = new DataTable();
            dt1 = con.getTabel("SELECT DISTINCT BrandName FROM Generation");
            for (int i = 0; i < dt1.Rows.Count;i++ )
            {
                comboBox1.Items.Add(dt1.Rows[i][0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose the client");
            }
            else {
                DataTable dtv = new DataTable();
                DataTable dtc = new DataTable();
                DataTable dtd = new DataTable();
                DataTable dti = new DataTable();

                dtv = con.getTabel("SELECT dov.* FROM DetailOrderVoucher dov, Tada t, Generation g WHERE dov.TadaNo = t.TadaNo AND g.GenerationID=t.GenerationID AND g.brandname='"+comboBox1.Text+"'");
                //dtc = con.getTabel("SELECT drc.* FROM ");

                
            }
        }
    }
}
