using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConductorAppAdmin.Models
{
    public class ConductorAppAdminContext : DbContext
    {
        public ConductorAppAdminContext (DbContextOptions<ConductorAppAdminContext> options)
            : base(options)
        {
        }

        public DbSet<ConductorAppAdmin.Models.Communication> Communication { get; set; }
    }
}
