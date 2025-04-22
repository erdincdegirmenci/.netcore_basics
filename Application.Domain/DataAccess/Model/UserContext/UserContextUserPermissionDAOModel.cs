
namespace Application.Domain.DataAccess.Model.UserContext
{
    public class UserContextUserPermissionDAOModel
    {
        public int PermissionId { get; set; }
        public string Permission { get; set; }
        public bool IsDeny { get; set; }
    }
}
