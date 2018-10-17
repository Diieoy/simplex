using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface ILayoutRepository : IRepository<Layout>
    {
        Layout GetByName(string name);
        void Create(IEnumerable<Layout> layouts);
    }
}
