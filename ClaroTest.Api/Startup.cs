using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClaroTest.Domain.Models;
using ClaroTest.Domain.Contracts;
using ClaroTest.Infrastructure.Implementations;
using ClaroTest.Infrastructure.Services;
using ClaroTest.Infrastructure.Services.Contracts;

namespace ClaroTest.Api
{
    public class Startup
    {
        private readonly string _myCors = "ClaroTestCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClaroTest.Api", Version = "v1" });
            });

            services.AddCors(builder =>
            {
                builder.AddPolicy(_myCors, policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddDbContext<ClaroTestDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ClaroTestConnection")));

            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDayOfWeekRepository, DayOfWeekRepository>();
            services.AddTransient<IClassRoomRepository, ClassRoomRepository>();
            services.AddTransient<IClassRoomAssignmentRepository, ClassRoomAssignmentRepository>();
            
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IClassRoomService, ClassRoomService>();
            services.AddTransient<IClassRoomAssignmentService, ClassRoomAssignmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClaroTest.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_myCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
