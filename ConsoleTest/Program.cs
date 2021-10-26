using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer;
using Accounting.DataLayer.UnitOfWork;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork db = new UnitOfWork();
            var List=db.CustomerRepository.GetAllCustomers();
            foreach (var p in List)
            {
                Console.WriteLine($"ID : {p.CustomerID}  Name : {p.FullName} Mobile : {p.Mobile} Email : {p.Emaill} Image : {p.CustomerImage}");
            }
            Console.ReadKey();
        }
    }
}
