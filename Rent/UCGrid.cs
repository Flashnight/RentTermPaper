using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rent.CRUDForms;

namespace Rent
{
    public partial class UCGrid : UserControl
    {
        private const string ClientName = "Client";
        private const string ContractName = "Contract";
        private const string PaymentName = "Payment";
        private const string RentName = "Rent";
        private const string TradePointName = "Trade_Point";

        public UCGrid()
        {
            InitializeComponent();
        }

        public object DataSource
        {
            set { dgvData.DataSource = value; }
        }

        public DataGridView DataGridView
        {
            get { return dgvData; }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int) dgvData[0, e.RowIndex].Value;

            switch (Name)
            {
                case "dgv" + ClientName:
                    UpdateClient updateClient = new UpdateClient(id);
                    updateClient.ShowDialog();
                    break;
                case "dgv" + ContractName:
                    UpdateContract updateContract = new UpdateContract(id);
                    updateContract.ShowDialog();
                    break;
                case "dgv" + PaymentName:
                    UpdatePayment updatePayment = new UpdatePayment(id);
                    updatePayment.ShowDialog();
                    break;
                case "dgv" + RentName:
                    UpdateRent updateRent = new UpdateRent(id);
                    updateRent.ShowDialog();
                    break;
                case "dgv" + TradePointName:
                    UpdateTradePoint updateTradePoint = new UpdateTradePoint(id);
                    updateTradePoint.ShowDialog();
                    break;
            }

            Refresh();
        }
    }
}
