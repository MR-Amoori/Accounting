using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        Accounting_DBEntities db = new Accounting_DBEntities(); //بانک
        
        private CustomerRepository _customerRepository;
        public CustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(db);
                }
                return _customerRepository;
            }
        }

        private GenericRepository<Accounting> _accountingReposiory;
        public GenericRepository<Accounting> AccountingReposiory
        {
            get
            {
                if (_accountingReposiory == null)
                {
                    _accountingReposiory = new GenericRepository<Accounting>(db);
                }
                return _accountingReposiory;
            }
        }

        private GenericRepository<Login> _login;
        public GenericRepository<Login> Login
        {
            get
            {
                if (_login==null)
                {
                    _login = new GenericRepository<Login>(db);
                }
                return _login;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
