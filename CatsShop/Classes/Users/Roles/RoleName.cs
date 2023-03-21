namespace CatsShop.Classes.Users.Roles;

/// <summary>
/// Роль пользователя в системе
/// </summary>
public class RoleName
{
    private string role = "";

    /// <summary>
    /// Название роли
    /// </summary>
    public string Role
    {
        get => role;
        set => role = value;
    }
}