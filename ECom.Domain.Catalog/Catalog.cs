using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Domain.Catalog.Exceptions;
using ECom.Domain.Exceptions;
using ECom.Messages;
using ECom.Utility;

namespace ECom.Domain.Catalog
{
	public class Catalog : AggregateRoot<CatalogId>
	{
		public static CatalogId MainCatalogId = new CatalogId(new Guid("CAA57ABC-68A8-4FDA-8300-295BDEE355C8"));

		private Dictionary<string, Category> _categories = new Dictionary<string, Category>();
		private Dictionary<string, CategoryTreeNode> _tree = new Dictionary<string, CategoryTreeNode>();

		public Catalog()
		{
		}

		public override CatalogId Id
		{
			get { return MainCatalogId; }
		}

		public void AddCategory(string name)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => name);

			if(_categories.ContainsKey(name))
			{
				throw new EntityAlreadyExistsException(String.Format("Category with the name '{0}' already exists.", name));
			}

			ApplyChange(new CategoryCreated(Id, name));
		}

		private void Apply(CategoryCreated e)
		{
			var category = new Category(e.Name);
			_categories.Add(category.Name, category);
			_tree.Add(category.Name, new CategoryTreeNode(category.Name, null));
		}

		public void MoveCategory(string categoryName, string targetCategoryName)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => categoryName);
			Argument.ExpectNotNullOrWhiteSpace(() => targetCategoryName);

			Argument.Expect(() => _categories.ContainsKey(categoryName), "categoryName", String.Format("Category '{0} ' not found", categoryName));
			Argument.Expect(() => _categories.ContainsKey(targetCategoryName), "targetCategoryName", String.Format("Category '{0} ' not found", targetCategoryName));

			//check circular references
			var parent = targetCategoryName;
			while (!String.IsNullOrWhiteSpace(parent))
			{
				if (parent.Equals(categoryName))
				{
					throw new CircularCategoryReferenceDetectedException("Detected circular category reference");
				}

                parent = _tree[parent].ParentName;
			}

			ApplyChange(new CategoryMoved(Id, categoryName, targetCategoryName));
		}

		private void Apply(CategoryMoved e)
		{
			_tree[e.Name].SetParent(e.TargetCategory);
		}
	}
}
