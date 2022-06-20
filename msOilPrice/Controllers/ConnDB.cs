using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace msOilPrice.Controllers
{
    public class ConnDB
    {
        private string _connectSting; //= ConfigurationManager.AppSetting["crm_db"];

        public ConnDB()
        {
            GetConnString();
        }

        public void GetConnString()
        {
            var appSettings = ConfigurationManager.AppSettings;
            _connectSting = appSettings["crm_db"];
        }

        public void PostPriceOil(DateTime priceDate, double price)
        {
            using (var myConn = new SqlConnection(_connectSting))
            {
                using (var myCmd = new SqlCommand("POST_OilPrice", myConn))
                {
                    try
                    {
                        myConn.Open();
                        myCmd.CommandType = CommandType.StoredProcedure;
                        myCmd.Parameters.AddWithValue("@Oil_date", ((object) priceDate) ?? DBNull.Value);
                        myCmd.Parameters.AddWithValue("@Oil_Price", ((object) price) ?? DBNull.Value);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = myCmd;
                        var ret = new DataTable();
                        da.Fill(ret);
                        myConn.Close();
                        myConn.Dispose();
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
    }
}