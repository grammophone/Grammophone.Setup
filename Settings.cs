using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Setup
{
	/// <summary>
	/// Used to resolve settings in configuration files.
	/// </summary>
	public class Settings : IDisposable
	{
		#region Private fields

		private readonly IUnityContainer diContainer;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="configurationSectionName">The name of a Unity configuration section.</param>
		/// <param name="configurator">configurator for the dependency injection container.</param>
		private Settings(string configurationSectionName, Configurator configurator)
		{
			if (configurationSectionName == null) throw new ArgumentNullException(nameof(configurationSectionName));

			this.ConfigurationSectionName = configurationSectionName;

			this.diContainer = new UnityContainer();

			configurator.Configure(configurationSectionName, diContainer);
		}

		/// <summary>
		/// Create and load settings using a <see cref="Configurator"/> of 
		/// type <typeparamref name="C"/>.
		/// </summary>
		/// <typeparam name="C">
		/// The type of configurator, derived from <see cref="Configurator"/> and
		/// having a default constructor.
		/// </typeparam>
		/// <param name="configurationSectionName">The name of a Unity configuration section.</param>
		public static Settings Load<C>(string configurationSectionName)
			where C : Configurator, new()
		{
			return new Settings(configurationSectionName, new C());
		}

		/// <summary>
		/// Create and load settings using the <see cref="DefaultConfigurator"/>.
		/// </summary>
		/// <param name="configurationSectionName">The name of a Unity configuration section.</param>
		public static Settings Load(string configurationSectionName)
		{
			return Load<DefaultConfigurator>(configurationSectionName);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The name of the configuration section being accessed.
		/// </summary>
		public string ConfigurationSectionName { get; }

		#endregion

		#region Public methods

		/// <summary>
		/// Resolve an instance registered for type <typeparamref name="T"/> under the default name.
		/// </summary>
		/// <typeparam name="T">The type for which the instance is registered.</typeparam>
		public T Resolve<T>() => diContainer.Resolve<T>();

		/// <summary>
		/// Resolve an instance registered for type <typeparamref name="T"/>
		/// with a given <paramref name="name"/>.
		/// </summary>
		/// <typeparam name="T">The type for which the instance is registered.</typeparam>
		/// <param name="name">The name of the instance.</param>
		public T Resolve<T>(string name) => diContainer.Resolve<T>(name);

		/// <summary>
		/// Resolve all instances registered for type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type for which the instances are registered.</typeparam>
		public IEnumerable<T> ResolveAll<T>() => diContainer.ResolveAll<T>();

		/// <summary>
		/// Check whether a type has been registered under the default name.
		/// </summary>
		/// <typeparam name="T">The type to check.</typeparam>
		public bool IsRegistered<T>() => diContainer.IsRegistered<T>();

		/// <summary>
		/// Check whether a type has been registered under a name.
		/// </summary>
		/// <typeparam name="T">The type to check.</typeparam>
		/// <param name="name">The name under which the type would be registered.</param>
		public bool IsRegistered<T>(string name) => diContainer.IsRegistered<T>(name);

		/// <summary>
		/// Get the collection of names under which a type is registered. The default name is returned as null.
		/// </summary>
		/// <typeparam name="T">The type to check.</typeparam>
		public IEnumerable<string> GetRegistrationNames<T>() => from r in diContainer.Registrations
																														where r.RegisteredType == typeof(T)
																														select r.Name;

		/// <summary>
		/// Resolve all instances registered under a non-default name for type <typeparamref name="T"/>
		/// and return them in a dictionary under their registration names.
		/// </summary>
		/// <typeparam name="T">The type for which the instances are registered.</typeparam>
		public IReadOnlyDictionary<string, T> ResolveAllToDictionary<T>()
			=> GetRegistrationNames<T>().Where(n => n != null).ToDictionary(n => n, n => Resolve<T>(n));

		/// <summary>
		/// Disposes the underlying dependency injection container.
		/// </summary>
		public void Dispose()
		{
			diContainer.Dispose();
		}

		#endregion
	}
}
