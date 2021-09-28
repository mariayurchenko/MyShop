using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SB.WebShared.DynamicsAuthentication;

namespace HappyBirthdayWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public IAuthenticationService _authenticationService;

        //private Logger _logger;

        public HomeController(IAuthenticationService authentication)
        {
            _authenticationService = authentication;
            // var _logger = LogManager.GetLogger("fileLogger");
        }

        [HttpGet]
        public string Get()
        {
            return ("Hello!");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string contactId)
        {
            try
            {
                if (contactId == null)
                {
                    return BadRequest($"{nameof(contactId)} can't be null");
                }
                if (Guid.TryParse(contactId, out var id))
                {
                    if (id == Guid.Empty)
                    {
                        throw new Exception($"{nameof(id)} is empty Guid");
                    }
                }
                else
                {
                    throw new Exception($"{nameof(contactId)} is not parse to Guid");
                }

                var token = await _authenticationService.GetToken();
                var serviceUrl = _authenticationService.GetServiceUri();

                var json = JsonCreatorService.FormStringContent(contactId, "SendBirthdayEmail");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await httpClient.PostAsync(serviceUrl, json);

                    // _logger.Info(response.RequestMessage.ToString());
                }

                return Ok();
            }

            catch (Exception e)
            {
                // _logger.Error(e, e.Message);

                return StatusCode(500);
            }
        }
    }
}