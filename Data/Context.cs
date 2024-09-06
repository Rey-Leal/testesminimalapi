using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Grupo> Grupo { get; set; } = default!;
        public DbSet<Produto> Produto { get; set; } = default!;
        public DbSet<Usuario> Usuario { get; set; } = default!;
    }
}
