using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Setup
{
	/// <summary>
	/// Factory  to obtain collections of instances of type <typeparamref name="T"/>
	/// from a dependencies container.
	/// </summary>
	/// <typeparam name="T">The type of object to obtain.</typeparam>
	public class CollectionFactory<T>
	{
		#region Private fields

		private readonly Settings settings;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="settings">The dependencies container.</param>
		public CollectionFactory(Settings settings)
		{
			if (settings == null) throw new ArgumentNullException(nameof(settings));

			this.settings = settings;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Obtain a collection of objects of <typeparamref name="T"/> as defined in the
		/// dependencies container.
		/// </summary>
		public IEnumerable<T> Get() => settings.ResolveAll<T>();

		/// <summary>
		/// Obtain a dictionary of objects of <typeparamref name="T"/> as defined in the
		/// dependencies container, where the key is the name under which they are defined.
		/// </summary>
		public IReadOnlyDictionary<string, T> GetToDictionary() => settings.ResolveAllToDictionary<T>();

		/// <summary>
		/// Determine whether named definitions for objects of type <typeparamref name="T"/>
		/// exist in the dependencies container.
		/// </summary>
		public bool IsDefined() => settings.GetRegistrationNames<T>().Where(n => n != null).Any();

		#endregion
	}
}
