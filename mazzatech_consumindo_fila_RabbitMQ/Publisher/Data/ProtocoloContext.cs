using Entity;
using Microsoft.EntityFrameworkCore;
 

namespace Publisher.Data
{
    public class ProtocoloContext : DbContext
    {
        public DbSet<Protocolo> Protocolos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("SuaStringDeConexao");
        }
    }
}
