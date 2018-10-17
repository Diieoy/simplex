using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        Venue GetByName(string name);
        void Create(IEnumerable<Venue> venues);
    }
}
