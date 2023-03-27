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
[Route("api/[controller]")]
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
    [HttpGet()]
    public List<CatModelFullDatas> GetList()
    {
        return CatModelList.GetModelsListFromDB().GetListFullDatas();
    }

    /// <summary>
    /// Получить список моделей котиков по ID цвета
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Color/{id}")]
    public List<CatModelFullDatas> GetByColor(int id)
        => GetList().FindAll(model => model.ColorID ==  id);

    /// <summary>
    /// Получить список моделей котиков по ID породы
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Species/{id}")]
    public List<CatModelFullDatas> GetBySpecies(int id)
        => GetList().FindAll(model => model.SpeciesID == id);

    /// <summary>
    /// Получить список моделей котиков по ID пола
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Gender/{id}")]
    public List<CatModelFullDatas> GetByGender(int id)
        => GetList().FindAll(model => model.GenderID == id);



    /// <summary>
    /// Получить полную информацию о моделе по её ID
    /// </summary>
    [HttpGet("By-ID/{id}")]
    public ActionResult<CatModelFullDatas> GetModelFullDatas(int id)
    {
        try
        {
            return Ok(CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(id)));
        }
        catch
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Получить полную информацию о моделе по ID котика
    /// </summary>
    [HttpGet("By-Cat/{id}")]
    public ActionResult<CatModelFullDatas> GetModelByCat(int id)
    {
        try
        {
            int modelID = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
            return CatModelFullDatas.GetModel(CatModelList.GetModelsListFromDB().GetModelFromID(modelID));
        }
        catch
        {
            return NotFound();
        }
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

}