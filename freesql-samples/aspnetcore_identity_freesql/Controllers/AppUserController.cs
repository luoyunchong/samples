using aspnetcore_identity_freesql.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore_identity_freesql.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppUserController : ControllerBase
    {
        private readonly ILogger<AppUserController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AppUserController(ILogger<AppUserController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

    }
}