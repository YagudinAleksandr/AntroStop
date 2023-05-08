using AntroStop.DAL.Context;
using AntroStop.DAL.Repositories;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.Repositories;
using AntroStop.WebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;

namespace AntroStop.WebAPI
{
    public record Startup(IConfiguration configuration)
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination"));
            });
            //Db Connection
            services.AddDbContext<DataDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("Data"), m => m.MigrationsAssembly("AntroStop.DAL.SqlServer")));//Добавление SQL Server

            //Add repositories
            services.AddScoped(typeof(IOrganizationRepository<>), typeof(OrganizationRepository<>));
            services.AddScoped(typeof(IOrganizationUserRepository<>), typeof(OrganizationUserRepository<>));
            services.AddScoped(typeof(IIntRepository<>), typeof(RoleRepository<>));
            services.AddScoped(typeof(IUsersRepository<>), typeof(UserRepository<>));
            services.AddScoped(typeof(IViolationsRepository<>), typeof(ViolationRepository<>));
            services.AddScoped(typeof(IElementRepository<>), typeof(ElementRepository<>));


            //JWT Auth
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                    };
                });
            //End of JWT

            //Add service to initialize DB
            services.AddTransient<DataDBInitializer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AntroStop.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataDBInitializer db)
        {
            //Initialize DB
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Подключение к браузеру для отладки Blazor
                app.UseWebAssemblyDebugging();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AntroStop.WebAPI v1"));
            }

            #region Blazor

            //Подключение Blazor
            app.UseBlazorFrameworkFiles();

            //Подключение статических файлов
            app.UseStaticFiles();

            #endregion


            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            //Подключить в случае обработки https
            //app.UseHttpsRedirection();

            app.UseRouting();

            //Auth system
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //Если не удалось расшифровать контроллер или маршрут, будет перенаправлено на файл index.html (Blazor)
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
