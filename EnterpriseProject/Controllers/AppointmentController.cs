using System.Diagnostics;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }
    }
}
