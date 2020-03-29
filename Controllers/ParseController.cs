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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FileParse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ParseController(IConfiguration config)
        {
            _config = config;
        }        

        // POST: api/Parse
        [HttpPost]
        public IActionResult Post(ParseRequest parse)
        {
            string inFolder = _config.GetValue<string>("InFolder");
            string proccessFolder = _config.GetValue<string>("ProcessFolder");
            string errorFolder = _config.GetValue<string>("ErrorFolder");
            string filePattern = _config.GetValue<string>("FilePattern");

            string sourceFolder = Path.Combine(parse.folderPath, inFolder);
            string destFolder = Path.Combine(parse.folderPath, proccessFolder);

            //
            ITask task = new TransferTask(sourceFolder, destFolder, filePattern);
            task.Run();
            //

            return StatusCode(500, task.ErrorMessage);
        }        
    }
}
