namespace MyWebAPI.QueryParameters
{
    //8.3.7 建立商品查詢的傳入參數類別ProductParam.cs(此例放置於QueryParameters資料夾)
    public class ProductParam
    {
        public string? catID { get; set; }
        public string? productName { get; set; }
        public short? minPrice { get; set; }
        public short? maxPrice { get; set; }
        public string? description { get; set; }
        public bool? discontinued { get; set; }


    }
}
