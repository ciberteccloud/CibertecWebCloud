using Cibertec.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.PartnerSalesProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ProcessSale(new CibertecUnitOfWork());
            service.ReadExcel();
        }
    }
}
