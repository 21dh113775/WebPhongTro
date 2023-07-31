using WebPhongTro.Models.DTO;

namespace WebPhongTro.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {

        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegisterModel model);
        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
