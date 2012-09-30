﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ECom.Utility;

namespace ECom.Messages
{
	[Serializable]
	public class EmailAddress
	{
		private readonly string _address;
		private readonly string _name;

		public EmailAddress(string address, string name = null)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => address);
			Argument.Expect(() => IsValidEmail(address), "address", String.Format(CultureInfo.InvariantCulture, "'{0}' is not valid email address", address));

			_address = address;
			_name = name;
		}

		public string Address 
		{
			get 
			{
				if (!String.IsNullOrWhiteSpace(_name))
				{
					return String.Format(CultureInfo.InvariantCulture, "{0} <{1}>", _name, _address);
				}

				return _address;
			}
		}

		public string RawAddress
		{
			get { return _address; }
		}

		private static bool IsValidEmail(string email)
		{
			// source: http://thedailywtf.com/Articles/Validating_Email_Addresses.aspx
			Regex rx = new Regex(
			@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
			return rx.IsMatch(email);
		}
	}
}
