using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SaitynuProjektas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Problem()
        {
            return StatusCode(400);
        }
    }
}