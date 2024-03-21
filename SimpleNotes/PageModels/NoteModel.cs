using System.ComponentModel.DataAnnotations;
using SimpleNotes.Common;

namespace SimpleNotes.PageModels;

public class NoteModel
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(NoteContractConstants.MaxTitleLength, MinimumLength = 1)]
    public string Title { get; set; } = null!;
    
    [Required]
    [StringLength(NoteContractConstants.MaxContentLength, MinimumLength = 1)]
    public string Content { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public bool IsContentVisible { get; set; } = false;
}
