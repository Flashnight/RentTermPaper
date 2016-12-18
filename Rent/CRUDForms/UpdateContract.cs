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
    public partial class UpdateContract : Form
    {
        private int Number;
        private DateTime ConclusionDate;
        private int ClientId;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public UpdateContract(int id)
        {
            Number = id;

            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();

            label1.Text = "Номер " + Number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConclusionDate = dateTimePicker1.Value;
            if (ConclusionDate == null)
            {
                MessageBox.Show("Дата заключения договора должна быть указана.");
                return;
            }
            try
            {
                ClientId = int.Parse(textBox2.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Id клиента должно быть указано число.");
                return;
            }

            try
            {
                daQueries.Contract_Update(Number, ClientId, ConclusionDate);
                Close();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show("Укажите Id действительного клиента.");
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
