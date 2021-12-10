using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PuppyPlace.Ui
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                    {
                        services.AddTransient<IPersonUi, PersonUi>();
                        services.AddTransient<IDogsUi, DogsUi>();
                        services.AddTransient<IConsoleMenu, ConsoleMenu>();
                    }
                );
            await ConsoleMainMenu.Show();
        }
    }
}