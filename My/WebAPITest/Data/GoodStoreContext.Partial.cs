using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPITest.DTOs;
using WebAPITest.Models;

namespace WebAPITest.Data;

public partial class GoodStoreContext
{
    public virtual DbSet<ProductDTO> ProductDTO { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductDTO>(Entity => Entity.HasNoKey());
    }
}
