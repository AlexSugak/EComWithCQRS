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
		protected readonly IReadModelFacade _readModel;

		public ReadModelView(IDtoManager manager, IReadModelFacade readModel)
		{
			Argument.ExpectNotNull(() => manager);
			Argument.ExpectNotNull(() => readModel);

			_manager = manager;
			_readModel = readModel;
		}
	}
}
