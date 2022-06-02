using AppBanco.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBanco
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //bool teste = Cadastro("aa", "aa", "bb", "bb");
            CreateHostBuilder(args).Build().Run();
            
        }

        public static bool LoginOk = false;
        public static string LoggedEmail = "";

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }

}
