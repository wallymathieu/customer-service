using Microsoft.AspNetCore.Mvc;

namespace Customers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}