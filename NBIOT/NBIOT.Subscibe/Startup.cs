using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Owin;
using log4net.Repository;
using log4net;
using System.IO;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace NBIOT.Subscibe
{
    public class Startup
    {
        public static ILoggerRepository Repository { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
            Log4netUtil.LogRepository = Repository;//类库中定义的静态变量
        }

        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }
}
