using BitCoinAnalyzer.API.DTOModels;
using BitCoinAnalyzer.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BitCoinAnalyzer.API.Controllers
{
    
    public class AccountController : BaseController
    {
        ILogger<AccountController> _logger;
        readonly IUserService _userService;
        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            AuthenticateResponse response = _userService.Authenticate(model);

            if (response == null)
            {
                _logger.LogWarning($"{model.Username} kullanıcı adı ile kullanıcı girişi yapılamadı");
                return BadRequest(new { message = "Kullanıcı adı ya da parola yanlış" });
            }

            _logger.LogWarning($"{model.Username} kullanıcı adlı kullanıcı giriş yaptı");
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                if (_userService.IsUsernameTaken(model.Username))
                {
                    return BadRequest("Kullanıcı adı zaten alınmış");
                }
                _userService.RegisterUser(model);
                return StatusCode(201, "Kullanıcı başarı ile oluşturuldu");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
                throw;
            }
        }

        [HttpGet("check-username/{username}")]
        public IActionResult CheckUserName(string username)
        {
            var taken = _userService.IsUsernameTaken(username);

            if (taken)
            {
                return StatusCode(409, "Kullanıcı adı zaten alınmış");
            }
            else
            {
                return Ok("Kullanıcı adı kullanılabilir.");
            }
        }


    }
}
