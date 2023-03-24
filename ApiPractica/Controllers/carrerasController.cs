using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carrerasController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public carrerasController(equiposContext equiposContext)
        {
            _equiposContext= equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            var listaCarreras = (from c in _equiposContext.Carreras join
                                 f in _equiposContext.Facultades on c.facultad_id equals f.facultad_id
                                            select new
                                            {
                                                c.carrera_id,
                                                c.nombre_carrera,
                                                c.facultad_id,
                                                f.nombre_facultad,
                                                c.estado
                                            }).ToList();
            if (listaCarreras.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaCarreras);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarCarrera([FromBody] carreras carreras)
        {
            try
            {
                _equiposContext.Carreras.Add(carreras);
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
        public IActionResult actualizar(int id, [FromBody] carreras carreraModificar)
        {
            carreras? carreraActual = (from c in _equiposContext.Carreras
                                     where c.carrera_id == id
                                     select c).FirstOrDefault();
            if(carreraActual == null)
            {
                return NotFound();
            }

            carreraActual.nombre_carrera = carreraModificar.nombre_carrera;
            carreraActual.facultad_id = carreraModificar.facultad_id;

            _equiposContext.Entry(carreraModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(carreraModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]

        public IActionResult ActualizarEstadoCarrera(int id, char estado)
        {
            carreras? carrera = (from c in _equiposContext.Carreras
                               where c.carrera_id == id
                               select c).FirstOrDefault();
            if (carrera == null)
            {
                return NotFound();
            }
            carrera.estado = estado;
            _equiposContext.Entry(carrera).State = EntityState.Modified;
            _equiposContext.SaveChanges();
            return Ok(carrera);
        }
    }
}
