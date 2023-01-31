using AntroStop.DAL.Context;
using AntroStop.DAL.Repositories;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.Repositories;
using AntroStop.WebAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AntroStop.WebAPI
{
    public record Startup(IConfiguration configuration)
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Db Connection
            services.AddDbContext<DataDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("Data"), m => m.MigrationsAssembly("AntroStop.DAL.SqlServer")));//Добавление SQL Server

            //Add repositories
            services.AddScoped(typeof(IOrganizationRepository<>), typeof(OrganizationRepository<>));
            services.AddScoped(typeof(IOrganizationUserRepository<>), typeof(OrganizationUserRepository<>));
            services.AddScoped(typeof(IIntRepository<>), typeof(RoleRepository<>));
            services.AddScoped(typeof(IStringRepository<>), typeof(UserRepository<>));
            services.AddScoped(typeof(IViolationRepository<>), typeof(ViolationRepository<>));
            services.AddScoped(typeof(IElementRepository<>), typeof(ElementRepository<>));

            
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

            //Подключить в случае обработки https
            //app.UseHttpsRedirection();

            app.UseRouting();

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
