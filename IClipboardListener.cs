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

		/// <summary>
		/// Copy data to the clipboard without having the listener react to it.
		/// </summary>
		/// <param name="data">Data to be copied.</param>
		void CopySafe(string data);
	}
	
	public sealed class ClipboardUpdatedEventArgs : EventArgs
	{
		public string Data { get; set; }
	}
}