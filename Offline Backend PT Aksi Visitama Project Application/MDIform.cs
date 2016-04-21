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
        Connect con = new Connect();
        ChangePasswordForm cpf;
        LoginForm login;
        UploadGeneration uploadGen;
        TopUpManual topUpMan;
        Verif ver;
        Verification_Report vr;
        UploadGenerationManual ugm;
        ViewHistoryTransaction vht;
        Performance p;
        UpdateTransactionForm utf;
        DeleteTopUp dtu;
        UploadFileTopUp uft;
        public static string role = "";
        public static string nameLogin = "";
        public static string username = "";
        public static int tempView = 0;
        public MDIform()
        {
            InitializeComponent();
            fileToolStripMenuItem.Visible = true;
            loginToolStripMenuItem.Visible = true;
            logoutToolStripMenuItem.Visible = false;
            uploadToolStripMenuItem.Visible = false;
            verificationToolStripMenuItem.Visible = false;
            reportToolStripMenuItem.Visible = false;
            toolStrip1.Visible = false;
            changePasswordToolStripMenuItem.Visible = false;
            checkToolStripMenuItem.Visible = false;
            updateTransactionToolStripMenuItem.Visible = false;
            backupDatabaseToolStripMenuItem.Visible = false;
            label1.Visible = false;
            seeViewFailedFalse();
        }

        public void getNewData() {
            dataGridView1.DataSource = con.getTabel("SELECT top 5 NoOrder,Claimer,Tanggal FROM OrderVoucher Where NoOrder like 'F%' order by Tanggal desc");
            dataGridView2.DataSource = con.getTabel("SELECT top 5 RedeemNo,Claimer,Tanggal FROM RedeemCash Where RedeemNo like 'F%' order by Tanggal desc");
            dataGridView3.DataSource = con.getTabel("SELECT top 5 RedeemNo,Claimer,Tanggal FROM Donate Where RedeemNo like 'F%' order by Tanggal desc");
            dataGridView4.DataSource = con.getTabel("SELECT top 5 NoOrder,Claimer,Tanggal FROM OrderItem Where NoOrder like 'F%' order by Tanggal desc");
        }

        public void seeViewFailedFalse() {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;
            button2.Visible = false;
        }

        public void seeViewFailedTrue()
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            dataGridView3.Visible = true;
            dataGridView4.Visible = true;
            button2.Visible = true;
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
                toolStrip1.Visible = true;
                uploadToolStripMenuItem.Visible = true;
                verificationToolStripMenuItem.Visible = true;
                reportToolStripMenuItem.Visible = true;
                fileToolStripMenuItem.Visible = true;
                changePasswordToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
                checkToolStripMenuItem.Visible = true;
                updateTransactionToolStripMenuItem.Visible = true;
                backupDatabaseToolStripMenuItem.Visible = true;

            }
            else if (role == "HRD Manager")
            {
                uploadToolStripMenuItem.Visible = true;
                verificationToolStripMenuItem.Visible = true;
                reportToolStripMenuItem.Visible = false;
                fileToolStripMenuItem.Visible = true;
                changePasswordToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
                checkToolStripMenuItem.Visible = true;
                updateTransactionToolStripMenuItem.Visible = true;
                backupDatabaseToolStripMenuItem.Visible = true;
            }
            else if (role == "Operation Manager")
            {
                toolStrip1.Visible = true;
                uploadToolStripMenuItem.Visible = true;
                verificationToolStripMenuItem.Visible = true;
                reportToolStripMenuItem.Visible = true;
                fileToolStripMenuItem.Visible = true;
                changePasswordToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
                checkToolStripMenuItem.Visible = true;
                updateTransactionToolStripMenuItem.Visible = true;
                backupDatabaseToolStripMenuItem.Visible = true;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backupDatabaseToolStripMenuItem.Visible = false;
            checkToolStripMenuItem.Visible = false;
            fileToolStripMenuItem.Visible = true;
            loginToolStripMenuItem.Visible = true;
            logoutToolStripMenuItem.Visible = false;
            uploadToolStripMenuItem.Visible = false;
            verificationToolStripMenuItem.Visible = false;
            reportToolStripMenuItem.Visible = false;
            updateTransactionToolStripMenuItem.Visible = false;
            toolStrip1.Visible = false;
            changePasswordToolStripMenuItem.Visible = false;
            username = "";
            role = "";
            nameLogin = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (login == null || login.IsDisposed)
            {
                login = new LoginForm();
            }
            login.Visible = true;
            login.MdiParent = this;
        }

        private void verificationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vr == null || vr.IsDisposed)
            {
                vr = new Verification_Report();
            }
            vr.Visible = true;
            vr.MdiParent = this;
        }

        private void uploadGenerationManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ugm == null || ugm.IsDisposed)
            {
                ugm = new UploadGenerationManual();
            }
            ugm.Visible = true;
            ugm.MdiParent = this;
        }

        private void transactionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vht == null || vht.IsDisposed)
            {
                vht = new ViewHistoryTransaction();
            }
            vht.Visible = true;
            vht.MdiParent = this;
        }

        private void orderVoucherPerformanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (p == null || p.IsDisposed)
            {
                p = new Performance();
            }
            p.Visible = true;
            p.MdiParent = this;
        }

        private void updateTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (utf == null || utf.IsDisposed)
            {
                utf = new UpdateTransactionForm();
            }
            utf.Visible = true;
            utf.MdiParent = this;
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cpf == null || cpf.IsDisposed)
            {
                cpf = new ChangePasswordForm();
            }
            cpf.Visible = true;
            cpf.MdiParent = this;
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            //C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\Backup
            con.executeUpdate("BACKUP DATABASE [AksiVisitama] TO DISK = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL10.MSSQLSERVER\\MSSQL\\Backup\\Database Backup " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + ".bak'");
            //con.executeUpdate("BACKUP DATABASE [AksiVisitama] TO DISK = N'C:\\Users\\AT\\Desktop\\PT AV\\Database Backup\\Database Backup " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + ".bak'");
            label1.Visible = false;
            MessageBox.Show("Backup Success!");
            
        }

        private void deleteTopUpManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtu == null || dtu.IsDisposed)
            {
                dtu = new DeleteTopUp();
            }
            dtu.Visible = true;
            dtu.MdiParent = this;
        }

        private void topUpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uft == null || uft.IsDisposed)
            {
                uft = new UploadFileTopUp();
            }
            uft.Visible = true;
            uft.MdiParent = this;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (topUpMan == null || topUpMan.IsDisposed)
            {
                topUpMan = new TopUpManual();
            }
            topUpMan.Visible = true;
            topUpMan.MdiParent = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tempView++;
            if (tempView % 2 != 0)
            {
                getNewData();
                seeViewFailedTrue();
            }
            else {
                seeViewFailedFalse();
            }
        }
    }
}
