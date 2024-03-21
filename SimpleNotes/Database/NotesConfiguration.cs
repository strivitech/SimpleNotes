using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleNotes.Common;
using SimpleNotes.Domain;

namespace SimpleNotes.Database;

public class NotesConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(NoteContractConstants.MaxTitleLength).IsRequired();
        builder.Property(x => x.Content).HasMaxLength(NoteContractConstants.MaxContentLength).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}