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
    }
}
