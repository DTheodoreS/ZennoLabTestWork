namespace backend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using backend.DatasetValidation;
    using backend.Repository;
    using backend.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IDatasetRepository, DatasetRepository>();
            var archiveRepositoryRootPath = Configuration.GetValue<string>("ArchiveRepositoryRootPath");
            services.AddScoped<IArchiveRepository>((container) => {
                var logger = container.GetRequiredService<ILogger<ArchiveRepository>>();
                return new ArchiveRepository(archiveRepositoryRootPath, logger);
            });
            var datasetValidators = GetAllImplementedInterfaces<IDatasetValidator>();
            datasetValidators.ForEach((t) => services.AddScoped(typeof(IDatasetValidator), t));
            services.AddTransient<IDatasetValidateProcessorFactory, DatasetValidateProcessorFactory>();
            services.AddTransient<IDatasetService, DatasetService>();
            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:5001");
                });
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("VueCorsPolicy");

            dbContext.Database.EnsureCreated();
            app.UseAuthentication();
            app.UseMvc();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSpaStaticFiles();
            app.UseSpa(configuration: builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });

            loggerFactory.AddFile("Logs/myapp-{Date}.txt");
        }

        private List<Type> GetAllImplementedInterfaces<T>()
        {
            var assembly = typeof(IDatasetValidator).Assembly;
            return assembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(T)))
                .Select(t => t.AsType())
                .ToList();
        }
    }
}
