using Assignments.API.Models.Users;
using Assignments.DAL.Models;

namespace Assignments.API.Extentions.ModelExtentions
{
    public static class UserExtention
    {
        public static User ToUser(this UserEntity user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                PictureId = user.ImageId,
                Role = user.Role.ToString()
            };
        }
    }
}
