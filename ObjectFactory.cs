using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Grammophone.Setup
{
	/// <summary>
	/// Factory to obtain objects of type <typeparamref name="T"/>
	/// from a dependencies container.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection to obtain.</typeparam>
	public class ObjectFactory<T>
	{
		#region Private fields

		private readonly Settings settings;

		private readonly string name;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="settings">The dependencies container.</param>
		[InjectionConstructor]
		public ObjectFactory(Settings settings)
			: this(settings, null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="settings">The dependencies container.</param>
		/// <param name="name">The name of the configuration definition for the object of type <typeparamref name="T"/> or null for unnamed or implied definition.</param>
		public ObjectFactory(Settings settings, string name)
		{
			if (settings == null) throw new ArgumentNullException(nameof(settings));

			this.settings = settings;
			this.name = name;
		}

		#endregion

		#region Public merthods

		/// <summary>
		/// Obtain an object of type <typeparamref name="T"/> as defined or implied from the
		/// dependencies container.
		/// </summary>
		public T Get() => 
			name != null ? settings.Resolve<T>(name) : settings.Resolve<T>();

		/// <summary>
		/// Determine whether explicit definition for objects of type <typeparamref name="T"/>
		/// exists in the dependencies container.
		/// </summary>
		public bool IsDefined() =>
			name != null ? settings.IsRegistered<T>(name) : settings.IsRegistered<T>();

		#endregion
	}
}
