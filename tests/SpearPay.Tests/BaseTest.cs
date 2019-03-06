using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace SpearPay.Tests
{
    public abstract class BaseTest
    {
        protected IServiceProvider Provider { get; private set; }
        protected readonly IConfigurationRoot Config;
        protected ILogger Logger { get; private set; }

        protected virtual void MapServices(IServiceCollection services) { }

        protected BaseTest()
        {
            Config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            Init();
        }

        private void Init()
        {
            var services = new ServiceCollection()
                .AddLogging(b =>
                {
                    b.SetMinimumLevel(LogLevel.Debug);
                    b.AddConsole();
                })
                .AddSpearPay();
            MapServices(services);
            Provider = services.BuildServiceProvider();
            Logger = Provider.GetService<ILoggerFactory>().CreateLogger(GetType());
        }

        protected T GetService<T>()
        {
            return Provider.GetService<T>();
        }

        protected T Gateway<T>(string appId) where T : IGateway
        {
            return Provider.GetGateway<T>(appId);
        }

        protected void Print(object obj)
        {
            string msg;
            switch (obj)
            {
                case null:
                    msg = "NULL";
                    break;
                case string _:
                case int _:
                case bool _:
                    msg = obj.ToString();
                    break;
                default:
                    msg = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });
                    break;
            }

            Logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} -> {Environment.NewLine} {msg}");
        }
    }
}
