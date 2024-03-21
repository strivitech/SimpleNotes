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

    public async Task<ErrorOr<List<Note>>> GetPreviewsAsync(int page, int pageSize)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var notes = await dbContext.Notes
                .OrderByDescending(note => note.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return notes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get note previews");
            return Errors.Notes.GetPreviewsFailed();
        }
    }

    public async Task<ErrorOr<Note>> GetByIdAsync(Guid id)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var note = await dbContext.Notes.FindAsync(id);
            if (note is null)
            {
                return Errors.Notes.NotFound(id);
            }
            return note;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get note");
            return Errors.Notes.GetFailed(id);
        }
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(Note note)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();
            return new Updated();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update note");
            return Errors.Notes.UpdateFailed(note.Id);
        }
    }
}