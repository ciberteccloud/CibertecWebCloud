using Dapper;
using System.Data.SqlClient;
using Cibertec.Models;

namespace Cibertec.Repository.Northwind
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User ValidateUser(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@password", password);

                return connection
                    .QueryFirstOrDefault<User>("dbo.ValidateUser",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
