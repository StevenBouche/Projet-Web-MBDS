using Assignments.Business.Services.Base;
using Assignments.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.Business.Services.CourseImage
{
    public interface ICourseImageService : IBaseService<CourseImageEntity>
    {
        Task UploadFile(int id, IFormFile file, CancellationToken cancellationToken);

        Task<CourseImageEntity> GetPictureById(int id);

        Task<CourseImageEntity> GetPictureOfCourseById(int id);
    }
}