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
    public class CaracteristicasController : ControllerBase
    {
        private readonly new_schemaContext _context;

        public CaracteristicasController(new_schemaContext context)
        {
            _context = context;
        }

        // GET: api/Caracteristicas
        [HttpGet]
        public IEnumerable<Caracteristica> GetCaracteristicas()
        {
            return _context.Caracteristicas;
        }
        [HttpGet("[action]/{IdFactor}")]
        public async Task<IActionResult> GetCaracteristicaPorFactor([FromRoute] int IdFactor)
        {

            try
            {
                var modelReturn = await _context.Identificacions
                                             .Where(c => c.Caracteristica.FactorId == IdFactor)
                                             .Include(x => x.Caracteristica)
                                              .Select(x => new
                                              {
                                                  x.Id,
                                                  x.Calificacion,
                                                  descripcionCarateristica = x.Caracteristica.Descripcion,
                                                  x.Impacto,
                                                  x.Probabilidad,
                                                  descripcionIdentificacion = x.Descripcion,
                                              })
                                             .AsNoTracking()
                                             .ToListAsync();
                return Ok(modelReturn);
            }
            catch (Exception)
            {

                return BadRequest("Error Inesperado");
            }
          

        }
        // GET: api/Caracteristicas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaracteristica([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            var caracteristica = await _context.Caracteristicas.FindAsync(id);

            if (caracteristica == null) 
            {
                return NotFound();
            }

            return Ok(caracteristica);
        }

        // PUT: api/Caracteristicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristica([FromRoute] int id, [FromBody] Caracteristica caracteristica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != caracteristica.Id)
            {
                return BadRequest();
            }

            _context.Entry(caracteristica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaracteristicaExists(id))
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

        // POST: api/Caracteristicas
        [HttpPost]
        public async Task<IActionResult> PostCaracteristica([FromBody] Caracteristica caracteristica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Caracteristicas.Add(caracteristica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaracteristica", new { id = caracteristica.Id }, caracteristica);
        }

        // DELETE: api/Caracteristicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristica([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caracteristica = await _context.Caracteristicas.FindAsync(id);
            if (caracteristica == null)
            {
                return NotFound();
            }

            _context.Caracteristicas.Remove(caracteristica);
            await _context.SaveChangesAsync();

            return Ok(caracteristica);
        }

        private bool CaracteristicaExists(int id)
        {
            return _context.Caracteristicas.Any(e => e.Id == id);
        }
    }
}