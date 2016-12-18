using Rent.Data;
using Rent.Data.RentDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent.CRUDForms
{
    public partial class DeleteContract : Form
    {
        private int Number;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public DeleteContract(int number)
        {
            Number = number;

            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();

            label1.Text = string.Format("Вы действительно хотите удалить договор №{0}", Number);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            daQueries.Contract_Delete(Number);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
