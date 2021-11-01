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

namespace Accounting.App
{
    public partial class FrmCustomers : Form
    {
        public FrmCustomers()
        {
            InitializeComponent();
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        void BindGrid()
        {
            using (UnitOfWork db=new UnitOfWork())
            {
                dgvCustomres.AutoGenerateColumns = false;
                dgvCustomres.DataSource = db.CustomerRepository.GetAllCustomers();
            }
        
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtFilter.Text = null;
            BindGrid();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db=new UnitOfWork())
            {
                dgvCustomres.DataSource = db.CustomerRepository.GetCustomerByFilter(txtFilter.Text);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomres.CurrentRow!=null)
            {
                if (RtlMessageBox.Show($"از حذف {dgvCustomres.CurrentRow.Cells[1].Value.ToString()} مطمئن هستید؟ (این عمل قابل بازگشت نیست)","اخطار",MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==DialogResult.Yes)
                { 
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        int CustomerID = int.Parse(dgvCustomres.CurrentRow.Cells[0].Value.ToString());
                        var valu = db.CustomerRepository.GetCustomerbyId(CustomerID);
                        db.CustomerRepository.DeleteCustomer(valu);
                        db.Save();
                        BindGrid();
                    }
                } 
            }
            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید!");
            }
        }
    }
}
