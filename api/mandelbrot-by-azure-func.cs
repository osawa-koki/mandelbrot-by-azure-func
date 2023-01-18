using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace mandelbrot
{
  public static class mandelbrot_by_azure_func
  {
    [FunctionName("mandelbrot_by_azure_func")]
    public static async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
      ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");

      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      dynamic data = JsonConvert.DeserializeObject(requestBody);

      int width;
      int height;
      int maxIter;
      double x0;
      double y0;
      double x1;
      double y1;

      // パラメータを取得する。
      if (int.TryParse(req.Query["width"], out width) == false) width = 500;
      if (int.TryParse(req.Query["height"], out height) == false) height = 500;
      if (int.TryParse(req.Query["maxIter"], out maxIter) == false) maxIter = 1000;
      if (double.TryParse(req.Query["x0"], out x0) == false) x0 = -2.0;
      if (double.TryParse(req.Query["y0"], out y0) == false) y0 = -2.0;
      if (double.TryParse(req.Query["x1"], out x1) == false) x1 = 2.0;
      if (double.TryParse(req.Query["y1"], out y1) == false) y1 = 2.0;

      var responseMessage = new
      {
        width = width,
        height = height,
        maxIter = maxIter,
        x0 = x0,
        y0 = y0,
        x1 = x1,
        y1 = y1,
      };

      return new OkObjectResult(responseMessage);

    }
  }
}
