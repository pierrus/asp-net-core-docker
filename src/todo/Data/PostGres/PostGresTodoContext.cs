using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Todo.Data
{
    public class PostGresTodoContext : DbContext
    {
        IConfigurationRoot _configuration;

        ILogger _logger;

        public DbSet<Models.TodoItem> TodoItems { get; set; }

        public PostGresTodoContext(IConfigurationRoot configuration, ILogger<PostGresTodoContext> logger)
        { 
            _configuration = configuration;
            _logger = logger;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             _logger.Log(LogLevel.Information,
                        string.Format("PostGresTodoContext initializing connection {0}",
                        _configuration.GetConnectionString("PostgresConnection")), null);

            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresConnection"));
        }
    }
}