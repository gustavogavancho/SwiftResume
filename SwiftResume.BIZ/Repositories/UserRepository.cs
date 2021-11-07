using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;
using System.Threading.Tasks;

namespace SwiftResume.BIZ.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SwiftResumeDbContext _context;

        public UserRepository(SwiftResumeDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> GetByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
