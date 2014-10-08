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

    [<Fact>]
    let ``Should add order item to order in payment expected state``() =
        let orderItem = OrderItem "item1"
        let orderItem' = OrderItem "item2"
        match (addToEmptyState orderItem) with
            | PaymentExpected state -> let order' = addToPaymentExpectedState state orderItem'
                                       match order' with
                                           | PaymentExpected state' -> state'.UnpaidItems.Length |> should equal 2
                                           | _  -> failwith "Exception"
            | _ -> failwith "Exception"