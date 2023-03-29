using System.Threading.Tasks;

namespace ClipGPT
{
	/// <summary>
	/// Implements methods to send requests to ChatGPT.
	/// </summary>
	public interface IAskGpt
	{
		/// <summary>
		/// Ask ChatGPT something.
		/// </summary>
		/// <param name="prompt">The prompt to send to ChatGPT.</param>
		/// <returns>ChatGPT's answer.</returns>
		Task<string> Prompt(string prompt);
	}
}