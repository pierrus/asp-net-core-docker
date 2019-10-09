using Microsoft.EntityFrameworkCore;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<Models.TodoItem> TodoItems { get; set; }

        public TodoContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Todo.db");
        }
    }
}