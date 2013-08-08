using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Contrib.Podcasts.Models {
  public class RecentPodcastEpisodesPart : ContentPart<RecentPodcastEpisodesPartRecord> {
    public int PodcastId {
      get { return Record.PodcastId; }
      set { Record.PodcastId = value; }
    }

    [Required]
    public int Count {
      get { return Record.Count; }
      set { Record.Count = value; }
    }
  }
}