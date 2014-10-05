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
    }
}