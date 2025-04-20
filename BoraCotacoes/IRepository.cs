namespace BoraCotacoes
{
    public interface IRepository
    {
        ICommitScope CommitScope { get; }
    }

    public interface ICommitScope
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        int Commit(CancellationToken cancellationToken = default);
        void Update(object entity);
        void Add(object entity);
    }
}
