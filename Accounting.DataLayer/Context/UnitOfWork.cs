﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.UnitOfWork
{
    public class UnitOfWork:IDisposable
    {
        Accounting_DBEntities db = new Accounting_DBEntities();
        private CustomerRepository _customerRepository;

         public CustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository==null)
                {
                    _customerRepository = new CustomerRepository(db);
                }
                return _customerRepository;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}