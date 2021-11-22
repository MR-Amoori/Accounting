using Accounting.DataLayer;
using Accounting.DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using System.IO;

namespace Accounting.App
{
    public partial class frmAddOrEditCustomer : Form
    {
        public string LocationPhoto;
        public frmAddOrEditCustomer()
        {
            InitializeComponent();
        }

        public int CustomerID = 0;
        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {

                pcCustomer.ImageLocation = openFile.FileName;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    string ImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(pcCustomer.ImageLocation);
                    string Location = Application.StartupPath + "/Images/";
                    if (!Directory.Exists(Location))
                    {
                        Directory.CreateDirectory(Location);
                    }
                    pcCustomer.Image.Save(Location + ImageName);

                    Customers customer = new Customers()
                    {
                        FullName = txtName.Text,
                        Mobile = txtMobile.Text,
                        Emaill = txtEmail.Text,
                        Address = txtAddress.Text,
                        CustomerImage = ImageName
                    };
                    if (CustomerID == 0)
                    {
                        db.CustomerRepository.InsertCustomer(customer);
                        db.Save();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        File.Delete(LocationPhoto);
                        customer.CustomerID = CustomerID;
                        db.CustomerRepository.UpdateCustomer(customer);
                        db.Save();
                        DialogResult = DialogResult.OK;
                    }

                }
            }


        }

        private void frmAddOrEditCustomer_Load(object sender, EventArgs e)
        {
            if (CustomerID != 0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var Customer = db.CustomerRepository.GetCustomerbyId(CustomerID);
                    txtName.Text = Customer.FullName;
                    txtMobile.Text = Customer.Mobile;
                    txtEmail.Text = Customer.Emaill;
                    txtAddress.Text = Customer.Address;
                    pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + Customer.CustomerImage;
                }
            }
        }

    }
}
