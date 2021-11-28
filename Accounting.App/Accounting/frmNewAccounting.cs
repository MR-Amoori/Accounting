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
using Accounting.Utility.Convertor;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        UnitOfWork db;
        public int AccountID = 0;
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNamesCustomers();
            if (AccountID != 0)
            {
                var Account = db.AccountingReposiory.GetById(AccountID);
                txtNmae.Text = db.CustomerRepository.GetCustomerNameById(Account.CostomerID);
                txtAmount.Text = Account.Amount.ToString();
                txtDescription.Text = Account.Description;
                if (Account.TypeID == 1)
                {
                    rbRecive.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویراش";
                btnSave.Text = "ویراش";

            }
            db.Dispose();
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNamesCustomers(txtfilter.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked || rbRecive.Checked)
                {
                    // DateTime date = DateConvertor.ToMiladi(DateTime.Now); // برای اینه توی همه سیستم ها تاریخ به صورت میلادی ذخیره شود
                    DateTime date = DateTime.Now;
                    db = new UnitOfWork();
                    DataLayer.Accounting accounting = new DataLayer.Accounting()
                    {
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        CostomerID = db.CustomerRepository.GetCustomerIdByName(txtNmae.Text),
                        TypeID = (rbRecive.Checked) ? 1 : 2,
                        DateTitle = date,
                        Description = txtDescription.Text,
                    };

                    if (AccountID == 0)
                    {
                        db.AccountingReposiory.Insert(accounting);
                        db.Save();
                    }
                    else
                    {

                        accounting.ID = AccountID;
                        db.AccountingReposiory.Update(accounting);
                    }
                    db.Save();
                    db.Dispose();
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
