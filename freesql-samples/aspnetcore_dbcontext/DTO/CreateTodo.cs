namespace aspnetcore_dbcontext.DTO;

public record CreateTodo(string Message, bool IsDone, DateTime? NotifictionTime);