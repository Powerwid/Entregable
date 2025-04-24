using appweb1.Models;
using Microsoft.EntityFrameworkCore;

namespace appweb1.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public DbSet<Cliente> Clientes {get;set;}
    }
}