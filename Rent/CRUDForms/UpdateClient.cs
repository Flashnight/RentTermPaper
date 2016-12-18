using Rent.Data;
using Rent.Data.RentDataSetTableAdapters;
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

namespace Rent.CRUDForms
{
    public partial class UpdateClient : Form
    {
        private int Id;
        private string _Name;
        private string Adress;
        private string Phone;
        private string Requisites;
        private string ContactPerson;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public UpdateClient(int id)
        {
            Id = id;

            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();

            label1.Text = "Id " + Id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Name = textBox2.Text;
            Adress = textBox3.Text;
            Phone = textBox4.Text;
            Requisites = textBox5.Text;
            ContactPerson = textBox6.Text;

            if (_Name.Length > 30)
            {
                MessageBox.Show("В поле Название должно быть не больше 30 символов.");
                return;
            }
            if (Adress.Length > 50)
            {
                MessageBox.Show("В поле Адрес должно быть не больше 50 символов.");
                return;
            }
            if (Phone.Length > 20)
            {
                MessageBox.Show("В поле Телефон должно быть не больше 20 символов.");
                return;
            }
            if (Requisites.Length > 2000)
            {
                MessageBox.Show("В поле Реквезиты должно быть не больше 2000 символов.");
                return;
            }
            if (ContactPerson.Length > 50)
            {
                MessageBox.Show("В поле Контактное лицо должно быть не больше 50 символов.");
                return;
            }
            if (string.IsNullOrWhiteSpace(_Name) || string.IsNullOrWhiteSpace(Adress)
                || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Requisites)
                || string.IsNullOrWhiteSpace(ContactPerson))
            {
                MessageBox.Show("Все поля должны быть указаны!");
                return;
            }

            try
            {
                daQueries.Client_Update(Id, _Name, Adress, Phone, Requisites, ContactPerson);
                Close();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Этот Id занят.");
                        break;
                    default:
                        throw ex;
                }
            }
        }
    }
}
