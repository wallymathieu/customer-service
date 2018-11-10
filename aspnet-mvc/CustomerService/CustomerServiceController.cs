using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Customers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content( @"
<!DOCTYPE html>
<html>
<head>
    <title>Customer service</title>
    <meta charset=""utf-8"" />
</head>
<body>
Copy of 
<a href=""http://www.galasoft.ch/labs/Customers/CustomerService.svc"">galasoft CustomerService</a>
used in order to demonstrate mvvm.
You can mock implementation of the api here:
  <a href=""/CustomerService.svc"">Customer service</a>.
</body>
</html>
", "text/html; charset=utf-8");
        }
    }
    [Route("CustomerService.svc")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerServiceController(ICustomerService customerService) => _customerService = customerService;

        [HttpGet("GetAllCustomers")]
        public ActionResult<ArrayOfCustomer> Get() =>
            _customerService.GetAllCustomers();
        [HttpPost("SaveCustomer")]
        public bool Post([FromBody]Customer body) =>
            _customerService.SaveCustomer(body);
    }
}