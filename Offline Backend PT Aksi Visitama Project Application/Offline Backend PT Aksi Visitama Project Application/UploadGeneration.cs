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
    public partial class UploadGeneration : Form
    {
        Connect con = new Connect();
        DataTable masterGeneration = new DataTable();
        MDIform mdi = new MDIform();
        public UploadGeneration()
        {
            InitializeComponent();
            button3.Enabled = false;
            textBox1.Text = "";
            dataGridView1.DataSource = null;
            masterGeneration = con.getTabel("select tadano from tada");
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
            string pathConn = "";
            //validasi jika belum ada file yg d pilih
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please choose the file.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //validasi jika format file yang dimasukan salah
            else if (!textBox1.Text.EndsWith(".xls") == true && !textBox1.Text.EndsWith(".xlsx") == true)
            {
                MessageBox.Show("Wrong file format, The file format must be .xls or .xlsx","Message Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                    OleDbConnection conn = new OleDbConnection(pathConn);
                    OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select Tanggal, [No#] as [Number], [Gen ID], [Tada No#] as [Tada No], Nominal, Brand, [Invoice No] from [Worksheet$]", conn);
                    //OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [Vouchers$]", conn);
                    DataTable dt = new DataTable();
                    myDataAdapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count;i++ )
                    {
                        if(dt.Rows[i][2].ToString()!=""){
                            dataGridView1.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString());   
                        }
                    }
                    //dataGridView1.DataSource = dt;
                    //agar datagridview tidak dapat disort oleh user
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                catch(Exception ex){
                    MessageBox.Show("File must be a Tada Generation", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            //con.executeUpdate("insert into generation values ('301','200000','1','11/04/2014','2151')");
            //con.executeUpdate("delete from generation where tanggalGeneration = '1900-01-01'");
            //con.executeUpdate("delete from generation where GenerationID = '390'");
            //con.executeUpdate("insert into tada values ('0411-1573-3490-1674-100','200000','AXA','301')");
            //MessageBox.Show("Test");
            DialogResult result1 = MessageBox.Show("Are you sure want to submit this file into database?","Confirmation", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
            int temptadano=0;
            if (result1==DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.Rows.Count;i++ )
                {
                    for (int j = 0; j < masterGeneration.Rows.Count;j++ )
                    {
                        if (dataGridView1.Rows[i].Cells[3].Value.ToString()==masterGeneration.Rows[j][0].ToString())
                        {
                            temptadano++;
                        }
                    }
                }

                if (temptadano != 0)
                {
                    if (temptadano == 1)
                    {
                        MessageBox.Show("There is " + temptadano + " data record with the specified key already exists, Please check the data again.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("There are " + temptadano + " data record with the specified key already exists, Please check the data again.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    dataGridView1.DataSource = null;
                    textBox1.Text = "";
                }
                else {
                    //MessageBox.Show(""+masterGeneration.Rows.Count);
                    List<string> tempGenID = new List<string>();
                    int tempJumlah = 0;
                    List<int> tempJumlahList = new List<int>();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        tempJumlah = 0;
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value.ToString() == dataGridView1.Rows[j].Cells[2].Value.ToString())
                            {
                                tempJumlah++;
                            }
                        }
                        tempJumlahList.Add(tempJumlah);
                        //MessageBox.Show("" + tempJumlah);
                        tempGenID.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                        //con.executeUpdate("insert into generation values ('" + dataGridView1.Rows[i].Cells["Gen ID"].Value + "','" + dataGridView1.Rows[i].Cells["Nominal"].Value + "','"+tempJumlah+"','" + dataGridView1.Rows[i].Cells["Tanggal"].Value + "','" + dataGridView1.Rows[i].Cells["Invoice No"].Value + "')"); 
                    }

                    //insert to Generation table
                    for (int i = 0; i < tempGenID.Count; i++) // Loop through List with for
                    {
                        if (i == 0)
                        {
                            //MessageBox.Show("" + tempGenID[i]);
                            //MessageBox.Show("" + tempJumlahList[i]);
                            con.executeUpdate("insert into generation values('" + tempGenID[i] + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "' , '" + tempJumlahList[i] + "', '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "', '" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "','"+dataGridView1.Rows[i].Cells[5].Value.ToString()+"','"+MDIform.username+"')");
                        }
                        else if (tempGenID[i] != tempGenID[i - 1])
                        {
                            //MessageBox.Show("" + tempGenID[i]);
                            //MessageBox.Show("" + tempJumlahList[i]);
                            con.executeUpdate("insert into generation values('" + tempGenID[i] + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "' , '" + tempJumlahList[i] + "', '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "', '" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + MDIform.username + "')");
                        }
                    }

                    //insert into tada table
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        con.executeUpdate("insert into tada values('" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "' , '" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "' , '" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "')");
                    }

                    MessageBox.Show("Insert Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                
            }
            else if(result1==DialogResult.No){
                dataGridView1.DataSource = null;
                textBox1.Text = "";
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                button3.Enabled = true;
            }
            else {
                button3.Enabled = false;
            }
        }
    }
}
