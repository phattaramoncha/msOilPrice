using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using msOilPrice.Models;
using Newtonsoft.Json;

namespace msOilPrice.Controllers
{
    public class PTTPrice
    {
        private string Url;

        public PTTPrice()
        {
            GetUrl();
        }

        public void GetUrl()
        {
            var appSettings = ConfigurationManager.AppSettings;
            Url = appSettings["api_ptt"];
        }

        public fJson.Data GetOilPriceData()
        {
            try
            {
                string html = string.Empty;
                // string url = @"https://orapiweb.pttor.com/api/oilprice/LatestOilPrice";

                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(Url);
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
                sendtoEmail.PostMail(e.ToString());
                throw;
            }
        }
    }
}