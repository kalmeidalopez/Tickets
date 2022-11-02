namespace TicketsTS.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        public int Empresa_id { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public List<EquipoTicket> Tickets { get; set; }
    }
}
