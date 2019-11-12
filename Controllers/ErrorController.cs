using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SaitynuProjektas.Controllers
{
    public class ErrorController : Controller
    {
        [Route("api/{*url}", Order = 999)]
        public IActionResult CatchAll()
        {
            return BadRequest();
        }
    }
}