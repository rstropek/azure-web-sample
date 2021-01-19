using Azure.Core;
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
using System.Net.Http.Json;
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

        record ApiResult(int A, int B, int Res);

        public async Task<IActionResult> AddNumbers()
        {
            var token = await GetAccessToken("https://vault.azure.net/.default");

            var kvClient = new SecretClient(new Uri($"https://{configuration["KeyVault"]}.vault.azure.net/"),
                new DefaultAzureCredential(new DefaultAzureCredentialOptions { VisualStudioTenantId = configuration["AADTenant"] }));
            var secret = kvClient.GetSecret("apiKey").Value;

            var result = await client.GetFromJsonAsync<ApiResult>("https://"+ configuration["FunctionUrl"] + ".azurewebsites.net/api/Add?a=1&b=2&code=" + secret.Value);
            return Ok(result);
        }

        async Task<string> GetAccessToken(string scope)
        {
            var tokenRequestResult = await new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                VisualStudioTenantId = configuration["AADTenant"]
            }).GetTokenAsync(
                new TokenRequestContext(new[] { scope }));
            return tokenRequestResult.Token;
        }
    }
}
