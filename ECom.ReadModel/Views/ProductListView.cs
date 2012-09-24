using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using SubSonic.Repository;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	public class ProductList : Dto
	{
		public string ID { get; set; }
		public decimal Price { get; set; }
		public string Name { get; set; }
        public string Category { get; set; }

		public ProductList()
		{
		}

		public ProductList(string id, decimal price, string name)
		{
			ID = id;
			Price = price;
			Name = name;
            Category = String.Empty;
		}
	}

	public class ProductListView : 
		IHandle<ProductAdded>, 
		IHandle<ProductPriceChanged>,
		IHandle<ProductRemoved>,
        IHandle<ProductAddedToCategory>
    {
		private IDtoManager _manager;

        public ProductListView(IDtoManager manager)
        {
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
        }

        public void Handle(ProductAdded message)
        {
            _manager.Add(new ProductList(message.Id.GetId(), message.Price, message.Name));
        }

		public void Handle(ProductPriceChanged message)
		{
            _manager.Update<ProductList>(message.Id, p => p.Price = message.NewPrice); 
		}

		public void Handle(ProductRemoved message)
		{
            _manager.Delete<ProductList>(message.Id); 
		}

        public void Handle(ProductAddedToCategory message)
        {
            _manager.Update<ProductList>(message.Id, p => p.Category = message.CategoryName); 
        }
    }
}
