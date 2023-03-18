using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;


[ApiController]
[Route("cats/api/users/[controller]")]
public class SessionsController
{
    private readonly ILogger<SessionsController> _datas;
    public SessionsController(ILogger<SessionsController> datas)
    {
        _datas = datas;
    }
    
    
    [HttpPost("SignIn")]
    public string Set(Account account)
    {
        return account.SignIn();

    }

    [HttpGet("SessionsList")]
    public SessionsList GetSessions(string session)
    {
        SessionsList sessions = SessionsList.GetSessions();
        sessions.GetSessionsFromDB(session);
        return sessions;
    }

    [HttpDelete("CloseSession")]
    public bool CloseSession(string session) => SessionsList.GetSessions().CloseSessionInDB(session);

}