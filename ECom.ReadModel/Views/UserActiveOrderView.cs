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

	public class UserActiveOrderView : ReadModelView,
		IHandle<NewOrderCreated>,
		IHandle<OrderSubmited>
	{
		public UserActiveOrderView(IDtoManager manager, IReadModelFacade readModel)
			: base(manager, readModel)
		{
		}

		public void Handle(NewOrderCreated e)
		{
			_manager.Add<ActiveUserOrderDetails>(new ActiveUserOrderDetails(e.UserId.Id, e.Id.Id));
		}

		public void Handle(OrderSubmited e)
		{
			string userId = e.UserId.Id;
			_manager.Delete<ActiveUserOrderDetails>(userId);
		}
	}
}
