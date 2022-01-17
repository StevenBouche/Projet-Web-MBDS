﻿using Assignments.API.Models.Courses;
using Assignments.DAL.Models;

namespace Assignments.API.Extentions.ModelExtentions
{
    public static class CourseExtention
    {
        public static Course ToCourse(this CourseEntity entity)
        {
            return new Course()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                PictureId = entity.ImageId,
                UserId = entity.UserId
            };
        }
    }
}