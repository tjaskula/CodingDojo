namespace DAPOrderProcessing.Tests

open DAPOrderProcessing
open DAPOrderProcessing.OrderHandling

open Xunit
open FsUnit.Xunit

module ``DAP order processing tests`` =

    [<Fact>]
    let ``Should add order item to empty order``() =
        let orderItem = OrderItem "item1"
        let order = addToEmptyState orderItem
        order |> should equal (PaymentExpected {UnpaidItems = [orderItem]})