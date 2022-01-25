using Assignments.Business.Extentions;
using System.ComponentModel;

namespace Assignments.Business.Exceptions.Authentification
{
    public enum AuthentificationExceptionTypes
    {
        [Description("Account not found")]
        ACCOUNT_NOT_FOUND,

        [Description("Bad credential")]
        BAD_CREDENTIAL,

        [Description("No refresh token valid")]
        REFRESH_TOKEN_NOT_VALID
    }

    public class AuthentificationException : Exception
    {
        public AuthentificationExceptionTypes Type;

        public AuthentificationException(AuthentificationExceptionTypes type) : base(type.ToDescriptionString())
        {
            Type = type;
        }
    }
}