using System;
using Xunit;

namespace Todo.Test
{
    public class RepositoryTests
    {
        [Fact]
        public void EmptyInitialisation() 
        {
            Todo.Data.SimpleRepository rep = new Todo.Data.SimpleRepository();
            
            Assert.Equal(0, rep.Get().Count);
        }

        [Fact]
        public void AddAnItem() 
        {
            Todo.Data.SimpleRepository rep = new Todo.Data.SimpleRepository();

            rep.Add(new Todo.Models.TodoItem { Title = "todo-1", CreatedOn = DateTime.Now });

            Assert.Equal(1, rep.Get().Count);
        }
    }
}
