using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirst_Test.Models;

public partial class dbstudentsContext : DbContext
{
    //public dbstudentsContext(DbContextOptions<dbstudentsContext> options)
    //    : base(options)
    //{
    //}

    public dbstudentsContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("Server=127.0.0.1;Database=dbstudents;User=root;Password=autc007;",
            new MySqlServerVersion(new Version(8, 0, 31)));

    public virtual DbSet<tstudent> tstudent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<tstudent>(entity =>
        {
            entity.HasKey(e => e.fStuId).HasName("PRIMARY");

            entity.Property(e => e.fStuId)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.fEmail).HasMaxLength(40);
            entity.Property(e => e.fName).HasMaxLength(30);
            entity.Property(e => e.fScore)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
