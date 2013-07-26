using System.Collections.Generic;

namespace Contrib.Podcasts.Models {
  public class PodcastHostRecord {
    public virtual int Id { get; set; }
    public virtual PodcastPartRecord PodcastPartRecord { get; set; }
    public virtual PersonRecord PersonRecord { get; set; }
  }
}