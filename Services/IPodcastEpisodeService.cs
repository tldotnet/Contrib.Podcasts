using System;
using System.Collections;
using System.Collections.Generic;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace Contrib.Podcasts.Services {
  public interface IPodcastEpisodeService : IDependency {
    // methods for getting specific episode
    PodcastEpisodePart Get(int id);
    PodcastEpisodePart Get(int id, VersionOptions versionOptions);
    
    // methods for getting all episodes for a podcast
    IEnumerable<PodcastEpisodePart> Get(PodcastPart podcastPart);
    IEnumerable<PodcastEpisodePart> Get(PodcastPart podcastPart, VersionOptions versionOptions);
    IEnumerable<PodcastEpisodePart> Get(PodcastPart podcastPart, ArchiveData archiveData);
    IEnumerable<PodcastEpisodePart> Get(PodcastPart podcastPart, int skip, int count);
    IEnumerable<PodcastEpisodePart> Get(PodcastPart podcastPart, int skip, int count, VersionOptions versionOptions);

    int EpisodeCount(PodcastPart podcastPart);
    int EpisodeCount(PodcastPart podcastPart, VersionOptions versionOptions);

    void Update(PodcastEpisodeViewModel viewModel, PodcastEpisodePart part);
    void Delete(ContentItem podcastPart);
  }
}