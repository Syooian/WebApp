using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirst_MySql1.Models;

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

    public virtual DbSet<department> department { get; set; }

    public virtual DbSet<tstudent> tstudent { get; set; }

    public virtual DbSet<tstudent2> tstudent2 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<department>(entity =>
        {
            entity.HasKey(e => e.DeptID).HasName("PRIMARY");

            entity.ToTable(tb => tb.HasComment("科系"));

            entity.Property(e => e.DeptID)
                .HasMaxLength(2)
                .HasComment("科系ID");
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .HasComment("科系名稱");
        });

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

        modelBuilder.Entity<tstudent2>(entity =>
        {
            entity.HasKey(e => e.fStuId).HasName("PRIMARY");

            entity.HasIndex(e => e.DeptID, "FK_tstudent2_department");

            entity.Property(e => e.fStuId)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasComment("學號");
            entity.Property(e => e.DeptID)
                .HasMaxLength(2)
                .HasComment("科系ID");
            entity.Property(e => e.fEmail)
                .HasMaxLength(40)
                .HasComment("電子郵件");
            entity.Property(e => e.fName)
                .HasMaxLength(30)
                .HasComment("姓名");
            entity.Property(e => e.fScore)
                .HasDefaultValueSql("'0'")
                .HasComment("分數")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.Dept).WithMany(p => p.tstudent2)
                .HasForeignKey(d => d.DeptID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tstudent2_department");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
