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
    public class SignInController : ControllerBase
    {
        private readonly IAppAuthService _appAuthService;

        public SignInController()
        {
            _appAuthService = new AppAuthService();
        }

        private List<UserWithRole> users = new List<UserWithRole>
        {
            new UserWithRole {Login="admin", Password="123", RoleEng = "admin" },
            new UserWithRole { Login="user", Password="555", RoleEng = "client" }
        };

        /// <summary>
        /// Авторизировать пользователя в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Token([FromBody]User user)
        {
            _appAuthService.Users = users;
            try
            {
                Token? token = _appAuthService.Authenticate(user);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }


        }

    }
}

