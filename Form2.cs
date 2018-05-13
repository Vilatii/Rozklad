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
    
    public partial class Form2 : Form
    {
        public MySqlDataAdapter mySqlDataAdapter;
        public Form2()
        {
            InitializeComponent();
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
                    mySqlDataAdapter = new MySqlDataAdapter("select * from Specialnosti", conn);
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
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "INSERT INTO Specialnosti(Kod,Nazva) VALUES(@Kod, @Nazva)";
            comm.Parameters.Add("@Kod", textBox1.Text);
            comm.Parameters.Add("@Nazva", textBox2.Text);
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from Specialnosti", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "DELETE FROM Specialnosti WHERE Kod = " + textBox1.Text;
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from Specialnosti", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE Specialnosti SET Nazva = '" + textBox2.Text + "' WHERE Specialnosti.Kod = '" + textBox1.Text + "'";
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from Specialnosti", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
