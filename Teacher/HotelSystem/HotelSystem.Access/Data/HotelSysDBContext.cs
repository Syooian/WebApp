using System;
using System.Collections.Generic;
using HotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Access.Data;

public partial class HotelSysDBContext : DbContext
{
    public HotelSysDBContext(DbContextOptions<HotelSysDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRole { get; set; }

    public virtual DbSet<Member> Member { get; set; }

    public virtual DbSet<MemberAccount> MemberAccount { get; set; }

    public virtual DbSet<MemberTel> MemberTel { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderDetail> OrderDetail { get; set; }

    public virtual DbSet<OrderStatus> OrderStatus { get; set; }

    public virtual DbSet<PayType> PayType { get; set; }

    public virtual DbSet<ProcessingStatus> ProcessingStatus { get; set; }

    public virtual DbSet<Room> Room { get; set; }

    public virtual DbSet<RoomPhoto> RoomPhoto { get; set; }

    public virtual DbSet<RoomService> RoomService { get; set; }

    public virtual DbSet<RoomStatus> RoomStatus { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF1372F2EC2");

            entity.Property(e => e.EmployeeID).IsFixedLength();
            entity.Property(e => e.Account).UseCollation("Latin1_General_CS_AS");
            entity.Property(e => e.RoleCode).IsFixedLength();

            entity.HasOne(d => d.RoleCodeNavigation).WithMany(p => p.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__RoleCo__4CA06362");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.RoleCode).HasName("PK__Employee__D62CB59D52120498");

            entity.Property(e => e.RoleCode).IsFixedLength();
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberID).HasName("PK__Member__0CF04B388536A735");

            entity.Property(e => e.MemberID).IsFixedLength();
        });

        modelBuilder.Entity<MemberAccount>(entity =>
        {
            entity.HasKey(e => e.Account).HasName("PK__MemberAc__B0C3AC470F77ED6E");

            entity.Property(e => e.Account).UseCollation("Latin1_General_CS_AS");
            entity.Property(e => e.MemberID).IsFixedLength();

            entity.HasOne(d => d.Member).WithMany(p => p.MemberAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberAcc__Membe__3C69FB99");
        });

        modelBuilder.Entity<MemberTel>(entity =>
        {
            entity.HasKey(e => e.SN).HasName("PK__MemberTe__32151C64DC140ADA");

            entity.Property(e => e.MemberID).IsFixedLength();

            entity.HasOne(d => d.Member).WithMany(p => p.MemberTel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberTel__Membe__398D8EEE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderID).HasName("PK__Order__C3905BAFA93EC8BD");

            entity.Property(e => e.OrderID).IsFixedLength();
            entity.Property(e => e.EmployeeID).IsFixedLength();
            entity.Property(e => e.MemberID).IsFixedLength();
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PayCode).IsFixedLength();
            entity.Property(e => e.StatusCode).IsFixedLength();

            entity.HasOne(d => d.Employee).WithMany(p => p.Order).HasConstraintName("FK__Order__EmployeeI__5535A963");

            entity.HasOne(d => d.Member).WithMany(p => p.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__MemberID__5441852A");

            entity.HasOne(d => d.PayCodeNavigation).WithMany(p => p.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__PayCode__5629CD9C");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__StatusCod__571DF1D5");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderID, e.RoomID }).HasName("PK__OrderDet__40B8383EFDD6786F");

            entity.Property(e => e.OrderID).IsFixedLength();
            entity.Property(e => e.RoomID).IsFixedLength();
            entity.Property(e => e.CheckInTime).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.CheckOutTime).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__5CD6CB2B");

            entity.HasOne(d => d.Room).WithMany(p => p.OrderDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__RoomI__5DCAEF64");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__OrderSta__6A7B44FD99F982F7");

            entity.Property(e => e.StatusCode).IsFixedLength();
        });

        modelBuilder.Entity<PayType>(entity =>
        {
            entity.HasKey(e => e.PayCode).HasName("PK__PayType__914DABCEED66F083");

            entity.Property(e => e.PayCode).IsFixedLength();
        });

        modelBuilder.Entity<ProcessingStatus>(entity =>
        {
            entity.HasKey(e => e.PSCode).HasName("PK__Processi__31BD33E01F45E763");

            entity.Property(e => e.PSCode).IsFixedLength();
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomID).HasName("PK__Room__328639196A1A712F");

            entity.Property(e => e.RoomID).IsFixedLength();
            entity.Property(e => e.Area).IsFixedLength();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StatusCode).IsFixedLength();

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Room)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__StatusCode__440B1D61");
        });

        modelBuilder.Entity<RoomPhoto>(entity =>
        {
            entity.HasKey(e => e.SN).HasName("PK__RoomPhot__32151C64D9844F05");

            entity.Property(e => e.RoomID).IsFixedLength();

            entity.HasOne(d => d.Room).WithMany(p => p.RoomPhoto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomPhoto_Room");
        });

        modelBuilder.Entity<RoomService>(entity =>
        {
            entity.HasKey(e => e.RoomServiceID).HasName("PK__RoomServ__A11E84A1BC97F1D3");

            entity.Property(e => e.RoomServiceID).IsFixedLength();
            entity.Property(e => e.CreatedTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EmployeeID).IsFixedLength();
            entity.Property(e => e.MemberID).IsFixedLength();
            entity.Property(e => e.PSCode).IsFixedLength();
            entity.Property(e => e.RoomID).IsFixedLength();

            entity.HasOne(d => d.Employee).WithMany(p => p.RoomService).HasConstraintName("FK__RoomServi__Emplo__6477ECF3");

            entity.HasOne(d => d.Member).WithMany(p => p.RoomService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoomServi__Membe__6383C8BA");

            entity.HasOne(d => d.PSCodeNavigation).WithMany(p => p.RoomService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoomServi__PSCod__656C112C");
        });

        modelBuilder.Entity<RoomStatus>(entity =>
        {
            entity.HasKey(e => e.StatusCode).HasName("PK__RoomStat__6A7B44FD956EC5BE");

            entity.Property(e => e.StatusCode).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);




}
