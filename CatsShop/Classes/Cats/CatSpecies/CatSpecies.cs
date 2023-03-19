namespace CatsShop.Classes.Cats.CatSpecies;

/// <summary>
/// Порода котиков
/// </summary>
public class CatSpecies
{
    private int id = 0;

    /// <summary>
    /// ID породы
    /// </summary>
    public int ID
    {
        get => id;
        set => id = value;
    }

    private string name = "";

    /// <summary>
    /// Название породы
    /// </summary>
    public string Name
    {
        get => name;
        set => name = value;
    }
}