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
    public partial class DeleteClient : Form
    {
        private int Id;
        private string _Name;

        private RentDataSet dsRent;
        private QueriesTableAdapter daQueries;

        public DeleteClient(int id, string name)
        {
            Id = id;
            _Name = name;

            dsRent = new RentDataSet();
            daQueries = new QueriesTableAdapter();

            InitializeComponent();

            label1.Text = string.Format("Вы действительно хотите удалить клиента {0}", _Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            daQueries.Client_Delete(Id);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
