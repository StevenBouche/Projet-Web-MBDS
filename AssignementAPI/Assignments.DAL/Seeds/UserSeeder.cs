using Assignments.DAL.Context;
using Assignments.DAL.Enumerations;
using Assignments.DAL.Models;

namespace Assignments.DAL.Seeds
{
    public class UserSeeder
    {
        private readonly AssignmentContext Context;

        public UserSeeder(AssignmentContext context)
        {
            Context = context;
        }

        public void SeedData()
        {
            AddUniqueUser(new UserEntity()
            {
                Name = "Admin",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin"),
                Role = UserRoles.ADMIN,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            AddUniqueUser(new UserEntity()
            {
                Name = "Professor",
                Password = BCrypt.Net.BCrypt.HashPassword("Professor"),
                Role = UserRoles.PROFESSOR,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            AddUniqueUser(new UserEntity()
            {
                Name = "Student",
                Password = BCrypt.Net.BCrypt.HashPassword("Student"),
                Role = UserRoles.STUDENT,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            Context.SaveChanges();
        }

        private void AddUniqueUser(UserEntity user)
        {
            var currentUser = Context.Users.FirstOrDefault(u => u.Name == user.Name);
            if (currentUser == null)
            {
                Context.Users.Add(user);
            }
        }
    }
}