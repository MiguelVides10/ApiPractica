using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public marcasController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            List<marcas> listaMarcas = (from m in _equiposContext.Marcas
                                                       select m).ToList();
            if (listaMarcas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaMarcas);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarMarca([FromBody] marcas marca)
        {
            try
            {
                _equiposContext.Marcas.Add(marca);
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
        public IActionResult actualizar(int id, [FromBody] marcas marcaModificar)
        {
            marcas? marcaActual = (from m in _equiposContext.Marcas
                                             where m.id_marcas == id
                                             select m).FirstOrDefault();
            if (marcaActual == null)
            {
                return NotFound();
            }

            marcaActual.nombre_marca = marcaModificar.nombre_marca;

            _equiposContext.Entry(marcaModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(marcaModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]

        public IActionResult ActualizarEstadoMarca(int id, char estado)
        {
            marcas? marca = (from m in _equiposContext.Marcas
                                       where m.id_marcas == id
                                       select m).FirstOrDefault();
            if (marca == null)
            {
                return NotFound();
            }
            marca.estados = estado;
            _equiposContext.Entry(marca).State = EntityState.Modified;
            _equiposContext.SaveChanges();
            return Ok(marca);
        }
    }
}
