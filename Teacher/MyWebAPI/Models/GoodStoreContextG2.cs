using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;

namespace MyWebAPI.Models;

public class GoodStoreContextG2 : GoodStoreContext
{
    public GoodStoreContextG2(DbContextOptions<GoodStoreContext> options)
       : base(options)
    {
    }


    //4.6.5 修改GoodStoreContext，增加ProductDTO的DbSet屬性
    public virtual DbSet<ProductDTO> ProductDTO { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //4.6.8 修改GoodStoreContext的OnModelCreating()，標示ProductDTO為HasNoKey
        modelBuilder.Entity<ProductDTO>(entity=>entity.HasNoKey());


        //4.7.5 加入base.OnModelCreating(modelBuilder);來繼承父類別所的方法
        base.OnModelCreating(modelBuilder);
    }


}
