namespace BoraCotacoes
{
    public interface IRepository
    {
        IDatabase Database { get; }
    }

    public interface IDatabase
    {
        Task<int> CommitAsync();
        int Commit();
        void Update(object entity);
        void Add(object entity);
    }
}
