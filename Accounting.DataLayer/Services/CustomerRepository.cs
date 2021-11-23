using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Accounting.ViewModels.Customers;

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

        public IEnumerable<Customers> GetCustomerByFilter(string Parameter)
        {
            return db.Customers.Where(p => p.FullName.Contains(Parameter) || p.Mobile.Contains(Parameter) || p.Emaill.Contains(Parameter)).ToList();
        }

        public Customers GetCustomerbyId(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public List<ListCustomerViewModel> GetNamesCustomers(string Filter = "")
        {
            if (Filter == "")
            {
                return db.Customers.Select(p => new ListCustomerViewModel()
                {
                    CustomerID = p.CustomerID,
                    FullName = p.FullName
                }
                ).ToList();
            }
            return db.Customers.Where(p => p.FullName.Contains(Filter)).Select(p => new ListCustomerViewModel()
            {
                CustomerID = p.CustomerID,
                FullName = p.FullName
            }).ToList();
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

        List<string> ICustomerRepository.GetNamesCustomers(string Filter)
        {
            throw new NotImplementedException();
        }
    }
}
