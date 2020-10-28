using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;
using NegociationServer;
using Microsoft.AspNetCore.SignalR;

namespace NegotiationServer.Controllers
{
    [ApiController]
    [Route("/api")]
    public class SignalRController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SignalRController(IConfiguration configuration)
        {
            var connectionString = configuration["Azure:SignalR:ConnectionString"];
            _serviceManager = new ServiceManagerBuilder()
                .WithOptions(o => o.ConnectionString = connectionString)
                .Build();
        }

        [HttpGet]
        public string Index()
        {
            var path = string.Format("{0}://{1}/api/{{hub}}/negotiate", HttpContext.Request.Scheme, HttpContext.Request.Host);
            return $"Make a POST request to \"{path}\" to get/negotiate signalR info! Optionally passing a user in the querystring";
        }

        [HttpPost("{hub}/negotiate")]
        public ActionResult GetSignalRInfo(string hub, string user)
        {
            //if (string.IsNullOrEmpty(user))
            //{
            //    return BadRequest("User ID is null or empty.");
            //}

            return new JsonResult(new Dictionary<string, string>()
            {
                { "url", _serviceManager.GetClientEndpoint(hub) },
                { "accessToken", _serviceManager.GenerateClientAccessToken(hub, user) }
            });
        }

        [HttpPost("{hub}/messages")]
        public async Task<ActionResult> SendMessage(string hub, [FromBody] Message msg)
        {
            IServiceHubContext _hubContext = await _serviceManager.CreateHubContextAsync(hub).ConfigureAwait(false);

            // function target on the client
            string method = "newMessage";

            await _hubContext.Clients.All.SendCoreAsync(method, new object [] { msg } );

            //_hubContext.Clients.Client(connectionId).SendCoreAsync(method, args, cancellationToken);
            //_hubContext.Groups.AddToGroupAsync
            //_hubContext.Clients.Users.SendMessage
            //_hubContext.Clients.Groups("dsds").SendCoreAsync
            //_hubContext.

            return new JsonResult("Message sent!");
        }


    }
}
