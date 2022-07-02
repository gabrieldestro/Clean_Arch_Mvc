using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Account
{
    public interface ISeedUserRoleInitial
    {
        void SeedUsers();
        void SeedRoles();
    }
}