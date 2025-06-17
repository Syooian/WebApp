using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyModel_DBFirst.Models;

public partial class dbStudentsContext : DbContext
{

    //1.2.5 在dbStudentsContext.cs裡撰寫一個空的建構子
    public dbStudentsContext()
    {
    }

    //public dbStudentsContext(DbContextOptions<dbStudentsContext> options)
    //    : base(options)
    //{
    //}

    //1.2.4 在dbStudentsContext.cs裡撰寫連線到資料庫的程式
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=TEACHER;Database=dbStudents;TrustServerCertificate=True;User ID=abc;Password=123");






    public virtual DbSet<tStudent> tStudent { get; set; }
    //5.2.4 在dbStudentsContext中加入Department的DbSet
    public virtual DbSet<Department> Department { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tStudent>(entity =>
        {
            entity.HasKey(e => e.fStuId).HasName("PK__tStudent__08E5BA9512D28852");

            entity.Property(e => e.fStuId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.fEmail).HasMaxLength(40);
            entity.Property(e => e.fName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
