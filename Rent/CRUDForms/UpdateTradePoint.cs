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
    public partial class UpdateTradePoint : Form
    {
        private int Number;
        private int Floor;
        private bool Conditioning;
        private int Cost;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public UpdateTradePoint(int id, int floor, bool conditioning, int cost)
        {
            Number = id;

            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();

            label1.Text = "Номер " + Number;
            textBox2.Text = floor.ToString();
            checkBox1.Checked = conditioning;
            textBox3.Text = cost.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                daQueries.Trade_Point_Update(Number, Floor, Conditioning, Cost);
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
