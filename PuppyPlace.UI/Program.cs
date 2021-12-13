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
            await ConsoleMainMenu.Show();
        }
    }
}