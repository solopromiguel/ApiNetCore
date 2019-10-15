using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication21.Data;
using WebApplication21.sakila;
using WebApplication21.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApplication21.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly new_schemaContext _context;
        private FileRepository fservice = new FileRepository();
        private IHostingEnvironment _hostingEnvironment;

        public FilesController(new_schemaContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        [HttpPost("[action]")]
        public IActionResult fileMethod([FromBody]  FileDto fileDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            var result = fservice.runFile2(fileDto);
            if (result=="TRUE")
            {
                return StatusCode(201);
            }

            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> File([FromForm] IList<IFormFile> File)
        {
            var files = Request.Form.Files; // now you have them

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploads, file.FileName);
                    try
                    {
                        using (var fileStream = new FileStream("D:\\AGUAS VERDES\\"+ file.FileName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    
                }
            }

            return Ok();

        }
    }
}