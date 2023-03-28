namespace CatsShop
{
    public interface IAppAuthService
    {
        Token Authenticate(User user);
        List<UserWithRole> Users { get; set; }
    }
}
