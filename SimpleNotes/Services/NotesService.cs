using ErrorOr;
using SimpleNotes.Contracts;
using SimpleNotes.Domain;
using SimpleNotes.Repositories;
using SimpleNotes.Validation;

namespace SimpleNotes.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _notesRepository;
    private readonly IRequestValidator _requestValidator;

    public NotesService(INotesRepository notesRepository, IRequestValidator requestValidator)
    {
        _notesRepository = notesRepository;
        _requestValidator = requestValidator;
    }
    
    public async Task<ErrorOr<Created>> CreateAsync(CreateNoteRequest request)
    { 
        var errorList = _requestValidator.Validate(request);
        if (errorList.Any())
        {
            return errorList;
        }
        
        var note = new Note
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };
        
        var createResponse = await _notesRepository.CreateAsync(note);
        return createResponse.Match<ErrorOr<Created>>(
            _ => new Created(),
            error => error
        );
    }
}