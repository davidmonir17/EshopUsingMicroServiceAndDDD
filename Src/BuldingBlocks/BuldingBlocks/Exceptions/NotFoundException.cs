namespace BuldingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string Name) : base(Name)
        {
        }
        public NotFoundException(string Name, object Id) : base($"{Name} \" {Id} not found.")
        {
        }
    }
}
