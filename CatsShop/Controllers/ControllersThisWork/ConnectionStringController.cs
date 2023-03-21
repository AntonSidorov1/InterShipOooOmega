using CatsShop.Classes.DataBaseConnection;
using Microsoft.AspNetCore.Mvc;

namespace CatsShop.Controllers;

/// <summary>
/// Функции API для строки подключения к базе данных
/// </summary>
[ApiController]
[Route("cats/api/connection/[controller]")]
public class ConnectionStringController : ControllerBase
{
    private readonly ILogger<ConnectionStringController> _datas;
    public ConnectionStringController(ILogger<ConnectionStringController> datas)
    {
        _datas = datas;
    }
    
    /// <summary>
    /// Получение строки подключения
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetConnectionString")]
    public DataBaseConnectionText Get()
    {
        //NowConnectionString.ConnectionDatas.FromSettings();
        return NowConnectionString.ConnectionDatas.Copy();
    }
   
    /// <summary>
    /// Изменения строки подключения
    /// </summary>
    /// <param name="connectionText"></param>
    /// <returns></returns>
    [HttpPost("SetConnectionString")]
    public bool Set(DataBaseConnectionText connectionText)
    {
        try
        {
            NowConnectionString.ConnectionDatas = new DataBaseDatas(connectionText);
            //NowConnectionString.ConnectionDatas.SaveSettings();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
       
    }

}