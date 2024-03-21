using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SimpleNotes.Common;
using SimpleNotes.Database;
using SimpleNotes.Domain;

namespace SimpleNotes.Repositories;

public class NotesRepository : INotesRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ILogger<NotesRepository> _logger;

    public NotesRepository(ILogger<NotesRepository> logger, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {   
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<ErrorOr<Guid>> CreateAsync(Note note)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            dbContext.Notes.Add(note);
            await dbContext.SaveChangesAsync();
            return note.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create note");
            return Errors.Notes.CreateFailed(note.Id);
        }
    }
}