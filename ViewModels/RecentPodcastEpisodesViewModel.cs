using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contrib.Podcasts.Models;

namespace Contrib.Podcasts.ViewModels {
  public class RecentPodcastEpisodesViewModel {
    
    [Required]
    public int Count { get; set; }

    [Required]
    public int PodcastId { get; set; }

    public IEnumerable<PodcastPart> Podcasts { get; set; }
  }
}