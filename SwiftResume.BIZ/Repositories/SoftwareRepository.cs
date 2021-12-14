using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class SoftwareRepository : Repository<Software>, ISoftwareRepository
{
    public SoftwareRepository(SwiftResumeDbContext context) : base(context)
    {
    }
}