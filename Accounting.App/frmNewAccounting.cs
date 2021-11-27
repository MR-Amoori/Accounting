using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.UnitOfWork;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        UnitOfWork db = new UnitOfWork();
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNamesCustomers();
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNamesCustomers(txtfilter.Text);
            string a = db.CustomerRepository.GetCustomerNameById(int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString()));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked||rbRecive.Checked)
                {
                    DataLayer.Accounting accounting = new DataLayer.Accounting()
                    {
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        CostomerID = db.CustomerRepository.GetCustomerIdByName(txtNmae.Text),
                        TypeID=(rbPay.Checked)?1:2,
                        DateTitle=DateTime.Now,
                        Description=txtDescription.Text,
                    };
                    db.AccountingReposiory.Insert(accounting);
                    db.Save();
                    DialogResult = DialogResult.OK;
                }

                else
                {
                    MessageBox.Show("لطفا نوع تراکنش را انتخاب کنید");
                }
            }

        }
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNmae.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
