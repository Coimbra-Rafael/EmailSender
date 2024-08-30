using Backgroud.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backgroud.Services.Context;

public class BackgroundContext : DbContext
{
    public DbSet<Email> Emails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Email>().ToTable("Emails");
        modelBuilder.Entity<Email>().HasKey(x => x.Id).HasName("EmailId");
        modelBuilder.Entity<Email>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Email>().Property(x => x.EmailAddress).HasColumnType("varchar(150) CHARACTER SET utf8)");
    }
}