using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;

namespace ECom.Domain.Aggregates.Catalog
{
	public class CategoryTreeNode
	{
		private string _name;
		private string _parentName;

		public CategoryTreeNode(string name, string parentCategoryName)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => name);

			_name = name;
			_parentName = parentCategoryName;
		}

		public string Name
		{
			get { return _name; }
		}

		public string ParentName
		{
			get { return _parentName; }
		}

		public bool IsRoot
		{
			get { return String.IsNullOrWhiteSpace(_parentName); }
		}

		public void SetParent(string newParent)
		{
			//if parent is null then this one of the root categories
			_parentName = newParent;
		}
	}
}
