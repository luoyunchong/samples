using FreeSql.DataAnnotations;
using IGeekFan.FreeKit.Extras.FreeSql;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_repository.Controllers
{
    /// <summary>
    /// 1.¸´ºÏÖ÷¼ü²Ö´¢
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IBaseRepository<UserRole, int, int> _repository;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(ILogger<UserRoleController> logger, IBaseRepository<UserRole, int, int> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet(Name = "Get")]
        public IResult Get(int userid, int roleid)
        {
            UserRole ur = _repository.Get(userid, roleid);
            _logger.LogInformation($"user role crate success");
            return Results.Ok(ur);
        }

        [HttpPost(Name = "Create")]
        public IResult Create([FromBody] UserRole userRole)
        {
            _repository.Insert(userRole);
            _logger.LogInformation($"user role crate success");
            return Results.Ok();
        }

        [HttpDelete(Name = "Delete")]
        public IResult Delete(int userid, int roleid)
        {
            _repository.Delete(userid, roleid);
            _logger.LogInformation($"user role delete success");
            return Results.Ok();
        }
    }


    public class UserRole
    {
        [Column(IsPrimary = true)]
        public int UserId { get; set; }
        [Column(IsPrimary = true)]
        public int RoleId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}