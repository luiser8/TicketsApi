using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsApi.Models
{
    public class TicketsDBContext : DbContext
    {
        public TicketsDBContext(DbContextOptions<TicketsDBContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Queue> Queue { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Attended> Attended { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        public static implicit operator TicketsDBContext(DbContextOptions<TicketsDBContext> v)
        {
            throw new NotImplementedException();
        }
    }
}
