using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contexto;
public class ProEventosContext : IdentityDbContext<
                                    User, Role, int, 
                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                    IdentityRoleClaim<int>, IdentityUserToken<int>
                                >
{
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Lote> Lotes { get; set; }
    public DbSet<Palestrante> Palestrantes { get; set; }
    public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
    public DbSet<RedeSocial> RedesSociais { get; set; }

    public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>(userRole => 
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<PalestranteEvento>(pe =>
        {
            pe.HasKey(pk => new { pk.EventoId, pk.PalestranteId });
            
            pe.HasOne(p => p.Palestrante)
                .WithMany(p => p.PalestrantesEventos)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.PalestranteId)
                .IsRequired();
            
            pe.HasOne(e => e.Evento)
                .WithMany(e => e.PalestrantesEventos)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.EventoId)
                .IsRequired();
        });

        modelBuilder.Entity<Evento>(evento =>
        {
            evento.HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.NoAction);
            
            evento.HasMany(pe => pe.PalestrantesEventos)
                .WithOne(p => p.Evento)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Palestrante>(palestrante => 
        {
            palestrante.HasMany(p => p.RedesSociais)
            .WithOne(rs => rs.Palestrante)
            .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Lote>(lote =>
        {
            lote.HasKey(l => l.Id);
            
            lote.Property(l => l.Preco).HasColumnType("DECIMAL(18,2)");
            
            lote.HasOne(l => l.Evento)
                .WithMany(e => e.Lotes)
                .OnDelete(DeleteBehavior.Cascade);

        });
    }
}
