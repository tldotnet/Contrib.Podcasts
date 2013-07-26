using Contrib.Podcasts.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Contrib.Podcasts.Handlers {
  public class PodcastEpisodeHandler : ContentHandler {

    public PodcastEpisodeHandler(IRepository<PodcastEpisodePartRecord> podcastEpisodePartRepository) {
      Filters.Add(StorageFilter.For(podcastEpisodePartRepository));
    }

  }
}