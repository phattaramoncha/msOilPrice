using System;
using System.Collections.Generic;

namespace msOilPrice.Models
{
    public class fJson
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class PriceData
        {
            public int oilTypeId { get; set; }
            public string nameTh { get; set; }
            public string nameEn { get; set; }
            public string image { get; set; }
            public double price { get; set; }
            public DateTime priceDate { get; set; }
            public int ordering { get; set; }
        }

        public class Data
        {
            public DateTime priceDate { get; set; }
            public List<PriceData> priceData { get; set; }
        }

        public class Root
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
        }
    }
}