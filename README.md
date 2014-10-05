Coding Dojos in C# and F#
=========================

This repository contains some coding dojos and exercices I've been runing in different companies or mayde by myself.

1. Tennis Kata
---------------

   **C# version**

   This is the tennis kata implementation coming in two ways

   * Regular implementation.
   * Implementation with a 'State' design pattern.

 2. DAP (Domain Application Protocol) Ordering and payment
 ---------------------------------------------------------

This example is inspired from a domain being discussed and implemented in ["REST in practice book"](http://shop.oreilly.com/product/9780596805838.do)

   **C# version**

   Business rules are as follows :

   	  * You can only add items to not payed and not cancelled order
      * You can only pay for an order with a least one item
      * You can only pay for an order which was not already payed
      * You cannot pay for a cancelled Order
      * You cannot remove items from an empty order
      * You cannot remove items from payed or cancelled order
      * You can only cancel an unpayed order
      * Only payed order can be completed
      * No operation is allowed on completed order

   Naive implementation

   This is just a bad design of C# code but the idea is to contrast it with an F# implementation. code but the idea is to contrast it with an F# implementation.


   **F# version**

   If you read carefully the business rules above, it's obvious that we can implement this as a state machine with 5 states and states transitions :

      * Empty State
      * PaymentExpected State
      * Payed State
      * Cancelled State
      * Completed State

   TODO : image here

   State transitions can be translated as follows :

      * When order is created it's in Empty state
      * When you add an OrderItem to the Order it transitions to PaymentExpected state
      * When all Order items are removed from the Order it transitions to Empty state
      * You can cancel an Order if it's not already Payed or Completed and transitions to Cancelled state
      * When an order is Received it transitions to Completed state

   Business rules according to states are as follows

   	  * You can Add order items only to Empty or PaymentExpected state
   	  * You can Remove order items only from PaymentExpected state
   	  * You can Pay an order only in PaymentExpected state
   	  * You can Cancel an order only in Empty or PaymentExpected state
   	  * You can Recieve an order only in Payed state