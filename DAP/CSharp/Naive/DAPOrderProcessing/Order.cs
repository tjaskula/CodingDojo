using System.Collections.Generic;

namespace DAPOrderProcessing
{
    public class Order
    {
        private decimal _payedAmount;
        private readonly List<OrderItem> _items;
        private OrderStatus _orderStatus = OrderStatus.Empty;

        public Order()
        {
            _items = new List<OrderItem>();
        }

        public IEnumerable<OrderItem> Items
        {
            get { return _items; }
        }

        public void AddItem(OrderItem orderItem)
        {
            if (_orderStatus != OrderStatus.Payed && _orderStatus != OrderStatus.Cancelled)
                _items.Add(orderItem);
        }

        public void Pay(decimal amount)
        {
            _payedAmount = amount;
            _orderStatus = OrderStatus.Payed;
        }

        public void Cancel()
        {
            _orderStatus = OrderStatus.Cancelled;
        }
    }
}