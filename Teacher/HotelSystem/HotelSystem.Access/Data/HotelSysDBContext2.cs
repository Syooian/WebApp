using System;
using System.Collections.Generic;


using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Access.Data;

public partial class HotelSysDBContext2 : HotelSysDBContext
{
    public HotelSysDBContext2(DbContextOptions<HotelSysDBContext> options)
        : base(options)
    {
    }

    public int GetRoomServiceCount()
    {
        return RoomService.CountAsync().Result;
    }

    public async Task<List<ViewModels.MemberWithTel>> CallTest222Async()
    {

        return await this.Set<ViewModels.MemberWithTel>()
            .FromSqlRaw("EXEC getMemberWithTel", "A0001")
            .ToListAsync();
    }
}
