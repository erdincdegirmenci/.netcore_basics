using Application.Business.Model.Request.User;
using Application.Business.Model.Response.User;
using DefineXwork.Library.Business;

namespace Application.Business.Service.Interface
{
    public interface IUserService: IService
    {
        public int CreateUser(CreateUserBusinessRequestModel createUserBusinessServiceRequestModel);
        public GetActiveUserInfoBusinessResponseModel GetActiveUser(string username);
        public GetActiveUserInfoBusinessResponseModel GetUser(int Id);
        void UpdateUserPassword(int userId, string password);
        void SetUserStatus(string userName, bool IsActive);
        int CreateUserVerification(CreateUserVerificationBusinessRequestModel request);
        int ForgotPasswordVerification(ForgotPasswordVerificationBusinessRequestModel request);
        void UpdateUserVerification(UpdateUserVerificationBusinessRequestModel request);
        GetUserVerificationBusinessResponseModel GetUserVerificationByUserId(GetUserVerificationBusinessRequestModel request);
        GetUserVerificationBusinessResponseModel GetUserVerificationByVerificationCode(string verificationCode);
        int RegisterUser(RegisterUserBusinessRequestModel request);
        void ActivateUserByMailVerifcation(int verificatonId, int userId, string username);
        void ChangeUserPasswordByMailVerification(int verificatonId, int userId, string password);

    }
}
