
using CatsShop.Classes.Cats.CatModel;
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
    
    [HttpPut("CatGendersID")]
    public int GetGender(CatGenderName name)
    {
        CatGendersList genders = new CatGendersList();
        genders.GetGendersFromDB();
        return genders.GetGenderFromName(name.CatGender).ID;
    }
	
		
	[HttpGet("CatModelGender/{id}")]
	public CatGender GetGenderFromModel(int id)
		=> CatModelList.GetModelsListFromDB().GetDatasFromID(id).GetGender();
		

    
}