using System;
using msOilPrice.Controllers;

namespace msOilPrice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            PTTPrice pttPrice = new PTTPrice();
            var data = pttPrice.GetOilPriceDatas();

            if (data.priceDate.Date == DateTime.Now.Date)
            {
                foreach (var im in data.priceData)
                {
                    // Console.WriteLine(im.oilTypeId);
                    if (im.nameEn == "Gasohol 95")
                    {
                        // Console.WriteLine(im.nameEn);
                        ConnDB connDb = new ConnDB();
                        connDb.postPriceOil(im.priceDate, im.price);
                        // price
                    }
                }
            }
        }
    }
}