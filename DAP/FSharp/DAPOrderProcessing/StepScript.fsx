
// 1. Define order types

type OrderItem = OrderItem of string
type PaymentReceipt = {OrderRef : string; PayedAmount : decimal}

// 2. define states

type EmptyState = NoItems // allows to handle explicitly this state.
type PaymentExpectedState = {UnpayedItem : OrderItem list}
type PayedState = {PayedItems : OrderItem list; PayedAmount : decimal}
type CancelledState = NoAction
type CompletedState = {Payed : PayedState; Received : PaymentReceipt}

type Order =
    | Empty of EmptyState
    | PaymentExpected of PaymentExpectedState
    | Payed of PayedState
    | Cancelled of CancelledState