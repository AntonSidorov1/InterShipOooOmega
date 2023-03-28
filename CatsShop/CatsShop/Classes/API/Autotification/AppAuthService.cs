using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatsShop.Classes.API.Autotification
{
    public class AppAuthService : IAppAuthService
    {
        List<UserWithRole> users = new List<UserWithRole>();

        public List<UserWithRole> Users { get { return users; } set => users = value; }
        public Token Authenticate(User user)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                if(!users.Any(account => account.Login == user.Login))
            {
                throw new Exception("Данный логин не существует");
            }
            UserWithRole userWithRole = Users.Find(account => account.Login == user.Login) ??new UserWithRole();
            if(userWithRole.Password != user.Password)
            {
                throw new Exception("Неверный пароль");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration.Build()["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userWithRole.Login),
                    new Claim(ClaimTypes.Role, userWithRole.RoleEng)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Token { AuthToken = tokenHandler.WriteToken(token) };
        }
    }
}
