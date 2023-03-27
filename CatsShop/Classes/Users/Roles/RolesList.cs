using CatsShop.Classes.DataBaseConnection;
using Npgsql;

namespace CatsShop.Classes.Users.Roles;

/// <summary>
/// ������ ����� ������������ � �������
/// </summary>
public class RolesList : List<Role>
{
    public static RolesList GetRoles() => new RolesList();

    public static RolesList GetListFromDB()
    {
        RolesList roles = GetRoles();
        roles.GetRolesFromDB();
        return roles;
    }


    /// <summary>
    /// �������� ������ ����� �� ���� ������
    /// </summary>
    public void GetRolesFromDB()
    {
        
        Clear();
        DataBaseDatas datas = NowConnectionString.ConnectionDatas;
        NpgsqlConnection connection = datas.Connection;
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("Select * From \"Role\"", connection);

        
        NpgsqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Role role = new Role();
                    role.ID = reader.GetInt32(reader.GetOrdinal("RoleID"));
                    role.Name = reader.GetString(reader.GetOrdinal("RoleName"));
                    Add(role);
                }
            }
        }
        catch (Exception e)
        {
            
        }
        reader.Close();
        
        connection.Close();
    }

    /// <summary>
    /// �������� ���� �� � ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Role GetRoleFromID(int id)
    {
        return Find(p => p.ID == id);
    }

    public RolesList GetRolesList() => this;
    
    /// <summary>
    /// �������� ���� �� � ��������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Role? GetRoleFromName(string name)
    {
        return GetRolesList().FirstOrDefault(p => p.Name.ToLower().Replace('_', ' ').Replace('-', ' ').Trim() == name.ToLower().Replace('_', ' ').Replace('-', ' ').Trim());
    }
}