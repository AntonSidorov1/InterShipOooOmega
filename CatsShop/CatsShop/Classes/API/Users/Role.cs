namespace CatsShop
{
    public class Role
    {

        public Role() { }

        public static Role GetRoleRus(string role) => new Role { RoleRus = role };
        public static Role GetRoleEng(string role) => new Role { RoleEng = role };

        string role = "";

        public string RoleRus { get { return role; } set { role = value.Replace('-', ' ').Replace('_', ' ').Trim(); } }

        public string RoleEng
        {
            get
            {
                if (StringNormalize.Normalize(RoleRus) == StringNormalize.Normalize("Админ"))
                    return "Admin";
                else
                    return "Client";
            }
            set
            {
                if (StringNormalize.Normalize(value) == StringNormalize.Normalize("Admin"))
                    RoleRus = "Админ";
                else
                    RoleRus = "Клиент";
            }
        }

    }
}
