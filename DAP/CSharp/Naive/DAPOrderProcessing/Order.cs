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

        public OrderStatus Status
        {
            get { return _orderStatus; }
        }

        public void AddItem(OrderItem orderItem)
        {
            if (_orderStatus == OrderStatus.Empty || _orderStatus == OrderStatus.PaymentExpecting)
            {
                _items.Add(orderItem);
                _orderStatus = OrderStatus.PaymentExpecting;
            }
        }

        public void Pay(decimal amount)
        {
            if (_orderStatus == OrderStatus.PaymentExpecting)
            {
                _payedAmount = amount;
                _orderStatus = OrderStatus.Payed;   
            }
        }

        public void Cancel()
        {
            _orderStatus = OrderStatus.Cancelled;
        }
    }
}