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
                    mySqlDataAdapter = new MySqlDataAdapter("select * from Rozklad", conn);
                    DataSet DS = new DataSet();
                    mySqlDataAdapter.Fill(DS);
                    dataGridView1.DataSource = DS.Tables[0];
                    CloseConnection();
                }
                groupBox2.Visible = true;
                
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
    }
}
