using System;
using System.IO;
using System.Net;
using System.Text;
using msOilPrice.Models;
using Newtonsoft.Json;

namespace msOilPrice.Controllers
{
    public class PTTPrice
    {
        public fJson.Data GetOilPriceDatas()
        {
            try
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
                    fJson.Data data = res.data;
                    return data;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                SendtoEmail sendtoEmail = new SendtoEmail();
                sendtoEmail.postMail(e.ToString());
                throw;
            }
        }
    }
}