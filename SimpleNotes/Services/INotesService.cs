using ErrorOr;
using SimpleNotes.Contracts;

namespace SimpleNotes.Services;

/// <summary>
/// Interface for notes service operations.
/// </summary>
public interface INotesService
{
    /// <summary>
    /// Asynchronously creates a new note based on the provided request.
    /// </summary>
    /// <param name="request">The request object containing all necessary data to create a note.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either a Created object indicating success or an error.</returns>
    Task<ErrorOr<Created>> CreateAsync(CreateNoteRequest request);
    
    /// <summary>
    /// Asynchronously retrieves a list of note previews based on the provided request.
    /// </summary>
    /// <param name="request">The request object containing criteria for filtering the notes to be previewed.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either a list of GetPreviewNoteResponse objects representing the note previews or an error.</returns>
    Task<ErrorOr<List<GetPreviewNoteResponse>>> GetPreviewsAsync(GetPreviewNotesRequest request);

    /// <summary>
    /// Asynchronously retrieves a note by its ID.
    /// </summary>
    /// <param name="request">The request object containing the ID of the note to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either a GetNoteResponse object containing the note details or an error.</returns>
    Task<ErrorOr<GetNoteResponse>> GetByIdAsync(GetNoteByIdRequest request);
    
    /// <summary>
    /// Asynchronously updates an existing note based on the provided request.
    /// </summary>
    /// <param name="request">The request object containing all necessary data to update the note.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either an Updated object indicating success or an error.</returns>
    Task<ErrorOr<Updated>> UpdateAsync(UpdateNoteRequest request);
    
    /// <summary>
    /// Asynchronously counts the total number of notes.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either the total number of notes as an integer or an error.</returns>
    Task<ErrorOr<int>> CountAsync();
}