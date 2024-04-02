using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Repositories
{
    public interface IStatusRepository
    {
        Task<int> AddStatus(string name);

        Task<Status> GetStatus(string name);
    }
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddStatus(string name)
        {
            var status = new Status() { Name = name };
            await _context.Status.AddAsync(status);
            await _context.SaveChangesAsync();
            return status.Id;
        }

        public async Task<Status> GetStatus(string name)
        {
            var status = await _context.Status.FirstOrDefaultAsync(f => f.Name == name);

            if (status == null)
            {
                status = new Status() { Name = name };
                await _context.Status.AddAsync(status);
                await _context.SaveChangesAsync();
            }

            return status;
            
        }
    }
}
