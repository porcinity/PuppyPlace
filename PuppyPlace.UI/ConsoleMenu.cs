namespace PuppyPlace.Ui;

public class ConsoleMenu : IConsoleMenu
{
    private readonly PersonUi PersonUi;
    private readonly DogsUi DogsUi;

    public ConsoleMenu(PersonUi personUi, DogsUi dogsUi)
    {
        PersonUi = personUi;
        DogsUi = dogsUi;
    }
        
    public async Task Show()
    {
        while (true)
        {
            Console.Clear();
            ShowMainMenuText();
            var userChoice = Console.ReadKey();
            switch (userChoice.Key)
            {
                case ConsoleKey.D1:
                    await PersonUi.AddPersonPrompt();
                    break;
                case ConsoleKey.D2:
                    await DogsUi.AddDog();
                    break;
                case ConsoleKey.D3:
                    await PersonUi.ShowPersons();
                    break;
                case ConsoleKey.D4:
                    await DogsUi.ShowDogs();
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
    
    void ShowMainMenuText()
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
    
    public void ShowInvalidMessage()
    {
        Console.Clear();
        Console.WriteLine("Invalided choice.");
        Thread.Sleep(1000);
    }
    
    public void ShowLoadingAnimation()
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
    
    public void Quit()
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }
}