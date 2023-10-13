using Microsoft.EntityFrameworkCore;
using ProEventos.API.Controllers;

namespace ProEventos.API.Data;
public class DataContext : DbContext
{
    public DbSet<Evento> Eventos { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
