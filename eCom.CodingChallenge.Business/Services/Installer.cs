using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace eCom.CodingChallenge.Business.Services
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; }

        public ServiceAttribute(ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
        }
    }

    public static class Installer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetCustomAttribute(typeof(ServiceAttribute)) is ServiceAttribute serviceAttribute)
                {
                    string lookupName = $"I{type.Name}";
                    var serviceInterface = type.GetInterface(lookupName)!;

                    if (serviceInterface == null)
                    {
                        throw new NullReferenceException($"Type: [{lookupName}] was not a interface on class [{type.FullName}]");
                    }

                    switch (serviceAttribute.ServiceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(serviceInterface, type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(serviceInterface, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(serviceInterface, type);
                            break;
                    }
                }
            }

            return services;
        }
    }
}
