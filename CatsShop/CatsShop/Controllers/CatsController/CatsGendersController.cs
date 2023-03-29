using Microsoft.AspNetCore.Mvc;

namespace CatsShop
{
    /// <summary>
    /// Функции для показа полов котиков
    /// </summary>
    [Route("api/[controller]")]
    public class CatsGendersController : ControllerBase
    {

        /// <summary>
        /// Получить список всех полов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public GendersList Get()
        {
            return GendersList.CreateGendersListFromDB();
        }

        /// <summary>
        /// Получить пол по его ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Gender Get(int id)
        {
            return Get().GetGender(id);
        }

        /// <summary>
        /// Получить пол по его названию
        /// </summary>
        /// <returns></returns>
        [HttpGet("By-Name/{name}")]
        public Gender Get(string name)
        {
            return Get().GetGender(name);
        }



    }
}
