﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication21.sakila;

namespace WebApplication21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly new_schemaContext _context;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IFileProvider _fileProvider;

        public UsersController(new_schemaContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
            //_fileProvider = fileProvider;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUsersPhoto([FromRoute] int id)
        {

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }
           var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "users", user.Id.ToString(), "imagenes/Captura.PNG");

            //var imageName = "Captura";
            //var name = $"{imageName}.PNG";
            ////var fileInfo = _fileProvider.GetFileInfo(uploads+@"/"+name);
            //var data = System.IO.File.ReadAllBytes(uploads);
            //var file= File(data, MediaTypeNames.Image.Jpeg, uploads);

            //return Ok(file);
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(uploads);
                string fileName = "myfile.ext";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.InnerException.Message);
            }
            

        }
        [HttpPost("[action]/{idUser}")]
        public async Task<IActionResult> SavePhotoUser([FromForm] IList<IFormFile> File,[FromRoute] int idUser)
        {
            var files = Request.Form.Files; // now you have them
            var user =await _context.Users.FirstOrDefaultAsync(x=>x.Id==idUser);

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath,"users" ,user.Id.ToString(),"imagenes");

            if (!(Directory.Exists(uploads)))
            {
                Directory.CreateDirectory(uploads);
            }
          
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploads, file.FileName);
                    try
                    {
                        using (var fileStream = new FileStream(filePath + file.FileName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                       // var directoriosE = Directory.GetFiles(filePath).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.InnerException.Message);
                    }
                  

                }
            }
           

            return Ok();

        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] int id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}