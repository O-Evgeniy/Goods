using GoodsManagerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace GoodsManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Redirect()
        {
            return Redirect("~/Home/Index");
        }

        public async  Task Privacy()
        {
            Response.ContentType = "text/html;charset=utf-8";
            StringBuilder sb = new StringBuilder("<h2>Request Headers</h2><table>");
            foreach(var header in Request.Headers)
            {
                sb.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
            }
            sb.Append("</table");
            await Response.WriteAsync(sb.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}