using Microsoft.EntityFrameworkCore;
using CodeUdd.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaUdd.Business;
using CodeUdd.Data;
using CodeUdd.Data.Handler;
using PruebaUdd.Swagger;
using CodeUdd.Config;
using MediatR;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Versioning;
using CodeUdd.Data.Command;
using CodeUdd.Data.Model;

namespace CodeUdd
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            services.AddDbContext<PersonaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddTransient<IRequestHandler<RegistrarPersonaCommand, int>, RegistrarPersonaCommandHandler>();
            services.AddTransient<IRequestHandler<EliminarPersonaByIdCommand, bool>, EliminarPersonaByIdCommandHandler>();
            services.AddTransient<IRequestHandler<BuscarPersonaByIdCommand, Persona>, BuscarPersonaByIdCommandHandler>();

            services.AddTransient<IFeriadoService, FeriadoService>();
            services.AddTransient<IPersonaService, PersonaService>();
            

            services.AddMvcCore().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddSwaggerExtension(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Mi API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

            });
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                var multiVersionReader = new HeaderApiVersionReader("x-version");
                options.ApiVersionReader = multiVersionReader;
                options.ReportApiVersions = true;
            });

            services.AddMvc().AddNewtonsoftJson();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseSwaggerExtension(Configuration);
            app.UseMiddleware<ApiExceptionHandlerMiddleware>();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("./swagger/v1/swagger.json", "Mi API v1");
                options.RoutePrefix = "docs";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
