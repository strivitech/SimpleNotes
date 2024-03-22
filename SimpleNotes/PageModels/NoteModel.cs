using System.ComponentModel.DataAnnotations;
using SimpleNotes.Common;

namespace SimpleNotes.PageModels;

/// <summary>
/// Represents a model for note data used in Razor Pages, including properties for validation.
/// </summary>
public class NoteModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the note.
    /// </summary>
    /// <value>The unique identifier as a Guid.</value>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the note. The title is required and must be between 1 and the maximum title length defined in NoteContractConstants.
    /// </summary>
    /// <value>The title of the note as a string.</value>
    [Required]
    [StringLength(NoteContractConstants.MaxTitleLength, MinimumLength = 1)]
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the content of the note. The content is required and must be between 1 and the maximum content length defined in NoteContractConstants.
    /// </summary>
    /// <value>The content of the note as a string.</value>
    [Required]
    [StringLength(NoteContractConstants.MaxContentLength, MinimumLength = 1)]
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the date and time when the note was created.
    /// </summary>
    /// <value>The creation date and time as a DateTime.</value>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the content of the note is visible.
    /// </summary>
    /// <value>A boolean value that is true if the content is visible; otherwise, false.</value>
    public bool IsContentVisible { get; set; } = false;
}

