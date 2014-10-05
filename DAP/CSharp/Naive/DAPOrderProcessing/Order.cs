using System.Collections.Generic;

namespace DAPOrderProcessing
{
    public class Order
    {
        private readonly string _orderRef;
        private decimal _payedAmount;
        private readonly List<OrderItem> _items;
        private OrderStatus _orderStatus = OrderStatus.Empty;
        private PaymentReceipt _paymentReceipt = PaymentReceipt.Empty;

        public Order(string orderRef)
        {
            _orderRef = orderRef;
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

        public decimal PayedAmount
        {
            get { return _payedAmount; }
        }

        public void AddItem(OrderItem orderItem)
        {
            if (_orderStatus == OrderStatus.Empty || _orderStatus == OrderStatus.PaymentExpected)
            {
                _items.Add(orderItem);
                _orderStatus = OrderStatus.PaymentExpected;
            }
        }

        public void Pay(decimal amount)
        {
            if (_orderStatus == OrderStatus.PaymentExpected)
            {
                _payedAmount = amount;
                _orderStatus = OrderStatus.Payed;
                _paymentReceipt = new PaymentReceipt(_orderRef, amount);
            }
        }

        public void Cancel()
        {
            if (_orderStatus == OrderStatus.Empty || _orderStatus == OrderStatus.PaymentExpected)
                _orderStatus = OrderStatus.Cancelled;
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if (_orderStatus == OrderStatus.PaymentExpected)
            {
                _items.Remove(orderItem);
                _orderStatus = _items.Count == 0 ? OrderStatus.Empty : _orderStatus;
            }
        }

        public PaymentReceipt GetPaymentReceipt()
        {
            return _paymentReceipt ?? PaymentReceipt.Empty;
        }

        public void Receive(PaymentReceipt receipt)
        {
            if (_orderStatus == OrderStatus.Payed && receipt == _paymentReceipt)
                _orderStatus = OrderStatus.Completed;
        }
    }
}