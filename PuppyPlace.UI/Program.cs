using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PuppyPlace.Data;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Ui
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConsoleMenu, ConsoleMenu>();
            services.AddSingleton<IMainMenu, MainMenu>();
            services.AddTransient<IPersonUi, PersonUi>();
            services.AddTransient<IPersonsRepository, PersonsRepository>();
            services.AddTransient<IDogsRepository, DogsRepository>();
            services.AddTransient<IAdoptionService, AdoptionService>();
            services.AddTransient<IDogsService, DogsService>();
            services.AddTransient<IPersonsService, PersonsService>();
            services.AddTransient<IDogsUi, DogsUi>();
            services.AddTransient<PuppyPlaceContext>();
            var serviceProvider = services.BuildServiceProvider();
            var menu = serviceProvider.GetService<IConsoleMenu>();
            await menu.Show();
        }
    }
}