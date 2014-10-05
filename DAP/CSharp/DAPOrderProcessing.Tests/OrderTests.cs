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
            var order = new Order();
            order.AddItem(new OrderItem());
            Assert.Equal(1, order.Items.Count());
        }

        // You can only add items to not payed and not cancelled order
        [Fact]
        public void ShouldNotAddItemWhenOrderIsPayed()
        {
            var order = new Order();
            order.AddItem(new OrderItem());
            order.Pay(1000);
            order.AddItem(new OrderItem());
            Assert.Equal(1, order.Items.Count());
        }

        // You can only add items to not payed and not cancelled order
        [Fact]
        public void ShouldNotAddItemWhenOrderIsCancelled()
        {
            var order = new Order();
            order.Cancel();
            order.AddItem(new OrderItem());
            Assert.Equal(0, order.Items.Count());
        }

        // You can only pay for an order with a least one item
        [Fact]
        public void ShouldOrderBePayableWithAtLeastOneItem()
        {
            var order = new Order();
            order.AddItem(new OrderItem());
            order.Pay(1000);
            Assert.Equal(OrderStatus.Payed, order.Status);
            Assert.Equal(1, order.Items.Count());
        }

        // You can only pay for an order with a least one item
        [Fact]
        public void ShouldOrderStatusBeExpectedPendingWhenOrderIsEmpty()
        {
            var order = new Order();
            order.Pay(1000);
            Assert.Equal(OrderStatus.Empty, order.Status);
        }

        // You can only pay for an order which was not already payed
        [Fact]
        public void ShouldPaymentSucceedWhenOrderIsNotAlreadyPayed()
        {
            var order = new Order();
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
            var order = new Order();
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
            var order = new Order();
            order.RemoveItem(new OrderItem());
            Assert.Equal(0, order.Items.Count());
        }

        // You cannot remove items from payed or cancelled order
        [Fact]
        public void ShouldNotRemoveItemsWhenOrderIsPayed()
        {
            var order = new Order();
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
            var order = new Order();
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
            var order = new Order();
            order.AddItem(new OrderItem());
            order.Pay(1000);
            order.Cancel();
            Assert.Equal(OrderStatus.Payed, order.Status);
        }

        // You can only cancel an unpayed order
        [Fact]
        public void ShouldCancelWhenOrderIsNotPayed()
        {
            var order = new Order();
            order.AddItem(new OrderItem());
            order.Cancel();
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        // You can only cancel an unpayed order
        [Fact]
        public void ShouldCancelWhenOrderIsEmpty()
        {
            var order = new Order();
            order.Cancel();
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }
    }
}