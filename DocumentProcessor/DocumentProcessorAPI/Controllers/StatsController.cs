using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DocumentProcessorAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentProcessorAPI.Controllers
{
    [Route("stats")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<DocumentStats>> GetDocumentStats()
        {
            return Ok(DocumentService.GetStats());
        }
    }
}
