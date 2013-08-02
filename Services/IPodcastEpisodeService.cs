using Contrib.Podcasts.Models;
using Contrib.Podcasts.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace Contrib.Podcasts.Services {
  public interface IPodcastEpisodeService : IDependency {
    PodcastEpisodePart Get(int id);
    PodcastEpisodePart Get(int id, VersionOptions versionOptions);
    void Update(PodcastEpisodeViewModel viewModel, PodcastEpisodePart part);
  }
}