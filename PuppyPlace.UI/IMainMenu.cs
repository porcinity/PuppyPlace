namespace PuppyPlace.Ui;

public interface IMainMenu
{
    Task Return();
    void ShowInvalidMessage();
    void Quit();
}