namespace BuldingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public string? Details { get; }
        public InternalServerException(string msg) : base(msg)
        {
        }

        public InternalServerException(string msg, string details) : base(msg)
        {
            Details = details;
        }

    }
}
