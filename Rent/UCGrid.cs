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
                    string name = (string) dgvData[1, e.RowIndex].Value;
                    string adress = (string) dgvData[2, e.RowIndex].Value;
                    string phone = (string) dgvData[3, e.RowIndex].Value;
                    string requisites = (string) dgvData[4, e.RowIndex].Value;
                    string contactPerson = (string) dgvData[5, e.RowIndex].Value;
                    UpdateClient updateClient = new UpdateClient(id, name, adress, phone, requisites, contactPerson);
                    updateClient.ShowDialog();
                    break;
                case "dgv" + ContractName:
                    DateTime conclusionDate = (DateTime) dgvData[1, e.RowIndex].Value;
                    int clientId = (int) dgvData[2, e.RowIndex].Value;
                    UpdateContract updateContract = new UpdateContract(id, conclusionDate, clientId);
                    updateContract.ShowDialog();
                    break;
                case "dgv" + PaymentName:
                    int sum = (int) dgvData[1, e.RowIndex].Value;
                    DateTime date = (DateTime) dgvData[2, e.RowIndex].Value;
                    int contractId = (int) dgvData[3, e.RowIndex].Value;
                    UpdatePayment updatePayment = new UpdatePayment(id, sum, date, contractId);
                    updatePayment.ShowDialog();
                    break;
                case "dgv" + RentName:
                    int contrId = (int) dgvData[1, e.RowIndex].Value;
                    int tradePointId = (int) dgvData[2, e.RowIndex].Value;
                    DateTime rentalStart = (DateTime) dgvData[3, e.RowIndex].Value;
                    DateTime rentalEnd = (DateTime) dgvData[4, e.RowIndex].Value;
                    UpdateRent updateRent = new UpdateRent(id, contrId, tradePointId, rentalStart, rentalEnd);
                    updateRent.ShowDialog();
                    break;
                case "dgv" + TradePointName:
                    int floor = (int) dgvData[1, e.RowIndex].Value;
                    bool conditioning = (bool) dgvData[2, e.RowIndex].Value;
                    int cost = (int) dgvData[3, e.RowIndex].Value;
                    UpdateTradePoint updateTradePoint = new UpdateTradePoint(id, floor, conditioning, cost);
                    updateTradePoint.ShowDialog();
                    break;
            }

            Refresh();
        }
    }
}
