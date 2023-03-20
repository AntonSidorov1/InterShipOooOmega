using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Position;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers.ControllersThisWork
{
    [ApiController]
    [Route("cats/api/positions/[controller]")]
    public class PozitionsController : ControllerBase
    {

        private readonly ILogger<PozitionsController> _datas;
        public PozitionsController(ILogger<PozitionsController> datas)
        {
            _datas = datas;
        }


        [HttpGet("List")]
        public PozitionsList GetList() => PozitionsList.GetPositionsListFromDB();


        [HttpGet("{id}/Get")]
        public PozitionWithDates Get(int id) => PozitionsList.GetPositionsListFromDB().GetPositionFromID(id);

        [HttpGet("{id}/Cat")]
        public Cat GetCat(int id)
            => PozitionsList.GetPositionsListFromDB().GetCatFromPozition(id);

        [HttpGet("{id}/CatModel")]
        public CatModel GetCatModel(int id)
            => PozitionsList.GetPositionsListFromDB().GetCatModelFromPozition(id);


        [HttpPost("Add")]
        public bool Add(string session, PozitionDatas pozition) 
            => PozitionsList.GetPositionsListFromDB().AddPozition(pozition, session);

        [HttpPut("UpdatePozition")]
        public bool Update(string session, Pozition pozition)
            => PozitionsList.GetPositionsListFromDB().UpdatePozition(pozition, session);

        [HttpPut("{id}/Update")]
        public bool Update(string session, int id,  PozitionDatas pozition)
            => PozitionsList.GetPositionsListFromDB().UpdatePozition(pozition, session, id);

        [HttpDelete("{id}/Delete")]
        public bool Delete(string session, int id)
            => PozitionsList.GetPositionsListFromDB().DeletePozition(id, session);

    }
}
