using Cibertec.Models;

namespace Cibertec.Repository.Northwind
{
    public interface IUserRepository: IRepository<User>
    {
        User ValidateUser(string email, string password);
    }
}
