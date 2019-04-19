using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SpearPay.Gateway;
using System;
using System.Linq;

namespace SpearPay
{
    public static class ServiceCollectionExtensions
    {
        /// <summary> 获取支付网关 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static T GetGateway<T>(this IServiceProvider provider, string appId) where T : IGateway
        {
            var gateways = provider.GetServices<IGateway>();
            var gateway = gateways.FirstOrDefault(t => t.Merchant.AppId == appId);
            return gateway == null ? default(T) : (T)gateway;
        }

        /// <summary> 添加Spear支付组件 </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSpearPay(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        /// <summary> 添加Spear支付组件,并指定单个支付网关 </summary>
        /// <param name="services"></param>
        /// <param name="implFunc"></param>
        /// <returns></returns>
        public static IServiceCollection AddSpearPay<T>(this IServiceCollection services, Func<IServiceProvider, T> implFunc)
            where T : class, IGateway
        {
            services.AddSpearPay();
            services.AddTransient<IGateway, T>(implFunc);
            return services;
        } 

        /// <summary> 添加Spear支付组件,并指定单个支付网关 </summary>
        /// <param name="services"></param>
        /// <param name="implFunc"></param>
        /// <returns></returns>
        public static IServiceCollection AddGateway<T>(this IServiceCollection services, Func<IServiceProvider, T> implFunc)
            where T : BaseGateway
        {
            services.AddTransient(implFunc);
            return services;
        }
    }
}
