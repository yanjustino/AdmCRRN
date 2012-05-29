using System.Data.Entity;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Context
{
    public class DataContext: DbContext
    {
        public DbSet<Centro> Centros { get; set; }
        public DbSet<Entidade> Entidades { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          //  modelBuilder.Entity<Membro>().HasRequired(b => b.Endereco).WithMany(b => b.Id()).WillCascadeOnDelete(true);

       }
    }
}