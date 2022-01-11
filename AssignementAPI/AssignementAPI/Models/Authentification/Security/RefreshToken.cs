using Assignment.DAL.Models;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.Authentification.Security
{
    public class RefreshToken
    {
        [JsonPropertyName("refreshToken")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("expireAt")]
        public long ExpireAt { get; set; }

        public RefreshToken()
        {
        }

        public RefreshToken(RefreshTokenEntity refreshToken)
        {
            Token = refreshToken.Token;
            ExpireAt = refreshToken.ExpireAt;
        }

        public override bool Equals(object? obj)
        {
            RefreshToken? element = obj as RefreshToken;
            if (element != null)
            {
                return (Token, ExpireAt).Equals((element.Token, element.ExpireAt));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, ExpireAt);
        }
    }
}
