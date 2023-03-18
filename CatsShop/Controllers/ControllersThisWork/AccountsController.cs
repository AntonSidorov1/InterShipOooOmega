using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;


[ApiController]
[Route("cats/api/users/[controller]")]
public class AccountsController : ControllerBase
{
    
    private readonly ILogger<AccountsController> _roles;
    public AccountsController(ILogger<AccountsController> roles)
    {
        _roles = roles;
    }

    /// <summary>
    /// Регистрация клиента в системе
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPut("Registrate")]
    public bool Registrate(Account account)
    {
        return account.PutAccountToDB();
    }


    [HttpGet("LoginFromSession")]
    public string GetLoginFromSessionKey(string session) 
        => SessionsList.GetSessions().GetLoginFromSession(session);

    [HttpPut("ChangePassword")]
    public bool ChangePassword(Key key)
        => SessionsList.GetSessions().ChangePassword(key);
    
    
    [HttpDelete("DropAccount")]
    public bool DropAccount(string session)
        => SessionsList.GetSessions().DropAccount(session);

    [HttpPost("{roleID}/AddUser")]
    public bool AddUser(string session, int roleID, Account account)
        => account.AddAccountToDB(session, roleID);

}