using System.Data.Entity;
using AdmCRRN.Models;

namespace AdmCRRN.Context
{
    public class DataContext: DbContext
    {
        public DbSet<Centro> Centros { get; set; }
        public DbSet<Entidade> Entidades { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Dirigente> Dirigentes { get; set; }
        public DbSet<Tesoureiro> Tesoureiros { get; set; }
        public DbSet<Secretario> Secretarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}