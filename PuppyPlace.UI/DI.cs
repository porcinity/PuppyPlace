using PuppyPlace.Data;

namespace PuppyPlace.UI;

public static class DI
{
    public static readonly PuppyPlaceContext PuppyPlaceContext = new ();
    
    public static readonly PersonUI PersonUi = new PersonUI();
    public static readonly DogsUI DogsUi = new DogsUI();
}