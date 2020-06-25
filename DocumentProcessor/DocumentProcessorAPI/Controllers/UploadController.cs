using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DocumentProcessorAPI.Services;
using DocumentProcessorAPI.Storage;
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
            string text = PDFService.GetTextFromPDFBytes(documentUpload.Contents);
            string id = DocumentService.SaveDocumentDataFromText(documentUpload.Email, text);
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            else
            {
                return Ok(id);
            }
            
        }
    }
}
