using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NBIOT.Common;

namespace NBIOT.Subscibe
{
    class Program
    {
        static void Main(string[] args)
        {
            HandleLog.WriteServerLog += (log) =>
            {
                Log4netUtil.Info(log);
                Console.WriteLine(log);
            };

            var url = "http://+:9999";

            var host = new WebHostBuilder()
                      .UseKestrel()
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseStartup<Startup>()
                      .UseUrls(url)
                      .Build();

            using (host)
            {
                host.Run();

                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");

                Console.ReadKey();
            }
        }
    }
}
