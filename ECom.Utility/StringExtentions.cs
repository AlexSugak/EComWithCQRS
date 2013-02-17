using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.Utility
{
	public static class StringExtentions
	{
		/// <summary>
		/// Makes StringInCamelCase to be splitted to String In Camel Case 
		/// </summary>
		public static string Wordify(this string str)
		{
			var newString = new StringBuilder();

			bool first = true;
			bool wasUpper = false;

			foreach (char c in str)
			{
				string delimeter = first ? String.Empty : " ";

				newString.Append(char.IsUpper(c) && !wasUpper ? delimeter + c : c.ToString());
				wasUpper = char.IsUpper(c);

				first = false;
			}

			return newString.ToString();
		}

        /// <summary>
        /// Reverses string value
        /// </summary>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
	}
}
