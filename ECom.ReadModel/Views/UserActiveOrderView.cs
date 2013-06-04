using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class ActiveUserOrderDetails : Dto
	{
		public string ID { get; set; }
		public int OrderId { get; set; }

		public ActiveUserOrderDetails()
		{
		}

		public ActiveUserOrderDetails(string userId, int orderId)
		{
			ID = userId;
			OrderId = orderId;
		}
	}

    public interface IUserActiveOrderView
    {
        OrderId GetUserActiveOrderId(UserId userId);
    }

	public class UserActiveOrderView : ReadModelView,
        IUserActiveOrderView,
		IHandle<NewOrderCreated>,
		IHandle<OrderSubmited>
	{
		public UserActiveOrderView(IDtoManager manager)
			: base(manager)
		{
		}

		public void Handle(NewOrderCreated e)
		{
			_manager.Add<ActiveUserOrderDetails>(e.UserId.Id, new ActiveUserOrderDetails(e.UserId.Id, e.Id.Id));
		}

		public void Handle(OrderSubmited e)
		{
			string userId = e.UserId.Id;
			_manager.Delete<ActiveUserOrderDetails>(userId);
		}

        public OrderId GetUserActiveOrderId(UserId userId)
        {
            Argument.ExpectNotNull(() => userId);

            var activeOrder = _manager.Get<ActiveUserOrderDetails>(userId);
            if (activeOrder != null)
            {
                return new OrderId(activeOrder.OrderId);
            }

            return null;
        }
    }
}
