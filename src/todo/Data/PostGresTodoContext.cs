using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Todo.Data
{
    public class PostGresTodoContext : DbContext
    {
        IConfigurationRoot _configuration;

        public DbSet<Models.TodoItem> TodoItems { get; set; }

        public PostGresTodoContext(IConfigurationRoot configuration)
        {
            _configuration = configuration;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = @"Server=postgres; Port=5432; User Id=myuser; Password=none; Database=todo";
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresConnection"));
        }
    }
}