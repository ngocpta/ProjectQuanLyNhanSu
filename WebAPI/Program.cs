using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI
{
    public class Program
    {
        private readonly IConfiguration _configuration;
        public Program(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public static void Main(string[] args)
        {                  
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            //.UseUrls();
        .UseUrls("http://127.0.0.1:5000");


    }
}
