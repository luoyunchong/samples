using FreeSql.Internal.Model;

namespace aspnetcore_repository.DTO;

public class QueryTodo : BasePagingInfo
{
    public string Message { get; set; }
}