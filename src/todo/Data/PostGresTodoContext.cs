using Microsoft.EntityFrameworkCore;

namespace Todo.Data
{
    public class PostGresTodoContext : DbContext
    {
        public DbSet<Models.TodoItem> TodoItems { get; set; }

        public PostGresTodoContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=postgres; Port=5432; User Id=myuser; Password=none; Database=todo";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}