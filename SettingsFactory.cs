using Grammophone.Caching;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Setup
{
	/// <summary>
	/// Factory for providing <see cref="Settings"/> corresponding to configuration sections.
	/// </summary>
	/// <typeparam name="C">
	/// The type of configurator to use. Must derive from <typeparamref name="C"/>
	/// and should have a public default constructor.
	/// </typeparam>
	public class SettingsFactory<C> : IDisposable
		where C : Configurator, new()
	{
		#region Private fields

		private DisposableMRUCache<string, Settings> settingsCache;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="cacheSize">Size of the cache of the settings per configuration section.</param>
		public SettingsFactory(int cacheSize = 2048)
		{
			settingsCache = 
				new DisposableMRUCache<string, Settings>(Settings.Load<C>, cacheSize);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get the settings which correspond to a configuration section name.
		/// </summary>
		/// <param name="configurationSectionName">The name of the configuraton section.</param>
		public Settings Get(string configurationSectionName)
		{
			if (configurationSectionName == null) throw new ArgumentNullException(nameof(configurationSectionName));

			return settingsCache.Get(configurationSectionName);
		}

		/// <summary>
		/// Disposes all settings in the cache.
		/// </summary>
		public void Dispose()
		{
			settingsCache.Clear();
		}

		#endregion
	}

	/// <summary>
	/// Factory for providing <see cref="Settings"/> corresponding to configuration sections
	/// and initializing them using the <see cref="DefaultConfigurator"/>.
	/// </summary>
	public class SettingsFactory : SettingsFactory<DefaultConfigurator>
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="cacheSize">Size of the cache of the settings per configuration section.</param>
		public SettingsFactory(int cacheSize = 4096) : base(cacheSize) { }
	}
}
