using System.Collections.Generic;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace Contrib.Podcasts.Services {
  public interface IPodcastService : IDependency {
    IEnumerable<PodcastPart> Get();
    ContentItem Get(int podcastId);
    void Update(PodcastViewModel viewModel, PodcastPart part);
    void Delete(ContentItem podcastPart);
  }
}