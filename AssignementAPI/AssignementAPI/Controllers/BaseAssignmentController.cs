using AssignmentAPI.Models.Authentification;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
{
    public class BaseAssignmentController : ControllerBase
    {
        protected readonly ILogger Logger;

        protected UserIdentity? Identity
        {
            get
            {
                return User != null ? new UserIdentity(this.User) : null;
            }
        }

        public BaseAssignmentController(ILogger logger)
        {
            Logger = logger;
        }
    }
}
