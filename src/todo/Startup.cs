using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Todo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton<Data.ITodoRepository, Data.PostGresTodoRepository>();

            services.AddScoped<Data.PostGresTodoContext, Data.PostGresTodoContext>();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();

            //Middleware static files pour le rendu de fichiers statiques: CSS, JS, HTML ....
            app.UseStaticFiles();

            //Middleware MVC pour l'exécution de contrôleurs API/MVC
            app.UseMvc();
        }
    }
}
