using TicketsTS.Models;

namespace TicketsTS.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAsync();
        Task<Ticket> GetAsync(int id);
        Task SaveAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Ticket Get(int id);

    }
}
