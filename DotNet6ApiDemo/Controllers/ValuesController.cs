﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6ApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase

    {
        //Route: işlemin hangi tarafa gönderilceğini gösteren yapı.

        [Route("[action")]
        [HttpGet]
        public string GetName()

        {

            return "Hello World!";
        }

        [Route("GetFullName")]
        [HttpGet]

        public string GetFullName()

        {

            return "Md Rasel";

        }

    }
}
