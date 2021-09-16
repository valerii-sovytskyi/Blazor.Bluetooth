using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Bluetooth
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add <see cref="IBluetoothNavigator"/> to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddBluetoothNavigator(this IServiceCollection services)
        {
            return services.AddTransient<IBluetoothNavigator, BluetoothNavigator>();
        }
    }
}
