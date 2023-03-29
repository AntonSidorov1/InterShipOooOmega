using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers.CatsController
{
    /// <summary>
    /// Список функций для котиков
    /// </summary>
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        /// <summary>
        /// Получить список котиков
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CatsList Get()
        {
            return CatsList.CreateCatsListFromDB();
        }

        /// <summary>
        /// Получить котика по его ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public Cat? Get(int id) => Get().GetCatFromID(id);

        /// <summary>
        /// Добавить котика
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<bool> Add([FromBody] Cat cat)
        {
            string name = User.Identity.Name ?? "";
            if (!UserList.CreateUsersFromDB().HaveLogin(name))
            {
                return Unauthorized("Ваш логин больше не существует в системе");
            }
            return Get().CatAdd(cat) ? Ok(true) : Conflict(false);
        }

        /// <summary>
        /// Изменить котика
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<bool> Update(int id, [FromBody] Cat cat)
        {
            string name = User.Identity.Name ?? "";
            if (!UserList.CreateUsersFromDB().HaveLogin(name))
            {
                return Unauthorized("Ваш логин больше не существует в cистеме");
            }
            return Get().UpdateCat(id, cat) ? Ok(true) : Conflict(false);
        }

        /// <summary>
        /// Удалить котика
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<bool> Delete(int id)
        {
            string name = User.Identity.Name ?? "";
            if (!UserList.CreateUsersFromDB().HaveLogin(name))
            {
                return Unauthorized("Ваш логин больше не существует в cистеме");
            }
            return Get().DeleteCat(id) ? Ok(true) : NotFound(false);
        }

        /// <summary>
        /// Купить котика
        /// </summary>
        /// <returns></returns>
        [HttpPut("Buy/{id}")]
        [Authorize(Roles = "Client")]
        public ActionResult<bool> Buy(int id)
        {
            string name = User.Identity.Name ?? "";
            if (!UserList.CreateUsersFromDB().HaveLogin(name))
            {
                return Unauthorized("Ваш логин больше не существует в системе");
            }
            return Get().BuyPozition(id) ? Ok(true) : NotFound(false);
        }

    }
}
