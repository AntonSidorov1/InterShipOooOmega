namespace CatsShop
{
    public class UserWithRole : User
    {
        Role role = new Role();

        public Role Role { get { return role; } set { role = value; } }


        public string RoleRus { get { return role.RoleRus; } set { role.RoleRus = value; } }

        public string RoleEng
        {
            get
            {
                return role.RoleEng;
            }
            set
            {
                role.RoleEng = value;
            }
        }

    }
}
