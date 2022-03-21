using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using msOilPrice.Controllers;
using msOilPrice.Models;
using Newtonsoft.Json;

namespace msOilPrice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string html = string.Empty;
            string url = @"https://orapiweb.pttor.com/api/oilprice/LatestOilPrice";
           
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Accept = "*/*";
            request.ContentType = "application/json";
            
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                html = reader.ReadToEnd();
                var res = JsonConvert.DeserializeObject<fJson.Root>(html);
                List<fJson.PriceData> data = res.data.priceData;
            
                if (res.data.priceDate.Date == DateTime.Now.Date)
                {
                    foreach (var im in data)
                    {
                        // Console.WriteLine(im.oilTypeId);
                        if (im.nameEn == "Gasohol 95")
                        {
                            Console.WriteLine(im.nameEn);
                            ConnDB connDb = new ConnDB();
                            connDb.postPriceOil(im.priceDate, im.price);
                            // price
                        }
                    }
                }
            }
        }
    }
}