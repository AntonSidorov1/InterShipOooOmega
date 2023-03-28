using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace CatsShop
{
    /// <summary>
    /// Контроллер для пользователя
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {



        
        /// <summary>
        /// Получить свой логин
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-login")]
        public IActionResult GetLogin()
        {
            return Ok(User.Identity.Name);
        }

        /// <summary>
        /// Получить свою роль
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-role")]
        public IActionResult GetRole()
        {

            Claim? claim = ((ClaimsIdentity)User.Identity).FindFirst(claim => claim.Type == ClaimsIdentity.DefaultRoleClaimType);
            Role role = new Role();
            if (claim != null)
            {
                role.RoleEng = claim.Value;
            }
            var response = new
            {
                
                roleEng = role.RoleEng,
                roleRus = role.RoleRus
            };

            return Ok(response);
        }
        
    }
}
