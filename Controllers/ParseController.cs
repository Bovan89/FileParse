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

        // POST: api/Parse
        [HttpPost]
        public IActionResult Post(ParseRequest parseRequest)
        {
            ////string inFolder = _config.GetValue<string>("InFolder");
            ////string proccessFolder = _config.GetValue<string>("ProcessFolder");
            ////string errorFolder = _config.GetValue<string>("ErrorFolder");
            ////string filePattern = _config.GetValue<string>("FilePattern");
            ////string connectionString = _config.GetValue<string>("connectionString");

            ////string sourceFolder = Path.Combine(parse.folderPath, inFolder);
            ////string destFolder = Path.Combine(parse.folderPath, proccessFolder);
            ////string errFolder = Path.Combine(parse.folderPath, errorFolder);

            /*using (ParseDb db = new ParseDb(connectionString))
            {
                Good g = db.Goods.FirstOrDefault(f => f.Id == 1);
            }*/

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
