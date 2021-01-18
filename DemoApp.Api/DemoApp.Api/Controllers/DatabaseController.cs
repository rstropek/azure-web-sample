using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public DatabaseController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AccessDatabase()
        {
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            var token = await new AzureServiceTokenProvider()
                .GetAccessTokenAsync("https://database.windows.net/", configuration["AADTenant"]);
            connection.AccessToken = token;
            await connection.OpenAsync();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT 42 AS ANSWER";
            return (int)await cmd.ExecuteScalarAsync();
        }
    }
}
