using BitCoinAnalyzer.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitCoinAnalyzer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BitCoinController : BaseController
    {
        BitCoinService _bitcoinService;

        public BitCoinController(BitCoinService bitcoinService)
        {
            _bitcoinService = bitcoinService;
        }

        [Route("daily")]
        [HttpGet]
        public IActionResult GetBitCoinDaily()
        {
            return Ok(_bitcoinService.GetBitCoinDaily());
        }

        [HttpGet]
        [Route("weekly")]
        public IActionResult GetBitCoinWeekly()
        {
            return Ok(_bitcoinService.GetBitCoinWeekly());
        }

        [HttpGet]
        [Route("monthly")]
        public IActionResult GetBitCoinMonthly()
        {
            return Ok(_bitcoinService.GetBitCoinMonthly());
        }
    }
}
