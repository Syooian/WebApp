using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Models
{
    public class MemberWithTel
    {
        public string MemberID { get; set; } = null!;

      
        public string Name { get; set; } = null!;

      
        public string City { get; set; } = null!;

        
        public string Address { get; set; } = null!;

      
        public DateTime Birthday { get; set; }


        public string Tel { get; set; } = null!;
    }
}
