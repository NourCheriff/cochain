using CochainAPI.Model.Authentication;
using CochainAPI.Model.DTOs;
using CochainAPI.Model.Utils;

namespace CochainAPI.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<bool> GenerateTemporaryCredentials(AuthenticateRequest request);
        Task<BaseResponse<AuthenticateResponse>> SignInWithCredentials(AuthenticateRequest request);
        Task<BaseResponse<JwtResponseVM>> RefreshToken(string refreshToken);
    }
}
