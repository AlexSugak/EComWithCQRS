using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.ReadModel.Views;

namespace ECom.ReadModel
{
    public interface IReadModelFacade
    {
		UserDetails GetUserDetails(UserId userId);

		OrderId GetUserActiveOrderId(UserId userId);

		UserOrderDetails GetOrderDetails(UserId userId, OrderId orderId);

		IEnumerable<OrderItemDetails> GetOrderItems(OrderId orderId);

		IEnumerable<UserOrderDetails> GetUserOders(UserId userId);
    }
}
