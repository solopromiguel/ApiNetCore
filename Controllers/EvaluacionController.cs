using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication21.Dtos;
using WebApplication21.sakila;

namespace WebApplication21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionController : ControllerBase
    {
        private readonly new_schemaContext _context;

        public EvaluacionController(new_schemaContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        [Produces("application/ms-word")]
        public async Task<IActionResult> GuardarEvaluacion([FromBody] EtapaIdentificacion model)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.UsersId = UserId;

                foreach (var item in model.Riesgos)
                {
                    item.UsersId = UserId;
                    item.Id = 0;
                }


                _context.Etapas.Add(model);
                _context.SaveChanges();

                OutDto outDto = new OutDto
                {
                    NameModel= "EtapaIdentificacion",
                    Result=model
                };

                return Ok(outDto);

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddControl([FromBody] Control control)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                control.UsersId = UserId;
                control.Estado = true;
                control.IsMain = true; // Es la actual version.
                control.Calificacion = "FUERTE"; // Por definir.

                _context.Control.Add(control);
                 await  _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
           

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCaracteristica([FromBody] Caracteristica caracteristica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                caracteristica.UsersId = UserId;
                caracteristica.Estado = true;
                caracteristica.FactorId = 1;

                _context.Caracteristicas.Add(caracteristica);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddIdent([FromBody] Identificacion identificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            identificacion.UsersId = UserId;

            return Ok();

        }


    }
}