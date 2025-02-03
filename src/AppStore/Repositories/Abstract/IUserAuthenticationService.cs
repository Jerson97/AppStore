using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract;

public interface IUserAuthenticationService
{
    Task<Status> Loginasync(LoginModel login);
    Task LogoutAsync();
}