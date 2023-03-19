using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatSpecies;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;


[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatSpeciesController : ControllerBase
{
    
       
    private readonly ILogger<CatSpeciesController> _datas;
    public CatSpeciesController(ILogger<CatSpeciesController> datas)
    {
        _datas = datas;
    }
    
       
    [HttpGet("CatSpeciesList")]
    public CatSpeciesList GetList()
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species;
    }
    
    
    [HttpGet("{id}/GetName")]
    public string GetSpecies(int id)
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species.GetSpeciesFromID(id).Name;
    }
    
    [HttpPut("GetSpeciesID")]
    public int GetSpecies(CatSpeciesName name)
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species.GetSpeciesFromName(name).ID;
    }

    [HttpPost("Add")]
    public bool AddSpecies(CatSpeciesName species, string session)
        => CatSpeciesList.GetSpecies().AddSpecies(species, session);

    [HttpPut("Update")]
    public bool UpdateSpecies(CatSpecies species, string session)
        => CatSpeciesList.GetSpecies().UpdateSpecies(species, session);

    [HttpDelete("{id}/Delete")]
    public bool DeleteSpecies(int id, string session)
        => CatSpeciesList.GetSpecies().DeleteSpecies(id, session);


}