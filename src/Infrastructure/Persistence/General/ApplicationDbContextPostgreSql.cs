using Application.Data.General;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.General;

public class ApplicationDbContextPostgreSql : ApplicationDbContext, IApplicationGeneralDbContext
{
    public ApplicationDbContextPostgreSql(
        DbContextOptions<ApplicationDbContextPostgreSql> options,
        IPublisher publisher
        )
        : base(options,  publisher) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("Instanciando ApplicationGeneralDbContext");
    }
}