using Microsoft.EntityFrameworkCore;

namespace API;

public class AppDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios {get; set;}
    public DbSet<Folha> Folhas {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=AppDataContext.db");
    }
}
