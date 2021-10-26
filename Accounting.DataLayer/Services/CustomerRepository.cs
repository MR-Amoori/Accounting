using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Accounting.DataLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        Accounting_DBEntities db;

        public CustomerRepository(Accounting_DBEntities Context)
        {
            db = Context;
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var Customer = GetCustomerbyId(customerId);
                DeleteCustomer(Customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customers GetCustomerbyId(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public bool InsertCustomer(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool UpdateCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
