using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite4.Configs;
using MyWebsite4.Services;

namespace MyWebsite4
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            HostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddMvc();

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(this.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{this.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            CacheConfig _cacheProvider = new CacheConfig();
            config.GetSection(nameof(CacheConfig)).Bind(_cacheProvider);

            if (_cacheProvider.RedisEnable)
            {
                //Use Redis
                services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
                {
                    Configuration = _cacheProvider.ConnectionString,
                    InstanceName = _cacheProvider.InstanceName
                }));
            }
            else
            {
                //Use MemoryCache
                services.AddSingleton<IMemoryCache>(factory => new MemoryCache(new MemoryCacheOptions()));
                services.AddSingleton<ICacheService, MemoryCacheService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    await context.Response.WriteAsync("Server is running!");
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
