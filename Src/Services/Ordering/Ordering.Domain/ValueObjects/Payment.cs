namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; } = default!;
        public string CardName { get; } = default!;
        public string Expiration { get; } = default!;
        public string Cvv { get; } = default!;
        public int PaymentMethod { get; } = default!;

        protected Payment() { }

        private Payment(string cardNumber, string cardName, string expiration, string cvv, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardName = cardName;
            Expiration = expiration;
            Cvv = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string cardName, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration, nameof(expiration));
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));

            return new Payment(cardNumber, cardName, expiration, cvv, paymentMethod);
        }
    }
}
