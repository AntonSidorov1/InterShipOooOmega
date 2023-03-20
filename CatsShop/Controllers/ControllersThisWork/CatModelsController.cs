using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatModelsController : ControllerBase
{
    
    
       
    private readonly ILogger<CatModelsController> _datas;
    public CatModelsController (ILogger<CatModelsController> datas)
    {
        _datas = datas;
    }


    
    [HttpGet("CatModelsList")]
    public List<CatModelFullDatas> GetList()
    {
        return CatModelList.GetModelsListFromDB().GetListFullDatas();
    }
    
    
    [HttpGet("{id}/GetModel")]
    public CatModelDatas GetModel(int id)
    {
        return CatModelList.GetModelsListFromDB().GetDatasFromID(id);
    }
    
    [HttpGet("{id}/FullDatas")]
    public CatModelFullDatas GetModelFullDatas(int id)
    {
        return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(id));
    }
    
    [HttpPost("AddModel")]
    public bool AddModel(string session, CatModelDatas model)
    {
        return CatModelList.GetModelsListFromDB().AddModel(model, session);
    }
	
    [HttpPut("UpdateModel")]
    public bool ApdateModel(string session, CatModel model)
    {
        return CatModelList.GetModelsListFromDB().UpdateModel(model, session);
    }

    [HttpPost("AddModelWithDatas")]
    public bool AddModelWithDatas(string session, CatModelDatasName model)
    {
        return CatModelList.GetModelsListFromDB().AddModel(model, session);
    }

    [HttpPut("{id}/UpdateModelWithDatas")]
    public bool ApdateModelWithDatas(int id, string session, CatModelDatasName model)
    {
        return CatModelList.GetModelsListFromDB().UpdateModel(model, session, id);
    }

    [HttpDelete("{id}/Delete")]
    public bool DeleteModel(string session, int id)
    {
        return CatModelList.GetModelsListFromDB().DeleteModel(id, session);
    }

    [HttpGet("FromCat/{id}")]
	public CatModelFullDatas GetModelFromCat(int id)
    {
        int idCat = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
        return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(idCat));
    }

}