using BookStore.Domain.Entities.Model;
using BookStore.Domain.Handler;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Contexts;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        :base(options)
    {
    }

    public DbSet<Autores> Autores { get; set; }
    public DbSet<Livros> Livros { get; set; }
    public DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
