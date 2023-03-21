namespace CatsShop.Classes.Cats.CatSpecies;

/// <summary>
/// Порода котика
/// </summary>
public class CatSpeciesName
{
    
    private string species = "";

    /// <summary>
    /// Название породы
    /// </summary>
    public string Species
    {
        get => species;
        set => species = value;
    }
}