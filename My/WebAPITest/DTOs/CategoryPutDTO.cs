using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebAPITest.Data;
using WebAPITest.Models;

namespace WebAPITest.DTOs
{
    public class CategoryPutDTO
    {
        [CateNameCheck]
        public string CateName { get; set; } = null!;
    }
}
