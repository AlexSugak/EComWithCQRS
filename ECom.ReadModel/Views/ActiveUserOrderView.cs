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

	public class ActiveUserOrderView : 
		IHandle<NewOrderCreated>,
		IHandle<OrderSubmited>
	{
		private IDtoManager _manager;

		public ActiveUserOrderView(IDtoManager manager)
		{
			Argument.ExpectNotNull(() => manager);

			_manager = manager;
		}

		public void Handle(NewOrderCreated e)
		{
			_manager.Add<ActiveUserOrderDetails>(new ActiveUserOrderDetails(e.UserId.Id, e.Id.Id));
		}

		public void Handle(OrderSubmited e)
		{
			int orderId = e.Id.Id;
			var userId = _manager.Get<ActiveUserOrderDetails>(o => o.OrderId == orderId).ID;
			_manager.Delete<ActiveUserOrderDetails>(userId);
		}
	}
}
