using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.Models;
using Microsoft.AspNetCore.Http;
using System;

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
            return Ok(Enumerable.Range(1, 5).Select(index => new DocumentId
            {
                Id = index.ToString()
            })
            .ToArray());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DocumentData> GetDocument()
        {
            return Ok(new DocumentData());
        }

    }
}
