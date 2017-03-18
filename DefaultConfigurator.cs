using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Setup
{
	/// <summary>
	/// Configures a container using the contents of a Unity configuration section.
	/// Subclass and override method <see cref="Configure(string, IUnityContainer)"/> 
	/// to add programmatic registrations to the container.
	/// </summary>
	public class DefaultConfigurator : Configurator
	{
		/// <summary>
		/// Configure the container by reading a configuration section.
		/// </summary>
		/// <param name="configurationSectionName">The name of the Unity configuration section.</param>
		/// <param name="unityContainer">The container to configure.</param>
		protected internal override void Configure(
			string configurationSectionName,
			IUnityContainer unityContainer)
		{
			if (configurationSectionName == null) throw new ArgumentNullException(nameof(configurationSectionName));
			if (unityContainer == null) throw new ArgumentNullException(nameof(unityContainer));

			var configurationSection = ConfigurationManager.GetSection(configurationSectionName);

			if (configurationSection == null)
				throw new SetupException(
					$"The '{configurationSectionName}' configuration section is not defined.");

			var unityConfigurationSection = configurationSection as UnityConfigurationSection;

			if (unityConfigurationSection == null)
				throw new SetupException(
					$"The '{configurationSectionName}' configuration section is not a Unity one.");

			unityContainer.LoadConfiguration(unityConfigurationSection);
		}
	}
}
