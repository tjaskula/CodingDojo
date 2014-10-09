// 1. Define order types
 
type OrderRef = string // placeholder for a more complicated typ
type OrderItem = OrderItem of string
type PaymentReceipt = {OrderRef : OrderRef; PaidAmount : decimal}
 
// 2. define states
 
type EmptyState = NoItems of OrderRef
type PaymentExpectedState = {OrderRef : OrderRef; UnpaidItems : OrderItem list}
type PayedState = {OrderRef : OrderRef; PaidItems : OrderItem list; PaymentReceipt : PaymentReceipt}
type CancelledState = NoAction of OrderRef
type CompletedState = {Paid : PayedState; Received : PaymentReceipt}
 
type Order =
    | Empty of EmptyState
    | PaymentExpected of PaymentExpectedState
    | Payed of PayedState
    | Cancelled of CancelledState
    | Completed of CompletedState
 
// 3. create the operations for the state (each operation takes one of the states as the input and returns an Order)
// this allows to start with a state but return a result as a wrapper of every possible state.
 
let addToEmptyState orderRef orderItem =
    Order.PaymentExpected {OrderRef = orderRef; UnpaidItems = [orderItem]}
 
let addToPaymentExpectedState state orderItemToAdd =
    let newList = orderItemToAdd :: state.UnpaidItems
    Order.PaymentExpected {state with UnpaidItems = newList}
 
let removeFromActiveState state orderItemToRemove =
    let newList = state.UnpaidItems |> List.filter (fun i -> i <> orderItemToRemove)
 
    match newList with
        | [] -> Order.Empty (NoItems state.OrderRef)
        | _ -> Order.PaymentExpected {state with UnpaidItems = newList}
 
 
let cancelEmptyOrder (NoItems orderRef) =
    Order.Cancelled (NoAction orderRef)
 
let cancelPaymentExpectedOrder {OrderRef = orderRef; UnpaidItems = _ } =
    Order.Cancelled (NoAction orderRef)
 
let payForPaymentExpectedState (state : PaymentExpectedState) amount =
    let recipt = {OrderRef = state.OrderRef; PaidAmount = amount}
    Order.Payed {OrderRef = state.OrderRef; PaidItems = state.UnpaidItems; PaymentReceipt = recipt}
 
let getPaymentReceiptForPayedState state =
    state.PaymentReceipt
 
let completFromPayedState state receipt =
    match receipt with
        | r when r = state.PaymentReceipt -> r |> ignore
        | _ -> failwith "Receipt is not correct for the order"
    Order.Completed {Paid = state; Received = state.PaymentReceipt}
 
 
// 4. Attach methods to states
type EmptyState with
    member x.Add = addToEmptyState
    member x.Cancel = cancelEmptyOrder x
 
type PaymentExpectedState with
    member x.Add = addToPaymentExpectedState x
    member x.Remove = removeFromActiveState x
    member x.Cancel = cancelPaymentExpectedOrder x
    member x.Pay = payForPaymentExpectedState x
 
type PayedState with
    member x.GetPaymentReceipt = getPaymentReceiptForPayedState x
    member x.Receive = completFromPayedState x
 
 
// 5. Helper functions
 
let addOrderItemToOrder order orderItem =
    match order with
        | Empty state -> let (NoItems orderRef) = state
                         state.Add orderRef orderItem
        | PaymentExpected state -> state.Add orderItem
        | Payed _ -> printfn "ERROR: The order is paid for"
                     order
        | Cancelled _ -> printfn "ERROR: The order is cancelled"
                         order
        | Completed _ -> printfn "ERROR: The order is completed"
                         order
 
let removeOrderItemFromOrder order orderItem =
    match order with
        | Empty _ -> printfn "ERROR: The order is empty"
                     order
        | PaymentExpected state -> state.Remove orderItem
        | _ -> printfn "ERROR: Not applicable"
               order
 
let payOrderForPaymentExpected order amount =
    match order with
        | PaymentExpected state -> state.Pay amount
        | _ -> printfn "ERROR: You can only pay for a PaymentExpected order"
               order
 
let cancelOrder order =
    match order with
        | Empty state -> state.Cancel
        | PaymentExpected state -> state.Cancel
        | _ -> printfn "ERROR: You can cancel only Empty or Payment expected state"
               order
 
let getReceipt order =
    match order with
        | Payed state -> Some(state.GetPaymentReceipt)
        | _ -> None
 
let receiveOrder order (receipt : Option<PaymentReceipt>) =
    match order with
        | Payed state -> match receipt with
                            | Some(r) -> state.Receive r
                            | None -> order
                        
        | _ -> printfn "ERROR: You can receive an already payed order"
               order
 
type Order with
    static member NewOrder = fun orderRef -> Order.Empty (NoItems orderRef)
    member x.Add = addOrderItemToOrder x
    member x.Remove = removeOrderItemFromOrder x
    member x.Pay = payOrderForPaymentExpected x
    member x.Cancel = cancelOrder x
    member x.GetPaymentReceipt = getReceipt x
    member x.Receive = receiveOrder x