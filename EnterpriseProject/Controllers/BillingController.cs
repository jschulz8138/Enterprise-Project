using System.Diagnostics;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    [Authorize(Roles = "Billing")]
    public class BillingController : Controller
    {
        private readonly ILogger<BillingController> _logger;

        public BillingController(ILogger<BillingController> logger)
        {
            _logger = logger;
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
