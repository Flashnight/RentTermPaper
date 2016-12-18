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
    public partial class CreateTradePoint : Form
    {
        private int Number;
        private int Floor;
        private bool Conditioning;
        private int Cost;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public CreateTradePoint()
        {
            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Number = int.Parse(textBox1.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Номер должно быть указано число.");
                return;
            }
            try
            {
                Floor = int.Parse(textBox2.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Этаж должно быть указано число.");
                return;
            }
            Conditioning = checkBox1.Checked;
            try
            {
                Cost = int.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("В поле Стоимость должно быть указано число.");
                return;
            }


            try
            {
                daQueries.Trade_Point_Insert(Number, Floor, Conditioning, Cost);
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
