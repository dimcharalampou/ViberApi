using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Viber.Bot;

namespace ViberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IViberBotClient _viberBotClient;

        private string _authToken;
        private string _webhookUrl;
        private string _adminId;

        public ClientsController(IConfiguration configuration)
        {
            _authToken = configuration["authToken"];
            _webhookUrl = configuration["webhookUrl"];
            _adminId = configuration["adminId"];
            var proxy = new WebProxy
            {
                Address = new Uri("http://10.72.180.27:8080"),
                BypassProxyOnLocal = true,
                UseDefaultCredentials = true
            };

            _viberBotClient = new ViberBotClient(_authToken, proxy);
        }


        [HttpPost]
        [Route("PostMessage")]
        public async Task<ActionResult> SendMessage([FromBody] TextMessage Msg)
        {
            try
            {
                var result = await _viberBotClient.SendTextMessageAsync(Msg);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
    }
}
