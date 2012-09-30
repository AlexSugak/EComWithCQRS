using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	public enum UserOrderStatus
	{
 		WaitingForApproval,
		Accepted,
		Purchased,
		Shipped,
		Delivered
	}

	[Serializable]
	public class UserOrderDetails : Dto
	{
		public string ID { get; set; }
		public string UserId { get; set; }
		public int OrderId { get; set; }
		public int NumberOfItems { get; set; }
		public decimal Total { get; set; }
		public UserOrderStatus Status { get; set; }

		public UserOrderDetails()
		{
		}

		public UserOrderDetails(UserId userId, OrderId orderId, int numberOfItems, decimal total, UserOrderStatus status)
		{
			ID = userId.Id + "-" + orderId.Id.ToString();
			UserId = userId.Id;
			OrderId = orderId.Id;
			NumberOfItems = numberOfItems;
			Total = total;
			Status = status;
		}
	}

	public class UserOrdersView : ReadModelView,
		IHandle<OrderSubmited>
	{
		public UserOrdersView(IDtoManager manager, IReadModelFacade readModel)
			: base(manager, readModel)
		{
		}

		public void Handle(OrderSubmited e)
		{
			IEnumerable<OrderItemDetails> orderItems = _readModel.GetOrderItems(e.Id);
			var orderDetails = new UserOrderDetails(e.UserId, e.Id, orderItems.Count(), orderItems.Sum(i => i.Total), UserOrderStatus.WaitingForApproval);

			_manager.Add(orderDetails);
		}
	}
}
