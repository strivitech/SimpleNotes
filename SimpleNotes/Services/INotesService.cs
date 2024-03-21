using ErrorOr;
using SimpleNotes.Contracts;

namespace SimpleNotes.Services;

public interface INotesService
{
    Task<ErrorOr<Created>> CreateAsync(CreateNoteRequest request);  
}