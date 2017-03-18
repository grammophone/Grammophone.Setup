using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.Setup
{
	/// <summary>
	/// Thrown when there is an error during configuration.
	/// </summary>
	[Serializable]
	public class SetupException : ApplicationException
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The error message.</param>
		public SetupException(string message) : base(message) { }

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="inner">The inner exception causing the error.</param>
		public SetupException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// USed for serialization.
		/// </summary>
		protected SetupException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
