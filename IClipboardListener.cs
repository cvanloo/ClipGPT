using System;

namespace ClipGPT
{
	/// <summary>
	/// Implements methods for listening to clipboard events.
	/// </summary>
	public interface IClipboardListener
	{
		event EventHandler<ClipboardUpdatedEventArgs> ClipboardUpdated;

		/// <summary>
		/// Register the listener.
		/// </summary>
		void Register();
		
		/// <summary>
		/// Deregister the listener.
		/// </summary>
		void Deregister();
	}
	
	public sealed class ClipboardUpdatedEventArgs : EventArgs
	{
		public string Data { get; set; }
	}
}