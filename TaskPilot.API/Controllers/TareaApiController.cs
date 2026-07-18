using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskPilot.AccesoDatos;
using TaskPilot.API.Models;
using TaskPilot.Entidades;
using TaskPilot.LogicaNegocio;

namespace TaskPilot.API.Controllers
{
    [RoutePrefix("api/tareas")]
    public class TareaApiController : ApiController
    {
        TareaLN ln = new TareaLN();

        // GET api/tareas
        [HttpGet]
        [Route("")]
        public IHttpActionResult Listar()
        {
            var tareas = ln.Listar()
                .Select(MapearDTO)
                .ToList();

            return Ok(tareas);
        }

        // GET api/tareas/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Obtener(int id)
        {
            var tarea = ln.Obtener(id);

            if (tarea == null)
                return NotFound();

            return Ok(MapearDTO(tarea));
        }

        // POST api/tareas
        [HttpPost]
        [Route("")]
        public IHttpActionResult Crear([FromBody] TareaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tarea = MapearEntidad(dto);
            ln.Insertar(tarea);

            return Content(HttpStatusCode.Created, MapearDTO(tarea));
        }

        // PUT api/tareas/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Actualizar(int id, [FromBody] TareaDTO dto)
        {
            var existente = ln.Obtener(id);
            if (existente == null)
                return NotFound();

            var tarea = MapearEntidad(dto);
            tarea.IdTarea = id;
            ln.Actualizar(tarea);

            return Ok(MapearDTO(tarea));
        }

        // DELETE api/tareas/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Eliminar(int id)
        {
            var existente = ln.Obtener(id);
            if (existente == null)
                return NotFound();

            ln.Eliminar(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // --- Helpers de mapeo ---
        private TareaDTO MapearDTO(Tarea t)
        {
            return new TareaDTO
            {
                IdTarea = t.IdTarea,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                FechaInicio = t.FechaInicio,
                FechaFin = t.FechaFin,
                Estado = t.Estado,
                Prioridad = t.Prioridad,
                NombreUsuario = t.NombreUsuario,
                UsuariosSeleccionados = t.UsuariosSeleccionados
            };
        }

        private Tarea MapearEntidad(TareaDTO dto)
        {
            return new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = dto.Estado,
                Prioridad = dto.Prioridad,
                UsuariosSeleccionados = dto.UsuariosSeleccionados ?? new List<int>()
            };
        }
    }
}