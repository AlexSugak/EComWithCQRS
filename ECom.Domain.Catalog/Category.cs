using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using ECom.Messages;

namespace ECom.Domain.Catalog
{
	public class Category
	{
		private string _name;

		public Category()
		{
		}

		public Category(string name)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => name);

			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}
	}
}
