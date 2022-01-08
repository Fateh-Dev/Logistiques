using System.Linq;
using Microsoft.Extensions.Configuration;

namespace System
{
    public static class EnvirementSettingsExtension
    {
        public static string CollectionString()
        {
            //Get Database Connection 
            //Environment.SetEnvironmentVariable("DATABASE_URL", "postgres://ojunflcdtkendq:be88fc41989efe90fda30380a6dae8ec9259cc19f237f11135b68a52371a6ce5@ec2-54-235-146-51.compute-1.amazonaws.com:5432/d8lhbkcpmedcej");
            string _connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (!string.IsNullOrEmpty(_connectionString))
            {
                if (_connectionString.ToLower().StartsWith("postgres"))
                {
                    _connectionString.Replace("//", "");

                    char[] delimiterChars = { '/', ':', '@', '?' };
                    string[] strConn = _connectionString.Split(delimiterChars);
                    strConn = strConn.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    // Config.User = strConn[1];
                    // Config.Pass = strConn[2];
                    // Config.Server = strConn[3];
                    // Config.Database = strConn[5];
                    // Config.Port = strConn[4];
                    // "Default": "sslmode=Require;Trust Server Certificate=true;Timeout=1000;Server=ec2-54-75-225-52.eu-west-1.compute.amazonaws.com;Database=dbsmrheht2qpjg;User Id=hhcgjxavfjaywf;Password=14779ea9cd065c53bd987b2db72d24bc717b549a0608869d756ac2f06377c61d"

                    var ret = "Server=" + strConn[3] + ";port=" + strConn[4] + ";Database=" + strConn[5] + ";User Id=" + strConn[1] + ";Password=" + strConn[2] + ";sslmode=Require;Trust Server Certificate=true;Timeout=1000";
                    Console.WriteLine(ret);
                    return ret;
                }
            }
            return null;

        }
        public static void SetHostEvirementSettings(this IConfiguration _appConfiguration, string appName = "FREETIME")
        {
            var cs = CollectionString();
            if (!string.IsNullOrWhiteSpace(cs))
            {

                _appConfiguration["ConnectionStrings:Default"] = cs;
                System.Console.WriteLine(_appConfiguration.GetConnectionString("Default"));
            }
            string url = Environment.GetEnvironmentVariable("APP_URL");
            if (!string.IsNullOrWhiteSpace(url))
            {
                _appConfiguration[appName + ":RootUrl"] = url;
                _appConfiguration["App:SelfUrl"] = url;
                _appConfiguration["App:CorsOrigins"] = _appConfiguration["App:CorsOrigins"] + ',' + url;
                _appConfiguration["AuthServer:Authority"] = url;
                _appConfiguration[appName + ":ValidIssuer"] = url;
            }

        }
    }
}