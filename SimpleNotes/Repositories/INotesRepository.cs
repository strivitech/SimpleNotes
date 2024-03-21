using ErrorOr;
using SimpleNotes.Domain;

namespace SimpleNotes.Repositories;

public interface INotesRepository
{
    Task<ErrorOr<Guid>> CreateAsync(Note note);

    Task<ErrorOr<List<Note>>> GetPreviewsAsync(int page, int pageSize, string? searchText = null);
    
    Task<ErrorOr<Note>> GetByIdAsync(Guid id);
    
    Task<ErrorOr<Updated>> UpdateAsync(Note note);
}