namespace PuppyPlace.UI;

public static class ConsoleMainMenu
{
    private static readonly PersonUI _personUi = DI.PersonUi;
    private static readonly DogsUI _dogsUi = DI.DogsUi;
    public static async Task Show()
    {
        while (true)
        {
            Console.Clear();
            ShowMainMenuText();
            var userChoice = Console.ReadKey();
            switch (userChoice.Key)
            {
                case ConsoleKey.D1:
                    await _personUi.AddPersonPrompt();
                    break;
                case ConsoleKey.D2:
                    await _dogsUi.AddDog();
                    break;
                case ConsoleKey.D3:
                    await _personUi.ShowPersons();
                    break;
                case ConsoleKey.D4:
                    await _dogsUi.ShowDogs();
                    break;
                case ConsoleKey.Q:
                    Quit();
                    break;
                default:
                    ShowInvalidMessage();
                    continue;
            }
            break;
        }
    }
    
    static void ShowMainMenuText()
    {
        Console.WriteLine(Figgle.FiggleFonts.Doom.Render("Puppy Place"));
        Console.WriteLine("\nM  A  I  N      M  E  N  U" +
                          "\n==========================" +
                          "\n");
        Console.WriteLine("What would you like to do?" +
                          "\n1 - Add new Person" +
                          "\n2 - Add new Dog" +
                          "\n3 - Show list of People" +
                          "\n4 - Show list of Dogs" +
                          "\n\n(Press q to quit)");
    }
    
    public static void ShowInvalidMessage()
    {
        Console.Clear();
        Console.WriteLine("Invalided choice.");
        Thread.Sleep(1000);
    }
    
    public static void ShowLoadingAnimation()
    {
        var dots = "";
        
        for (int i = 0; i < 5; i++)
        {
            Console.Clear();
            dots = dots + ".";
            Console.WriteLine("Loading" + dots);
            Thread.Sleep(50);
        }
    }
    
    public static void Quit()
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }
}