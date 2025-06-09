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
    /// <summary>
    /// 
    /// </summary>
    public dbstudentsContext()
    {
        //空建構子
    }

    /// <summary>
    /// 資料庫連線
    /// </summary>
    /// <param name="OptionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        => OptionsBuilder.UseMySql("Server=127.0.0.1;Database=dbstudents;User=root;Password=autc007;",
            new MySqlServerVersion(new Version(8, 0, 31)));

    public virtual DbSet<tstudent> tstudent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tstudent>(entity =>
        {
            entity.HasKey(e => e.fStuId).HasName("PRIMARY");

            entity.Property(e => e.fStuId)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.fEmail)
                .HasMaxLength(40)
                .HasDefaultValueSql("''''''");
            entity.Property(e => e.fName)
                .HasMaxLength(30)
                .HasDefaultValueSql("''''''");
            entity.Property(e => e.fScore)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
