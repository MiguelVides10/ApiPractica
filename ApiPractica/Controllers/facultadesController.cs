using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public facultadesController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            List<facultades> listaFacultades = (from f in _equiposContext.Facultades
                                            select f).ToList();
            if (listaFacultades.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaFacultades);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarFacultad([FromBody] facultades facultad)
        {
            try
            {
                _equiposContext.Facultades.Add(facultad);
                _equiposContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] facultades facultadModificar)
        {
            facultades? facultadActual = (from f in _equiposContext.Facultades
                                       where f.facultad_id == id
                                       select f).FirstOrDefault();
            if (facultadActual == null)
            {
                return NotFound();
            }

            facultadActual.nombre_facultad = facultadModificar.nombre_facultad;

            _equiposContext.Entry(facultadModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(facultadModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]

        public IActionResult ActualizarEstadoFacultad(int id, char estado)
        {
            facultades? facultad = (from f in _equiposContext.Facultades
                                 where f.facultad_id == id
                                 select f).FirstOrDefault();
            if (facultad == null)
            {
                return NotFound();
            }
            facultad.estado = estado;
            _equiposContext.Entry(facultad).State = EntityState.Modified;
            _equiposContext.SaveChanges();
            return Ok(facultad);
        }
    }
}
