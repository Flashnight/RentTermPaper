using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rent.CRUDForms;
using Rent.Data;
using Rent.Data.RentDataSetTableAdapters;

namespace Rent
{
    public partial class Form1 : Form
    {
        private const string ClientName = "Client";
        private const string ContractName = "Contract";
        private const string PaymentName = "Payment";
        private const string RentName = "Rent";
        private const string TradePointName = "Trade_Point";

        private string currentEntity;
        private List<string> displayTabPageNames;
        private RentDataSet dsRent;

        private Client_SelectTableAdapter daClient;
        private Contract_SelectTableAdapter daContract;
        private Payment_SelectTableAdapter daPayment;
        private Rent_SelectTableAdapter daRent;
        private Trade_Point_SelectTableAdapter daTradePoint;

        private UCGrid grid;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dsRent = new RentDataSet();
            
            daClient = new Client_SelectTableAdapter();
            daContract = new Contract_SelectTableAdapter();
            daPayment = new Payment_SelectTableAdapter();
            daRent = new Rent_SelectTableAdapter();
            daTradePoint = new Trade_Point_SelectTableAdapter();

            displayTabPageNames = new List<string>();

            treeView1.Nodes.Add("Rent", "Rent", 0, 0);
            treeView1.Nodes[0].Nodes.Add(ClientName, ClientName, 1, 1);
            treeView1.Nodes[0].Nodes.Add(ContractName, ContractName, 1, 1);
            treeView1.Nodes[0].Nodes.Add(PaymentName, PaymentName, 1, 1);
            treeView1.Nodes[0].Nodes.Add(RentName, RentName, 1, 1);
            treeView1.Nodes[0].Nodes.Add(TradePointName, TradePointName, 1, 1);
            treeView1.ExpandAll();
            tabControl1.ContextMenuStrip = tabContextMenu;
            bindingNavigator1.BindingSource = bindingSource1;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Parent != null)
            {
                currentEntity = treeView1.SelectedNode.Name;
                switch (currentEntity)
                {
                    case ClientName:
                        bindingSource1.DataSource = dsRent.Client_Select;
                        daClient.Fill(dsRent.Client_Select, null);
                        lbRecordsCount.Text = string.Format("{0} records", dsRent.Client_Select.Count);
                        break;
                    case ContractName:
                        bindingSource1.DataSource = dsRent.Contract_Select;
                        daContract.Fill(dsRent.Contract_Select, null, null);
                        lbRecordsCount.Text = string.Format("{0} records", dsRent.Contract_Select.Count);
                        break;
                    case PaymentName:
                        bindingSource1.DataSource = dsRent.Payment_Select;
                        daPayment.Fill(dsRent.Payment_Select, null, null);
                        lbRecordsCount.Text = string.Format("{0} records", dsRent.Payment_Select.Count);
                        break;
                    case RentName:
                        bindingSource1.DataSource = dsRent.Rent_Select;
                        daRent.Fill(dsRent.Rent_Select, null, null, null);
                        lbRecordsCount.Text = string.Format("{0} records", dsRent.Payment_Select.Count);
                        break;
                    case TradePointName:
                        bindingSource1.DataSource = dsRent.Trade_Point_Select;
                        daTradePoint.Fill(dsRent.Trade_Point_Select, null);
                        lbRecordsCount.Text = string.Format("{0} records", dsRent.Payment_Select.Count);
                        break;
                }

                if (!displayTabPageNames.Contains(currentEntity))
                {
                    displayTabPageNames.Add(currentEntity);
                    TabPage page = new TabPage(currentEntity);
                    page.Name = currentEntity;
                    grid = new UCGrid();
                    grid.Name = "dgv" + currentEntity;
                    grid.Parent = page;
                    grid.Dock = DockStyle.Fill;
                    grid.DataSource = bindingSource1;
                    tabControl1.TabPages.Add(page);
                }
                else
                {
                    grid = (UCGrid) tabControl1.TabPages[currentEntity].Controls[0];
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                currentEntity = tabControl1.SelectedTab.Name;
                treeView1.Focus();
                treeView1.SelectedNode = treeView1.Nodes[0].Nodes[currentEntity];
            }
            else
            {
                currentEntity = "";
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayTabPageNames.Remove(currentEntity);
            TabPage page = tabControl1.TabPages[currentEntity];
            tabControl1.TabPages.Remove(page);
        }

        private void closeAllBuThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = displayTabPageNames.Count - 1; i >= 0; i--)
            {
                string name = displayTabPageNames[i];
                if (currentEntity != name)
                {
                    tabControl1.TabPages.Remove(tabControl1.TabPages[name]);
                    displayTabPageNames.Remove(name);
                }
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            displayTabPageNames.Clear();
            treeView1.SelectedNode = null;
            bindingSource1.DataSource = null;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent != null)
            {
                if (currentEntity == ClientName)
                {
                    CreateClient createClient = new CreateClient();
                    createClient.ShowDialog();
                }
                if (currentEntity == ContractName)
                {
                    CreateContract createContract = new CreateContract();
                    createContract.ShowDialog();
                }
                if (currentEntity == PaymentName)
                {
                    CreatePayment createPayment = new CreatePayment();
                    createPayment.ShowDialog();
                }
                if (currentEntity == RentName)
                {
                    CreateRent createRent = new CreateRent();
                    createRent.ShowDialog();
                }
                if (currentEntity == TradePointName)
                {
                    CreateTradePoint createTradePoint = new CreateTradePoint();
                    createTradePoint.ShowDialog();
                }
                grid.Refresh();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (grid != null)
            {
                var dgv = grid.DataGridView;
                var selectedCell = dgv.SelectedCells;
                var selectedRow = selectedCell[0].OwningRow.Index;
                int id = (int)dgv[0, selectedRow].Value;

                switch (grid.Name)
                {
                    case "dgv" + ClientName:
                        string name = (string) dgv[1, selectedRow].Value;
                        DeleteClient deleteClient = new DeleteClient(id, name);
                        deleteClient.ShowDialog();
                        break;
                    case "dgv" + ContractName:
                        DeleteContract deleteContract = new DeleteContract(id);
                        deleteContract.ShowDialog();
                        break;
                    case "dgv" + PaymentName:
                        DeletePayment deletePayment = new DeletePayment(id);
                        deletePayment.ShowDialog();
                        break;
                    case "dgv" + RentName:
                        DeleteRent deleteRent = new DeleteRent(id);
                        deleteRent.ShowDialog();
                        break;
                    case "dgv" + TradePointName:
                        DeleteTradePoint deleteTradePoint = new DeleteTradePoint(id);
                        deleteTradePoint.ShowDialog();
                        break;
                }
                dgv.Refresh();
            }
            
        }
    }
}
