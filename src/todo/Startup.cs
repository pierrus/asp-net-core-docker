using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Todo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            

            // REPOSITORY POSTGRESQL
            services.AddSingleton<Data.ITodoRepository, Data.PostGresTodoRepository>();
            services.AddScoped<Data.PostGresTodoContext, Data.PostGresTodoContext>();

            // REPOSITORY SQL LITE
            //services.AddSingleton<Data.ITodoRepository, Data.TodoRepository>();
            //services.AddScoped<Data.TodoContext, Data.TodoContext>();

            services.AddTransient<IConfigurationRoot>(s => { return Configuration; });

            services.AddHealthChecks()                
                .AddCheck<Todo.Health.RepositoryHealthCheck>("repository");

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());        

            services.AddLogging(loggingBuilder => 
                {
                    loggingBuilder.AddConsole();
                });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();

            //Middleware static files pour le rendu de fichiers statiques: CSS, JS, HTML ....
            app.UseStaticFiles();

            //Expose health checks endpoints
            app.UseHealthChecks("/health", new HealthCheckOptions()
                {
                //that's to the method you created 
                ResponseWriter = Health.RepositoryHealthCheck.WriteHealthCheckResponse 
                });

            //Middleware MVC pour l'exécution de contrôleurs API/MVC
            app.UseMvc();
        }
    }
}
