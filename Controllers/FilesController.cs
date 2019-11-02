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
using CloudinaryDotNet;
using WebApplication21.Helpers;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;

namespace WebApplication21.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly new_schemaContext _context;
        private FileRepository fservice = new FileRepository();
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private IHostingEnvironment _hostingEnvironment;
        private Cloudinary _cloudinary;

        public FilesController(new_schemaContext context, IHostingEnvironment environment, 
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _context = context;
            _hostingEnvironment = environment;

            Account acc = new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret
           );

            _cloudinary = new Cloudinary(acc);
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
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> currentPhotoUrl([FromRoute] int userId)
        {
            var files = Request.Form.Files; // now you have them
            var uploadResult = new ImageUploadResult();
            Users user = await _context.Users.FindAsync(userId);

            return Ok(new { user.Url});

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Descomprimir([FromBody] string path)
        {

            fservice.readZipFile(path);

            return Ok();

        }


        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> AddPhotoForUser([FromRoute] int userId,  [FromForm] IList<IFormFile> File)
        {
            var files = Request.Form.Files; // now you have them
            var uploadResult = new ImageUploadResult();
            Users user = await _context.Users.FindAsync(userId);

           // var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                   // var filePath = Path.Combine(uploads, file.FileName);

                    // Cloudinary
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation()
                                .Width(500).Height(500).Crop("fill").Gravity("face")
                        };

                        uploadResult = _cloudinary.Upload(uploadParams);
                    }

                    user.Url = uploadResult.Uri.ToString();
                    user.PublicId = uploadResult.PublicId;

                    await _context.SaveChangesAsync();
                    

                }
            }

            return Ok(user);

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

                    // Local
                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
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