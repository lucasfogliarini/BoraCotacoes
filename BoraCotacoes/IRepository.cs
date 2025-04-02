namespace BoraCotacoes
{
    public interface IRepository
    {
        IDatabase Database { get; }
    }

    public interface IDatabase
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        int Commit(CancellationToken cancellationToken = default);
        void Update(object entity);
        void Add(object entity);
    }
}
