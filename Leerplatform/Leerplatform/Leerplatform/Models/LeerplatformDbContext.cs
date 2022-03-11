using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Leerplatform.Models
{
    public partial class LeerplatformDbContext : DbContext
    {
        public LeerplatformDbContext(DbContextOptions<LeerplatformDbContext> options) : base(options)
        {

        }
        public DbSet<Les> Lessen { get; set; }
        public DbSet<Lokaal> Lokalen { get; set; }
        public DbSet<Middel> Middelen { get; set; }
        public DbSet<Planning> Planningen { get; set; }
        public DbSet<Vak> Vakken { get; set; }
    }
}
