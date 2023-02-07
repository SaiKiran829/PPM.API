
using PPM.Model;

namespace IEntityOperation
{
    public interface IRole
    {
        void RoleAdd(Role role);
        void ViewRoles();
        void SearchById(int roleId);
        void DeleteById(int roleId);
    }
}
