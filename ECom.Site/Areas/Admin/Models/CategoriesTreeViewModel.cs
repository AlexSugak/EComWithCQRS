using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel.Views;
using MvcContrib.Pagination;
using ECom.Utility;
using ECom.Messages;

namespace ECom.Site.Areas.Admin.Models
{
	public class CategoriesTreeViewModel
	{
		public CategoriesTreeViewModel(IEnumerable<CategoryNode> categories)
		{
			Argument.ExpectNotNull(() => categories);

			Roots = LoadTree(categories);
		}

		public IEnumerable<CategoriesTreeNodeViewModel> Roots { get; private set; }

		public CreateCategory CreateCategory { get { return new CreateCategory(); } }

		private IEnumerable<CategoriesTreeNodeViewModel> LoadTree(IEnumerable<CategoryNode> categories)
		{
			Dictionary<string, CategoriesTreeNodeViewModel> categoriesTree = new Dictionary<string, CategoriesTreeNodeViewModel>();

			foreach (var category in categories)
			{
				categoriesTree.Add(category.Name, new CategoriesTreeNodeViewModel(category));
			}

			foreach (var category in categories)
			{
				if (!String.IsNullOrWhiteSpace(category.ParentName))
				{
					categoriesTree[category.ParentName].ChildNodes.Add(categoriesTree[category.Name]);
				}
			}

			//return roots
			return categoriesTree.Values.Where(c => String.IsNullOrWhiteSpace(c.ParentName));
		}
	}

	public class CategoriesTreeNodeViewModel
	{
		public CategoriesTreeNodeViewModel(CategoryNode dto)
		{
			Name = dto.Name;
			ParentName = dto.ParentName;
			ChildNodes = new List<CategoriesTreeNodeViewModel>();
		}

		public string Name { get; set; }
		public string ParentName { get; set; }
		public List<CategoriesTreeNodeViewModel> ChildNodes { get; set; }

        public MoveCategory MoveCategoryCommand { get { return new MoveCategory(Name, null); } }
	}
}