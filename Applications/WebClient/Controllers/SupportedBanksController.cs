using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Applications.WebClient.Controllers
{
    public class SupportedBanksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
