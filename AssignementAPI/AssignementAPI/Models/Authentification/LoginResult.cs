using AssignmentAPI.Extentions;
using AssignmentAPI.Models.Authentification.Security;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.Authentification
{
    public enum LoginResultCode
    {
        [Description("Account not found")]
        ACCOUNT_NOT_FOUND,
        [Description("Bad credential")]
        BAD_CREDENTIAL,
        [Description("Success")]
        SUCCESS,
        [Description("No refresh token valid")]
        REFRESH_TOKEN_NOT_VALID
    }

    public class LoginResult
    {
        [JsonPropertyName("status")]
        public LoginResultCode Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get => Status.ToDescriptionString(); }
        [JsonPropertyName("jwtToken")]
        public JwtToken? JwtToken { get; set; } = null;
        [JsonPropertyName("refreshToken")]
        public RefreshToken? RefreshToken { get; set; } = null;
    }
}
