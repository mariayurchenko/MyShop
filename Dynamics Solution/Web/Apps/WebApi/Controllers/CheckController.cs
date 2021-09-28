using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SB.SharedModels.CreateCheck;
using SB.WebShared.DynamicsAuthentication;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckController : ControllerBase
    {
        public IAuthenticationService _authenticationService;

        //private Logger _logger;

        public CheckController(IAuthenticationService authentication)
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
        public async Task<IActionResult> Post([FromBody] Check check)
        {
            try
            {
                if (check.Shop == null)
                {
                    return BadRequest("Shop can't be null");
                }
                if (check.Client == null)
                {
                    return BadRequest("Client can't be null");
                }
                if (check.Products == null)
                {
                    return BadRequest("Should specify at least one product");
                }

                var token = await _authenticationService.GetToken();
                var serviceUrl = _authenticationService.GetServiceUri();

                var json = JsonCreatorService.FormStringContent(check, "CreateCheck");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await httpClient.PostAsync(serviceUrl, json);

                   // _logger.Info(response.RequestMessage.ToString());
                }

                return Ok();
            }

            catch(Exception e)
            {
               // _logger.Error(e, e.Message);

                return StatusCode(500);
            }
        }
    }
}