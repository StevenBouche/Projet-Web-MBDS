using Assignments.API.Configurations.Authentification;
using Assignments.API.Exceptions.Authentification;
using Assignments.API.Models.Authentification;
using Assignments.API.Models.Authentification.Tokens;
using Assignments.API.Services.Base;
using Assignments.API.Services.Users;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Authentification;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Assignments.API.Services.Authentification
{
    public class SecurityService : BaseService<RefreshTokenEntity, IRefreshTokenRepository>, ISecurityService
    {
        private readonly JwtTokenConfig Config;
        private readonly IUserService UserService;
        private readonly RandomNumberGenerator RandomGenerator = RandomNumberGenerator.Create();

        public SecurityService(
            IOptions<JwtTokenConfig> config,
            IRefreshTokenRepository repository,
            IUserService userService,
            ILogger<SecurityService> logger
        ) : base(repository, logger)
        {
            Config = config.Value;
            UserService = userService;
        }

        public async Task<LoginResult> LoginAsync(LoginForm login)
        {
            var result = new LoginResult();
            UserEntity? userAccount = UserService.GetUserWithUserName(login.Name);

            if (userAccount == null)
            {
                throw new AuthentificationException(AuthentificationExceptionTypes.ACCOUNT_NOT_FOUND);
            }
            else if (!userAccount.Password.Equals(login.Password))
            {
                throw new AuthentificationException(AuthentificationExceptionTypes.BAD_CREDENTIAL);
            }
            else
            {
                result.JwtToken = GetJwtToken(userAccount);
                result.RefreshToken = await GetRefreshToken(userAccount);
            }

            return result;
        }

        public async Task<LoginResult> RefreshLoginAsync(RefreshToken token)
        {
            var result = new LoginResult();
            RefreshTokenEntity? tokenEntity = await Repository.GetByTokenNameAsync(token.Token);

            if (tokenEntity != null && tokenEntity.ExpireAt > UnixTimeNow())
            {
                result.JwtToken = GetJwtToken(tokenEntity.User);
                result.RefreshToken = token;
            }
            else
            {
                throw new AuthentificationException(AuthentificationExceptionTypes.REFRESH_TOKEN_NOT_VALID);
            }

            return result;
        }

        public async Task LogoutAsync(UserIdentity? userAccount)
        {
            if (userAccount != null)
            {
                await Repository.DeleteByUserIdAsync(userAccount.Id);
            }
        }

        private JwtToken GetJwtToken(UserEntity user)
        {
            return new JwtToken
            {
                AccessToken = GenerateToken(user),
                ExpireAt = UnixTimeNow() + Config.AccessTokenExpiration * 60 * 1000
            };
        }

        private async Task<RefreshToken> GetRefreshToken(UserEntity account)
        {
            //if dont have valid token, generate new refresh token
            var unixTimestamp = UnixTimeNow();

            if (account.RefreshToken is null)
            {
                account.RefreshToken = new RefreshTokenEntity
                {
                    UserId = account.Id,
                    Token = GenerateRefreshToken(),
                    ExpireAt = unixTimestamp + Config.RefreshTokenExpiration * 60 * 1000
                };

                await Repository.AddAsync(account.RefreshToken);
            }
            else if (account.RefreshToken.ExpireAt < unixTimestamp)
            {
                account.RefreshToken.ExpireAt = unixTimestamp + Config.RefreshTokenExpiration * 60 * 1000;
                account.RefreshToken.Token = GenerateRefreshToken();
                await Repository.UpdateAsync(account.RefreshToken);
            }
            return new RefreshToken(account.RefreshToken);
        }

        private string GenerateToken(UserEntity account)
        {
            var identity = new UserIdentity(account);

            var claims = identity.GetClaims();

            byte[] key = Convert.FromBase64String(Config.Secret);
            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(Config.RefreshTokenExpiration * 60 * 1000),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public long UnixTimeNow()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}
