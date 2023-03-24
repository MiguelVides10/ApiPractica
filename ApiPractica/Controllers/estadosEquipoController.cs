using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estadosEquipoController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public estadosEquipoController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            List<estados_equipo> listaEstadosEquipo = (from ee in _equiposContext.Estados_Equipo
                                                select ee).ToList();
            if (listaEstadosEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaEstadosEquipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardareEquipo([FromBody] estados_equipo eEquipo)
        {
            try
            {
                _equiposContext.Estados_Equipo.Add(eEquipo);
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
        public IActionResult actualizar(int id, [FromBody] estados_equipo eEquipoModificar)
        {
            estados_equipo? eEquipoActual = (from ee in _equiposContext.Estados_Equipo
                                          where ee.id_estados_equipo == id
                                          select ee).FirstOrDefault();
            if (eEquipoActual == null)
            {
                return NotFound();
            }

            eEquipoActual.descripcion = eEquipoModificar.descripcion;

            _equiposContext.Entry(eEquipoModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(eEquipoModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]

        public IActionResult ActualizarEstadoEquipo(int id, char estado)
        {
            estados_equipo? eEquipo = (from ee in _equiposContext.Estados_Equipo
                                    where ee.id_estados_equipo == id
                                    select ee).FirstOrDefault();
            if (eEquipo == null)
            {
                return NotFound();
            }
            eEquipo.estado = estado;
            _equiposContext.Entry(eEquipo).State = EntityState.Modified;
            _equiposContext.SaveChanges();
            return Ok(eEquipo);
        }
    }
}
