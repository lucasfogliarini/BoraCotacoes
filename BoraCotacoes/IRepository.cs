namespace BoraCotacoes
{
    public interface IRepository
    {
        ICommitScope CommitScope { get; }
    }
    public interface IAddRepository<T> : IRepository
    {
        void Add(T entity);
    }

    public interface ICommitScope
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        int Commit(CancellationToken cancellationToken = default);
    }
}
