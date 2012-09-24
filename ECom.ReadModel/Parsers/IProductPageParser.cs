using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECom.ReadModel.Parsers
{
	public interface IProductPageParser
	{
		ProductPageInfo Parse(Uri productUri);
	}
}
