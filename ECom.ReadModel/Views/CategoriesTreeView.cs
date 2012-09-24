using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using ECom.Messages;

namespace ECom.ReadModel.Views
{
	public class CategoryNode : Dto
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string ParentName { get; set; }

		public CategoryNode()
		{
		}

		public CategoryNode(string id, string name)
		{
			ID = id;
            Name = name;
            ParentName = String.Empty;
		}
	}

	public class CategoriesTreeView : 
		IHandle<CategoryCreated>,
		IHandle<CategoryMoved>
	{
		private IDtoManager _manager;

		public CategoriesTreeView(IDtoManager manager)
        {
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
        }

		public void Handle(CategoryCreated message)
		{
			_manager.Add(new CategoryNode(message.Name, message.Name));
		}

		public void Handle(CategoryMoved message)
		{
			_manager.Update<CategoryNode>(message.Name, c => c.ParentName = message.TargetCategory); 
		}
	}
}
