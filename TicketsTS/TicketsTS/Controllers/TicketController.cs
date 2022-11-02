using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketsTS.Models;
using TicketsTS.Services;

namespace TicketsTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpGet(Name = "GetTicket")]
        public async Task<IActionResult> Get()
        {
            var ticket = await ticketService.GetAsync();
            if (ticket != null)
            {
                return Ok(ticket);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ticket = await ticketService.GetAsync(id);

            if (ticket != null)
            {
                return Ok(ticket);
            }
            return NotFound();
        }

        private Ticket GetTicket(int id)
        {
            var ticket = ticketService.Get(id);

            if (ticket != null)
            {
                return ticket;
            }
            return null;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseWebApi), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseWebApi), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromBody] TicketInsertDTO ticket)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                Ticket model = new Ticket();
                model.Id = 0;
                model.Column_name = ticket.Column_name;
                model.Reporta = ticket.Reporta;
                model.Descripcion = ticket.Descripcion;
                EquipoTicket equipoticket = new EquipoTicket();
                foreach (var eq in ticket.Tickets)
                {
                    equipoticket.Equipo_id = eq.Id;
                    if (model.Tickets == null)
                        model.Tickets = new List<EquipoTicket>();
                    model.Tickets.Add(equipoticket);
                }
                await ticketService.SaveAsync(model);
                response.Id = model.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TicketUpdateDTO ticket)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = ticket.Id;
                var ticketupd = GetTicket(ticket.Id);
                if (ticketupd != null)
                {
                    Ticket model = new Ticket();
                    model.Column_name = ticket.Column_name;
                    model.Reporta = ticket.Reporta;
                    model.Descripcion = ticket.Descripcion;
                    model.Id = ticket.Id;
                    await ticketService.UpdateAsync(model);
                }
                else
                {
                    response.Message = "Ticket no existe";
                    return Conflict(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
            return Ok(response);
        }

    }
}
