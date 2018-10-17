using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface ISeatRepository : IRepository<Seat>
    {
        void Create(IEnumerable<Seat> list);
    }
}
