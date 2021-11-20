using Microsoft.EntityFrameworkCore;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

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

    public async Task<RegistrationResult> ValidateUserRegistration(User user)
    {
        RegistrationResult result = RegistrationResult.Success;

        User emailUser = await GetByEmail(user.Email);

        if (emailUser != null)
        {
            result = RegistrationResult.EmailAlreadyExists;
        }

        User userName = await GetByUserName(user.Username);

        if (userName != null)
        {
            result = RegistrationResult.UsernameAlreadyExists;
        }

        return result;
    }

}
