using System.ComponentModel.DataAnnotations;

namespace BitCoinAnalyzer.API.DTOModels
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
