using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.AuthServices;

public interface IAuthService
{
    Task<AccessToken> CreateAccessTokenAsync(User user);
    Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
}