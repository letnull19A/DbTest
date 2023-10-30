using Microsoft.VisualBasic.Logging;
using System.Data;
using System.Data.SqlClient;

namespace DbTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var connection = new SqlConnection("Data Source=DESKTOP-EDPUPHE\\SQLEXPRESS; Initial Catalog=TestDB; Integrated Security=true; TrustServerCertificate=True;");

            connection.Open();

            var adapter = new SqlDataAdapter("SELECT * FROM users", connection);

            var dataSet = new DataSet();

            adapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];

            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection("Data Source=DESKTOP-EDPUPHE\\SQLEXPRESS; Initial Catalog=TestDB; Integrated Security=true; TrustServerCertificate=True;"))
            {
                // вставка
                connection.Open();
                var login = new SqlParameter("@Login", textBoxLogin.Text);
                var name = new SqlParameter("@Name", textBoxName.Text);
                var surname = new SqlParameter("@Surname", textBoxSurname.Text);
                var fatherName = new SqlParameter("@FatherName", textBoxFatherName.Text);

                var query = new SqlCommand("INSERT INTO users (name, surname, fatherName, login) VALUES (@Name, @Surname, @FatherName, @Login)", connection);

                query.Parameters.Add(login);
                query.Parameters.Add(name);
                query.Parameters.Add(surname);
                query.Parameters.Add(fatherName);

                query.ExecuteNonQuery();

                // обновление
                var adapter = new SqlDataAdapter("SELECT * FROM users", connection);

                var dataSet = new DataSet();

                adapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection("Data Source=DESKTOP-EDPUPHE\\SQLEXPRESS; Initial Catalog=TestDB; Integrated Security=true; TrustServerCertificate=True;"))
            {
                connection.Open();

                // поиск
                var query = new SqlCommand($"SELECT * FROM users WHERE surname LIKE '%{textBoxSearch.Text}%'", connection);

                var adapter = new SqlDataAdapter(query);

                var dataSet = new DataSet();

                adapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
                connection.Close();
            }
        }
    }
}