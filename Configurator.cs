using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Setup
{
	/// <summary>
	/// Contract for setting up a <see cref="IUnityContainer"/>
	/// from a configuration section and possibly from programmatic registrations.
	/// </summary>
	public abstract class Configurator
	{
		/// <summary>
		/// Configure the container by either reading 
		/// a configuration section or programmatically registering
		/// resources.
		/// </summary>
		/// <param name="configurationSectionName">The name of the configuration section.</param>
		/// <param name="unityContainer">The container to configure.</param>
		protected internal abstract void Configure(
			string configurationSectionName,
			IUnityContainer unityContainer);
	}
}
