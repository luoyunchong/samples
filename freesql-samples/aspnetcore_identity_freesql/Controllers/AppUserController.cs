using aspnetcore_identity_freesql.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel;
using System.Text;

namespace aspnetcore_identity_freesql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : ControllerBase
    {
        private readonly ILogger<AppUserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly SignInManager<AppUser> _signInManager;
        public AppUserController(ILogger<AppUserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserStore<AppUser> userStore)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }
        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
        /// <summary>
        /// 创建用户示例
        /// </summary>
        /// <param name="createAppUser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost]
        public async Task<IResult> CreateUserAsync([FromBody] CreateAppUser createAppUser)
        {
            AppUser user = new AppUser()
            {
                FirstName = createAppUser.FirstName,
                LastName = createAppUser.LastName,
                EmailConfirmed = true //直接让邮件激活
            };
            await _userStore.SetUserNameAsync(user, createAppUser.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, createAppUser.Email, CancellationToken.None);
            var identityResult = await _userManager.CreateAsync(user, createAppUser.Password);

            if (identityResult.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                //获取userid
                var userId = await _userManager.GetUserIdAsync(user);
                //生成邮件激活码
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                return Results.Ok("Register Success");
                //如果邮件 EmailConfirmed 为false时，通过此方式发送激活邮件
                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }
            if (identityResult.Errors == null)
            {
                throw new ArgumentException("identityResult.Errors should not be null.");
            }
            string errorMsg = string.Join(',', identityResult.Errors.Select(r => r.Description));

            _logger.LogInformation(errorMsg);

            return Results.BadRequest(errorMsg);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLoginInfo"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IResult> LoginAsync([FromBody] UserLoginInput userLoginInfo)
        {
            _logger.LogInformation("JwtLogin Begin");
            var result = await _signInManager.PasswordSignInAsync(userLoginInfo.Email, userLoginInfo.Password, userLoginInfo.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                AppUser user = await _userManager.FindByEmailAsync(userLoginInfo.Email);

                //创建Token等等
                var token = Guid.NewGuid().ToString();

                _logger.LogInformation($"User {userLoginInfo},Login Success");

                return Results.Ok(token);
            }
            else
            {
                return Results.BadRequest(result.ToString());
            }
        }
    }

    /// <summary>
    /// 创建用户实体
    /// </summary>
    /// <param name="FirstName">姓</param>
    /// <param name="LastName">名</param>
    /// <param name="Email">邮件、登录名</param>
    /// <param name="Password">密码</param>
    public record CreateAppUser(string FirstName, string LastName, string Email, string Password);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Email">邮件、登录名</param>
    /// <param name="Password">密码</param>
    /// <param name="RememberMe">记住密码</param>
    public record UserLoginInput(string Email, string Password, bool RememberMe);
}