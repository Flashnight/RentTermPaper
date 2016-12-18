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
    public partial class CreatePayment : Form
    {
        private int Id;
        private int Sum;
        private DateTime Date;
        private int ContractNumber;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public CreatePayment()
        {
            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Id = int.Parse(textBox1.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Id должно быть указано число.");
                return;
            }
            try
            {
                Sum = int.Parse(textBox2.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Сумма должно быть указано число.");
                return;
            }
            Date = dateTimePicker1.Value;
            if (Date == null)
            {
                MessageBox.Show("Дата платежа должна быть указана.");
                return;
            }
            try
            {
                ContractNumber = int.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Договор должно быть указано число.");
                return;
            }

            try
            {
                daQueries.Payment_Insert(Id, Date, Sum, ContractNumber);
                Close();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show("Укажите действительный номер договора.");
                        break;
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
