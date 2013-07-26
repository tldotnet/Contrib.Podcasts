using Contrib.Podcasts.Models;
using Contrib.Podcasts.Services;
using Orchard.ContentManagement.Drivers;

namespace Contrib.Podcasts.Drivers {
  public class PodcastEpisodePartDriver : ContentPartDriver<PodcastEpisodePart> {
    private readonly IPodcastEpisodeService _podcastEpisodeService;

    public PodcastEpisodePartDriver(IPodcastEpisodeService podcastEpisodeService) {
      _podcastEpisodeService = podcastEpisodeService;
    }

    protected override string Prefix {
      get { return "PodcastEpisode"; }
    }

  }
}