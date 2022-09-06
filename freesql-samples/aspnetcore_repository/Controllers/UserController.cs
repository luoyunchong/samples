using aspnetcore_repository.DTO;
using aspnetcore_repository.Models;
using IGeekFan.FreeKit.Extras.FreeSql;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_repository.Controllers;

public class UserController : ControllerBase
{
    private readonly IAuditBaseRepository<User> _repository;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IAuditBaseRepository<User> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// µÇÂ¼
    /// </summary>
    /// <param name="loginInput"></param>
    /// <returns></returns>
    [HttpPost]
    public IResult Login([FromBody] LoginInput loginInput)
    {
        string token = "";
        return Results.Ok(loginInput);
    }
}