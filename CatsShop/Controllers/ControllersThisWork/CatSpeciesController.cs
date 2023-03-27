using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatModel;
using CatsShop.Classes.Cats.Cats;
using CatsShop.Classes.Cats.CatSpecies;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// ������� API ��� ������ � �������� �������
/// </summary>
[ApiController]
[Route("api/[controller]")]
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
    [HttpGet()]
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
    [HttpGet("By-ID/{id}")]
    public CatSpecies? GetSpecies(int id)
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species.GetSpeciesFromID(id);
    }
    
    /// <summary>
    /// �������� ID ������ �� � ��������
    /// </summary>
    /// <returns></returns>
    [HttpGet("By-Name/{speciesName}")]
    public CatSpecies? GetSpecies(string speciesName)
    {
        CatSpeciesList species = new CatSpeciesList();
        species.GetSpeciesFromDB();
        return species.GetSpeciesFromName(speciesName);
    }

    /// <summary>
    /// �������� ������ �� ID ������ ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Model/{id}")]
    public ActionResult<CatSpecies> GetSpeciesFromModel(int id)
    {
        try
        {
            return Ok(CatModelList.GetModelsListFromDB().GetDatasFromID(id).GetSpecies());
        }
        catch
        {
            return NotFound();
        }
    }

    /// <summary>
    /// �������� ������ �� ID ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-Cat/{id}")]
    public ActionResult<CatSpecies> GetSpeciesFromCat(int id)
    {
        try
        {
            int modelID = CatsList.GetCatsListFromDB().GetCatFromID(id).ModelID;
            return GetSpeciesFromModel(modelID);
        }
        catch
        {
            return NotFound();
        }
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



}