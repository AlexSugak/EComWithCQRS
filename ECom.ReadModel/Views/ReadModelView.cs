using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	public abstract class ReadModelView
	{
		protected readonly IDtoManager _manager;

		public ReadModelView(IDtoManager manager)
		{
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
		}
	}
}
