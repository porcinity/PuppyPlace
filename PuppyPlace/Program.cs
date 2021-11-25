namespace PuppyPlace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Prompt.SeedDogs();
            Prompt.SeedPersons();
            
            Prompt.ShowMainMenu();
        }
    }
}