using Microsoft.AspNetCore.Mvc;
using System;
using CatsShop.Classes.Users.Roles;
using CatsShop.Classes.Users.Sessions;
using Npgsql;

namespace CatsShop.Controllers;

[ApiController]
[Route("cats/api/users/[controller]")]
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
   [HttpGet("RolesList")]
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

    [HttpGet("{id}/RoleName")]
    public string GetRoleFromID(int id)
    {
        roles.GetRolesFromDB();
        return roles.GetRoleFromID(id).Name;
    }
    
    
    [HttpPut("RoleID")]
    public int GetRoleFromName(RoleName role)
    {
        roles.GetRolesFromDB();
        return roles.GetRoleFromName(role.Role).ID;
    }


    [HttpGet("SessionRole")]
    public Role GetRole(string session) => SessionsList.GetSessions().GetRoleFromSession(session);
}