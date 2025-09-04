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

    public async Task<int> ExecSPAddNewOrderAsync(DateTime ExpectedCheckInDate, DateTime ExpectedCheckOutDate, string Note, string MemberID, string PayCode, string StatusCode, string Cart)
    {
        var result = this.Database.ExecuteSqlRawAsync("EXEC AddNewOrder {0},{1},{2},{3},{4},{5},{6}", ExpectedCheckInDate, ExpectedCheckOutDate, Note, MemberID, PayCode, StatusCode, Cart);
        return await result;
    }

}
