
using MediatR;

namespace BuldingBlocks.CQRS
{
    public interface IQueryHandler<in TQuert, TResponse> : IRequestHandler<TQuert, TResponse>
        where TQuert : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
