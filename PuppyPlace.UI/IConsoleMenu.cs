namespace PuppyPlace.Ui;

public interface IConsoleMenu
{
    Task Show();
    void ShowInvalidMessage();
    void ShowLoadingAnimation();
    void Quit();
}