namespace CatsShop;

public class Role
{
    public Role() : this(0, "Role")
    {
        
    }

    public Role(Role role)
    {
        ID = role.ID;
        Name = role.Name;
    }

    public Role Copy()
    {
        return new Role(this);
    }
    
    public Role(int id, string name)
    {
    ID = id;
    Name = name;
    }

    private int id = 0;

    /// <summary>
    /// ID роли
    /// </summary>
    public int ID
    {
        get => id;
        set => id = value;
    }

    private string name = "";

    /// <summary>
    /// Имя роли
    /// </summary>
    public string Name
    {
        get => name;
        set => name = value;
    }
}