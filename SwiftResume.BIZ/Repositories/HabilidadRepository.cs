using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class HabilidadRepository : Repository<Habilidad>, IHabilidadRepository
{
    public HabilidadRepository(SwiftResumeDbContext context) : base(context)
    {
    }
}