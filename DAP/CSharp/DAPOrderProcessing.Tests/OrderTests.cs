using System.Linq;
using Xunit;

namespace DAPOrderProcessing.Tests
{
    public class OrderTests
    {
        // You can only add items to not payed and not cancelled order
        [Fact]
        public void ShouldAddItemWhenOrderIsNotPayed()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            Assert.Equal(1, order.Items.Count());
        }

        // You can only add items to not payed and not cancelled order
        [Fact]
        public void ShouldNotAddItemWhenOrderIsPayed()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Pay(1000);
            order.AddItem(new OrderItem());
            Assert.Equal(1, order.Items.Count());
        }

        // You can only add items to not payed and not cancelled order
        [Fact]
        public void ShouldNotAddItemWhenOrderIsCancelled()
        {
            var order = new Order("123");
            order.Cancel();
            order.AddItem(new OrderItem());
            Assert.Equal(0, order.Items.Count());
        }

        // You can only pay for an order with a least one item
        [Fact]
        public void ShouldOrderBePayableWithAtLeastOneItem()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Pay(1000);
            Assert.Equal(OrderStatus.Payed, order.Status);
            Assert.Equal(1, order.Items.Count());
        }

        // You can only pay for an order with a least one item
        [Fact]
        public void ShouldOrderStatusBeExpectedPendingWhenOrderIsEmpty()
        {
            var order = new Order("123");
            order.Pay(1000);
            Assert.Equal(OrderStatus.Empty, order.Status);
        }

        // You can only pay for an order which was not already payed
        [Fact]
        public void ShouldPaymentSucceedWhenOrderIsNotAlreadyPayed()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Pay(1000);
            order.AddItem(new OrderItem());
            order.Pay(5000);
            Assert.Equal(1, order.Items.Count());
            Assert.Equal(1000, order.PayedAmount);
        }

        // You cannot pay for a cancelled order
        [Fact]
        public void ShouldNotAcceptPaymentWhenOrderIsCancelled()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Cancel();
            order.Pay(1000);
            Assert.Equal(OrderStatus.Cancelled, order.Status);
            Assert.Equal(0, order.PayedAmount);
        }

        // You cannot remove items from an empty order
        [Fact]
        public void ShouldNotRemoveItemsWhenOrderIsEmpty()
        {
            var order = new Order("123");
            order.RemoveItem(new OrderItem());
            Assert.Equal(0, order.Items.Count());
        }

        // You cannot remove items from payed or cancelled order
        [Fact]
        public void ShouldNotRemoveItemsWhenOrderIsPayed()
        {
            var order = new Order("123");
            var orderItem = new OrderItem();
            order.AddItem(orderItem);
            order.Pay(1000);
            order.RemoveItem(orderItem);
            Assert.Equal(1, order.Items.Count());
            Assert.Equal(OrderStatus.Payed, order.Status);
        }

        // You cannot remove items from payed or cancelled order
        [Fact]
        public void ShouldNotRemoveItemsWhenOrderIsCancelled()
        {
            var order = new Order("123");
            var orderItem = new OrderItem();
            order.AddItem(orderItem);
            order.Cancel();
            order.RemoveItem(orderItem);
            Assert.Equal(1, order.Items.Count());
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        // You can only cancel an unpayed order
        [Fact]
        public void ShouldNotCancelWhenOrderIsPayed()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Pay(1000);
            order.Cancel();
            Assert.Equal(OrderStatus.Payed, order.Status);
        }

        // You can only cancel an unpayed order
        [Fact]
        public void ShouldCancelWhenOrderIsNotPayed()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Cancel();
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        // You can only cancel an unpayed order
        [Fact]
        public void ShouldCancelWhenOrderIsEmpty()
        {
            var order = new Order("123");
            order.Cancel();
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        // Only payed order can be completed
        [Fact]
        public void ShouldCompleteOrderWhenOrderIsPayed()
        {
            var order = new Order("123");
            order.AddItem(new OrderItem());
            order.Pay(1000);
            var receipt = order.GetPaymentReceipt();
            order.Receive(receipt);
            Assert.Equal(OrderStatus.Completed, order.Status);
        }

        // Only payed order can be completed
        [Fact]
        public void ShouldNotCompleteOrderWhenReceiptIsWrong()
        {
            var order = new Order("123");
            var order2 = new Order("ABC");
            order.AddItem(new OrderItem());
            order2.AddItem(new OrderItem());
            order.Pay(1000);
            order2.Pay(5000);
            var receipt = order2.GetPaymentReceipt();
            order.Receive(receipt);
            Assert.Equal(OrderStatus.Payed, order.Status);
        }

        // Only payed order can be completed
        [Fact]
        public void ShouldNotCompleteOrderWhenOrderInNonPayedStatus()
        {
            var order = new Order("Empty");
            var order2 = new Order("PaymentExpected");
            var order3 = new Order("Cancelled");
            var order4 = new Order("Payed");
            order2.AddItem(new OrderItem());
            order4.AddItem(new OrderItem());
            order4.Pay(1000);
            var receipt = order4.GetPaymentReceipt();
            order3.Cancel();
            order.Receive(receipt);
            order2.Receive(receipt);
            order3.Receive(receipt);
            Assert.Equal(OrderStatus.Empty, order.Status);
            Assert.Equal(OrderStatus.PaymentExpected, order2.Status);
            Assert.Equal(OrderStatus.Cancelled, order3.Status);
        }

        // No operation is allowed on completed order
        [Fact]
        public void ShouldNotOrderChangeStateWhenIsCompleted()
        {
            var order = new Order("Completed");
            var orderItem1 = new OrderItem();
            order.AddItem(orderItem1);
            order.Pay(1000);
            var receipt = order.GetPaymentReceipt();
            order.Receive(receipt);
            Assert.Equal(OrderStatus.Completed, order.Status);

            order.AddItem(new OrderItem());
            Assert.Equal(OrderStatus.Completed, order.Status);

            order.RemoveItem(orderItem1);
            Assert.Equal(OrderStatus.Completed, order.Status);

            order.Cancel();
            Assert.Equal(OrderStatus.Completed, order.Status);

            order.Pay(2000);
            Assert.Equal(OrderStatus.Completed, order.Status);

        }
    }
}