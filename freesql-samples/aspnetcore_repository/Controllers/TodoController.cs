using aspnetcore_repository.DTO;
using aspnetcore_repository.Models;
using FreeSql.Internal.Model;
using IGeekFan.FreeKit.Extras.FreeSql;
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
        /// 分页获取Todo
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResult> GetPageAsync([FromQuery] BasePagingInfo pagingInfo)
        {
            List<Todo> data = await _repository.Select.Page(pagingInfo).ToListAsync();

            return Results.Ok(new { data = data, count = pagingInfo.Count });
        }


        /// <summary>
        /// 获取一个TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
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
}