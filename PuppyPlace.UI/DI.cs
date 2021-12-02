using PuppyPlace.Data;
using PuppyPlace.Service;

namespace PuppyPlace.UI;

public static class DI
{
    public static readonly PuppyPlaceContext PuppyPlaceContext = new ();

    public static readonly PersonUI PersonUi = new PersonUI(new PersonsService(PuppyPlaceContext), new DogsService(DI.PuppyPlaceContext, new PersonsService(PuppyPlaceContext)));

    public static readonly DogsUI DogsUi =
        new DogsUI(new DogsService(DI.PuppyPlaceContext, new PersonsService(DI.PuppyPlaceContext)),
            new PersonsService(DI.PuppyPlaceContext));
}