using System;
using System.Collections.Generic;

namespace Todo.Data
{
    public interface ITodoRepository
    {
        void Add (Models.TodoItem newItem);

        IList<Models.TodoItem> Get();
    }
}