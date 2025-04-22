namespace Application.Domain.DataAccess.Model.User
{
    public class CreateUserDAOModel: BaseDAOModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
