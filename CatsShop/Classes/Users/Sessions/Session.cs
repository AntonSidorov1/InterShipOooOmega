namespace CatsShop.Classes.Users.Sessions;

/// <summary>
/// ���� ������ ������������
/// </summary>
public class Key
{
    
    private string key = "";

    /// <summary>
    /// ���� ������
    /// </summary>
    public string Session
    {
        get => key;
        set => key = value;
    }

    private string password = "";

    /// <summary>
    /// ������
    /// </summary>
    public string Password
    {
        get => password;
        set => password = value;
    }

}