using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatColorsController : ControllerBase
{
    
    
       
    private readonly ILogger<CatColorsController> _datas;
    public CatColorsController(ILogger<CatColorsController> datas)
    {
        _datas = datas;
    }


    [HttpGet("CatColorsList")]
    public CatColorsList GetList()
    {
        CatColorsList colors = new CatColorsList();
        colors.GetColorsFromDB();
        return colors;
    }
    
    
    [HttpGet("{id}/CatColorsName")]
    public string GetColor(int id)
    {
        CatColorsList colors = new CatColorsList();
        colors.GetColorsFromDB();
        return colors.GetColorFromID(id).Name;
    }
    
    [HttpGet("{name}/CatColorsID")]
    public int GetColor(string name)
    {
        CatColorsList colors = new CatColorsList();
        colors.GetColorsFromDB();
        return colors.GetColorFromName(name).ID;
    }

    [HttpPost("{color}/Add")]
    public bool AddColor(string color, string session)
        => CatColorsList.GetColors().AddColor(color, session);

    [HttpPut("/UpdateColor")]
    public bool AddColor(CatColor color, string session)
        => CatColorsList.GetColors().UpdateColor(color, session);

}