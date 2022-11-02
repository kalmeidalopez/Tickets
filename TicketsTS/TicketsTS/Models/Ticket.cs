namespace TicketsTS.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Column_name { get; set; }
        public string Descripcion { get; set; }
        public string Reporta { get; set; }
        public List<EquipoTicket> Tickets { get; set; }
    }

    public class TicketInsertDTO
    {
        public int Column_name { get; set; }
        public string Descripcion { get; set; }
        public string Reporta { get; set; }
        public List<EquipoDTO> Tickets { get; set; }
    }
    public class TicketUpdateDTO
    {
        public int Id { get; set; }
        public int Column_name { get; set; }
        public string Descripcion { get; set; }
        public string Reporta { get; set; }
        public List<EquipoDTO> Tickets { get; set; }
    }
}
