using CatsShop.Classes.Users.Accounts;
using CatsShop.Classes.Users.Sessions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatsShop.Controllers;

/// <summary>
/// Функции API для работы с пользователями
/// </summary>
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

    /// <summary>
    /// Получить логин пользователя по его ключу сессии
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpGet("LoginFromSession")]
    public string GetLoginFromSessionKey(string session) 
        => SessionsList.GetSessions().GetLoginFromSession(session);

    /// <summary>
    /// Поменять пароль пользователя
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpPut("ChangePassword")]
    public bool ChangePassword(Key key)
        => SessionsList.GetSessions().ChangePassword(key);

    /// <summary>
    /// Оптимальный метод обновления пароля - возвращаются статус-коды
    /// </summary>
    /// <param name="session"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPatch("ChangePassword")]
    public ActionResult ChangePassword(string session, [FromBody] string password)
    {
        SessionsList sessions = SessionsList.GetSessions();
        if (!sessions.HaveSession(session))
        {
            return this.StatusCode((int)HttpStatusCode.NoContent);
        }
        try
        {
            sessions.ChangePassword(session, password);
            return this.Ok();
        }
        catch
        {
            return this.StatusCode((int)HttpStatusCode.NotFound);
        }
    }

    
    /// <summary>
    /// Удалить аккаунт авторизированного пользователя
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpDelete("DropAccount")]
    public bool DropAccount(string session)
        => SessionsList.GetSessions().DropAccount(session);

    /// <summary>
    /// Добавить пользователя с определённой ролью
    /// </summary>
    /// <param name="session"></param>
    /// <param name="roleID"></param>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost("{roleID}/AddUser")]
    public bool AddUser(string session, int roleID, Account account)
        => account.AddAccountToDB(session, roleID);

}