namespace WebAPITest.DTOs
{
    public class CategoryDTO
    {
        public string CateID { get; set; } = null!;

        public string CateName { get; set; } = null!;

        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
