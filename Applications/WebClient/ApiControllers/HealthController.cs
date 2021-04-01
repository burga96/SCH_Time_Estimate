using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Applications.WebClient.ApiControllers
{
    [Route("[controller]")]
    public class HealthController : Controller
    {
        public HealthController()
        {
        }

        [Route("Status")]
        public IActionResult Status()
        {
            return Ok();
        }
    }
}