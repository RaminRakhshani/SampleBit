﻿using Bit.OwinCore;
using Microsoft.AspNetCore.Hosting;

namespace Wallet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            BitWebHost.CreateDefaultBuilder(args)
                .UseStartup<AppStartup>();
    }
}
