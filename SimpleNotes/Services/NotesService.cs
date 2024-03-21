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

    public async Task<ErrorOr<List<GetPreviewNoteResponse>>> GetPreviewsAsync(GetPreviewNotesRequest request)
    {
        var errorList = _requestValidator.Validate(request);
        if (errorList.Any())
        {
            return errorList;
        }
        
        var notes = await _notesRepository.GetPreviewsAsync(request.Page, request.PageSize);
        return notes.Match<ErrorOr<List<GetPreviewNoteResponse>>>(
            list => list.Select(note => new GetPreviewNoteResponse(note.Id, note.Title, note.CreatedAt)).ToList(),
            error => error
        );
    }

    public async Task<ErrorOr<GetNoteResponse>> GetByIdAsync(GetNoteByIdRequest request)
    {
        var errorList = _requestValidator.Validate(request);
        if (errorList.Any())
        {
            return errorList;
        }
        
        var note = await _notesRepository.GetByIdAsync(request.Id);
        return note.Match<ErrorOr<GetNoteResponse>>(
            value => new GetNoteResponse(value.Id, value.Title, value.Content, value.CreatedAt),
            error => error
        );
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(UpdateNoteRequest request)
    {
        var errorList = _requestValidator.Validate(request);
        if (errorList.Any())
        {
            return errorList;
        }
        
        var noteResponse = await _notesRepository.GetByIdAsync(request.Id);
        
        if (noteResponse.IsError)
        {
            return noteResponse.Errors;
        }
        
        var note = noteResponse.Value;
        note.Title = request.Title;
        note.Content = request.Content;
        
        var updateResponse = await _notesRepository.UpdateAsync(note);
        return updateResponse.Match<ErrorOr<Updated>>(
            _ => new Updated(),
            error => error
        );
    }
}