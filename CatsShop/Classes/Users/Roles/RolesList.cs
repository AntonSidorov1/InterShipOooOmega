
using System;
using Npgsql;

namespace CatsShop;

public class RolesList : List<Role>
{
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

    public Role GetRoleFromID(int id)
    {
        return Find(p => p.ID == id);
    }
    
    public Role GetRoleFromName(string name)
    {
        return Find(p => p.Name.ToLower() == name.ToLower());
    }
}