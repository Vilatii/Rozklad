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
    public partial class Form7 : Form
    {
        public MySqlDataAdapter mySqlDataAdapter;
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
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
                    mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaGroup", conn);
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
            DataTable GroupTable = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from Groups";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(GroupTable);
            comboBox1.DataSource = GroupTable;
            comboBox1.ValueMember = "Kod";
            comboBox1.DisplayMember = "Nazva";

            DataTable PredmetiTable = new DataTable();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "Select * from Predmeti";
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd2);
            adapter1.Fill(PredmetiTable);
            comboBox2.DataSource = PredmetiTable;
            comboBox2.ValueMember = "Kod";
            comboBox2.DisplayMember = "Nazva";
        }

        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO NavantazhenyaGroup(Kod,Grupa,Predmet,HourOfLekcia,HourOfLB,HourOfPR,SemestrControl,Semestr) VALUES(@Kod, @Grupa, @Predmet, @HourOfLekcia, @HourOfLB, @HourOfPR, @SemestrControl, @Semestr)";
                comm.Parameters.Add("@Kod", textBox1.Text);
                comm.Parameters.Add("@Grupa", comboBox1.SelectedValue);
                comm.Parameters.Add("@Predmet", comboBox2.SelectedValue);
                comm.Parameters.Add("@HourOfLekcia", textBox2.Text);
                comm.Parameters.Add("@HourOfLB", textBox3.Text);
                comm.Parameters.Add("@HourOfPR", textBox4.Text);
                comm.Parameters.Add("@SemestrControl", textBox5.Text);
                comm.Parameters.Add("@Semestr", textBox6.Text);
                comm.ExecuteNonQuery();
                mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaGroup", conn);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
<<<<<<< HEAD
=======
=======
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "INSERT INTO NavantazhenyaGroup(Kod,Grupa,Predmet,HourOfLekcia,HourOfLB,HourOfPR,SemestrControl,Semestr) VALUES(@Kod, @Grupa, @Predmet, @HourOfLekcia, @HourOfLB, @HourOfPR, @SemestrControl, @Semestr)";
            comm.Parameters.Add("@Kod", textBox1.Text);
            comm.Parameters.Add("@Grupa", comboBox1.SelectedValue);
            comm.Parameters.Add("@Predmet", comboBox2.SelectedValue);
            comm.Parameters.Add("@HourOfLekcia", textBox2.Text);
            comm.Parameters.Add("@HourOfLB", textBox3.Text);
            comm.Parameters.Add("@HourOfPR", textBox4.Text);
            comm.Parameters.Add("@SemestrControl", textBox5.Text);
            comm.Parameters.Add("@Semestr", textBox6.Text);
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaGroup", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
        }

        private void button2_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "DELETE FROM NavantazhenyaGroup WHERE Kod = " + textBox1.Text;
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaGroup", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
<<<<<<< HEAD
=======
=======
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
        }

        private void button3_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
<<<<<<< HEAD
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
=======
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE NavantazhenyaGroup SET HourOfLekcia = '" + textBox2.Text + "', Grupa = '" + comboBox1.SelectedValue + "', Predmet = '" + comboBox2.SelectedValue + "', HourOfLB = '" + textBox3.Text + "', HourOfPR = '" + textBox4.Text + "', SemestrControl = '" + textBox5.Text + "', Semestr = '" + textBox6.Text + "' WHERE NavantazhenyaGroup.Kod = '" + textBox1.Text + "'";
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("select * from NavantazhenyaGroup", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conn.Close();
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
<<<<<<< HEAD
=======
=======
>>>>>>> b39c37e61a0ed733585e177e67087753a5675a73
>>>>>>> d9987b4d89360c24a36f19e2d5a8e4fe4b2629ba
        }
    }
}
