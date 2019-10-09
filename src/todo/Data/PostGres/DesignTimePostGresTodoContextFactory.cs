
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Todo.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostGresTodoContext>
    {
        public PostGresTodoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostGresTodoContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new PostGresTodoContext(configuration);
        
        }
    }
}