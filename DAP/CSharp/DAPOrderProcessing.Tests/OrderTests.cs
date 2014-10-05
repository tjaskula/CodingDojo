using System.Linq;
using Xunit;

namespace DAPOrderProcessing.Tests
{
    public class OrderTests
    {
        [Fact]
        public void ShouldAddItemWhenOrderIsNotPayed()
        {
            var order = new Order();
            order.AddItem(new OrderItem());
            Assert.Equal(1, order.Items.Count());
        }
    }
}