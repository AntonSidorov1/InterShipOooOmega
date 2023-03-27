using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// ������� API ��� ������ � ������� �������
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CatColorsController : ControllerBase
{
    
    
       
    private readonly ILogger<CatColorsController> _datas;
    public CatColorsController(ILogger<CatColorsController> datas)
    {
        _datas = datas;
    }


    /// <summary>
    /// �������� ������ ������
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public CatColorsList GetList()
    {
        CatColorsList colors = new CatColorsList();
        colors.GetColorsFromDB();
        return colors;
    }
    
    /// <summary>
    /// �������� �������� ����� �� ��� ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-ID/{id}")]
    public CatColor GetColor(int id)
    {
        CatColorsList colors = new CatColorsList();
        colors.GetColorsFromDB();
        return colors.GetColorFromID(id);
    }
    
    /// <summary>
    /// �������� ID ����� �� ��� ��������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("By-Name/{name}")]
    public CatColor GetColor(string name)
    {
        CatColorsList colors = new CatColorsList();
        colors.GetColorsFromDB();
        return colors.GetColorFromName(name);
    }

    /// <summary>
    /// �������� ���� �� ID ������ ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Model/{id}")]
    public ActionResult<CatColor> GetColorFromModel(int id)
    {
        try
        {
            return Ok(CatModelList.GetModelsListFromDB().GetDatasFromID(id).GetColor());
        }
        catch
        {
            return NotFound();
        }
    }

    /// <summary>
    /// �������� ���� �� ID ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Cat/{id}")]
    public ActionResult<CatColor> GetColorFromCat(int id)
    {
        try
        {
            int modelID = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
            return GetColorFromModel(modelID);
        }
        catch
        {
            return NotFound();
        }
    }



    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="color"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPost("Add")]
    public bool AddColor(CatColorName color, string session)
        => CatColorsList.GetColors().AddColor(color.Color, session);

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="color"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPut("Update")]
    public bool UpdateColor(CatColor color, string session)
        => CatColorsList.GetColors().UpdateColor(color, session);

    /// <summary>
    /// ������� ����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpDelete("{id}/Delete")]
    public bool DeleteColor(int id, string session)
        => CatColorsList.GetColors().DeleteColor(id, session);
	

}