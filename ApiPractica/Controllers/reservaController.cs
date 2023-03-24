using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservaController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public reservaController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            var listaReserva = (from r in _equiposContext.Reservas join
                                e in _equiposContext.Equipos on r.equipo_id equals e.id_equipos join
                                u in _equiposContext.Usuarios on r.usuario_id equals u.usuario_id join
                                er in _equiposContext.Estados_Reserva on r.estado_reserva_id equals er.estado_res_id
                                select new
                                {
                                    r.reserva_id,
                                    r.equipo_id,
                                    e.nombre,
                                    r.usuario_id,
                                    nombre_usuario= u.nombre,
                                    r.fecha_salida,
                                    r.hora_salida,
                                    r.tiempo_reserva,
                                    r.estado_reserva_id,
                                    estado_reserva = er.estado,
                                    r.fecha_retorno,
                                    r.hora_retorno,
                                    r.estado
                                }).ToList();
            if (listaReserva.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaReserva);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarReserva([FromBody] reservas reserva)
        {
            try
            {
                _equiposContext.Reservas.Add(reserva);
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
        public IActionResult actualizar(int id, [FromBody] reservas reservaModificar)
        {
            reservas? reservaActual = (from r in _equiposContext.Reservas
                                               where r.reserva_id == id
                                               select r).FirstOrDefault();
            if (reservaActual == null)
            {
                return NotFound();
            }

            reservaActual.equipo_id = reservaModificar.equipo_id;
            reservaActual.usuario_id = reservaModificar.usuario_id;
            reservaActual.fecha_salida = reservaModificar.fecha_salida;
            reservaActual.hora_salida = reservaModificar.hora_salida;
            reservaActual.tiempo_reserva = reservaModificar.tiempo_reserva;
            reservaActual.estado_reserva_id = reservaModificar.estado_reserva_id;
            reservaActual.fecha_retorno = reservaModificar.fecha_retorno;
            reservaActual.hora_retorno = reservaModificar.hora_retorno;

            _equiposContext.Entry(reservaModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(reservaModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]
        public IActionResult actualizarEstado(int id, char estado)
        {
            reservas? reserva = (from r in _equiposContext.Reservas
                                         where r.reserva_id == id
                                         select r).FirstOrDefault();
            if (reserva == null)
            {
                return NotFound();
            }

            reserva.estado = estado;

            _equiposContext.Entry(reserva).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(reserva);
        }
    }
}
