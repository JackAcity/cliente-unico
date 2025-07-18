using Application.Data.Auditoria;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Auditoria;

public class ApplicationDbContextPostgreSql : ApplicationDbContext, IApplicationAuditoriaDbContext
{
    public ApplicationDbContextPostgreSql(DbContextOptions<ApplicationDbContextPostgreSql> options, IPublisher publisher)
        : base(options,publisher) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("Instanciando ApplicationAuditoriaDbContextPostgreSql");
       
    }
}