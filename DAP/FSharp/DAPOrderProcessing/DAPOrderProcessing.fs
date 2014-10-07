namespace DAPOrderProcessing

type OrderItem = OrderItem of string
type PaymentReceipt = {OrderRef : string; PayedAmount : decimal}

type EmptyState = NoItems // allows to handle explicitly this state.
type PaymentExpectedState = {UnpaidItems : OrderItem list}
type PayedState = {PayedItems : OrderItem list; PayedAmount : decimal}
type CancelledState = NoAction
type CompletedState = {Payed : PayedState; Received : PaymentReceipt}

type Order =
    | Empty of EmptyState
    | PaymentExpected of PaymentExpectedState
    | Payed of PayedState
    | Cancelled of CancelledState


module OrderHandling =

    let addToEmptyState orderItem =
        Order.PaymentExpected {UnpaidItems = [orderItem]}

    let addToPaymentExpectedState state orderItemToAdd =
        let newList = orderItemToAdd :: state.UnpaidItems
        Order.PaymentExpected {state with UnpaidItems = newList}