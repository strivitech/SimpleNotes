using ErrorOr;
using FluentAssertions;
using NSubstitute;
using SimpleNotes.Contracts;
using SimpleNotes.Domain;
using SimpleNotes.Repositories;
using SimpleNotes.Services;
using SimpleNotes.Validation;

namespace SimpleNotes.Tests.UnitTests;

public class NotesServiceTests
{
    private readonly INotesRepository _notesRepository = Substitute.For<INotesRepository>();
    private readonly IRequestValidator _requestValidator = Substitute.For<IRequestValidator>();
    private readonly NotesService _notesService;

    public NotesServiceTests()
    {
        _notesService = new NotesService(_notesRepository, _requestValidator);
    }

    [Fact]
    public async Task CreateAsync_ValidRequest_ReturnsCreated()
    {
        var request = new CreateNoteRequest("Test Title", "Test Content");
        _requestValidator.Validate(request).Returns(new List<Error>());
        _notesRepository.CreateAsync(Arg.Any<Note>()).Returns(Guid.NewGuid());

        var result = await _notesService.CreateAsync(request);

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Created>();
    }

    [Fact]
    public async Task CreateAsync_InvalidRequest_ReturnsValidationError()
    {
        var request = new CreateNoteRequest("", "");
        _requestValidator.Validate(request).Returns(new List<Error>
        {
            Error.Validation("Title", "Title is required"),
            Error.Validation("Content", "Content is required")
        });

        var result = await _notesService.CreateAsync(request);

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Code == "Title" && e.Description == "Title is required");
        result.Errors.Should().ContainSingle(e => e.Code == "Content" && e.Description == "Content is required");
    }

    [Fact]
    public async Task GetPreviewsAsync_ValidRequest_ReturnsPreviews()
    {
        var request = new GetPreviewNotesRequest { Page = 1, PageSize = 10, SearchQuery = "Test" };
        _requestValidator.Validate(request).Returns(new List<Error>());
        var notes = new List<Note>
            { new Note { Id = Guid.NewGuid(), Title = "Test", Content = "Content", CreatedAt = DateTime.UtcNow } };
        _notesRepository.GetPreviewsAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>())
            .Returns(notes);

        var result = await _notesService.GetPreviewsAsync(request);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull().And.HaveCountGreaterThan(0).And.AllBeOfType<GetPreviewNoteResponse>();
    }

    [Fact]
    public async Task GetPreviewsAsync_InvalidRequest_ReturnsValidationError()
    {
        var request = new GetPreviewNotesRequest { Page = -1, PageSize = 0, SearchQuery = null };
        _requestValidator.Validate(request).Returns(new List<Error>
        {
            Error.Validation("Page", "Page must be greater than 0"),
            Error.Validation("PageSize", "PageSize must be greater than 0")
        });

        var result = await _notesService.GetPreviewsAsync(request);

        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == "Page" && e.Description == "Page must be greater than 0");
        result.Errors.Should().Contain(e => e.Code == "PageSize" && e.Description == "PageSize must be greater than 0");
    }
    
    [Fact]
    public async Task GetByIdAsync_ValidRequest_ReturnsNote()
    {
        var request = new GetNoteByIdRequest(Guid.NewGuid());
        _requestValidator.Validate(request).Returns(new List<Error>());
        var note = new Note
            { Id = request.Id, Title = "Test Note", Content = "Test Content", CreatedAt = DateTime.UtcNow };
        _notesRepository.GetByIdAsync(request.Id).Returns(note);

        var result = await _notesService.GetByIdAsync(request);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull().And.BeOfType<GetNoteResponse>();
    }

    [Fact]
    public async Task GetByIdAsync_InvalidRequest_ReturnsValidationError()
    {
        var request = new GetNoteByIdRequest(Guid.Empty);
        _requestValidator.Validate(request).Returns(new List<Error>
        {
            Error.Validation("Id", "Id must not be empty")
        });

        var result = await _notesService.GetByIdAsync(request);

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Code == "Id" && e.Description == "Id must not be empty");
    }
    
    [Fact]
    public async Task UpdateAsync_ValidRequest_ReturnsUpdated()
    {
        var request = new UpdateNoteRequest(Guid.NewGuid(), "Updated Title", "Updated Content");
        _requestValidator.Validate(request).Returns(new List<Error>());
        _notesRepository.GetByIdAsync(request.Id).Returns(new Note
            { Id = request.Id, Title = "Original Title", Content = "Original Content", CreatedAt = DateTime.UtcNow });
        _notesRepository.UpdateAsync(Arg.Any<Note>()).Returns(new Updated());

        var result = await _notesService.UpdateAsync(request);

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Updated>();
    }

    [Fact]
    public async Task UpdateAsync_InvalidRequest_ReturnsValidationError()
    {
        var request = new UpdateNoteRequest(Guid.Empty, "", "");
        _requestValidator.Validate(request).Returns(new List<Error>
        {
            Error.Validation("Id", "Id must not be empty"),
            Error.Validation("Title", "Title is required"),
            Error.Validation("Content", "Content is required")
        });

        var result = await _notesService.UpdateAsync(request);

        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Code == "Id" && e.Description == "Id must not be empty");
        result.Errors.Should().Contain(e => e.Code == "Title" && e.Description == "Title is required");
        result.Errors.Should().Contain(e => e.Code == "Content" && e.Description == "Content is required");
    }
    
    [Fact]
    public async Task CountAsync_ReturnsCount()
    {
        _notesRepository.CountAsync().Returns(5);

        var result = await _notesService.CountAsync();

        result.IsError.Should().BeFalse();
        result.Value.Should().Be(5);
    }
}