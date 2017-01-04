using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : Controller
    {
        private readonly Data.ITodoRepository _todoRepository;

        public TodoController(Data.ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        //
        // GET: /Api/Todo
        [HttpGet]
        [AllowAnonymous]
        public IList<Models.TodoItem> Get()
        {
            return _todoRepository.Get();
        }

        //
        // POST: /Api/Todo
        [HttpPostAttribute]
        [AllowAnonymous]
        public IList<Models.TodoItem> Post([FromBodyAttribute]Models.TodoItem newItem)
        {
            newItem.CreatedOn = DateTime.Now;

            _todoRepository.Add(newItem);

            return _todoRepository.Get();
        }
    }
}
