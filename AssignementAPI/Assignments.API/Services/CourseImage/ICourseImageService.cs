using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Services.CourseImage
{
    public interface ICourseImageService : IBaseService<CourseImageEntity>
    {
        Task UploadFile(int id, IFormFile file, CancellationToken cancellationToken);
        Task<CourseImageEntity> GetPictureById(int id);
        Task<CourseImageEntity> GetPictureOfCourseById(int id);
    }
}
