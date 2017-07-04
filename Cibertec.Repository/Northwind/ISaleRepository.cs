using Cibertec.Models;

namespace Cibertec.Repository.Northwind
{
    public interface ISaleRepository
    {
        bool ProcessSale(Sale sale);
    }
}
