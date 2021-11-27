using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer
{
    public interface ICustomerRepository
    {
        List<Customers> GetAllCustomers();
        IEnumerable<Customers> GetCustomerByFilter(string Parameter);
        List<string> GetNamesCustomers(string Filter = "");
        Customers GetCustomerbyId(int customerId);
        bool InsertCustomer(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(int customerId);
        bool DeleteCustomer(Customers customer);
        string GetCustomerNameById(int customerId);
    }
}
