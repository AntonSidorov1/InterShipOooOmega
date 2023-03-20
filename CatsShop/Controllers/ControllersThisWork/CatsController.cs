using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Position;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers.ControllersThisWork
{

    [ApiController]
    [Route("cats/api/cats/[controller]")]
    public class CatsController : ControllerBase
    {


        private readonly ILogger<CatsController> _datas;
        public CatsController(ILogger<CatsController> datas)
        {
            _datas = datas;
        }


        [HttpGet("List")]
        public CatsList GetList()
        {
            return CatsList.GetCatsListFromDB();
        }

        [HttpGet("{id}/Datas")]
        public CatDatas GetDatas(int id)
        {
            return CatsList.GetCatsListFromDB().GetCatDatasFromID(id);
        }


        [HttpGet("{id}/Get")]
        public Cat GetCat(int id)
        {
            return CatsList.GetCatsListFromDB().GetCatFromID(id);
        }

        [HttpPost("Add")]
        public bool Add(CatDatas cat, string session)
        {
            return CatsList.GetCatsListFromDB().AddCat(cat, session);
        }

        [HttpPut("UpdateCat")]
        public bool Update(Cat cat, string session)
        {
            return CatsList.GetCatsListFromDB().UpdateCat(cat, session);
        }

        [HttpPut("{id}/Update")]
        public bool Update(CatDatas cat, string session, int id)
        {
            return CatsList.GetCatsListFromDB().UpdateCat(cat, session, id);
        }

        [HttpDelete("{id}/Delete")]
        public bool Delete(string session, int id)
        {
            return CatsList.GetCatsListFromDB().DeleteCat(id, session);
        }

        [HttpGet("{id}/Positions")]
        public List<PozitionWithDates> GetPositions(int id) => PozitionsList.GetPositionsListFromDB().GetPositionForCat(id);


        [HttpGet("{id}/Model")]
        public CatModelFullDatas GetModelFromCat(int id)
        {
            int idCat = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
            return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(idCat));
        }


    }
}
