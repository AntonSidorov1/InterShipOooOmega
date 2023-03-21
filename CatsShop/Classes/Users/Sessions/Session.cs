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

    private string password = "";

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password
    {
        get => password;
        set => password = value;
    }

}