using FreeSql.Internal.Model;

namespace aspnetcore_dbcontext.DTO;

public class QueryTodo : BasePagingInfo
{
    public string Message { get; set; }
}