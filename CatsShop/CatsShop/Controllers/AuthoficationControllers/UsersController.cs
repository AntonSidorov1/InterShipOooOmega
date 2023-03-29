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
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Get()
        {
            try
            {
                string name = User.Identity.Name ?? "";
                if (!UserList.CreateUsersFromDB().HaveLogin(name))
                {
                    throw new Exception("Данный пользователь не существует в системе");
                }
                return Ok(UserList.CreateUsersFromDB());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Зарегистрироваться в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost("registrate")]
        [AllowAnonymous]
        public ActionResult Registrate([FromBody]User user) 
        {
            return UserList.CreateUsersFromDB().AddUser(user, 1) ? Ok(true) : Conflict(false);
        }

        /// <summary>
        /// Добавить администратора
        /// </summary>
        /// <returns></returns>
        [HttpPost("Admins")]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAdmin([FromBody]User user)
        {
            try
            {
                string name = User.Identity.Name ?? "";
                if (!UserList.CreateUsersFromDB().HaveLogin(name))
                {
                    throw new Exception("Данный пользователь не существует в системе");
                }
                return UserList.CreateUsersFromDB().AddUser(user, 2) ? Ok(true) : Conflict(false);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Получить свой логин
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-login")]
        public IActionResult GetLogin()
        {
            string name = User.Identity.Name??"";
            if (UserList.CreateUsersFromDB().HaveLogin(name))
                return Ok(name);
            else
                return NotFound();
        }

        /// <summary>
        /// Получить свою роль
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-role")]
        public IActionResult GetRole()
        {
            string name = User.Identity.Name??"";
            if (UserList.CreateUsersFromDB().HaveLogin(name))
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
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Сменить пароль
        /// </summary>
        /// <returns></returns>
        [HttpPatch("change-password")]
        public ActionResult ChangePassword([FromBody] string password)
        {
            return UserList.CreateUsersFromDB().ChangePassword(User.Identity.Name??"", password) ? Ok(true) : Conflict(false);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{login}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DropUser(string login)
        {
            string name = User.Identity.Name ?? "";
            if (!UserList.CreateUsersFromDB().HaveLogin(name))
            {
                return Unauthorized("Ваш логин больше не существует с вистеме");
            }
            return UserList.CreateUsersFromDB().DeleteUser(login) ? Ok(true) : NotFound(false);
        }

        /// <summary>
        /// Удалить аккаунт
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public ActionResult DropUser()
        {
            return UserList.CreateUsersFromDB().DeleteUser(User.Identity.Name ?? "") ? Ok(true) : NotFound(false);
        }
    }
}
