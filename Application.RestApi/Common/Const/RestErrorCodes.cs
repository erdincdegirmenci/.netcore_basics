namespace Application.RestApi.Common.Const
{
    public class RestErrorCodes
    {
        public const string LoginFailedError = "10001";
        public const string RegistrationFailedError = "10002";

        //User
        public const string UserCreationFailedError = "11001";
        public const string UserDetailFailedError = "11002";
        public const string UserUpdateFailedError = "11003";
        public const string UserDeletionFailedError = "11004";
        public const string UserListFailedError = "11005";

        //Company
        public const string CompanyCreationFailedError = "11101";
        public const string CompanyDetailFailedError = "11102";
        public const string CompanyUpdateFailedError = "11103";
        public const string CompanyDeletionFailedError = "11104";

        //Role
        public const string RoleCreationFailedError = "11201";
        public const string RoleDetailFailedError = "11202";
        public const string RoleUpdateFailedError = "11203";
        public const string RoleDeletionFailedError = "11204";

    }
}
