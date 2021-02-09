using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace DemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DnsController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public DnsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        record ApiResult(int A, int B, int Res);

        public IActionResult GetIpAddresses()
        {
            var addressesString = new StringBuilder();
            var addresses = Dns.GetHostAddresses($"{configuration["KeyVault"]}.vault.azure.net");
            return Ok(string.Join(", ", addresses.ToArray().Select(x => x.ToString())));
        }
    }
}
