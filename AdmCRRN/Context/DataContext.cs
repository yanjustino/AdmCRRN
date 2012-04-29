using System.Data.Entity;
using AdmCRRN.Models;

namespace AdmCRRN.Context
{
    public class DataContext: DbContext
    {
        public DbSet<CentroAdministrativo> CentrosAdministrativos { get; set; }
        public DbSet<Entidade> Entidades { get; set; }
        public DbSet<Membro> Membros { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}