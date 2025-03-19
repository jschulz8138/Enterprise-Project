using System.Diagnostics;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
    }
}
