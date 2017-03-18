# Grammophone.Setup
This library abstracts the Unity dependency injection framework and helps discriminating between the phase of loading 
and setting up the container and the phase of using it to resolve instances.

Loading and setting up a container is taken care by `Configurator` descendants by implementing the method `Configure`.
The `DefaultConfigurator` sets up the container by loading a Unity configuration section of the
given name defined in your main application configuration file. Note that this can easily point to a different file
outside your main .config file by using the `configSource` attribute. This facilitates housekeeping and build transforms.
If you wish to add programmatic setup after loading the configuration, subclass `DefaultConfigurator`
and add your own setup in method `Configure` after calling the base implementation.
If you wish to have programmatic-only setup, derive from `Configurator` directly.

Resolving of instances is performed by the `Settings` class. To load and use the `Settings` class directly,
use the static methods `Create`. The first overload allows specification of the `Configurator`
to use while the second one uses the `DefaultConfigurator`.

In more advanced scenarios, one might require caching of `Settings` or having the same component
running multiple instances concurrently with different settings. For example, three instances of an image
resizing component for an art website might be used with three configurations, one for producing thumbnails,
one for low resolutions and one for high resolutions, all defined in their respective configuration sections.
Use `SettingsFactory<C>` with configurator `C` or `SettingsFactory` with `DefaultConfigurator` to load
and cache settings of the same type by the name of their configuration sections.

Both `Settings` and `SettingsFactory` implement `IDisposable`. When using `Settings` as stand-alone,
you are responsible for disposing it.
When you it via `SettingsFactory`, do not dispose `Settings`, dispose the `SettingsFactory` instead.

This library requires at least Visual Studio 2015 to compile and has project reference to
[Grammophone.Caching](https://github.com/grammophone/Grammophone.Caching) library,
which must reside in a sibling directory.
