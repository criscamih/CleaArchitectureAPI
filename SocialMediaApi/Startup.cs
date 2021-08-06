using System;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Data;
using SocialMediaInfrastructure.Filters;
using SocialMediaInfrastructure.Repositories;
using SocialMediaCore.Services;
using SocialMediaInfrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using SocialMediaInfrastructure.Services;
using SocialMediaCore.CustomEntities;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace SocialMediaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BlogContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerBlog"));
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(options => 
            {
                options.Filters.Add<GlobalExceptionFilter>(); //Agrega excepciones controladas
            });
            // .ConfigureApiBehaviorOptions(options => 
            // {
            //     options.SuppressModelStateInvalidFilter = true; el modelo se valida manualmente no por el ApiController
            // });

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination")); //acceder al appsettings
            
            // ---------documentación API-------
            services.AddSwaggerGen(doc => 
            {
                doc.SwaggerDoc("v1", new OpenApiInfo{Title = "Social Media API", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
            });
            
            services.AddMvc(
            //     options => 
            // {
            //     options.Filters.Add<ValidationFilter>(); para validar el modelo manualmente
            // }
            ).AddFluentValidation(options => //evita utilizar data annotations en las validaciones
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                options.LocalizationEnabled =false;
            });
            services.AddTransient<IPostService,PostService>();
            // services.AddScoped<IPostRepository,PostRepository>();Fueron reemplazadas por el BaseRepository
            // services.AddScoped<IUserRepository,UserRepository>();
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            services.AddSingleton<IUriService>( provider => 
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var baseUri = string.Concat(request.Scheme,"://",request.Host.ToUriComponent());
                return new UriService(baseUri);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger();
            
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media API");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
