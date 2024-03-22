using ErrorOr;
using SimpleNotes.Domain;

namespace SimpleNotes.Repositories;

/// <summary>
/// Interface for notes repository operations.
/// </summary>
public interface INotesRepository
{
    /// <summary>
    /// Asynchronously creates a new note in the repository.
    /// </summary>
    /// <param name="note">The note object to be created.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either a Guid representing the ID of the created note or an error.</returns>
    Task<ErrorOr<Guid>> CreateAsync(Note note);

    /// <summary>
    /// Asynchronously retrieves a list of notes from the repository, optionally filtered by a search text and paginated.
    /// </summary>
    /// <param name="page">The page number of the paginated result set to return.</param>
    /// <param name="pageSize">The size of each page in the paginated result set.</param>
    /// <param name="searchText">Optional. The text to search for within note content. If null, no filtering is applied.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either a list of Note objects representing the note previews or an error.</returns>
    Task<ErrorOr<List<Note>>> GetPreviewsAsync(int page, int pageSize, string? searchText = null);
    
    /// <summary>
    /// Asynchronously retrieves a note by its ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the note to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either a Note object containing the note details or an error.</returns>
    Task<ErrorOr<Note>> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Asynchronously updates an existing note in the repository.
    /// </summary>
    /// <param name="note">The note object containing the updated data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either an Updated object indicating success or an error.</returns>
    Task<ErrorOr<Updated>> UpdateAsync(Note note);
    
    /// <summary>
    /// Asynchronously counts the total number of notes in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ErrorOr object, which is either the total number of notes as an integer or an error.</returns>
    Task<ErrorOr<int>> CountAsync();
}