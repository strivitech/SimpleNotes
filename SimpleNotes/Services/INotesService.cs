using ErrorOr;
using SimpleNotes.Contracts;

namespace SimpleNotes.Services;

public interface INotesService
{
    Task<ErrorOr<Created>> CreateAsync(CreateNoteRequest request);
    
    Task<ErrorOr<List<GetPreviewNoteResponse>>> GetPreviewsAsync(GetPreviewNotesRequest request);

    Task<ErrorOr<GetNoteResponse>> GetByIdAsync(GetNoteByIdRequest request);
    
    Task<ErrorOr<Updated>> UpdateAsync(UpdateNoteRequest request);
    
    Task<ErrorOr<int>> CountAsync();
}