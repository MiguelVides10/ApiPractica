using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        
        public equiposController(equiposContext equipoContexto) {
            _equiposContext= equipoContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipos  = (from e in _equiposContext.Equipos select e).ToList();
            if(listadoEquipos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipos);
        }
    }
}
