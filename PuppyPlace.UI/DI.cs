using PuppyPlace.Data;
using PuppyPlace.Service;

namespace PuppyPlace.Ui;

public static class DI
{
    public static readonly PuppyPlaceContext PuppyPlaceContext = new ();

    public static readonly PersonUi PersonUi = new PersonUi(
        new PersonsService(PuppyPlaceContext),
        new DogsService(DI.PuppyPlaceContext, new PersonsService(PuppyPlaceContext)));

    public static readonly DogsUi DogsUi =
        new DogsUi(new DogsService(DI.PuppyPlaceContext, new PersonsService(DI.PuppyPlaceContext)),
            new PersonsService(DI.PuppyPlaceContext));
}