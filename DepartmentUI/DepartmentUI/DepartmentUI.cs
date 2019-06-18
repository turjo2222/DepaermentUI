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

namespace DepartmentUI
{
    public partial class DepartmentUI : Form
    {
        class Department
        {
            public int ID { set; get; }
            public string Name { set; get; }
            public string Code { set; get; }
        }
        public DepartmentUI()
        {
            InitializeComponent();
        }

        private void Insert(Department department)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection();
                string connectionString1 = @"Server=GUB_IT\SQLEXPRESS; Database=StudentDb; integrated security=True";
                sqlConnection.ConnectionString = connectionString1;

                SqlCommand sqlCommand = new SqlCommand();
                string commandString = @"INSERT INTO Departments (Name,Code) values ('"+department.Name+"','"+department.Code+"')";
                sqlCommand.CommandText = commandString;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                int ifExcuted = 0;
                ifExcuted = sqlCommand.ExecuteNonQuery();

                if (ifExcuted > 0)
                {
                    MessageBox.Show("Save");

                }
                else
                {
                    MessageBox.Show("notsave");
                }

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // String name = nameTextBox.Text;
            //String code = codeTextBox.Text;
            // Insert(name, code);

            Department department = new Department();

            department.Name = nameTextBox.Text;
           department.Code = codeTextBox.Text;
            Insert(department);
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            string connectionString1 = @"Server=GUB_IT\SQLEXPRESS; Database=StudentDb; integrated security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString1);
            string sqlString = @"SELECT * FROM Departments";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            showDataGridView.DataSource = dataTable;
            sqlConnection.Close();
        }


        private void Delete(Department department)
        {
            string connectionString = @"Server=GUB_IT\SQLEXPRESS; Database=StudentDb; integrated security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string sqlstring = @"DELETE Departments WHERE ID = "+department.ID+"";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Department department = new Department();

            department.ID = Convert.ToInt32(idTextBox.Text);
          
            Delete(department);

        }
    }
}
