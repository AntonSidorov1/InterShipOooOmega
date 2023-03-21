using CatsShop.Classes.Users.Accounts;
using CatsShop.Classes.Users.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// ������� API ��� ������ � ��������
/// </summary>
[ApiController]
[Route("cats/api/users/[controller]")]
public class SessionsController
{
    private readonly ILogger<SessionsController> _datas;
    public SessionsController(ILogger<SessionsController> datas)
    {
        _datas = datas;
    }
    
    /// <summary>
    /// ���������������� �� ������ � ������, � �������� ���� ������
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost("SignIn")]
    public string Set(Account account)
    {
        return account.SignIn();

    }

    /// <summary>
    /// �������� ������ ������, �� ����� ������ ����������������� ������������
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpGet("SessionsList")]
    public SessionsList GetSessions(string session)
    {
        SessionsList sessions = SessionsList.GetSessions();
        sessions.GetSessionsFromDB(session);
        return sessions;
    }

    /// <summary>
    /// ������� ������
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    [HttpDelete("CloseSession")]
    public bool CloseSession(string session) => SessionsList.GetSessions().CloseSessionInDB(session);

}