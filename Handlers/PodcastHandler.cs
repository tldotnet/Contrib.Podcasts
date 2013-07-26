using Contrib.Podcasts.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Contrib.Podcasts.Handlers {
  public class PodcastHandler : ContentHandler {
    
    public PodcastHandler(IRepository<PodcastPartRecord> podcastPartRepository) {
       Filters.Add(StorageFilter.For(podcastPartRepository));
     }

  }
}