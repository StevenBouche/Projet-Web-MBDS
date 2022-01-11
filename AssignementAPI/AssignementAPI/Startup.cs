using Assignment.DAL.Repositories.Assignment;
using Assignment.DAL.Repositories.Course;
using Assignment.DAL.Repositories.CourseImage;
using Assignment.DAL.Repositories.User;
using Assignment.DAL.Repositories.UserProfilImage;
using AssignmentAPI.Configurations.Database;
using AssignmentAPI.Configurations.Policies;
using AssignmentAPI.Services.Assignment;
using AssignmentAPI.Services.Course;
using AssignmentAPI.Services.CourseImage;
using AssignmentAPI.Services.User;
using AssignmentAPI.Services.UserProfilImage;
using AssignmentAPI.Configurations.Authentification;
using AssignmentAPI.Configurations.Authorization;
using AssignmentAPI.Services.Authentification;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Assignment.DAL.Repositories.Authentification;
using Assignment.DAL.Repositories.WorkSubmit;
using AssignmentAPI.Services.WorkSubmit;

namespace AssignmentAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly PoliciesConfig policiesConfig = new();

        public Startup(IWebHostEnvironment env)
        {
            var basePath = $"{env.ContentRootPath}/AppSettings";

            Configuration = BuildConfig(basePath, env.EnvironmentName, ""); 
            Configuration.GetSection(nameof(PoliciesConfig)).Bind(policiesConfig);
        }

        private static IConfiguration BuildConfig(string basePath, string environmentName, string name)
        {

            var path = string.IsNullOrEmpty(name) ? basePath : $"{basePath}/{name}";
            var file = string.IsNullOrEmpty(name) ? $"appsettings.{environmentName}.json" : $"appsettings.{name}.{environmentName}.json";

            return new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(file, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            // Policies CORS
            services.ConfigurePolicies(policiesConfig);

            // Configure db
            services.ConfigureDb(Configuration);

            //Security
            services.ConfigureAuthentification(Configuration);
            services.ConfigureAuthorization();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseImageRepository, CourseImageRepository>();
            services.AddTransient<IUserProfilImageRepository, UserProfilImageRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IWorkSubmitRepository, WorkSubmitRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAssignmentService, AssignmentService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ICourseImageService, CourseImageService>();
            services.AddTransient<IUserProfilImageService, UserProfilImageService>();
            services.AddTransient<IWorkSubmitService, WorkSubmitService>();

            //Security
            services.AddTransient<ISecurityService, SecurityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssignmentAPI v1"));
            }

            app.UsePolicies(policiesConfig);
            //app.UseHttpsRedirection();

            app.UseRouting();

            // Security
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
