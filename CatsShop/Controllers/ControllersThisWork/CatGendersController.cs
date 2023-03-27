
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using CatsShop.Classes.Transformers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CatsShop.Controllers;

/// <summary>
/// ������� API ��� ������ � ������ �������
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CatGendersController : ControllerBase
{
    
    private readonly ILogger<CatGendersController> _datas;
    public CatGendersController(ILogger<CatGendersController> datas)
    {
        _datas = datas;
    }

    /// <summary>
    /// �������� ������ �����
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public CatGendersList GetList()
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders;
    }
    
    /// <summary>
    /// �������� �������� ���� �� ��� ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-ID/{id}")]
    public CatGender GetGender(int id)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromID(id);
    }
    
    /// <summary>
    /// �������� ID ���� �� ��� ��������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("By-Name/{name}")]
    public CatGender GetGender(string name)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromName(name);
    }

    /// <summary>
    /// �������� ��� ������ �� ��� ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Model/{id}")]
    public ActionResult<CatGender?> GetGenderFromModel(int id)
    {
        try
        {
            return Ok(CatModelList.GetModelsListFromDB().GetDatasFromID(id).GetGender());
        }
        catch (Exception ex)
        {
            return NotFound(null);
        }
    }

    /// <summary>
    /// �������� ��� ������ �� ID ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Cat/{id}")]
    public ActionResult<CatGender?> GetGenderFromCat(int id)
    {
        try
        {
            int modelID = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
            return Ok(CatModelList.GetModelsListFromDB().GetDatasFromID(modelID).GetGender());
        }
        catch (Exception ex)
        {
            return NotFound(null);
        }
    }



}