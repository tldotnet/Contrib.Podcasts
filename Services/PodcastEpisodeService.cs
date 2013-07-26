using Contrib.Podcasts.Models;
using Orchard.ContentManagement;
using Orchard.Tasks.Scheduling;

namespace Contrib.Podcasts.Services {
  public class PodcastEpisodeService : IPodcastEpisodeService {
    private readonly IContentManager _contentManager;
    private readonly IPublishingTaskManager _publishingTaskManager;

    public PodcastEpisodeService(IContentManager contentManager, IPublishingTaskManager publishingTaskManager) {
      _contentManager = contentManager;
      _publishingTaskManager = publishingTaskManager;
    }

    public PodcastEpisodePart Get(int id) {
      return Get(id, VersionOptions.Published);
    }

    public PodcastEpisodePart Get(int id, VersionOptions versionOptions) {
      return _contentManager.Get<PodcastEpisodePart>(id, versionOptions);
    }
  }
}