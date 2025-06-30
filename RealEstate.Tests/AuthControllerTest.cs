using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public  class AuthControllerTest:Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> GetSomething()
        {
            return "You are authorized user";
        }

        [HttpGet("{someValue:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult<string> GetSomething(int someValue)
        {
            return "You are authorized user, with Role of Admin";
        }
    }
}
