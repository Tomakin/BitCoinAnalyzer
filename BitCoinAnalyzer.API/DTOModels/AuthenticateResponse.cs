using BitCoinAnalyzer.Entity;

namespace BitCoinAnalyzer.API.DTOModels
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.ID;
            FirstName = user?.FirstName ?? "";
            LastName = user?.LastName ?? "";
            Username = user?.Username ?? "";
            Token = token;
        }
        public AuthenticateResponse()
        {
                
        }
    }
}
