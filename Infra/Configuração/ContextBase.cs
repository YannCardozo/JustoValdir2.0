using Entities.Entidades;
using Justo.Entities.Entidades;
using JustoNovo.Domain.ProcessosEntidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Configuração
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Advogado> Advogado { get; set; }
        public DbSet<AdvogadoEspecialidade> AdvogadoEspecialidade { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Polo> Polo { get; set; }
        public DbSet<Processo> Processo { get; set; }
        public DbSet<ProcessosAtualizacao> ProcessosAtualizacao { get; set; }
        public DbSet<ProcessosCompromissos> ProcessosCompromissos { get; set; }
        public DbSet<SiteContato> SiteContato { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

                //optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.CPF)
                .IsUnique();

            builder.Entity<Advogado>()
                .HasIndex(u => u.Cpf)
                .IsUnique();

            builder.Entity<Advogado>()
                .HasIndex(u => u.Oab)
                .IsUnique();

            builder.Entity<Processo>()
                .HasIndex(u => u.CodPJEC)
                .IsUnique();

            builder.Entity<Cliente>()
                .HasIndex(u => u.Cpf)
                .IsUnique();



            //manipulando a tabela de perfis do IDENTITTY

            builder.Entity<IdentityRole>().HasData
                (new IdentityRole
                {
                    Name = "Usuário",
                    NormalizedName = "USUÁRIO",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

            builder.Entity<IdentityRole>().HasData
                (new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
            base.OnModelCreating(builder);



        }

        //public string ObterStringConexao()
        //{
        //    return "Server=localhost\\SQLEXPRESS;Database=JustoTesteValdir;Trusted_Connection=True;TrustServerCertificate=true;";
        //}
    }
}
