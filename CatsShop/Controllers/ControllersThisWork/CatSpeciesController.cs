using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.CatSpecies;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// ������� API ��� ������ � �������� �������
/// </summary>
[ApiController]
[Route("cats/api/cats/[controller]")]
public class CatSpeciesController : ControllerBase
{
    
       
    private readonly ILogger<CatSpeciesController> _datas;
    public CatSpeciesController(ILogger<CatSpeciesController> datas)
    {
        _datas = datas;
    }
    
       /// <summary>
       /// ������ �����
       /// </summary>
       /// <returns></returns>
    [HttpGet("CatSpeciesList")]
    public CatSpeciesList GetList()
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species;
    }
    
    /// <summary>
    /// �������� �������� ������ �� � ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/GetName")]
    public string GetSpecies(int id)
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species.GetSpeciesFromID(id).Name;
    }
    
    /// <summary>
    /// �������� ID ������ �� � ��������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPut("GetSpeciesID")]
    public int GetSpecies(CatSpeciesName name)
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species.GetSpeciesFromName(name).ID;
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="species"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPost("Add")]
    public bool AddSpecies(CatSpeciesName species, string session)
        => CatSpeciesList.GetSpecies().AddSpecies(species, session);

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="species"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPut("Update")]
    public bool UpdateSpecies(CatSpecies species, string session)
        => CatSpeciesList.GetSpecies().UpdateSpecies(species, session);

    /// <summary>
    /// ������� ������
    /// </summary>
    /// <param name="id"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpDelete("{id}/Delete")]
    public bool DeleteSpecies(int id, string session)
        => CatSpeciesList.GetSpecies().DeleteSpecies(id, session);

    /// <summary>
    /// �������� ������ �� ID ������ ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
	[HttpGet("CatModelSpecies/{id}")]
	public CatSpecies GetSpeciesFromModel(int id)
		=> CatModelList.GetModelsListFromDB().GetDatasFromID(id).GetSpecies();
		


}