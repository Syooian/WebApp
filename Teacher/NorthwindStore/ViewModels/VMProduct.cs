using Microsoft.Identity.Client;
using NorthwindStore.Models;

namespace NorthwindStore.ViewModels
{
    public class VMProduct
    {
        public List<Categories>? Category { get; set; }

        public List<Products>?  Product{ get; set; } 
    }
}

