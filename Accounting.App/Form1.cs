using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.Businesss;
using Accounting.ViewModels.Accounting;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmCustomers frm = new FrmCustomers();
            frm.ShowDialog();
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            frmNewAccounting frmNew = new frmNewAccounting();
            if (frmNew.ShowDialog()==DialogResult.OK)
            {
                Report();
            } 
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 2;
            frmReport.ShowDialog();
        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 1;
            frmReport.ShowDialog();
        }

        void Report()
        {
            ReportViewModel report = Account.Report();
            lblRecivie.Text = report.Recivie.ToString("#,0");
            lblPay.Text = report.Pay.ToString("#,0");
            lblAccountingBalance.Text = report.AccountBalance.ToString("#,0");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frmLogin frmLog = new frmLogin();
            if (frmLog.ShowDialog()==DialogResult.OK)
            {
                Report();
                lblDate.ForeColor = Color.DarkGreen;
                lblTime.ForeColor = Color.DarkGreen;
                lblDate.Text = DateTime.Now.ToString("ddd  yyyy/MM/dd");
                // lblTime.Text = DateTime.Now.ToShortTimeString();
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            else
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void تنظیماتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.IsEdit = true;
            frm.ShowDialog();            
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            this.Hide();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
        }
    }
}
