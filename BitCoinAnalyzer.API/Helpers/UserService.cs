using BitCoinAnalyzer.API.DTOModels;
using BitCoinAnalyzer.Entity;
using BitCoinAnalyzer.Service.Abstract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BitCoinAnalyzer.API.Helpers
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        bool IsUsernameTaken(string username);
        void RegisterUser(RegisterModel model);
    }
    public class UserService : IUserService
    {
        IDatabaseService _db;
        readonly IOptions<AppSettings> _appSettings;
        public UserService(IDatabaseService db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            User user = _db.User.Get(user => user.Username == model.Username && user.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public bool IsUsernameTaken(string username)
        {
            var user = _db.User.Get(g => g.Username == username);
            return user != null;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterModel model)
        {
            _db.User.Add(new User()
            {
                Username = model.Username,
                Password = model.Password
            });

            _db.SaveChanges();
        }
    }
}
