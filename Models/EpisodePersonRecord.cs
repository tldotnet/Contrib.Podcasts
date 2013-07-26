using System.Collections.Generic;

namespace Contrib.Podcasts.Models {
  public class EpisodePersonRecord {
    public virtual int Id { get; set; }
    public virtual PodcastEpisodePartRecord PodcastEpisodePartRecord { get; set; }
    public virtual PersonRecord PersonRecord { get; set; }
    public virtual bool IsHost { get; set; }
  }
}