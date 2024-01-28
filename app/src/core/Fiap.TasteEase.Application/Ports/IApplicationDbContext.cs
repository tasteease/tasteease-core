namespace Fiap.TasteEase.Application.Ports
{
    public interface IApplicationDbContext : IDisposable
    {
        Task SaveChangesAsync();
        Task<IApplicationDbContextTransaction> BeginTransactionAsync();

    }

    public interface IApplicationDbContextTransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}