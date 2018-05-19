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
    public partial class Form1 : Form
    {
        public MySqlDataAdapter mySqlDataAdapter;
        private string serverName; // Адрес сервера
        private string userName; // Имя пользователя
        private string dbName; //Имя базы данных
        private string port = "3306"; // Порт для подключения
        private string password; // Пароль для подключения
        public string connStr;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string serverName = textBox1.Text; // Адрес сервера
            string userName = textBox2.Text; // Имя пользователя
            string dbName = textBox3.Text; //Имя базы данных
            string password = textBox4.Text; // Пароль для подключения
            SQL.connStr = "server=" + serverName +
                ";user=" + userName +
                ";database=" + dbName +
                ";port=" + port +
                ";password=" + password + ";charset=utf8;";
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
                groupBox1.Visible = false;
                menuStrip1.Visible = true;
                if (OpenConnection() == true)
                {
                    mySqlDataAdapter = new MySqlDataAdapter("SELECT Rozklad.Kod,Rozklad.DayOfWeek,Rozklad.Para,(SELECT Groups.Nazva FROM Groups WHERE Rozklad.Grupa = Groups.Kod) as `Grupa`, (SELECT Vikladachi.PIB FROM Vikladachi WHERE Rozklad.Vikladach = Vikladachi.Number) as `Vikladach`, (SELECT Predmeti.Nazva FROM Predmeti WHERE Rozklad.Predmet = Predmeti.Kod) as `Predmet`, Rozklad.Auditoria, Rozklad.Week FROM Rozklad", conn);
                    DataSet DS = new DataSet();
                    mySqlDataAdapter.Fill(DS);
                    dataGridView1.DataSource = DS.Tables[0];
                    CloseConnection();
                }
                groupBox2.Visible = true;
                DataTable GroupTable = new DataTable();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Groups";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(GroupTable);
                comboBox1.DataSource = GroupTable;
                comboBox1.ValueMember = "Kod";
                comboBox1.DisplayMember = "Nazva";

                DataTable VikladachTable = new DataTable();
                MySqlCommand cmd1 = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Vikladachi";
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                adapter.Fill(VikladachTable);
                comboBox2.DataSource = VikladachTable;
                comboBox2.ValueMember = "Number";
                comboBox2.DisplayMember = "PIB";

                DataTable PredmetTable = new DataTable();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Predmeti";
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
                adapter.Fill(PredmetTable);
                comboBox3.DataSource = PredmetTable;
                comboBox3.ValueMember = "Kod";
                comboBox3.DisplayMember = "Nazva";

                DataTable AuditoriiTable = new DataTable();
                MySqlCommand cmd3 = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Auditorii";
                MySqlDataAdapter adapter3 = new MySqlDataAdapter(cmd3);
                adapter.Fill(AuditoriiTable);
                comboBox4.DataSource = AuditoriiTable;
                comboBox4.ValueMember = "Kod";
                comboBox4.DisplayMember = "Kod";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        //СПЕЦІАЛЬНОСТІ
        Form2 form2;
        private void спеціальностіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.ShowDialog();
            
        }
        //ВИКЛАДАЧІ
        Form3 form3;
        private void викладачіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.ShowDialog();
        }

        //ГРУПИ
        Form4 form4;
        private void групиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form4 = new Form4();
            form4.ShowDialog();
        }

        //АУДИТОРІЇ
        Form5 form5;
        private void аудиторіїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form5 = new Form5();
            form5.ShowDialog();
        }

        //ПРЕДМЕТИ
        Form6 form6;
        private void предметиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form6 = new Form6();
            form6.ShowDialog();
        }

        //НАВАНТАЖЕННЯ ГРУП
        Form7 form7;
        private void навантаженняГрупToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form7 = new Form7();
            form7.ShowDialog();
        }

        //НАВАНТАЖЕННЯ ВИКЛАДАЧІВ
        Form8 form8;
        private void навантаженняВикладачівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form8 = new Form8();
            form8.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(SQL.connStr);
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO Rozklad(Kod,DayOfWeek,Para,Grupa,Vikladach,Predmet,Auditoria,Week) VALUES(@Kod, @DayOfWeek, @Para, @Grupa, @Vikladach, @Predmet, @Auditoria, @Week)";
                comm.Parameters.Add("@Kod", textBox5.Text);
                comm.Parameters.Add("@DayOfWeek", comboBox6.SelectedItem);
                comm.Parameters.Add("@Para", comboBox5.SelectedItem);
                comm.Parameters.Add("@Grupa", comboBox1.SelectedValue);
                comm.Parameters.Add("@Vikladach", comboBox2.SelectedValue);
                comm.Parameters.Add("@Predmet", comboBox3.SelectedValue);
                comm.Parameters.Add("@Auditoria", comboBox4.SelectedValue);
                comm.Parameters.Add("@Week", comboBox7.SelectedItem);
                comm.ExecuteNonQuery();
                mySqlDataAdapter = new MySqlDataAdapter("SELECT Rozklad.Kod,Rozklad.DayOfWeek,Rozklad.Para,(SELECT Groups.Nazva FROM Groups WHERE Rozklad.Grupa = Groups.Kod) as `Grupa`, (SELECT Vikladachi.PIB FROM Vikladachi WHERE Rozklad.Vikladach = Vikladachi.Number) as `Vikladach`, (SELECT Predmeti.Nazva FROM Predmeti WHERE Rozklad.Predmet = Predmeti.Kod) as `Predmet`, Rozklad.Auditoria, Rozklad.Week FROM Rozklad", conn);
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
                comm.CommandText = "DELETE FROM Rozklad WHERE Kod = " + textBox5.Text;
                comm.ExecuteNonQuery();
                mySqlDataAdapter = new MySqlDataAdapter("SELECT Rozklad.Kod,Rozklad.DayOfWeek,Rozklad.Para,(SELECT Groups.Nazva FROM Groups WHERE Rozklad.Grupa = Groups.Kod) as `Grupa`, (SELECT Vikladachi.PIB FROM Vikladachi WHERE Rozklad.Vikladach = Vikladachi.Number) as `Vikladach`, (SELECT Predmeti.Nazva FROM Predmeti WHERE Rozklad.Predmet = Predmeti.Kod) as `Predmet`, Rozklad.Auditoria, Rozklad.Week FROM Rozklad", conn);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
            MySqlConnection conn = new MySqlConnection(SQL.connStr);
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
                string dw = comboBox6.SelectedItem.ToString();
                if (dw.Equals("П'ятниця"))
                {
                    dw = "П\\'ятниця";
                }
            comm.CommandText = "UPDATE Rozklad SET DayOfWeek = '" + dw + "', Para = '" + comboBox5.SelectedItem + "', Grupa = '" + comboBox1.SelectedValue + "', Vikladach = '" + comboBox2.SelectedValue + "', Predmet = '" + comboBox3.SelectedValue + "', Auditoria = '" + comboBox4.SelectedValue + "', Week = '" + comboBox7.SelectedItem + "' WHERE Rozklad.Kod = '" + textBox5.Text + "'";
            comm.ExecuteNonQuery();
            mySqlDataAdapter = new MySqlDataAdapter("SELECT Rozklad.Kod,Rozklad.DayOfWeek,Rozklad.Para,(SELECT Groups.Nazva FROM Groups WHERE Rozklad.Grupa = Groups.Kod) as `Grupa`, (SELECT Vikladachi.PIB FROM Vikladachi WHERE Rozklad.Vikladach = Vikladachi.Number) as `Vikladach`, (SELECT Predmeti.Nazva FROM Predmeti WHERE Rozklad.Predmet = Predmeti.Kod) as `Predmet`, Rozklad.Auditoria, Rozklad.Week FROM Rozklad", conn);
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

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "C:\\DataBaseBackup.sql";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "SQL Dump File (*.sql)|*.sql";
                sfd.FileName = "DataBaseBackup.sql";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    file = sfd.FileName;
                }
                using (MySqlConnection conn = new MySqlConnection(SQL.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            mySqlDataAdapter = new MySqlDataAdapter("SELECT Rozklad.Kod,Rozklad.DayOfWeek,Rozklad.Para,(SELECT Groups.Nazva FROM Groups WHERE Rozklad.Grupa = Groups.Kod) as `Grupa`, (SELECT Vikladachi.PIB FROM Vikladachi WHERE Rozklad.Vikladach = Vikladachi.Number) as `Vikladach`, (SELECT Predmeti.Nazva FROM Predmeti WHERE Rozklad.Predmet = Predmeti.Kod) as `Predmet`, Rozklad.Auditoria, Rozklad.Week FROM Rozklad", conn);
                            DataSet DS = new DataSet();
                            mySqlDataAdapter.Fill(DS);
                            dataGridView1.DataSource = DS.Tables[0];
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void зберегтиВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "C:\\DataBaseBackup.sql";
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.Filter = "SQL Dump File (*.sql)|*.sql";
                sfd.FileName = "DataBaseBackup.sql";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    file = sfd.FileName;
                }
                using (MySqlConnection conn = new MySqlConnection(SQL.connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(file);
                            mySqlDataAdapter = new MySqlDataAdapter("SELECT Rozklad.Kod,Rozklad.DayOfWeek,Rozklad.Para,(SELECT Groups.Nazva FROM Groups WHERE Rozklad.Grupa = Groups.Kod) as `Grupa`, (SELECT Vikladachi.PIB FROM Vikladachi WHERE Rozklad.Vikladach = Vikladachi.Number) as `Vikladach`, (SELECT Predmeti.Nazva FROM Predmeti WHERE Rozklad.Predmet = Predmeti.Kod) as `Predmet`, Rozklad.Auditoria, Rozklad.Week FROM Rozklad", conn);
                            DataSet DS = new DataSet();
                            mySqlDataAdapter.Fill(DS);
                            dataGridView1.DataSource = DS.Tables[0];
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
