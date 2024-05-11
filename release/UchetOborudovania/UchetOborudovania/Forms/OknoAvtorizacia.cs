using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UchetOborudovania.Classes;

namespace UchetOborudovania
{
    public partial class OknoAvtorizacia : Form
    {
        ClassAvtorizacia classAvtorizacia = new ClassAvtorizacia();
        


        public OknoAvtorizacia()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OknoSotrudnik oknoSotrudnik = new OknoSotrudnik();
            oknoSotrudnik.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OknoRucovoditel oknoRucovoditel = new OknoRucovoditel();
            oknoRucovoditel.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OknoAdministrator oknoAdministrator = new OknoAdministrator();
            oknoAdministrator.Show();
        }

        private void buttonVhod_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            DataSet user = classAvtorizacia.getUser(login, password);

            if (user.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Пользователь не найден!");

            }
            else if(user.Tables[0].Rows.Count > 1)
            {
                MessageBox.Show("Пользователей найдено больше одного!");
            }
            else
            {
                switch (user.Tables[0].Rows[0][4])
                {
                    case "Сотрудник":
                        MessageBox.Show("Пользователь сотрудник!");

                        OknoSotrudnik oknoSotrudnik = new OknoSotrudnik();
                        oknoSotrudnik.ds = user;
                        oknoSotrudnik.Show();

                        break;
                    case "Администратор":
                        MessageBox.Show("Пользователь администратор!");
                        OknoAdministrator oknoAdministrator = new OknoAdministrator();
                        oknoAdministrator.ds = user;
                        oknoAdministrator.Show();
                        break;
                    case "Руководитель":
                        MessageBox.Show("Пользователь руководитель!");
                        OknoRucovoditel oknoRucovoditel = new OknoRucovoditel();
                        oknoRucovoditel.ds = user;
                        oknoRucovoditel.Show();
                        break;
                    default:
                        MessageBox.Show("Данные из БД выгружены, но роль не найдена!");
                        break;
                }
            }


        }

        private void checkBoxMaska_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxMaska.Checked == true)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '*';
            }
        }
    }
}
