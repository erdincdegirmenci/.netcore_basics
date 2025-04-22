
namespace Application.Domain.DataAccess.Model.Authentication
{
    public class LoginRolePermissionDAOModel
    {
        public int PermissionId { get; set; }
        public string Permission { get; set; }
        public bool IsDeny { get; set; }
    }
}
