using Leerplatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Leerplatform.Services
{
    public class LokalenService
    {
        private readonly LeerplatformDbContext _context;

        public LokalenService(LeerplatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lokaal>> GetLokalen()
        {
            return await _context.Lokalen.ToListAsync();
        }

        public async Task<Lokaal> GetLokaalById(string id)
        {
            return await _context.Lokalen.Include(m => m.Middelen).FirstOrDefaultAsync(m => m.LokaalId == id); ;
        }

        public async Task<List<Middel>> GetMiddelen()
        {
            return await _context.Middelen.ToListAsync();
        }

        public async Task<int> AddLokaal(Lokaal lokaal)
        {
            _context.Lokalen.Add(lokaal);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateLokaal(Lokaal lokaal)
        {
            _context.Update(lokaal);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteLokaal(Lokaal lokaal)
        {
            _context.Lokalen.Remove(lokaal);
            return await _context.SaveChangesAsync();
        }

        public bool LokaalExists(string id)
        {
            return _context.Lokalen.Any(e => e.LokaalId == id);
        }
    }
}
