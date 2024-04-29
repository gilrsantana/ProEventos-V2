using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;

public class UserPersist(ProEventosContext context) : GeralPersist(context), IUserPersist
{
    private readonly ProEventosContext _context = context;

    public async Task<IEnumerable<User>> GetUsersAsync()
        => await _context.Users.ToListAsync();
    

    public async Task<User?> GetUserByIdAsync(int id)
        => await _context.Users
            .FindAsync(id);
    

    public async Task<User?> GetUserByUserNameAsync(string? username)
        => await _context.Users
            .SingleOrDefaultAsync(u => u.UserName != null &&
                u.UserName.ToLower().Equals(username.ToLower()));
}