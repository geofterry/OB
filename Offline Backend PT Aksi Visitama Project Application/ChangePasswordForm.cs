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
    public partial class ChangePasswordForm : Form
    {
        Connect con = new Connect();
        DataTable temp = new DataTable();

        public ChangePasswordForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temp = con.getTabel("SELECT * FROM Employee WHERE Username='"+MDIform.username+"' AND EmployeePassword='"+textBox1.Text+"'");
            if(textBox1.Text==""){
                MessageBox.Show("Current password must be filled!");
            }
            else if(textBox2.Text==""){
                MessageBox.Show("New password must be filled!");
            }
            else if(textBox3.Text ==""){
                MessageBox.Show("Re-type new password must be filled!");
            }
            else if(temp.Rows.Count==0){
                MessageBox.Show("Wrong current password!");
            }
            else if(textBox2.Text.Length>100){
                MessageBox.Show("Password too long!");
            }
            else if(textBox3.Text!=textBox2.Text){
                MessageBox.Show("Re-type new password must be the same with new password");
            }
            else{
                con.executeUpdate("Update Employee SET EmployeePassword = '"+textBox3.Text+"' WHERE Username='"+MDIform.username+"'");
                MessageBox.Show("Change password success!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
