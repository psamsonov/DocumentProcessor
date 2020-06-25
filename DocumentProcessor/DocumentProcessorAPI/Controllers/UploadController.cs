using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentProcessorAPI.Controllers
{
    [Route("upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> UploadDocument(DocumentUpload documentUpload)
        {
            Console.WriteLine("You uploaded a file, good job! " + documentUpload.Contents.Length);
            return Ok("123");
        }
    }
}
