using ErrorOr;
using SimpleNotes.Domain;

namespace SimpleNotes.Repositories;

public interface INotesRepository
{
    Task<ErrorOr<Guid>> CreateAsync(Note note);
}