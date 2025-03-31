using System.Diagnostics;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    [Authorize(Roles = "Practitioner")]
    public class PractitionerController : Controller
    {
        private readonly ILogger<PractitionerController> _logger;

        public PractitionerController(ILogger<PractitionerController> logger)
        {
            _logger = logger;
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
