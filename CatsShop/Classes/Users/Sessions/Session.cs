namespace CatsShop.Classes.Users.Sessions;

/// <summary>
/// Ключ сессии пользователя
/// </summary>
public class Key
{
    
    private string key = "";

    /// <summary>
    /// Ключ сессии
    /// </summary>
    public string Session
    {
        get => key;
        set => key = value;
    }

    DateTime timeOpen = DateTime.Now;

    public DateTime TimeOpen
    {
        get => timeOpen;
        set => timeOpen = value;
    }

}