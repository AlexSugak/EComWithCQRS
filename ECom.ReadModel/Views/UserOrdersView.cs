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
    public class UserOrders : Dto
    {
        public List<OrderId> Orders { get; set; }
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
			ID = CompositeId(userId, orderId);
			UserId = userId.Id;
			OrderId = orderId.Id;
			NumberOfItems = numberOfItems;
			Total = total;
			Status = status;
		}

		public static string CompositeId(UserId userId, OrderId orderId)
		{
			return userId.Id + "-" + orderId.Id.ToString();
		}
	}

    public interface IUserOrdersView
    {
        UserOrderDetails GetOrderDetails(UserId userId, OrderId orderId);
        IEnumerable<UserOrderDetails> GetUserOders(UserId userId);
    }

	public class UserOrdersView : Projection,
        IProjection<UserOrderDetails>,
        IUserOrdersView,
		IHandle<OrderSubmited>
	{
		public UserOrdersView(IDtoManager manager)
			: base(manager)
		{
		}

		public void Handle(OrderSubmited e)
		{
			var orderDetails = new UserOrderDetails(e.UserId, e.Id, e.ItemsCount, e.Total, UserOrderStatus.WaitingForApproval);

			_manager.Add(orderDetails.ID, orderDetails);

            var orders = _manager.Get<UserOrders>(e.UserId);

            if (orders == null)
            {
                _manager.Add<UserOrders>(e.UserId, new UserOrders { Orders = new List<OrderId> { e.Id } });
            }
            else
            {
                _manager.Update<UserOrders>(e.UserId, i => i.Orders.Add(e.Id));
            }
		}

        public UserOrderDetails GetOrderDetails(UserId userId, OrderId orderId)
        {
            Argument.ExpectNotNull(() => userId);
            Argument.ExpectNotNull(() => orderId);

            return _manager.Get<UserOrderDetails>(UserOrderDetails.CompositeId(userId, orderId));
        }

        public IEnumerable<UserOrderDetails> GetUserOders(UserId userId)
        {
            var orderIds = _manager.Get<UserOrders>(userId);

            if (orderIds == null)
            {
                return Enumerable.Empty<UserOrderDetails>();
            }

            return orderIds.Orders.Select(id => _manager.Get<UserOrderDetails>(UserOrderDetails.CompositeId(userId, id)));
        }
    }
}
