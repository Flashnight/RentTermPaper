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
    public partial class CreateRent : Form
    {
        private int Id;
        private int ContractNumber;
        private int TradePoint;
        private DateTime RentalStart;
        private DateTime RentalEnd;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public CreateRent()
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
                ContractNumber = int.Parse(textBox2.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Договор должно быть указано число.");
                return;
            }
            try
            {
                TradePoint = int.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Торговая точка должно быть указано число.");
                return;
            }
            RentalStart = dateTimePicker1.Value;
            if (RentalStart == null)
            {
                MessageBox.Show("Начало аренды должно быть указано.");
            }
            RentalEnd = dateTimePicker2.Value;
            if (RentalEnd == null)
            {
                MessageBox.Show("Конец аренды должен быть указан.");
            }
            if (RentalStart > RentalEnd)
            {
                MessageBox.Show("Дата начала не должна быть больше даты конца.");
                return;
            }

            try
            {
                daQueries.Rent_Insert(Id, TradePoint, ContractNumber, RentalStart, RentalEnd);
                Close();
            }
            catch (SqlException ex)
            {

                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show("Проверьте номер договора и номер торговой точки.");
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
