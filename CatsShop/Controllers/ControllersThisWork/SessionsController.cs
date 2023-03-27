using CatsShop.Classes.Users.Accounts;
using CatsShop.Classes.Users.Sessions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// Функции API для работы с сессиями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly ILogger<SessionsController> _datas;
    public SessionsController(ILogger<SessionsController> datas)
    {
        _datas = datas;
    }
    
    /// <summary>
    /// Авторизироваться по логину и паролю, и получить ключ сессии
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost("Sign-In")]
    public ActionResult<string?> Set(User account)
    {
        string session = account.SignIn();
        return (session != "null") ? this.Ok(session):this.NotFound(null);

    }

    /// <summary>
    /// Получить список сессий, по ключу сессии авторизированного пользователя
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpGet]
    [Autorization(SaveSession = true, SaveAccount = false)]
    public ActionResult<SessionsList?> GetSessions()
    {
        try
        {
            string session = SessionNow.Session;
            SessionsList sessions = SessionsList.GetSessions();
            sessions.GetSessionsFromDB(session);
            return Ok(sessions);
        }
        catch
        {
            return this.Unauthorized(null);
        }
    }

    /// <summary>
    /// Закрыть сессию
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpPost("Sign-Out")]
    public ActionResult<bool> CloseSession([FromBody] string session)
    {
        return SessionsList.GetSessions().CloseSessionInDB(session) ? Ok(true): Unauthorized(false);
    }

}