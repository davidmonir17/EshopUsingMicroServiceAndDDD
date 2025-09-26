namespace Basket.API.Dtos
{
    public class BasketCheckoutDto
    {

        public string UserName { get; set; } = default;
        public Guid CustomerId { get; set; } = default;
        //public decimal TotalPrice { get; set; } = default;
        // shiping adrress

        public string FirstName { get; set; } = default;
        public string LastName { get; set; } = default;
        public string Email { get; set; } = default;
        public string AddressLine { get; set; } = default;
        public string Ciuntry { get; set; } = default;
        public string State { get; set; } = default;
        public string ZipCode { get; set; } = default;

        //payment
        public string CardNumber { get; set; } = default!;
        public string CardName { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string Cvv { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;



    }
}
