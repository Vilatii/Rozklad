using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using MySql.Data;

namespace Rozklad
{
    public partial class Form8 : Form
    {
        public MySqlDataAdapter mySqlDataAdapter;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
MySqlConnection conn = new MySqlConnection(SQL.connStr);
            bool CloseConnection()
            {
                try
                {
                    conn.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            bool OpenConnection()
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Cannot connect to server. Contact administrator");
                            break;
                        case 1045:
                            MessageBox.Show("Invalid username/password, please try again");
                            break;
                        default:
                            MessageBox.Show(ex.Message);
                            break;
                    }
                    return false;
                }
            }
            try
            {
                if (OpenConnection() == true)
                {
                    mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaVikladachiv", conn);
                    DataSet DS = new DataSet();
                    mySqlDataAdapter.Fill(DS);
                    dataGridView1.DataSource = DS.Tables[0];
                    CloseConnection();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataTable VikladachiTable = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from Vikladachi";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(VikladachiTable);
            comboBox2.DataSource = VikladachiTable;
            comboBox2.ValueMember = "Number";
            comboBox2.DisplayMember = "PIB";

            DataTable NGroupTable = new DataTable();
            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "Select * from NavantazhenyaGroup";
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
            adapter1.Fill(NGroupTable);
            comboBox1.DataSource = NGroupTable;
            comboBox1.ValueMember = "Kod";
        }

        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "INSERT INTO NavantazhenyaVikladachiv(Kod,Nomer,Vikladachi) VALUES(@Kod, @Nomer, @Vikladachi)";
            comm.Parameters.Add("@Kod", textBox1.Text);
            comm.Parameters.Add("@Nomer", comboBox1.SelectedValue);
            comm.Parameters.Add("@Vikladachi", comboBox2.SelectedValue);
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaVikladachiv", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
<<<<<<< HEAD
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
=======
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
        }

        private void button2_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "DELETE FROM NavantazhenyaVikladachiv WHERE Kod = " + textBox1.Text;
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaVikladachiv", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
<<<<<<< HEAD
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
=======
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
        }

        private void button3_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE NavantazhenyaVikladachiv SET Nomer = '" + comboBox1.SelectedValue + "', Vikladachi = '" + comboBox2.SelectedValue + "' WHERE NavantazhenyaVikladachiv.Kod = '" + textBox1.Text + "'";
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaVikladachiv", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
<<<<<<< HEAD
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
=======
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
        }
    }
}
