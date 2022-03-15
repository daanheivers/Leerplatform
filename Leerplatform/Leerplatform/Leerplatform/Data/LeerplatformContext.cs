using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Leerplatform.Models;

namespace Leerplatform.Data
{
    public class LeerplatformContext : DbContext
    {
        public LeerplatformContext (DbContextOptions<LeerplatformContext> options)
            : base(options)
        {
        }

        public DbSet<Leerplatform.Models.Vak> Vak { get; set; }
    }
}
