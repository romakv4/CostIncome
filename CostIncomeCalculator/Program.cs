#pragma warning disable 1591
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CostIncomeCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://localhost:5000", "http://192.168.1.195:5000")
                .UseStartup<Startup>();
    }
}
