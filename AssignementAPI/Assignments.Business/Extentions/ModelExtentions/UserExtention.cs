using Assignments.Business.Dto.Users;
using Assignments.DAL.Models;

namespace Assignments.Business.Extentions.ModelExtentions
{
    public static class UserExtention
    {
        public static User ToUser(this UserEntity user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                PictureId = user.Image?.Id ?? null,
                Role = user.Role.ToString()
            };
        }
    }
}