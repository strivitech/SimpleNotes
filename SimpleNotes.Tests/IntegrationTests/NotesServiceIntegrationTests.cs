using ErrorOr;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Contracts;
using SimpleNotes.Database;
using SimpleNotes.Domain;
using SimpleNotes.Services;

namespace SimpleNotes.Tests.IntegrationTests;

public class NotesServiceIntegrationTests : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly INotesService _notesService;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public NotesServiceIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        _notesService = scope.ServiceProvider.GetRequiredService<INotesService>();
        _dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateNoteRequest(Title: "Test Note", Content: "This is a test note.");

        // Act
        var result = await _notesService.CreateAsync(request);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Created>();
    }

    [Fact]
    public async Task GetPreviewsAsync_ShouldReturnPreviews_WhenNotesExist()
    {
        // Arrange
        await ClearNotesAsync();
        var notes = (await Task.WhenAll(
            CreateNoteAsync(Guid.NewGuid(), "Note 1", "Content 1", DateTime.UtcNow),
            CreateNoteAsync(Guid.NewGuid(), "Note 2", "Content 2", DateTime.UtcNow),
            CreateNoteAsync(Guid.NewGuid(), "Note 3", "Content 3", DateTime.UtcNow)
        )).Select(x => new { x.Id, x.Title }).ToList();

        // Act
        var result = await _notesService.GetPreviewsAsync(new GetPreviewNotesRequest { Page = 1, PageSize = 10 });

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().HaveCount(notes.Count);
        var actualResult = result.Value.Select(x => new { x.Id, x.Title });
        actualResult.Should().BeEquivalentTo(notes);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNote_WhenNoteExists()
    {
        // Arrange
        var note = await CreateNoteAsync(Guid.NewGuid(), "Specific Note", "This is a specific note for GetByIdAsync test.", DateTime.UtcNow);

        // Act
        var result = await _notesService.GetByIdAsync(new GetNoteByIdRequest(note.Id));

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(note.Id);
        result.Value.Title.Should().Be("Specific Note");
        result.Value.Content.Should().Be("This is a specific note for GetByIdAsync test.");
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldUpdateNote_WhenRequestIsValid()
    {
        // Arrange
        var originalNote = await CreateNoteAsync(Guid.NewGuid(), "Original Title", "Original Content", DateTime.UtcNow);
        var updateRequest = new UpdateNoteRequest(originalNote.Id, "Updated Title", "Updated Content");

        // Act
        var updateResult = await _notesService.UpdateAsync(updateRequest);

        // Assert
        updateResult.IsError.Should().BeFalse();
        updateResult.Value.Should().BeOfType<Updated>();

        // Verify the update was successful
        var getResult = await _notesService.GetByIdAsync(new GetNoteByIdRequest(originalNote.Id));
        getResult.Value.Title.Should().Be("Updated Title");
        getResult.Value.Content.Should().Be("Updated Content");
    }
    
    [Fact]
    public async Task CountAsync_ShouldReturnCorrectCount_WhenNotesExist()
    {
        // Arrange
        await ClearNotesAsync();
        await Task.WhenAll(
            CreateNoteAsync(Guid.NewGuid(), "Note 1", "Content 1", DateTime.UtcNow),
            CreateNoteAsync(Guid.NewGuid(), "Note 2", "Content 2", DateTime.UtcNow),
            CreateNoteAsync(Guid.NewGuid(), "Note 3", "Content 3", DateTime.UtcNow)
        );

        // Act
        var result = await _notesService.CountAsync();

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(3);
    }

    private async Task<Note> CreateNoteAsync(Guid id, string title, string content, DateTime createdAt)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var note = new Note { Title = title, Content = content, CreatedAt = DateTime.UtcNow };
        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync();
        return note;
    }
    
    private async Task ClearNotesAsync()
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        dbContext.Notes.RemoveRange(await dbContext.Notes.ToListAsync());
        await dbContext.SaveChangesAsync();
    }
}