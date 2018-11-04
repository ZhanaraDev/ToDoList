using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Trace.WriteLine("ReallYYYY?");
            return new string[] { "value1", "value2" };
        }
    }
}
