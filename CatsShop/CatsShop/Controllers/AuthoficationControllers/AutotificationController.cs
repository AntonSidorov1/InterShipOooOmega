using CatsShop.Classes.API.Autotification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatsShop
{
    /// <summary>
    /// Контроллер для авторизации
    /// </summary>
    [Route("api/[controller]")]
    public class AutotificationController : ControllerBase
    {
        private readonly IAppAuthService _appAuthService;

        public AutotificationController()
        {
            _appAuthService = new AppAuthService();
        }

        /// <summary>
        /// Авторизировать пользователя в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost("Sign-In")]
        public ActionResult Token([FromBody] User user)
        {
            _appAuthService.Users = UserList.CreateUsersFromDB();
            try
            {
                Token? token = _appAuthService.Authenticate(user);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }


        }

    }
}

