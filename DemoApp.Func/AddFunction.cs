using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoApp.Func
{
    public class AddFunction
    {
        [FunctionName("Add")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Function entered");

            string aString = req.Query["a"];
            string bString = req.Query["b"];
            var a = int.Parse(aString ?? "21");
            var b = int.Parse(bString ?? "21");

            return new OkObjectResult(new
            {
                a,
                b,
                res = a + b
            });
        }
    }
}
