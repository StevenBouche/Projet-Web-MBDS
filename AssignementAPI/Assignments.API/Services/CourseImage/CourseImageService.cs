﻿using Assignments.API.Exceptions.Business;
using Assignments.API.Models.Authentification;
using Assignments.API.Models.Image;
using Assignments.API.Services.Base;
using Assignments.API.Services.Courses;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.CourseImage;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Services.CourseImage
{
    public class CourseImageService : BaseService<CourseImageEntity, ICourseImageRepository>, ICourseImageService
    {

        private readonly UserIdentity Identity;
        private readonly ICourseService CourseService;

        public CourseImageService(ICourseImageRepository repository, ICourseService courseService, UserIdentity identity, ILogger<CourseImageService> logger) : base(repository, logger)
        {
            Identity = identity;
            CourseService = courseService;
        }

        public async Task<CourseImageEntity> GetPictureById(int id)
        {
            return await VerifyAndGetEntity(id);
        }

        public async Task<CourseImageEntity> GetPictureOfCourseById(int id)
        {
            var course = await CourseService.GetCourseByIdAsync(id);
            return await VerifyAndGetEntity(course.PictureId);
        }

        public async Task UploadFile(int id, IFormFile file, CancellationToken cancellationToken)
        {

            if (!ImageConstants.ContentTypes.Contains(file.ContentType))
                throw new ArgumentException("Content type is not accepted");

            var course = await CourseService.GetCourseByIdAsync(id);

            if (course.UserId != Identity.Id)
                throw new CourseImageBusinessException(CourseImageBusinessExceptionTypes.COURSE_UNAUTHORIZE, "Not authorize to upload image for this course");

            var image = await Repository.GetFirstByCriteria(entity => entity.CourseId == id);

            if (image == null)
            {
                image = new CourseImageEntity()
                {
                    CourseId = id
                };
            }

            image.Extention = file.ContentType;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                image.Data = ms.ToArray();
            }

            await Repository.UpsertAsync(image);

            await CourseService.AddPictureId(image.CourseId, image.Id);
        }
    }
}
