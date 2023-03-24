using ApiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public usuariosController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult get()
        {
            var listaUsuarios = (from u in _equiposContext.Usuarios join
                                 c in _equiposContext.Carreras on u.carrera_id equals c.carrera_id
                                 select new
                                            {
                                              u.usuario_id,
                                              u.nombre,
                                              u.documento,
                                              u.tipo,
                                              u.carnet,
                                              u.carrera_id,
                                              c.nombre_carrera,
                                               u.estado
                                                 }).ToList();
            if (listaUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaUsuarios);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuario([FromBody] usuarios usuario)
        {
            try
            {
                _equiposContext.Usuarios.Add(usuario);
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
        public IActionResult actualizar(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioActual = (from u in _equiposContext.Usuarios
                                    where u.usuario_id == id
                                    select u).FirstOrDefault();
            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.documento = usuarioModificar.documento;
            usuarioActual.tipo = usuarioModificar.tipo;
            usuarioActual.carnet = usuarioModificar.carnet;
            usuarioActual.carrera_id = usuarioModificar.carrera_id;

            _equiposContext.Entry(usuarioModificar).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(usuarioModificar);
        }

        [HttpPut]
        [Route("actualizarEstado/{id}")]

        public IActionResult ActualizarEstadoUsuario(int id, char estado)
        {
            usuarios? usuario = (from u in _equiposContext.Usuarios
                                    where u.usuario_id == id
                                    select u).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.estado = estado;
            _equiposContext.Entry(usuario).State = EntityState.Modified;
            _equiposContext.SaveChanges();
            return Ok(usuario);
        }
    }
}
