using Contrib.Podcasts.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Contrib.Podcasts.Handlers {
  public class RecentPodcastEpisodesHandler : ContentHandler {
    public RecentPodcastEpisodesHandler(IRepository<RecentPodcastEpisodesPartRecord> repository) {
      Filters.Add(StorageFilter.For(repository));
    }
  }
}