using Worker_SAP.Model;

namespace Worker_SAP.Service.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync();
    }
}
