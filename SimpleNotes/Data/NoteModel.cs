using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Data;

public class NoteModel
{
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; } = null!;
    
    [Required]
    public string Content { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public bool IsContentVisible { get; set; } = false;
}
