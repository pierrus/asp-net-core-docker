using System.Collections.Generic;
using System.Linq;

namespace Todo.Data
{
    public class TodoRepository : ITodoRepository
    {
        TodoContext _todoContext;

        public TodoRepository (TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public void Add (Models.TodoItem newItem)
        {
            _todoContext.TodoItems.Add(newItem);
            _todoContext.SaveChanges();
        }

        public IList<Models.TodoItem> Get()
        {
            return _todoContext.TodoItems.ToList();
        }
    }
}