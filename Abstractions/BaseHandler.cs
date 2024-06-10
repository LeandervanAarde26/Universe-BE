namespace UniVerServer.Abstractions;

public abstract class BaseHandler
{
    protected readonly ApplicationDbContext _context;

    protected BaseHandler(ApplicationDbContext context)
    {
        _context = context;
    }
}