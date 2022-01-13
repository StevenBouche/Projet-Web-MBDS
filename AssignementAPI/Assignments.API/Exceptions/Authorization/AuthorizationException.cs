namespace Assignments.API.Exceptions.Authorization
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base() { }
        public AuthorizationException(string message) : base(message) { }
        public AuthorizationException(string message, Exception inner) : base(message, inner) { }
    }
}
