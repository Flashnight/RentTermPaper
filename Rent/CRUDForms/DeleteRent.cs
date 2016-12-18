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
    public partial class DeleteRent : Form
    {
        private int Id;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public DeleteRent(int id)
        {
            Id = id;

            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();

            label1.Text = string.Format("Вы действительно хотите удалить аренду №{0}", Id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            daQueries.Rent_Delete(Id);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
