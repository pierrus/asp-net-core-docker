using System;
using System.Collections.Generic;

namespace Todo.Data
{
    public class SimpleRepository : ITodoRepository
    {
        private IList<Models.TodoItem> _todoItems = new List<Models.TodoItem>();

        public void Add (Models.TodoItem newItem)
        {
            _todoItems.Add(newItem);
        }

        public IList<Models.TodoItem> Get()
        {
            return _todoItems;
        }
    }
}