using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork1
{
    public partial class Form1 : Form
    {
        private string stringConnection =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=QuoteData;";
        SqlConnection conn = null;
        SqlCommand cmd = null;
        public Form1()
        {
            InitializeComponent();

            conn = new SqlConnection();
            conn.ConnectionString = stringConnection;
            conn.Open();
            conn.Close();

            cmd = conn.CreateCommand();
            string select = "select name, @selectCol from person " +
                "where name like @startNameChar";
            cmd.CommandText = select;
            cmd.Parameters
                .Add("@startNameChar", SqlDbType.NVarChar, 10)
                .Value = "L%";
            cmd.Parameters.AddWithValue("@selectCol", "salary");

            SqlDataReader reader = cmd.ExecuteReader();
            // считывание результата
            reader.Close();

            string select2 = "select * from person";
            cmd.CommandText = select;
            reader = cmd.ExecuteReader();
            // считывание результата
            while (reader.Read()) { }
            reader.Close();

            string select3 = "select * from person; select name from person;";
            cmd.CommandText = select3;
            string select4 = "select * from person";
            cmd.CommandText += select4;
            do
            {
                while (reader.Read()) { }
            }
            while (reader.NextResult());

            string select5 = "select avg(salary) from person";
            cmd.CommandText = select5;
            float avgSalary = (float)cmd.ExecuteScalar();

            string select6 = "select name from person" +
                "having name > sum(salary)";
        }
    }
}
