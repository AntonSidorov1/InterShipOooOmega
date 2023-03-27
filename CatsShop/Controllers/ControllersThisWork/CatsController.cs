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
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {


        private readonly ILogger<CatsController> _datas;
        public CatsController(ILogger<CatsController> datas)
        {
            _datas = datas;
        }

        /// <summary>
        /// Получить список котиков
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CatsList GetList()
        {
            return CatsList.GetCatsListFromDB();
        }

        /// <summary>
        /// Получить список котиков по ID цвета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("By-Color/{id}")]
        public List<CatWithModel> ByColor(int id)
        {
            return GetList().FindAll(model => model.Model.ColorID == id);
        }

        /// <summary>
        /// Получить список котиков по ID породы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("By-Species/{id}")]
        public List<CatWithModel> BySpecies(int id)
        {
            return GetList().FindAll(model => model.Model.SpeciesID == id);
        }

        /// <summary>
        /// Получить список котиков по ID пола
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("By-Gender/{id}")]
        public List<CatWithModel> ByGender(int id)
        {
            return GetList().FindAll(model => model.Model.GenderID == id);
        }

        /// <summary>
        /// Получить список котиков по ID модели
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("By-Model/{id}")]
        public List<CatWithModel> ByModel(int id)
        {
            return GetList().FindAll(model => model.Model.ID == id);
        }

        /// <summary>
        /// Получить котика по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CatWithModel?> Get(int id)
        {
            try
            {
                return Ok(GetList().FirstOrDefault(cat => cat.ID == id));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Получить котика в позиции по её ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("By-Pozition/{id}")]
        public ActionResult<CatWithModel?> ByPozition(int id)
        {
            try
            {
                int catID = PozitionsList.GetPositionsListFromDB().GetCatFromPozition(id).ID;
                return Get(catID);
            }
            catch
            {
                return NotFound();
            }
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

        ///// <summary>
        ///// Получить список позиций котика
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}/Positions")]
        //public List<PozitionWithDates> GetPositions(int id) => PozitionsList.GetPositionsListFromDB().GetPositionForCat(id);

        ///// <summary>
        ///// Получить модель котика
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}/Model")]
        //public CatModelFullDatas GetModelFromCat(int id)
        //{
        //    int idCat = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
        //    return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(idCat));
        //}


    }
}
