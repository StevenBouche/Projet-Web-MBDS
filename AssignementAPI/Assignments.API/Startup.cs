using Microsoft.OpenApi.Models;
using Assignments.DAL.Repositories.UserProfilImage;
using Assignments.DAL.Repositories.Assignments;
using Assignments.DAL.Repositories.Authentification;
using Assignments.DAL.Repositories.CourseImage;
using Assignments.API.Configurations.Policies;
using Assignments.DAL.Repositories.Users;
using Assignments.API.Extentions;
using Assignments.DAL.Repositories.Courses;
using Assignments.DAL.Repositories.WorkSubmits;
using Assignments.API.Handlers.Authentification;
using Assignments.Business.Services.Users;
using Assignments.Business.Services.Assignments;
using Assignments.Business.Services.Courses;
using Assignments.Business.Services.CourseImage;
using Assignments.Business.Services.UserProfilImage;
using Assignments.Business.Services.WorkSubmits;
using Assignments.Business.Services.Authentification;

namespace Assignments.API
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
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // Add services to the container.
            services.AddControllers(option =>
            {
                option.Filters.Add(new ActiveUserFilter());
            });

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            // Policies CORS
            services.ConfigurePolicies(policiesConfig);

            // Configure db
            services.ConfigureDb(Configuration);

            //Security
            services.ConfigureAuthentification(Configuration);

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
            services.AddTransient<IAuthentificationService, AuthentificationService>();
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