using Assignments.Business.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.Business.Exceptions.Entities
{
    public enum EntityExceptionTypes
    {
        [Description("Entity not found")]
        NOT_FOUND
    }

    public class EntityException : Exception
    {
        public HttpStatusCode HttpStatusCode;
        public EntityExceptionTypes Type;

        public EntityException(EntityExceptionTypes type) : base(type.ToDescriptionString())
        {
            Type = type;
            HttpStatusCode = SelectHttpCode(Type);
        }

        private HttpStatusCode SelectHttpCode(EntityExceptionTypes type)
        {
            return type switch
            {
                EntityExceptionTypes.NOT_FOUND => HttpStatusCode.NotFound,
                _ => HttpStatusCode.BadRequest,
            };
        }
    }
}