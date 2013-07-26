using System.Collections.Generic;

namespace Contrib.Podcasts.Models {
  public class PersonRecord {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Email { get; set; }
    public virtual string Url { get; set; }
    public virtual string TwitterName { get; set; }

    public PersonRecord() {
      PodcastHosted = new List<PodcastHostRecord>();
      PodcastEpisodes = new List<EpisodePersonRecord>();
    }

    public virtual IList<PodcastHostRecord> PodcastHosted { get; set; }
    public virtual IList<EpisodePersonRecord> PodcastEpisodes { get; set; }
  }
}