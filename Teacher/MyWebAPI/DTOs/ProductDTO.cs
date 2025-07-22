namespace MyWebAPI.DTOs
{
    //4.2.2 建立ProductDTO類別
    public class ProductDTO
    {
        public string ProductID { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string Picture { get; set; } = null!;

        public string CateID { get; set; } = null!;

        //在這裡補上CateName屬性
        public string CateName { get; set; } = null!;
    }
}
