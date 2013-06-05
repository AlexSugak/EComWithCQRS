using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
    public interface IProjection<T>
      where T : Dto
    {
    }

	public abstract class Projection
	{
		protected readonly IDtoManager _manager;

		public Projection(IDtoManager manager)
		{
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
		}
	}
}
