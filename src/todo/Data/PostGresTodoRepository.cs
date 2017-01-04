using System.Collections.Generic;
using System.Linq;

namespace Todo.Data
{
    public class PostGresTodoRepository : ITodoRepository
    {
        PostGresTodoContext _todoContext;

        public PostGresTodoRepository (PostGresTodoContext todoContext)
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