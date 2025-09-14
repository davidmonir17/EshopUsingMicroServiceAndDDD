namespace BuldingBlocks.Pagination
{
    public class PaginationResult<TEntity>(int PageIndex,int PageSize, long TotalCount, IEnumerable<TEntity> Data) where TEntity : class
    {
        public int PageIndex { get; } = PageIndex;
        public int PageSize { get; } = PageSize;
        public long TotalCount { get; } = TotalCount;
        public IEnumerable<TEntity> Data { get; } = Data; 
    }
}
