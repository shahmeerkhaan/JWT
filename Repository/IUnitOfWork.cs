using Resume_Analyzer_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Resume_Analyzer_Backend.Repository;

public interface IUnitOfWork : IDisposable
{
    DbContext Context { get; }
    public Task SaveChangesAsync();
}
public class UnitOfwork : IUnitOfWork
{
    private readonly DBContext _context;
    private bool _disposed = false;

    public UnitOfwork(DBContext context)
    {
        _context = context;
    }
    public DbContext Context => _context;

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}