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

    DateTime timeOpen = DateTime.Now;

    public DateTime TimeOpen
    {
        get => timeOpen;
        set => timeOpen = value;
    }

}