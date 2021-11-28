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
using Accounting.DataLayer;
using Accounting.DataLayer.UnitOfWork;

namespace Accounting.App
{
    public partial class frmLogin : Form
    {
        public bool IsEdit = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    //if (db.Login.Get(p=>p.UserName==txtName.Text.ToLower()).Any()==true)
                    //{
                    //    if (db.Login.Get(p=>p.Password==txtPassword.Text.ToLower()).Any()==true)
                    //    {
                    //        DialogResult = DialogResult.OK;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("کلمه عبور اشتباه است");
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("نام کاربری اشتاه است");
                    //}

                    // Or

                    if (IsEdit)
                    {
                        var Edit = db.Login.Get().First();
                        Edit.UserName = txtName.Text;
                        Edit.Password = txtPassword.Text;
                        db.Login.Update(Edit);
                        db.Save();
                        Application.Restart();
                    }

                    else
                    {

                        if (db.Login.Get(p => p.UserName == txtName.Text.ToLower() && p.Password == txtPassword.Text.ToLower()).Any() == true)
                        {
                            DialogResult = DialogResult.OK;
                        }

                        else
                        {
                            MessageBox.Show("اطلاعات وارد شده صحیح نمی باشد");
                        }
                    }
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                this.Text = "تغییر اطلاعات ورود";
                btnSave.Text = "ذخیره";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var Res = db.Login.Get().First();
                    txtName.Text = Res.UserName;
                    txtPassword.Text = Res.Password;
                }
            }
        }
    }
}
