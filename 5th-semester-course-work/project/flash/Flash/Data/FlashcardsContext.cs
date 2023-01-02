using System;
using System.Collections.Generic;
using Flash.Models;
using Flash.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flash.Data;

public partial class FlashcardsContext : DbContext
{
    public FlashcardsContext() { }

    public FlashcardsContext(DbContextOptions<FlashcardsContext> options)
        : base(options) { }

    public virtual DbSet<Box> Boxes { get; set; }

    public virtual DbSet<Deck> Decks { get; set; }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deck>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Decks).HasConstraintName("FK_Deck_User");
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.ToTable("Flashcard", tb =>
                {
                    tb.HasTrigger("OnFlashcardInsert");
                    tb.HasTrigger("OnFlashcardUpdate");
                });

            entity.HasOne(d => d.Box).WithMany(p => p.Flashcards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flashcard_Box");

            entity.HasOne(d => d.Deck).WithMany(p => p.Flashcards).HasConstraintName("FK_Flashcard_Deck");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
