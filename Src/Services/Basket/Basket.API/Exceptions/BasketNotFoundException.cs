
namespace Basket.API.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException() : base("Basket not found.")
        {
        }
        public BasketNotFoundException(string username)
            : base("Basket", username)
        {
        }
    }
}
