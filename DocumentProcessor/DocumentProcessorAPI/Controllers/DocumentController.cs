using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using DocumentProcessorAPI.Services;

namespace DocumentProcessorAPI.Controllers
{
    [ApiController]
    [Route("document")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DocumentId>> GetDocuments()
        {
            var documents = DocumentService.GetDocuments();
            return Ok(documents);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DocumentData> GetDocument([FromRoute]string id)
        {
            var document = DocumentService.GetDocument(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

    }
}
