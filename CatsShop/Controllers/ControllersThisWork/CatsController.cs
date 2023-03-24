using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Position;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design.Serialization;

namespace CatsShop.Controllers.ControllersThisWork
{
    /// <summary>
    /// Функции API для работы с котиками
    /// </summary>
    [ApiController]
    [Route("cats/api/cats/[controller]", Name = "Cats")]
    public class CatsController : ControllerBase
    {


        private readonly ILogger<CatsController> _datas;
        public CatsController(ILogger<CatsController> datas)
        {
            _datas = datas;
        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns></returns>
        [HttpGet("List", Name = "CatsList")]
        public CatsList GetList()
        {
            return CatsList.GetCatsListFromDB();
        }

        /// <summary>
        /// Получить котика по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Datas")]
        public CatDatas GetDatas(int id)
        {
            return GetList().GetCatDatasFromID(id);
        }

        // <summary>
        /// Получить котика по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Get")]
        public Cat GetCat(int id)
        {
            return GetList().GetCatFromID(id);
        }

        /// <summary>
        /// Оптимальный метод получения котика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Cat? GetCatOptimized(int id)
        {
            return GetList().FirstOrDefault(c => c.ID == id);
        }

        /// <summary>
        /// Добавить котика
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public bool Add(CatDatas cat, string session)
        {
            return CatsList.GetCatsListFromDB().AddCat(cat, session);
        }

        /// <summary>
        /// Изменить котика
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpPut("UpdateCat")]
        public bool Update(Cat cat, string session)
        {
            return Update(cat, session, cat.ID);
        }

        /// <summary>
        /// Изменить котика
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/Update")]
        public bool Update(CatDatas cat, string session, int id)
        {
            return CatsList.GetCatsListFromDB().UpdateCat(cat, session, id);
        }

        /// <summary>
        /// Удалить котика
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}/Delete")]
        public bool Delete(string session, int id)
        {
            return CatsList.GetCatsListFromDB().DeleteCat(id, session);
        }

        /// <summary>
        /// Получить список позиций котика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Positions")]
        public List<PozitionWithDates> GetPositions(int id) => PozitionsList.GetPositionsListFromDB().GetPositionForCat(id);

        /// <summary>
        /// Получить модель котика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Model")]
        public CatModelFullDatas GetModelFromCat(int id)
        {
            int idCat = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
            return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(idCat));
        }


    }
}
