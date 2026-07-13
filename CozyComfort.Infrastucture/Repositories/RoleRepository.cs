using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using CozyComfort.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .OrderBy(x => x.RoleName)
                .ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role> CreateAsync(Role role)
        {
            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> UpdateAsync(Role role)
        {
            var existing = await _context.Roles
                .FindAsync(role.Id);

            if (existing == null)
                return null;

            existing.RoleName = role.RoleName;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles
                .FindAsync(id);

            if (role == null)
                return false;

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}