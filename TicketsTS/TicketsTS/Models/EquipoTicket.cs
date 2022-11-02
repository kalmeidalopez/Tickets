namespace TicketsTS.Models
{
    public class EquipoTicket
    {
        public int Id { get; set; }
        public int Equipo_id { get; set; }
        public int Ticket_id { get; set; }
        public Equipo Equipo { get; set; }
        public Ticket Ticket { get; set; }
    }

    public class EquipoDTO
    {
        public int Id { get; set; }
    }
}
