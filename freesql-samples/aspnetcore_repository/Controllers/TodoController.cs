using aspnetcore_repository.Application;
using aspnetcore_repository.DTO;
using aspnetcore_repository.Models;
using IGeekFan.FreeKit.Extras.Extensions;
using IGeekFan.FreeKit.Extras.FreeSql;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_repository.Controllers;

/// <summary>
/// 2.基于审计仓储类（IAuditBaseRepository）的实现 
/// </summary>
[ApiController]
//[Authorize]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly IAuditBaseRepository<Todo, int> _repository;
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _todoService;

    public TodoController(ILogger<TodoController> logger, IAuditBaseRepository<Todo, int> repository, ITodoService todoService)
    {
        _logger = logger;
        _repository = repository;
        _todoService = todoService;
    }

    /// <summary>
    /// 分页获取Todo
    /// </summary>
    /// <param name="pagingInfo"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> GetPageAsync([FromQuery] QueryTodo pagingInfo)
    {
        List<Todo> data = await _repository.Select
            .WhereIf(pagingInfo.Message.IsNotNullOrWhiteSpace(), r => r.Message.Contains(pagingInfo.Message))
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
    public async Task<IResult> GetAsync(int id)
    {
        Todo todo = await _repository.GetAsync(id);
        _logger.LogInformation($"Todo get success");
        return Results.Ok(todo);
    }

    /// <summary>
    /// 创建一个TODO
    /// </summary>
    /// <param name="createTodo"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> Create([FromBody] CreateTodo createTodo)
    {
        Todo todo = new Todo { IsDone = createTodo.IsDone, Message = createTodo.Message, NotifictionTime = createTodo.NotifictionTime };
        await _repository.InsertAsync(todo);
        _logger.LogInformation($"todo crate success");
        return Results.Ok(todo);
    }

    /// <summary>
    /// 更新一个TODO
    /// </summary>
    /// <param name="updateTodo"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IResult> Update([FromBody] UpdateTodo updateTodo)
    {
        Todo todo = await _repository.GetAsync(updateTodo.Id);
        if (todo == null)
        {
            return Results.BadRequest($"to do id {updateTodo.Id} not found.");
        }
        todo.Message = updateTodo.Message;
        todo.IsDone = updateTodo.IsDone;
        if (todo.IsDone) todo.DoneTime = DateTime.Now;
        todo.NotifictionTime = updateTodo.NotifictionTime;
        await _repository.UpdateAsync(todo);
        _logger.LogInformation($"todo update success");
        return Results.Ok(todo);
    }
    [HttpPut("update")]
    public async Task<IResult> UpdateAsync([FromBody] UpdateTodo updateTodo)
    {
        await _todoService.Update(updateTodo);
        _logger.LogInformation($"todo update success");
        return Results.Ok(updateTodo);
    }
    /// <summary>
    /// 删除一个TODO 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IResult> Delete(int id)
    {
        int row = await _repository.DeleteAsync(id);
        _logger.LogInformation($"todo delete success");
        return Results.Ok(row);
    }
}