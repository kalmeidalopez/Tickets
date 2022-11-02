using TicketsTS.Models;
using WebApiKal.DAL;

namespace TicketsTS.Services
{
    public class TicketService : ITicketService
    {
        Context _ticktetContext;
        public TicketService(Context dbcontext)
        {
            _ticktetContext = dbcontext;
        }

        public async Task SaveAsync(Ticket ticket)
        {
            int maxid = 1;
            if (_ticktetContext.Tickets.Any())
                maxid = _ticktetContext.Tickets.Count() + 1;


            int maxeq = 1;
            if (_ticktetContext.EquipoTickets.Any())
                maxeq = _ticktetContext.EquipoTickets.Count() + 1;

            ticket.Id =   Convert.ToInt32(DateTime.Now.ToString("yyyymmdd") + maxid);

            foreach (var eq in ticket.Tickets)
            {
                eq.Ticket_id = ticket.Id;
                eq.Id = maxeq;
                maxeq++;
            }
            await _ticktetContext.AddAsync(ticket);
            await _ticktetContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            var ticketup = _ticktetContext.Tickets.Find(ticket.Id);
            ticketup.Column_name = ticket.Column_name;
            ticketup.Reporta = ticket.Reporta;
            ticketup.Descripcion = ticket.Descripcion;
            await _ticktetContext.SaveChangesAsync();
        }

        public Ticket Get(int id)
        {
            var tickets = _ticktetContext.Tickets.Find(id);
            return tickets;
        }

        public async Task<Ticket> GetAsync(int id)
        {
            var ticket = _ticktetContext.Tickets.Find(id);
            return await Task.FromResult(ticket);
        }
        public Task<List<Ticket>> GetAsync()
        {
            var tickets = _ticktetContext.Tickets.ToList();
            return Task.FromResult(tickets);
        }

    }
}
