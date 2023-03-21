namespace CatsShop.Classes.Cats.CatsGender.CatGender;

/// <summary>
/// Пол котика
/// </summary>
public class CatGenderName
{
    private string catGender = "";

    /// <summary>
    /// Название пола
    /// </summary>
    public string CatGender
    {
        get => catGender;
        set => catGender = value;
    }
}