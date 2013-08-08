using Orchard.ContentManagement.Records;

namespace Contrib.Podcasts.Models {
  public class RecentPodcastEpisodesPartRecord : ContentPartRecord {
    public RecentPodcastEpisodesPartRecord() {
      Count = 10;
    }

    public virtual int PodcastId { get; set; }
    public virtual int Count { get; set; }
  }
}