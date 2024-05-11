using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UchetOborudovania.Classes
{
    internal class ClassAvtorizacia
    {
        //  string podcklucheniekBD = @"Data Source = DESKTOP-LJ3RRK0\MSSQLSERVER2; DataBase = UchetOborudovania; User ID = user01; Password = 123";
        string podcklucheniekBD = @"Data Source = DESKTOP-LJ3RRK0\MSSQLSERVER2; Initial Catalog = UchetOborudovania; Integrated Security = true";
        DataSet ds = new DataSet();


        // Вход пользователя в систему в соответствии с введённым логином и паролем
        public DataSet getUser(string login, string password)
        {
            string zapros = string.Format("Select * from Sotrudniki where login = '{0}' and password = '{1}'", login, password);

            using (SqlConnection connection = new SqlConnection(podcklucheniekBD))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(zapros, connection);
                    adapter.Fill(ds);
                    connection.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if(connection.State == ConnectionState.Open)
                    {
                        connection.Dispose();
                    }
                }
            }
            return ds;
        }


    }
}
