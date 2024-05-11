using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UchetOborudovania
{
    public partial class OknoAdministrator : Form
    {
        string podcklucheniekBD = @"Data Source = DESKTOP-LJ3RRK0\MSSQLSERVER2; Initial Catalog = UchetOborudovania; Integrated Security = true";
        public DataSet ds = new DataSet();
        public DataTable dataTable = new DataTable();

        public OknoAdministrator()
        {
            InitializeComponent();
            loadData();
        }

        public void loadData()
        {
            using (SqlConnection connection = new SqlConnection(podcklucheniekBD))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from Oborudovanie", connection);
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Dispose();
                    }
                }
            }
        }




        private void buttonNazad_Click(object sender, EventArgs e)
        {
            OknoAvtorizacia oknoAvtorizacia = new OknoAvtorizacia();
            oknoAvtorizacia.Show();
        }

        private void OknoAdministrator_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "uchetOborudovaniaDataSet1.IstoriaVhoda". При необходимости она может быть перемещена или удалена.
            this.istoriaVhodaTableAdapter.Fill(this.uchetOborudovaniaDataSet1.IstoriaVhoda);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "uchetOborudovaniaDataSet.Oborudovanie". При необходимости она может быть перемещена или удалена.
            // this.oborudovanieTableAdapter.Fill(this.uchetOborudovaniaDataSet.Oborudovanie);

            label3.Text = ds.Tables[0].Rows[0][1].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите сохранить изменения?", "Сохранение изменений", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(podcklucheniekBD))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Oborudovanie", connection);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataTable);
                        connection.Close();
                        MessageBox.Show("Изменения сохранены!");
                    }


                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Dispose();
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Изменения не сохранены!");
            }
        }
    }
}

