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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
