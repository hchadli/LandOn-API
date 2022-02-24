using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using LandonApi.Filters;
using LandonAPI.Filters;
using LandonAPI.Infrastructure;
using LandonAPI.Models;
using LandonAPI.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LandonAPI
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

            services.Configure<HotelInfo>(Configuration.GetSection("Info"));

            services.AddScoped<IRoomService, DefaultRoomService>();

            // Use in-memory database for quick dev and testing
            // TODO : SWAP OUT FOR A REAL DATABASE IN PRODUCTION

            services.AddDbContext<HotelApiDbContext>(options => options.UseInMemoryDatabase("landondb"));

            services
                .AddControllers();

            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsOrCloseAttribute>();
                options.Filters.Add<LinkRewritingFilter>();
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.1", new OpenApiInfo
                {
                    Version = "v1.1",
                    Title = "LandonAPI",
                    Description = "Landon Hotel  .NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),

                    Contact = new OpenApiContact
                    {
                        Name = "Chadli H.",
                        Email = String.Empty,
                        Url = new Uri("https://example.com/terms"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/terms"),
                    }
                });
            });


            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 1);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });


            services.AddCors(options =>
                options.AddPolicy("AllowMyApp", 
                    builder => builder.AllowAnyOrigin())
                );

            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "LandonAPI v1.1");
                });

            }

            app.UseCors("AllowMyApp");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
