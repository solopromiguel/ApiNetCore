using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication21.sakila;

namespace WebApplication21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlsController : ControllerBase
    {
        private readonly new_schemaContext _context;

        public ControlsController(new_schemaContext context)
        {
            _context = context;
        }

        // GET: api/Controls
        [HttpGet]
        public IEnumerable<Control> GetControl()
        {
            return _context.Control;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetListControls()
        {

            return Ok(
                await _context.Control
                .Where(x=>x.IsMain)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: api/Controls/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetControl([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var control = await _context.Control.FindAsync(id);

            if (control == null)
            {
                return NotFound();
            }

            return Ok(control);
        }

        // PUT: api/Controls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControl([FromRoute] int id, [FromBody] Control control)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != control.Id)
            {
                return BadRequest();
            }

            _context.Entry(control).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlExists(id))
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

        // POST: api/Controls
        [HttpPost]
        public async Task<IActionResult> PostControl([FromBody] Control control)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Control.Add(control);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControl", new { id = control.Id }, control);
        }

        // DELETE: api/Controls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControl([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var control = await _context.Control.FindAsync(id);
            if (control == null)
            {
                return NotFound();
            }

            _context.Control.Remove(control);
            await _context.SaveChangesAsync();

            return Ok(control);
        }

        private bool ControlExists(int id)
        {
            return _context.Control.Any(e => e.Id == id);
        }
    }
}