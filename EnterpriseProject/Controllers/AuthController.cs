using System.Diagnostics;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }
    }
}
