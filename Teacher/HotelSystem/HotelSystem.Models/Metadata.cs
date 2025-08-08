using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Models
{
    public  class RoomData
    {
        [DisplayFormat(DataFormatString ="{0:C0}",ApplyFormatInEditMode =true)]
        public decimal Price { get; set; }
    }


    public class EmployeeRoleData
    {
        [Display(Name = "角色代碼")]
        [RegularExpression("[A-Z]", ErrorMessage = "角色代碼只能填A-Z")]
        public string RoleCode { get; set; } = null!;

        [Display(Name = "角色")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "角色名稱最少要2個字")]
        public string RoleName { get; set; } = null!;
    }

    [ModelMetadataType(typeof(EmployeeRoleData))]
    public partial class EmployeeRole
    { }


    [ModelMetadataType(typeof(RoomData))]
    public partial class Room
    { }
}
