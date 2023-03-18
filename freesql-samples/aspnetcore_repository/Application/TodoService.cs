using aspnetcore_repository.DTO;
using aspnetcore_repository.Models;
using IGeekFan.FreeKit.Extras.FreeSql;

namespace aspnetcore_repository.Application;

public class TodoService : ITodoService
{
    private readonly IAuditBaseRepository<Todo, int> _repository;

    public TodoService(IAuditBaseRepository<Todo, int> repository)
    {
        _repository = repository;
    }


    [Transactional]
    public async Task Update(UpdateTodo updateTodo)
    {
        Todo todo = await _repository.GetAsync(updateTodo.Id);
        todo.Message = updateTodo.Message;
        todo.IsDone = updateTodo.IsDone;
        if (todo.IsDone) todo.DoneTime = DateTime.Now;
        todo.NotifictionTime = updateTodo.NotifictionTime;
        await _repository.UpdateAsync(todo);
    }
}

public interface ITodoService
{
    Task Update(UpdateTodo updateTodo);
}