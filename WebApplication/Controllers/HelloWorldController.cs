using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World";
            //http://localhost:5001/api/HelloWorld
        }
    }
}
