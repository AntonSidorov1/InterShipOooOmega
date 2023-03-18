namespace CatsShop.Classes.Cats.CatColor;

/// <summary>
/// Цвет котика
/// </summary>
public class CatColor
{
    private int id = 0;

    /// <summary>
    /// ID цвета
    /// </summary>
    public int ID
    {
        get => id;
        set => id = value;
    }

    private string name = "";

    /// <summary>
    /// Название цвета
    /// </summary>
    public string Name
    {
        get => name;
        set => name = value;
    }
}