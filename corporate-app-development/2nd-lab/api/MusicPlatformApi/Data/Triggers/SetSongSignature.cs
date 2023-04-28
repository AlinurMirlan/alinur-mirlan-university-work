using EntityFrameworkCore.Triggered;
using MusicPlatformApi.Data.Entities;

namespace MusicPlatformApi.Data.Triggers
{
    public class SetSongSignature : IBeforeSaveTrigger<Song>
    {
        public Task BeforeSave(ITriggerContext<Song> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType != ChangeType.Added)
                return Task.CompletedTask;

            Song song = context.Entity;
            song.Signature = song.Album?.Name + song.Title + song.Authors.Aggregate("", (authorNames, author) => authorNames + author.Nickname);
            return Task.CompletedTask;
        }
    }
}
