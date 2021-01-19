using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;

        public AddController(IHttpClientFactory factory, IConfiguration configuration)
        {
            client = factory.CreateClient();
            this.configuration = configuration;
        }

        public async Task<IActionResult> AddNumbers()
        {
            var client = new SecretClient(new Uri($"https://{configuration["KeyVault"]}.vault.azure.net/"), new DefaultAzureCredential());
            var secret = client.GetSecret("apiKey");
            await Task.Delay(1);
            return Ok(secret.Value);
        }
    }
}
