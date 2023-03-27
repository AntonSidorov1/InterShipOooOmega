using Microsoft.AspNetCore.Mvc;
using System;
using CatsShop.Classes.Users.Roles;
using CatsShop.Classes.Users.Sessions;
using Npgsql;
using System.Net;

namespace CatsShop.Controllers;

/// <summary>
/// Функции API Для работы с ролями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{

    private static RolesList roles = new RolesList();
    
    

    private readonly ILogger<RolesController> _roles;
    public RolesController(ILogger<RolesController> roles)
    {
        _roles = roles;
    }


        
    /// <summary>
    /// Получить список ролей
    /// </summary>
    /// <returns></returns>
   [HttpGet]
    public IEnumerable<Role> Get()
    {
        roles.GetRolesFromDB();
        //GetRolesFromDB();
        return Enumerable.Range(1, roles.Count()).Select(index => new Role
            (
            roles[index - 1]
            ))
            .ToArray();
    }

    /// <summary>
    /// Получить названия роли по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("By-ID/{id}")]
    public ActionResult<Role?> GetRoleFromID(int id)
    {
        try
        {
            roles.GetRolesFromDB();
            Role? role = roles.FirstOrDefault(role => role.ID == id);
            if (role == null)
                throw new ArgumentNullException("Роли с данным ID не существует");
            return Ok(role);
        }
        catch
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }
    
    /// <summary>
    /// Получить ID роли по её названию
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [HttpGet("By-Name/{name}")]
    public Role? GetRoleFromName(string name)
    {
        roles.GetRolesFromDB();
        return roles.GetRoleFromName(name);
    }

    ///// <summary>
    ///// Получить роль авторизированного пользователя, по его ключу сессии
    ///// </summary>
    ///// <param name="session"></param>
    ///// <returns></returns>
    //[HttpGet("SessionRole")]
    //public Role GetRole(string session) => SessionsList.GetSessions().GetRoleFromSession(session);
}