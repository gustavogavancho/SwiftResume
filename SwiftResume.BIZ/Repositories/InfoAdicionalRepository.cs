using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class InfoAdicionalRepository : Repository<InfoAdicional>, IInfoAdicionalRepository
{
    public InfoAdicionalRepository(SwiftResumeDbContext context) : base(context) {}
}
