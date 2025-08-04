namespace WebAPITest.Services
{
    public class TestProductService
    {
        public struct Data
        {
            public string ID;
            public string Name;
        }

        /// <summary>
        /// 
        /// </summary>
        Data[] Products = new Data[]
        {
            new Data { ID = "A0001", Name = "Product A" },
            new Data { ID = "A0002", Name = "Product B" },
            new Data { ID = "A0003", Name = "Product C" }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Data[] GetProducts()
        {
            return Products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Data GetProduct(int ID)
        {
            return Products[ID];
        }
    }
}
