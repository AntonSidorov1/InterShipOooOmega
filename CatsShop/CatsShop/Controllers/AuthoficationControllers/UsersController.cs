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
                if (!HaveLogin(name))
                {
                    throw new Exception("Данный пользователь не существует в системе");
                }
                return Ok(GetUsers());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        private UserList GetUsers() => UserList.CreateUsersFromDB();

        /// <summary>
        /// Существует ли пользователь с логином name в системе
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool HaveLogin(string name) => GetUsers().HaveLogin(name);

        /// <summary>
        /// Добавить пользователя в систему с ролью roleID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        private ActionResult AddUser(User user, int roleID)
        {
            return GetUsers().AddUser(user, roleID) ? Ok(true) : Conflict(false);
        }

        /// <summary>
        /// Зарегистрироваться в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost("registrate")]
        [AllowAnonymous]
        public ActionResult Registrate([FromBody]User user) 
        {
            return AddUser(user, 1);
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
                if (!HaveLogin(name))
                {
                    throw new Exception("Данный пользователь не существует в системе");
                }
                return AddUser(user, 2);
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
        public ActionResult<string> GetLogin()
        {
            string name = User.Identity.Name??"";
            if (HaveLogin(name))
                return Ok(name);
            else
                return NotFound("null");
        }

        /// <summary>
        /// Получить свою роль
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-role")]
        public IActionResult GetRole()
        {
            string name = User.Identity.Name??"";
            if (HaveLogin(name))
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
            return GetUsers().ChangePassword(User.Identity.Name??"", password) ? Ok(true) : Conflict(false);
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
            if (!HaveLogin(name))
            {
                return Unauthorized("Ваш логин больше не существует с cистеме");
            }
            return GetUsers().DeleteUser(login) ? Ok(true) : NotFound(false);
        }

        /// <summary>
        /// Удалить аккаунт
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public ActionResult DropUser()
        {
            return GetUsers().DeleteUser(User.Identity.Name ?? "") ? Ok(true) : NotFound(false);
        }
    }
}
