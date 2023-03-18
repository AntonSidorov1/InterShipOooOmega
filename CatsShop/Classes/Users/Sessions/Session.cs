namespace CatsShop;

public class Key
{
    
    private string key = "";

    public string Session
    {
        get => key;
        set => key = value;
    }

    private string password = "";

    public string Password
    {
        get => password;
        set => password = value;
    }

}