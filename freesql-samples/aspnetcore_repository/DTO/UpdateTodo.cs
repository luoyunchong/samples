namespace aspnetcore_repository.DTO;

public record UpdateTodo(int Id, string Message, bool IsDone, DateTime? NotifictionTime);