using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Position;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers.ControllersThisWork
{
    /// <summary>
    /// Функции API для позиций котиков
    /// </summary>
    [ApiController]
    [Route("cats/api/positions/[controller]")]
    public class PozitionsController : ControllerBase
    {

        private readonly ILogger<PozitionsController> _datas;
        public PozitionsController(ILogger<PozitionsController> datas)
        {
            _datas = datas;
        }

        /// <summary>
        /// Список позиций
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public PozitionsList GetList() => PozitionsList.GetPositionsListFromDB();

        /// <summary>
        /// Получить позицию по её ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Get")]
        public PozitionWithDates Get(int id) => PozitionsList.GetPositionsListFromDB().GetPositionFromID(id);

        /// <summary>
        /// Получить позицию по её ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Cat")]
        public Cat GetCat(int id)
            => PozitionsList.GetPositionsListFromDB().GetCatFromPozition(id);

        /// <summary>
        /// Получить модель котика в данной позиции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/CatModel")]
        public CatModel GetCatModel(int id)
            => PozitionsList.GetPositionsListFromDB().GetCatModelFromPozition(id);

        /// <summary>
        /// Добавить позицию
        /// </summary>
        /// <param name="session"></param>
        /// <param name="pozition"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public bool Add(string session, PozitionDatas pozition) 
            => PozitionsList.GetPositionsListFromDB().AddPozition(pozition, session);

        /// <summary>
        /// Изменить позицию
        /// </summary>
        /// <param name="session"></param>
        /// <param name="pozition"></param>
        /// <returns></returns>
        [HttpPut("UpdatePozition")]
        public bool Update(string session, Pozition pozition)
            => PozitionsList.GetPositionsListFromDB().UpdatePozition(pozition, session);

        /// <summary>
        /// Изменить позицию
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <param name="pozition"></param>
        /// <returns></returns>
        [HttpPut("{id}/Update")]
        public bool Update(string session, int id,  PozitionDatas pozition)
            => PozitionsList.GetPositionsListFromDB().UpdatePozition(pozition, session, id);

        /// <summary>
        /// Удалить позицю
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}/Delete")]
        public bool Delete(string session, int id)
            => PozitionsList.GetPositionsListFromDB().DeletePozition(id, session);

    }
}
