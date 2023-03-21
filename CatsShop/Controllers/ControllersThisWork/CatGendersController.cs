
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using Microsoft.AspNetCore.Mvc;
namespace CatsShop.Controllers;

/// <summary>
/// Функции API для работы с полами котиков
/// </summary>
[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatGendersController : ControllerBase
{
    
    
    
    private readonly ILogger<CatGendersController> _datas;
    public CatGendersController(ILogger<CatGendersController> datas)
    {
        _datas = datas;
    }

    /// <summary>
    /// Получить список полов
    /// </summary>
    /// <returns></returns>
    [HttpGet("CatGendersList")]
    public CatGendersList GetList()
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders;
    }
    
    /// <summary>
    /// Получить название пола по его ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/CatGendersName")]
    public string GetGender(int id)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromID(id).Name;
    }
    
    /// <summary>
    /// Получить ID пола по его названию
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPut("CatGendersID")]
    public int GetGender(CatGenderName name)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromName(name.CatGender).ID;
    }

    /// <summary>
    /// Получить пол котика по его моделе
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("CatModelGender/{id}")]
	public CatGender GetGenderFromModel(int id)
		=> CatModelList.GetModelsListFromDB().GetDatasFromID(id).GetGender();
		

    
}