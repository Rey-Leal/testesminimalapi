using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MinimalAPI.Models;

namespace MinimalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        [HttpGet, Authorize]
        public IActionResult GetAgendamentos()
        {
            List<Agenda> agenda = new List<Agenda>();
            agenda.Add(new Agenda()
            {
                Id = 1,
                Evento = "Curso DDD na prática",
                Data = System.DateTime.Now
            });
            agenda.Add(new Agenda()
            {
                Id = 1,
                Evento = "Evento - Design Patterns",
                Data = System.DateTime.Now.AddDays(15)
            });
            agenda.Add(new Agenda()
            {
                Id = 1,
                Evento = "Palestra - Os recursos do .NET 6.0",
                Data = System.DateTime.Now.AddDays(30)
            });
            return new ObjectResult(agenda);
        }
    }
}