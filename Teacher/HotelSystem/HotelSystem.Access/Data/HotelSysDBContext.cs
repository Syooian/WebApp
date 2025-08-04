using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Access.Data
{
    public class HotelSysDBContext : DbContext
    {
        public HotelSysDBContext(DbContextOptions<HotelSysDBContext> Options) : base(Options)
        {

        }
    }
}
