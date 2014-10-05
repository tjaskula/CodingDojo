using System.Collections.Generic;

namespace DAPOrderProcessing
{
    public class Order
    {
        private readonly List<OrderItem> _items;

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
            _items.Add(orderItem);
        }
    }
}