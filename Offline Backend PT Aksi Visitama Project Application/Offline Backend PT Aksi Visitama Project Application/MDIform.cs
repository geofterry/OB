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
    public partial class MDIform : Form
    {
        //Connect con = new Connect();
        LoginForm login;
        UploadGeneration uploadGen;
        TopUpManual topUpMan;
        Verification verif;
        Verif ver;
        public static string role = "";
        public static string nameLogin = "";
        public static string username = "";
        public MDIform()
        {
            InitializeComponent();
        }

        private void MDIform_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = con.getTabel("SELECT * FROM Generation");
        }

        private void tadaGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uploadGen == null || uploadGen.IsDisposed)
            {
                uploadGen = new UploadGeneration();
            }
            uploadGen.Visible = true;
            uploadGen.MdiParent = this;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (uploadGen == null || uploadGen.IsDisposed)
            {
                uploadGen = new UploadGeneration();
            }
            uploadGen.Visible = true;
            uploadGen.MdiParent = this;
        }

        private void topUpManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topUpMan == null || topUpMan.IsDisposed)
            {
                topUpMan = new TopUpManual();
            }
            topUpMan.Visible = true;
            topUpMan.MdiParent = this;
        }

        private void verificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (verif == null || verif.IsDisposed)
            {
                verif = new Verification();
            }
            verif.Visible = true;
            verif.MdiParent = this;*/
            if (ver == null || ver.IsDisposed)
            {
                ver = new Verif();
            }
            ver.Visible = true;
            ver.MdiParent = this;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(role=="CEO"){
                uploadToolStripMenuItem.Visible = true;
                verificationToolStripMenuItem.Visible = true;
                reportToolStripMenuItem.Visible = true;
                fileToolStripMenuItem.Visible = true;
            }
            else if(role=="Manager"){
                uploadToolStripMenuItem.Visible = true;
                verificationToolStripMenuItem.Visible = true;
                reportToolStripMenuItem.Visible = false;
                fileToolStripMenuItem.Visible = true;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            login = new LoginForm();
            login.Visible = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
