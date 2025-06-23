using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirst_MySql2.Models;

public partial class dbstudentsContext : DbContext
{
    public dbstudentsContext(DbContextOptions<dbstudentsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<department> department { get; set; }

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
