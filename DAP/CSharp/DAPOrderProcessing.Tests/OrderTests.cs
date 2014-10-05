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
            order.Pay(1000);
            order.AddItem(new OrderItem());
            Assert.Equal(0, order.Items.Count());
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
    }
}