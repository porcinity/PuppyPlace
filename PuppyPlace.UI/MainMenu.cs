namespace PuppyPlace.Ui;

public class MainMenu : IMainMenu
{
    private readonly IConsoleMenu _consoleMenu;

    public MainMenu(IConsoleMenu consoleMenu)
    {
        _consoleMenu = consoleMenu;
    }

    public async Task Return()
    {
        await _consoleMenu.Show();
    }

    public void ShowInvalidMessage()
    {
        _consoleMenu.ShowInvalidMessage();
    }

    public void Quit()
    {
        _consoleMenu.Quit();
    }
}