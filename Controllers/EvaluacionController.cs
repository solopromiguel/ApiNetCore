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
                }

                _context.Etapas.Add(model);
               // _context.SaveChanges();

                return Ok(model);

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }  
        



      }
}