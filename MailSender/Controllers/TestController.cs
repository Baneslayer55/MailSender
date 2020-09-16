using System.Threading.Tasks;
using MailSender.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<MailConfig>> Get()
        {
            return MailConfig.GetConfig("mailconfig.json");
        }
    }
}
