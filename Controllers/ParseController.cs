using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FileParse.Assets;
using FileParse.Assets.Operation;
using FileParse.Assets.Task;
using FileParse.Model;
using FileParse.ParseDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FileParse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController : ControllerBase
    {
        private readonly IConfiguration Config;

        public ParseController(IConfiguration config)
        {
            Config = config;
        }

        // GET api/parse
        [HttpGet]
        public string Get()
        {
            return "Привет";
        }

        // POST: api/Parse
        [HttpPost]
        public IActionResult Post(ParseRequest parseRequest)
        {
            ParseProcess process = new ParseProcess();

            process.SetParm(Config);

            process.Start(parseRequest.folderPath);

            if (!process.IsSuccess)
            {
                return StatusCode(500, process.Error);
            }

            return StatusCode(200);
        }        
    }
}
