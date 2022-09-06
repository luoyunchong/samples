namespace aspnetcore_repository.DTO;

public record CreateTodo(string Message, bool IsDone, DateTime? NotifictionTime);