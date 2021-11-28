using Accounting.ViewModels.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.UnitOfWork;
namespace Accounting.Businesss
{
    public class Account
    {
        public static ReportViewModel Report()
        {
            ReportViewModel report = new ReportViewModel();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endtDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);

                var recive = db.AccountingReposiory.Get(p => p.TypeID == 1 && p.DateTitle >= startDate && p.DateTitle <= endtDate).Select(p => p.Amount).ToList();
                var pay = db.AccountingReposiory.Get(p => p.TypeID == 2 && p.DateTitle >= startDate && p.DateTitle <= endtDate).Select(p => p.Amount).ToList();

                report.Recivie = recive.Sum();
                report.Pay = pay.Sum();
                report.AccountBalance = (recive.Sum() - pay.Sum());
            }
            return report;
        }
    }
}
