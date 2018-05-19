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
    public partial class Form4 : Form
    {
        public MySqlDataAdapter mySqlDataAdapter;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
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
                    mySqlDataAdapter = new MySqlDataAdapter("SELECT Groups.Kod, Groups.Nazva,(SELECT Specialnosti.Nazva FROM Specialnosti WHERE Groups.Specialnist = Specialnosti.Kod) AS `Specialnist` FROM Groups", conn);
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
            DataTable SpecialnistTable = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from Specialnosti";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(SpecialnistTable);
            comboBox1.DataSource = SpecialnistTable;
            comboBox1.ValueMember = "Kod";
            comboBox1.DisplayMember = "Nazva";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO Groups(Kod,Nazva,Specialnist) VALUES(@Kod, @Nazva, @Specialnist)";
                comm.Parameters.Add("@Kod", textBox1.Text);
                comm.Parameters.Add("@Nazva", textBox2.Text);
                comm.Parameters.Add("@Specialnist", comboBox1.SelectedValue);
                //MessageBox.Show("Kod= " + comboBox1.SelectedValue);
                comm.ExecuteNonQuery();
                mySqlDataAdapter = new MySqlDataAdapter("SELECT Groups.Kod, Groups.Nazva,(SELECT Specialnosti.Nazva FROM Specialnosti WHERE Groups.Specialnist = Specialnosti.Kod) AS `Specialnist` FROM Groups", conn);
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
            comm.CommandText = "DELETE FROM Groups WHERE Kod = " + textBox1.Text;
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("SELECT Groups.Kod, Groups.Nazva,(SELECT Specialnosti.Nazva FROM Specialnosti WHERE Groups.Specialnist = Specialnosti.Kod) AS `Specialnist` FROM Groups", conn);
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
            comm.CommandText = "UPDATE Groups SET Nazva = '" + textBox2.Text + "', Specialnist = '" + comboBox1.SelectedValue + "' WHERE Groups.Kod = '" + textBox1.Text + "'";
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("SELECT Groups.Kod, Groups.Nazva,(SELECT Specialnosti.Nazva FROM Specialnosti WHERE Groups.Specialnist = Specialnosti.Kod) AS `Specialnist` FROM Groups", conn);
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
