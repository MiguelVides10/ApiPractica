using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estadoReservaController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public estadoReservaController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            List<estados_reserva> listaEstadoReserva = (from er in _equiposContext.Estados_Reserva
                                                 select er).ToList();
            if (listaEstadoReserva.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaEstadoReserva);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEstadoReserva([FromBody] estados_reserva eReserva)
        {
            try
            {
                _equiposContext.Estados_Reserva.Add(eReserva);
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
        public IActionResult actualizar(int id, [FromBody] estados_reserva eReservaModificar)
        {
            estados_reserva? eReservaActual = (from er in _equiposContext.Estados_Reserva
                                    where er.estado_res_id == id
                                    select er).FirstOrDefault();
            if (eReservaActual == null)
            {
                return NotFound();
            }

            eReservaActual.estado = eReservaModificar.estado;

            _equiposContext.Entry(eReservaModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(eReservaModificar);
        }

        [HttpPut]
        [Route("actualizarUso/{id}")]
        public IActionResult actualizarUso(int id, char uso)
        {
            estados_reserva? eReserva = (from er in _equiposContext.Estados_Reserva
                                               where er.estado_res_id == id
                                               select er).FirstOrDefault();
            if (eReserva == null)
            {
                return NotFound();
            }

            eReserva.en_uso = uso;

            _equiposContext.Entry(eReserva).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(eReserva);
        }

    }
}
