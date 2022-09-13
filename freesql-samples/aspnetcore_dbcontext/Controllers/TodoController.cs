using aspnetcore_dbcontext.Data;
using aspnetcore_dbcontext.DTO;
using aspnetcore_dbcontext.Models;
using FreeSql.Internal.Model;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_dbcontext.Controllers
{
    /// <summary>
    /// 基于DbContext实现
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly ILogger<TodoController> _logger;
        private readonly TodoDbContext _dbContext;

        public TodoController(ILogger<TodoController> logger, TodoDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 分页获取Todo
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResult> GetPageAsync([FromQuery] QueryTodo pagingInfo)
        {
            List<Todo> data = await _dbContext.Todos
                .Select
                .WhereIf(!string.IsNullOrWhiteSpace(pagingInfo.Message), r => r.Message.Contains(pagingInfo.Message))
                .Page(pagingInfo)
                .ToListAsync();

            return Results.Ok(new { data = data, count = pagingInfo.Count });
        }


        /// <summary>
        /// 获取一个TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IResult> GetAsync(long id)
        {
            Todo todo = await _dbContext.Todos.Select.WhereDynamic(id).FirstAsync();
            Todo todo2 = await _dbContext.Todos.Select.Where(r => r.Id == id).FirstAsync();
            _logger.LogInformation($"Todo get success");
            return Results.Ok(todo);
        }

        /// <summary>
        /// 创建一个TODO
        /// </summary>
        /// <param name="createTodo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> CreateAsync([FromBody] CreateTodo createTodo)
        {
            Todo todo = new Todo { IsDone = createTodo.IsDone, Message = createTodo.Message, NotifictionTime = createTodo.NotifictionTime };
            await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"todo crate success");
            return Results.Ok(todo);
        }

        /// <summary>
        /// 更新一个TODO
        /// </summary>
        /// <param name="updateTodo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> UpdateAsync([FromBody] UpdateTodo updateTodo)
        {
            Todo todo = await _dbContext.Todos.Where(r => r.Id == updateTodo.Id).FirstAsync();
            if (todo == null)
            {
                return Results.BadRequest($"to do id {updateTodo.Id} not found.");
            }
            todo.Message = updateTodo.Message;
            todo.IsDone = updateTodo.IsDone;
            todo.NotifictionTime = updateTodo.NotifictionTime;

            await _dbContext.Todos.UpdateAsync(todo);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"todo update success");
            return Results.Ok(todo);
        }

        /// <summary>
        /// 删除一个TODO 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IResult> Delete(int id)
        {
            Todo todo = await _dbContext.Todos.Where(r => r.Id == id).FirstAsync();
            _dbContext.Todos.Remove(todo);
            await _dbContext.SaveChangesAsync();

            //await _dbContext.Todos.RemoveAsync(r => r.Id == id);
            //await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"todo delete success");
            return Results.Ok(1);
        }
    }
}