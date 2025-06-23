using BuldingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base("Product not found.")
        {
        }

        public ProductNotFoundException(Guid productId)
            : base("Product", productId)
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
