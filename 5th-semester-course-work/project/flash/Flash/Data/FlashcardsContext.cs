using Flash.Models;
using Flash.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flash.Data;

public partial class FlashcardsContext : DbContext
{
    public FlashcardsContext()
    {
    }

    public FlashcardsContext(DbContextOptions<FlashcardsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deck> Decks { get; set; }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deck>(entity =>
        {
            entity.Property(e => e.DifficultyRetentionRate).HasDefaultValueSql("((1.2))");
            entity.Property(e => e.FlashcardsCount).HasDefaultValueSql("((0))");
            entity.Property(e => e.SuccessRetentionRate).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.User).WithMany(p => p.Decks).HasConstraintName("FK_Deck_User");
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.ToTable("Flashcard", tb =>
                {
                    tb.HasTrigger("OnFlashcardDelete");
                    tb.HasTrigger("OnFlashcardInsert");
                    tb.HasTrigger("OnFlashcardUpdate");
                });

            entity.Property(e => e.RepetitionInterval).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Deck).WithMany(p => p.Flashcards).HasConstraintName("FK_Flashcard_Deck");
        });

        OnModelCreatingPartial(modelBuilder);
    }   

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
