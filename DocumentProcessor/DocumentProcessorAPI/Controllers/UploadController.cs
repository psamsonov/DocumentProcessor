using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DocumentProcessorAPI.Services;
using DocumentProcessorAPI.Storage;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentProcessorAPI.Controllers
{
    [Route("upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public ActionResult<DocumentId> UploadDocument(DocumentUpload documentUpload)
        {
            int length = 0;
            if (String.IsNullOrEmpty(documentUpload.Text))
            {
                documentUpload.Text = PDFService.GetTextFromPDFBytes(documentUpload.Contents);
                length = documentUpload.Contents.Length;
            }
            else
            {
                length = documentUpload.Text.Length;
            }

            string id = DocumentService.SaveDocumentDataFromText(documentUpload.Email, length, documentUpload.Text);
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            else
            {
                return Ok(new DocumentId { Id = id });
            }


        }
    }
}
