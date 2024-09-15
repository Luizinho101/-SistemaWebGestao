using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaWebGestao.Models;

namespace SistemaWebGestao.Data.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Mensageiro> Mensageiros { get; set; }
        public DbSet<Contribuinte> Contribuintes { get; set; }
        public DbSet<TiposPagamento> TiposPagamentos { get; set; }
        public DbSet<MovimentoDiario> MovimentoDiarios { get; set; }
        public DbSet<Contribuicao> Contribuicoes { get; set; }
    }
}