using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class OrderItemDetails : Dto
	{
		public string ID { get; set; }

		public string OrderId { get; set; }
		public int ItemId { get; set; }
		public string ProductUrl { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }

		public OrderItemDetails()
		{
		}

		public OrderItemDetails(
			string id, 
			string orderId,
			int itemId,
			string productUrl,
			string name,
			string description,
			decimal price,
			string imageUrl,
			string size,
			string color
			)
		{
			ID = id;
			OrderId = orderId;
			ItemId = itemId;
			ProductUrl = productUrl;
			Name = name;
			Description = description;
			Price = price;
			ImageUrl = imageUrl;
			Size = size;
			Color = color;
		}
	}

	public class OrderItemsView : 
		IHandle<ProductAddedToOrder>,
		IHandle<ItemRemovedFromOrder>
	{
		private IDtoManager _manager;

		public OrderItemsView(IDtoManager manager)
		{
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
		}

		public void Handle(ProductAddedToOrder e)
		{
			_manager.Add<OrderItemDetails>(new OrderItemDetails(
				GetItemCompositeId(e.Id, e.OrderItemId), 
				e.Id.GetId(), 
				e.OrderItemId.Id,
				e.ProductUri.ToString(), 
				e.Name, 
				e.Description, 
				e.Price, 
				e.ImageUri.ToString(), 
				e.Size, 
				e.Color));
		}

		public void Handle(ItemRemovedFromOrder e)
		{
			_manager.Delete<OrderItemDetails>(GetItemCompositeId(e.Id, e.OrderItemId));
		}

		private static string GetItemCompositeId(OrderId orderId, OrderItemId itemId)
		{
			return orderId.GetId() + "-" + itemId.GetId();
		}
	}
}
