using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipoController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public TipoEquipoController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            List<tipo_equipo> listaTipoEquipo = (from te in _equiposContext.tipo_Equipo
                                                       select te).ToList();
            if (listaTipoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaTipoEquipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarTipoEquipo([FromBody] tipo_equipo tipoE)
        {
            try
            {
                _equiposContext.tipo_Equipo.Add(tipoE);
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
        public IActionResult actualizar(int id, [FromBody] tipo_equipo equipoModificar)
        {
            tipo_equipo? tEquipo = (from te in _equiposContext.tipo_Equipo
                                             where te.id_tipo_equipo == id
                                             select te).FirstOrDefault();
            if (tEquipo == null)
            {
                return NotFound();
            }

            tEquipo.descripcion = equipoModificar.descripcion;

            _equiposContext.Entry(equipoModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(equipoModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]

        public IActionResult ActualizarTipoEquipo(int id, char estado)
        {
            tipo_equipo? tEquipo = (from te in _equiposContext.tipo_Equipo
                                       where te.id_tipo_equipo == id
                                       select te).FirstOrDefault();
            if (tEquipo == null)
            {
                return NotFound();
            }
            tEquipo.estado = estado;
            _equiposContext.Entry(tEquipo).State = EntityState.Modified;
            _equiposContext.SaveChanges();
            return Ok(tEquipo);
        }
    }
}
