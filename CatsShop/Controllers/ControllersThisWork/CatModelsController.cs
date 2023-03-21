using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// Функции API для работы с моделями котиков
/// </summary>
[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatModelsController : ControllerBase
{
    
    
       
    private readonly ILogger<CatModelsController> _datas;
    public CatModelsController (ILogger<CatModelsController> datas)
    {
        _datas = datas;
    }


    /// <summary>
    /// Спиоск моделей
    /// </summary>
    /// <returns></returns>
    [HttpGet("CatModelsList")]
    public List<CatModelFullDatas> GetList()
    {
        return CatModelList.GetModelsListFromDB().GetListFullDatas();
    }
    
    /// <summary>
    /// Получить модель по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/GetModel")]
    public CatModelDatas GetModel(int id)
    {
        return CatModelList.GetModelsListFromDB().GetDatasFromID(id);
    }

    /// <summary>
    /// Получить полную информацию о моделе по её ID
    /// </summary>
    /// <param name="id"></para
    [HttpGet("{id}/FullDatas")]
    public CatModelFullDatas GetModelFullDatas(int id)
    {
        return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(id));
    }
    
    /// <summary>
    /// Добавить модель
    /// </summary>
    /// <param name="session"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("AddModel")]
    public bool AddModel(string session, CatModelDatas model)
    {
        return CatModelList.GetModelsListFromDB().AddModel(model, session);
    }
	
    /// <summary>
    /// Изменить модель
    /// </summary>
    /// <param name="session"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("UpdateModel")]
    public bool ApdateModel(string session, CatModel model)
    {
        return CatModelList.GetModelsListFromDB().UpdateModel(model, session);
    }

    /// <summary>
    /// Добавить модель (ввод текстовых значений параметров)
    /// </summary>
    /// <param name="catModel"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPost("AddModelWithDatas")]
    public bool AddModelWithDatas(string session, CatModelDatasName model)
    {
        return CatModelList.GetModelsListFromDB().AddModel(model, session);
    }

    /// <summary>
    /// Изменить модель (ввод текстовых значений параметров)
    /// </summary>
    /// <param name="catModel"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPut("{id}/UpdateModelWithDatas")]
    public bool ApdateModelWithDatas(int id, string session, CatModelDatasName model)
    {
        return CatModelList.GetModelsListFromDB().UpdateModel(model, session, id);
    }

    /// <summary>
    /// Удалить модель
    /// </summary>
    /// <param name="session"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}/Delete")]
    public bool DeleteModel(string session, int id)
    {
        return CatModelList.GetModelsListFromDB().DeleteModel(id, session);
    }

    /// <summary>
    /// Получить модель котика по его ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("FromCat/{id}")]
	public CatModelFullDatas GetModelFromCat(int id)
    {
        int idCat = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
        return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(idCat));
    }

}