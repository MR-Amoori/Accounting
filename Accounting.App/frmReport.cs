using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer;
using Accounting.DataLayer.UnitOfWork;
using Accounting.Utility.Convertor;
using Accounting.ViewModels.Customers;

namespace Accounting.App
{
    public partial class frmReport : Form
    {
        UnitOfWork db;
        public int TypeID = 0;
        public frmReport()
        {
            InitializeComponent();
            db = new UnitOfWork();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
                list.Add(new ListCustomerViewModel()
                {
                    CustomerID = 0,
                    FullName = "انتخاب کنبد"
                });
                list.AddRange(db.CustomerRepository.GetNamesCustomers());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "FullName";
                cbCustomer.ValueMember = "CustomerID";
            }

            if (TypeID == 1)
            {
                this.Text = "گزارش دریافتی ها";
            }

            else
            {
                this.Text = "گزارش پرداختی ها";
            }
            Filter();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();
                DateTime? StartDate;
                DateTime? EndDate;

                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int CustomerId = (int)cbCustomer.SelectedValue;
                    result.AddRange(db.AccountingReposiory.Get(p => p.TypeID == TypeID && p.CostomerID == CustomerId));
                }
                else
                {
                    result.AddRange(db.AccountingReposiory.Get(p => p.TypeID == TypeID));
                }

                try
                {
                    if (txtFromDate.Text != "    /  /")
                    {
                        StartDate = Convert.ToDateTime(txtFromDate.Text);
                        // StartDate = DateConvertor.ToMiladi(StartDate.Value);
                        result = result.Where(p => p.DateTitle >= StartDate.Value).ToList();
                    }
                    if (txtToDate.Text != "    /  /")
                    {
                        EndDate = Convert.ToDateTime(txtToDate.Text);
                        // EndDate = DateConvertor.ToMiladi(EndDate.Value);
                        result = result.Where(p => p.DateTitle <= EndDate.Value).ToList();
                    }

                }
                catch (Exception)
                {

                    RtlMessageBox.Show("لطفا فرمت تاریخ را صحیح وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                dgReport.Rows.Clear();
                foreach (var Accounting in result)
                {
                    string customer = db.CustomerRepository.GetCustomerNameById(Accounting.CostomerID);
                    dgReport.Rows.Add(Accounting.ID, customer, Accounting.Amount, Accounting.DateTitle.ToShamsi(), Accounting.Description);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int Id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show("آیا از حذف این تراکنش مطمئن هستید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AccountingReposiory.Delete(Id);
                        db.Save();
                        Filter();
                    }

                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int Id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                frmNewAccounting frm = new frmNewAccounting();
                frm.AccountID = Id;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
        }
    }
}
