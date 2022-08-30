using IGeekFan.FreeKit.Extras.AuditEntity;
using IGeekFan.FreeKit.Extras.FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_repository.Controllers
{
    /// <summary>
    /// 2.基于审计类的FreeSql仓储
    /// </summary>
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IAuditBaseRepository<Todo, int> _repository;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger, IAuditBaseRepository<Todo, int> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// 获取一个TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IResult Get(int id)
        {
            Todo todo = _repository.Get(id);
            _logger.LogInformation($"Todo get success");
            return Results.Ok(todo);
        }

        /// <summary>
        /// 创建一个TODO
        /// </summary>
        /// <param name="createTodo"></param>
        /// <returns></returns>
        [HttpPost]
        public IResult Create([FromBody] CreateTodo createTodo)
        {
            Todo todo = new Todo { IsDone = createTodo.IsDone, Message = createTodo.Message, NotifictionTime = createTodo.NotifictionTime };
            _repository.Insert(todo);
            _logger.LogInformation($"todo crate success");
            return Results.Ok(todo);
        }

        /// <summary>
        /// 更新一个TODO
        /// </summary>
        /// <param name="updateTodo"></param>
        /// <returns></returns>
        [HttpPut]
        public IResult Update([FromBody] UpdateTodo updateTodo)
        {
            Todo todo = _repository.Get(updateTodo.Id);
            if (todo == null)
            {
                return Results.BadRequest($"to do id {updateTodo.Id} not found.");
            }
            todo.Message = updateTodo.Message;
            todo.IsDone = updateTodo.IsDone;
            todo.NotifictionTime = updateTodo.NotifictionTime;
            _repository.Update(todo);
            _logger.LogInformation($"todo update success");
            return Results.Ok(todo);
        }

        /// <summary>
        /// 删除一个TODO 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResult Delete(int id)
        {
            int row = _repository.Delete(id);
            _logger.LogInformation($"todo delete success");
            return Results.Ok(row);
        }
    }

    public record CreateTodo(string Message, bool IsDone, DateTime? NotifictionTime);
    public record UpdateTodo(int Id, string Message, bool IsDone, DateTime? NotifictionTime);

    public class Todo : FullAuditEntity<int, Guid>
    {
        public string Message { get; set; }
        public bool IsDone { get; set; }
        public DateTime? NotifictionTime { get; set; }
    }


    public class UserController : ControllerBase
    {
        private readonly IAuditBaseRepository<User> _repository;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IAuditBaseRepository<User> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [HttpPost]
        public IResult Login([FromBody] LoginInput loginInput)
        {
            string token = "";
            return Results.Ok(loginInput);
        }
    }

    public record LoginInput(string UserName, bool Password);
    public class User : Entity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}