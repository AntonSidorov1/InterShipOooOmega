using CatsShop.Classes.Buy;
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Position;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers.ControllersThisWork
{
    /// <summary>
    /// Функции API для работы с покупками
    /// </summary>
    [ApiController]
    [Route("cats/api/[controller]")]
    public class BuysController : ControllerBase
    {
        private readonly ILogger<BuysController> _roles;
        public BuysController(ILogger<BuysController> roles)
        {
            _roles = roles;
        }

        /// <summary>
        /// Получить список покупок
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpGet("List/{session}")]
        public BuysList Get(string session)
        {
            return BuysList.GetBuysListFromDB(session);
        }

        /// <summary>
        /// Получить список покупок
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpGet("{id}/Buy/{session}")]
        public Buy Get(string session, int id)
        {
            return BuysList.GetBuysListFromDB(session).GetBuyFromID(id);
        }

        /// <summary>
        /// Купить позицию
        /// </summary>
        /// <param name="session"></param>
        /// <param name="buy"></param>
        /// <returns></returns>
        [HttpPost("BuyPozition/{pozitionID}")]
        public bool Add(string session, int pozitionID) => BuysList.GetBuys().AddBuy(pozitionID, session);

        /// <summary>
        /// Получить позицию в покупке
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Pozition")]
        public Pozition GetPozition(string session, int id)
        {
            int pozitionID = Get(session, id).PozitionID;
            return PozitionsList.GetPositionsListFromDB().GetPositionFromID(pozitionID);

        }

        /// <summary>
        /// Получить котика в покупке
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Cat")]
        public Cat GetCat(string session, int id)
        {
            int catID = GetPozition(session, id).CatID;
            return CatsList.GetCatsListFromDB().GetCatFromID(catID);

        }

        /// <summary>
        /// Получить модель котика в покупке
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Cat/Model")]
        public CatModelFullDatas GetCatModel(string session, int id)
        {
            int modelID = GetCat(session, id).ModelID;
            return CatModelList.GetModelsListFromDB().GetModelFromID(modelID).GetFullDatas();

        }

    }
}
