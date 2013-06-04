using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
    [Serializable]
    public class OrderItems : Dto
    {
        public List<OrderItemDetails> Items { get; set; }

        public OrderItems()
        {
        }
    }

	[Serializable]
	public class OrderItemDetails : Dto
	{
		public string ID { get; set; }

		public int OrderId { get; set; }
		public int ItemId { get; set; }
		public string ProductUrl { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public int Quantity { get; set; }
		public decimal Total { get; set; }

		public OrderItemDetails()
		{
		}

		public OrderItemDetails(
			OrderId orderId,
			OrderItemId itemId,
			Uri productUrl,
			string name,
			string description,
			decimal price,
			Uri imageUrl,
			string size,
			string color,
			int quantity
			)
		{
			ID = GetItemCompositeId(orderId, itemId);
			OrderId = orderId.Id;
			ItemId = itemId.Id;
			ProductUrl = productUrl.ToString();
			Price = price;
			Quantity = quantity;
			Total = Price * Quantity;

			Name = name ?? String.Empty;
			Description = description ?? String.Empty;
			ImageUrl = imageUrl != null ? imageUrl.ToString() : String.Empty;
			Size = size ?? String.Empty;
			Color = color ?? String.Empty;
		}

		public static string GetItemCompositeId(OrderId orderId, OrderItemId itemId)
		{
			return orderId.GetId() + "-" + itemId.GetId();
		}
	}

    public interface IOrderItemsView
    {
        IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId);
    }
    
    //TODO: merge with order details
	public class OrderItemsView : Projection,
        IProjection<OrderItemDetails>,
        IOrderItemsView,
		IHandle<ProductAddedToOrder>,
		IHandle<ItemRemovedFromOrder>
	{
		public OrderItemsView(IDtoManager manager)
			: base(manager)
		{
		}

		public void Handle(ProductAddedToOrder e)
		{
            var itemDetails = new OrderItemDetails(
                e.Id,
                e.OrderItemId,
                e.ProductUri,
                e.Name,
                e.Description,
                e.Price,
                e.ImageUri,
                e.Size,
                e.Color,
                e.Quantity);

            var items = _manager.Get<OrderItems>(e.Id);

            if (items == null)
            {
                _manager.Add<OrderItems>(e.Id, new OrderItems { Items = new List<OrderItemDetails> { itemDetails } });
            }
            else
            {
                _manager.Update<OrderItems>(e.Id, i => i.Items.Add(itemDetails));
            }
		}

		public void Handle(ItemRemovedFromOrder e)
		{
			_manager.Update<OrderItems>(e.Id, i => i.Items.Remove(i.Items.First(it => it.ID == OrderItemDetails.GetItemCompositeId(e.Id, e.OrderItemId))));
		}

        public IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId)
        {
            var dto = _manager.Get<OrderItems>(orderId);

            return dto != null ? dto.Items : Enumerable.Empty<OrderItemDetails>();
        }
    }
}
