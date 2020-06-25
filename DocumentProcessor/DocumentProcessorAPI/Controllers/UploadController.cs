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
            string text = Services.PDFService.GetTextFromPDFBytes(documentUpload.Contents);
            return Ok(text);
        }
    }
}
