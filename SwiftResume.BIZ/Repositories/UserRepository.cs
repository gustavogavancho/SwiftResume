using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;
using System.Threading.Tasks;

namespace SwiftResume.BIZ.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SwiftResumeDbContextFactory context) : base(context)
        {

        }
        public async Task<User> GetByUserName(string username)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            using (SwiftResumeDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            }
        }
    }
}
