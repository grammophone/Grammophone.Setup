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

		private MRUCache<string, Settings> settingsCache;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="cacheSize">Size of the cache of the settings per configuration section.</param>
		public SettingsFactory(int cacheSize = 2048)
		{
			settingsCache = 
				new MRUCache<string, Settings>(Settings.Load<C>, cacheSize);
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
		/// Flushes and disposes all settings in the cache.
		/// </summary>
		public void Dispose()
		{
			FlushAll();
		}

		/// <summary>
		/// Attempt to flush and dispose existing cached settings under a configuration section name.
		/// If the settings existed in the cache, a subsequent call to <see cref="Get(string)"/>
		/// for the configuration section name
		/// will cause the settings to be reloaded and reconfigured.
		/// </summary>
		/// <param name="configurationSectionName">The name of the configuration section.</param>
		/// <returns>Returns true if the settings were found in the cache and removed, else returns false.</returns>
		public bool Flush(string configurationSectionName)
		{
			if (configurationSectionName == null) throw new ArgumentNullException(nameof(configurationSectionName));

			return settingsCache.Remove(configurationSectionName);
		}

		/// <summary>
		/// Flushes and removes all settings in the cache.
		/// Subsequent calls to <see cref="Get(string)"/>
		/// will cause the settings to be reloaded and reconfigured.
		/// </summary>
		public void FlushAll()
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
