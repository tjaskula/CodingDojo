namespace DAPOrderProcessing
{
    public class PaymentReceipt
    {
        internal PaymentReceipt(string orderRef, decimal payedAmount)
        {
            OrderRef = orderRef;
            PayedAmount = payedAmount;
        }

        public static PaymentReceipt Empty
        {
            get
            {
                return new PaymentReceipt("", 0);
            }
        }

        public decimal PayedAmount { get; private set; }
        public string OrderRef { get; private set; }

        protected bool Equals(PaymentReceipt other)
        {
            return PayedAmount == other.PayedAmount && string.Equals(OrderRef, other.OrderRef);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PaymentReceipt)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PayedAmount.GetHashCode() * 397) ^ (OrderRef != null ? OrderRef.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PaymentReceipt left, PaymentReceipt right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PaymentReceipt left, PaymentReceipt right)
        {
            return !Equals(left, right);
        }
    }
}