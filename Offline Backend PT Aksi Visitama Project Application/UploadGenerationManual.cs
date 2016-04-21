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
    public partial class UploadGenerationManual : Form
    {
        Connect con = new Connect();
        MDIform mdi = new MDIform();
        public UploadGenerationManual()
        {
            InitializeComponent();
            comboBox1.Items.Add("Retail");
            comboBox1.Items.Add("Gift");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtCekTada = new DataTable();
            dtCekTada = con.getTabel("SELECT * FROM Tada WHERE TadaNo='"+textBox3.Text+"'");
            int flag=0;
            for (int i = 0; i < listBox1.Items.Count;i++ )
            {
                if(listBox1.Items[i].ToString()==textBox3.Text){
                    flag++;
                }
            }
            if(flag!=0){
                MessageBox.Show("Tada No. Already Exists In List Box");
                textBox3.Text = "";
            }
            else if(dtCekTada.Rows.Count>0){
                MessageBox.Show("Tada No. Already Exists In Database");
                textBox3.Text = "";
            }
            else if(textBox3.Text!=""){
                listBox1.Items.Add(""+textBox3.Text);
                textBox3.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose the Tada");
            }
            else {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
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

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""){
                MessageBox.Show("Generation ID must be filled");
            }
            else if(listBox1.Items.Count==0){
                MessageBox.Show("Tada No must be filled");
            }
            else if(textBox2.Text==""){
                MessageBox.Show("Nominal generation must be filled");
            }
            else if(cekAngka(textBox2.Text)==false){
                MessageBox.Show("Nominal harus angka");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Brand harus dipilih");
            }
            else {
                DialogResult result1 = MessageBox.Show("Are you sure want to upload this generation?\nGeneration ID\t: " + textBox1.Text + "\nNominal\t\t: " + textBox2.Text + "\nJumlah Tada\t: " + listBox1.Items.Count + "\nBrand\t\t: " + comboBox1.Text, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result1 == DialogResult.Yes) {
                    con.executeUpdate("Insert into generation values('" + textBox1.Text + "','" + textBox2.Text + "','" + listBox1.Items.Count.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','OB" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + comboBox1.Text + "','" + MDIform.username + "')");
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        con.executeUpdate("INSERT INTO tada values('" + listBox1.Items[i].ToString() + "','" + textBox2.Text + "','" + textBox1.Text + "')");
                    }
                    MessageBox.Show("Submit Success!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    listBox1.Items.Clear();
                }
            }
        }
    }
}
