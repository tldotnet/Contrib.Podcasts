using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contrib.Podcasts.Models;

namespace Contrib.Podcasts.ViewModels {

  public class PodcastViewModel {
    public string Description { get; set; }
    public IEnumerable<int> Hosts { get; set; }
    public IEnumerable<PersonRecord> AvailablePeople { get; set; }
    public CreativeCommonsLicenseTypes License { get; set; }
    public SimpleRatingTypes Rating { get; set; }
    public bool IncludeEpisodeTranscriptInFeed { get; set; }
  }

}