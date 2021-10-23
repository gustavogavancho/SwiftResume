using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE.Services;
using System.Collections.Generic;

namespace SwiftResume.BIZ.Repositories
{
    public interface IResumeRepository : IRepository<Resume>
    {
        IEnumerable<Resume> GetTopResumes(int count);
        IEnumerable<Resume> GetResumes(int pageIndex, int pageSize);
    }
}
