using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : Controller
    {
        private readonly Data.ITodoRepository _todoRepository;
        ILogger _logger;

        public TodoController(Data.ITodoRepository todoRepository, ILogger<TodoController> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }

        //
        // GET: /Api/Todo
        [HttpGet]
        [AllowAnonymous]
        public IList<Models.TodoItem> Get()
        {
            _logger.Log(LogLevel.Information, "HttpGet request on api/todo", null);

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
