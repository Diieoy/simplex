using Microsoft.AspNet.Identity;

namespace BLLStandard.DTO
{
    public class RoleDTO : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
