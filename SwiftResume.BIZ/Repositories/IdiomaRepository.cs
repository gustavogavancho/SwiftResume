using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class IdiomaRepository : Repository<Idioma>, IIdiomaRepository
{
    public IdiomaRepository(SwiftResumeDbContext context) : base(context)
    {
    }
}