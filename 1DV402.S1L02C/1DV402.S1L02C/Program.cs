using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace _1DV402.S1L02C
{
	class DrawDiamond	
	{
		// Declare a resource manager to retrieve resources in all class methods.
		static ResourceManager rm;
		const int MAX_ASTERISK = 79;
	
		static void Main(string[] args)
		{
			// Create a resource manager to retrieve resources.
			rm = new ResourceManager("_1DV402.S1L02C.Strings", Assembly.GetExecutingAssembly());
			byte waist = 0;
			do
			{
				waist = ReadOddByte(rm.GetString("OddNumberAsterisk_Prompt"));
				RenderDiamond(waist);

			} while (IsContinuing());
		}
		/// <summary>
		/// Prints a message and expects a key respons from the user.
		/// </summary>
		/// <returns>Returns true if user input is anything but ESC</returns>
		private static bool IsContinuing()
		{ 
			ViewMessage(rm.GetString("Continue_Prompt"), false);
			ConsoleKeyInfo cki = Console.ReadKey(true);
			Console.Clear();
			return (cki.Key != ConsoleKey.Escape);
		}
		/// <summary>
		/// Read a number from user/screen which must be odd.
		/// </summary>
		/// <param name="prompt"></param>
		/// <param name="maxWaist"></param>
		/// <returns>The odd number entered by the user.</returns>
		private static byte ReadOddByte(string prompt = null, byte maxWaist = MAX_ASTERISK)
		{
			byte waist = 0;
			do
			{
				try
				{
					Console.Write(prompt, maxWaist);
					waist = byte.Parse(Console.ReadLine());

					// Check input (waist) is less or equal than max value and odd	
					if ((waist > maxWaist) || ((waist % 2) != 1))
					{
						ViewMessage(string.Format(DrawDiamond.rm.GetString("Error_Message"), maxWaist), true);
					}
				}
				catch
				{
					ViewMessage(string.Format(DrawDiamond.rm.GetString("Error_Message"), maxWaist), true);
				}
			}
			while ((waist > maxWaist) || ((waist % 2) != 1));	// keep going until a valid value is entered 
			return waist;	
		}
		/// <summary>
		/// Calculates and draws a diamond shaped figure on screen.
		/// Prameter maxWaist describes the waist size of the diamond i.e. how broad it is
		/// </summary>
		/// <param name="maxWaist"></param>
		private static void RenderDiamond(byte maxWaist)
		{
			for (int i = 0; i < (maxWaist - 1) / 2; i++)	// Upper half, start with one * and finish 
				RenderRow(maxWaist, i * 2 + 1);				// with maxCount minus one on each side

			RenderRow(maxWaist, maxWaist);					// middle row, all *

			for (int i = (maxWaist - 1) / 2; i > 0; i--)	// lower half, start with maxCount minus one on each side
				RenderRow(maxWaist, i * 2 - 1);				// and finish with just one in the middle
		}
		/// <summary>
		/// Calculate and draw row of asterisks defined by the two parameters.
		/// asteriskCount determines how many asterisks that should be drawn 
		/// centered in relation to parameter maxWaist i.e. if maxWaist=7 and astriskCount=3 
		/// the row starts with two spaces, followed by three asterisks and ends with two more spaces.
		/// </summary>
		/// <param name="maxWaist"></param>
		/// <param name="asteriskCount"></param>
		private static void RenderRow(int maxWaist, int asteriskCount)
		{
			int index = (maxWaist - asteriskCount) / 2;	// How many spaces before first *

			for (int i = 0; i < index; i++)						// step spaces			
				Console.Write(" ");

			for (int i = index; i < index + asteriskCount; i++)	// draw calculated number of *, given as parameter
				Console.Write("*");

			Console.WriteLine();		// line feed
		}
		/// <summary>
		/// Prints a message given as a parameter on the screen. 
		/// Backgorund color is red if determined to be an error (second parameter), otherwise dark green background color.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="isError"></param>
		private static void ViewMessage(string message, bool isError = false)
		{
			Console.WriteLine("");
			if (isError)
				Console.BackgroundColor = ConsoleColor.Red;
			else
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("");
		}
	}
}
