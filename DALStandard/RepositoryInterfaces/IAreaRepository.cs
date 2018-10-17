using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        void Create(IEnumerable<Area> areas);
    }
}
