using System.Diagnostics;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class PractitionerController : Controller
    {
        private readonly ILogger<PractitionerController> _logger;

        public PractitionerController(ILogger<PractitionerController> logger)
        {
            _logger = logger;
        }
    }
}
