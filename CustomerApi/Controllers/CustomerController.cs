using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Controllers
{
    [Route("Customer/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private CustomerDbContext db;

        public CustomerController(CustomerDbContext db, ILogger<CustomerController> logger)
        {
            this.db = db;
            this.logger = logger;

        }


        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = db.Customer.ToList();
                return customers;

            }
            catch (Exception e)
            {
                logger.LogError("get customer" + e.ToString());
                return null;
            }

        }


        [HttpPost]
        public int AddCustomer([FromBody] Customer customer)
        {
            try
            {
                db.Add(customer);
                db.SaveChanges();
                return customer.Id;
            }
            catch (Exception)
            {
                return 0;
            }

        }


    }
}
