
// 1. Define order types

type OrderItem = OrderItem of string
type PaymentReceipt = {OrderRef : string; PayedAmount : decimal}

// 2. define states

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


// 3. create the operations for the state (each operation takes one of the states as the input and returns an Order)
// this allows to start with a state but return a result as a wrapper of every possible state.

let addToEmptyState orderItem =
    Order.PaymentExpected {UnpaidItems = [orderItem]}

let addToPaymentExpectedState state orderItemToAdd =
    let newList = orderItemToAdd :: state.UnpaidItems
    Order.PaymentExpected {state with UnpaidItems = newList}

let removeFromActiveState state orderItemToRemove =
    let newList = state.UnpaidItems |> List.filter (fun i -> i <> orderItemToRemove)

    match newList with
        | [] -> Order.Empty NoItems
        | _ -> Order.PaymentExpected {state with UnpaidItems = newList}