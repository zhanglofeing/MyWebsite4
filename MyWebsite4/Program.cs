using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MyWebsite4
{
    /// <summary>
    /// 封装缓存框架
    /// 测试MemoryCache和RedisCache
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
