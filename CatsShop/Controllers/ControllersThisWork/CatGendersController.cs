
using CatsShop.Classes.Cats.CatsGender.CatGender;
using Microsoft.AspNetCore.Mvc;
namespace CatsShop.Controllers;


[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatGendersController : ControllerBase
{
    
    
    
    private readonly ILogger<CatGendersController> _datas;
    public CatGendersController(ILogger<CatGendersController> datas)
    {
        _datas = datas;
    }


    [HttpGet("CatGendersList")]
    public CatGendersList GetList()
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders;
    }
    
    
    [HttpGet("{id}/CatGendersName")]
    public string GetGender(int id)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromID(id).Name;
    }
    
    [HttpGet("{name}/CatGendersID")]
    public int GetGender(string name)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromName(name).ID;
    }
    
}