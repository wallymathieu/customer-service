using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Customers
{
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