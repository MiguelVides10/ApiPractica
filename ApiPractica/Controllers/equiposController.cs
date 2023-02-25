using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public equiposController(equiposContext equipoContexto) {
            _equiposContext = equipoContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipos = (from e in _equiposContext.Equipos select e).ToList();
            if (listadoEquipos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipos);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContext.Equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        [HttpGet]
        [Route("find/{filtro}")]

        public IActionResult FinfByDescription(string filtro) {
            List<equipos> equipos = (from e in _equiposContext.Equipos
                                     where e.descripcion.Contains(filtro)
                                     select e).ToList();
            if (equipos == null)
            {
                return NotFound();
            }

            return Ok(equipos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipos)
        {
            try
            {
                _equiposContext.Equipos.Add(equipos);
                _equiposContext.SaveChanges();
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar) {
            equipos? equipoActual = (from e in _equiposContext.Equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipoActual == null)
            {
                return NotFound();
            }
            equipoActual.nombre = equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.marca_id = equipoModificar.marca_id;
            equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equipoActual.anio_compra = equipoModificar.anio_compra;
            equipoActual.costo = equipoModificar.costo;

            _equiposContext.Entry(equipoActual).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            equipos? equipo = (from e in _equiposContext.Equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }

            _equiposContext.Attach(equipo);
            _equiposContext.Remove(equipo);
            _equiposContext.SaveChanges(); 
            return Ok(equipo);
        }
    }
}
