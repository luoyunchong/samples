namespace aspnetcore_dbcontext.DTO;

public record UpdateTodo(int Id, string Message, bool IsDone, DateTime? NotifictionTime);