using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class CategoryDetailsDto : Dto
	{
		public string ID { get; set; }

		public CategoryDetailsDto()
		{
		}

		public CategoryDetailsDto(string name)
		{
			ID = name;
		}
	}

	public class CategoryDetailsView :
		IHandle<CategoryRemoved>,
		IHandle<CategoryCreated>
	{
		private IDtoManager _manager;

		public CategoryDetailsView(IDtoManager manager)
        {
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
        }

		public void Handle(CategoryCreated message)
		{
			_manager.Add<CategoryDetailsDto>(new CategoryDetailsDto(message.Name));
		}

		public void Handle(CategoryRemoved message)
		{
			_manager.Delete<CategoryDetailsDto>(message.Name);
		}
	}
}
