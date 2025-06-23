using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirst.Models;

public partial class dbStudentsContext : DbContext
{
    //public dbStudentsContext(DbContextOptions<dbStudentsContext> options)
    //    : base(options)
    //{
    //}

    public readonly string DB_Server = "C501A117";
    public readonly string DB_Name = "dbStudents";
    public readonly string DB_User = "Syooian";
    public readonly string DB_Password = "a123456";

    /// <summary>
    /// 連線到資料庫
    /// <para>講義：1.2.4</para>
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer($"Data Source={DB_Server};Database={DB_Name};TrustServerCertificate=True;User ID={DB_User};Password={DB_Password}");

    /// <summary>
    /// 
    /// </summary>
    //public dbStudentsContext()
    //{
    //    //空建構子
    //}

    /// <summary>
    /// 學生資料表
    /// </summary>
    public virtual DbSet<tStudent> tStudent { get; set; }
    /// <summary>
    /// 科系資料表
    /// </summary>
    public virtual DbSet<Department> Department { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tStudent>(entity =>
        {
            entity.HasKey(e => e.fStuId).HasName("PK__tStudent__08E5BA95D5F47CCA");

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
