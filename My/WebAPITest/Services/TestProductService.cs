namespace WebAPITest.Services
{
    public struct TestProductData
    {
        public string ID;
        public string Name;
    }

    public class TestProductService
    {
        /// <summary>
        /// 
        /// </summary>
        TestProductData[] Products = new TestProductData[]
        {
            new TestProductData { ID = "A0001", Name = "Product A" },
            new TestProductData { ID = "A0002", Name = "Product B" },
            new TestProductData { ID = "A0003", Name = "Product C" }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TestProductData[] GetProducts()
        {
            return Products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TestProductData GetProduct(int ID)
        {
            return Products[ID];
        }
    }
}
