using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE.Services;
using System.Threading.Tasks;

namespace SwiftResume.BIZ.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserName(string username);
        Task<User> GetByEmail(string email);
    }
}
