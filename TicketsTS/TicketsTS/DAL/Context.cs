using Microsoft.EntityFrameworkCore;
using TicketsTS.Models;

namespace WebApiKal.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<EquipoTicket> EquipoTickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipo>(equipo =>
            {
                equipo.ToTable("equipo");
                equipo.HasKey(p => p.Id);             
                equipo.Property(p => p.Id).IsRequired().HasColumnName("id");
                equipo.Property(p => p.Empresa_id).HasColumnName("empresa_id");
                equipo.Property(p => p.Nombre).HasMaxLength(100).HasColumnName("nombre");
                equipo.Property(p => p.Marca).HasMaxLength(100).HasColumnName("marca");      
            });

            modelBuilder.Entity<Ticket>(ticket =>
            {
                ticket.ToTable("ticket");
                ticket.HasKey(p => p.Id);
                ticket.Property(p => p.Id).IsRequired().HasColumnName("id");
                ticket.Property(p => p.Column_name).HasColumnName("column_name");
                ticket.Property(p => p.Descripcion).HasMaxLength(1).HasColumnName("descripcion");
                ticket.Property(p => p.Reporta).HasMaxLength(1).HasColumnName("reporta");
            });

            modelBuilder.Entity<EquipoTicket>(equipoTicket =>
            {
                equipoTicket.ToTable("equipo_ticket");
                equipoTicket.HasKey(p => p.Id);
                equipoTicket.Property(p => p.Id).IsRequired().HasColumnName("id");
                equipoTicket.Property(p => p.Equipo_id);
                equipoTicket.Property(p => p.Ticket_id);
                equipoTicket.HasOne(p => p.Equipo).WithMany(p => p.Tickets).HasForeignKey(p => p.Equipo_id);
                equipoTicket.HasOne(p => p.Ticket).WithMany(p => p.Tickets).HasForeignKey(p => p.Ticket_id);
            });
        }
    }
}
