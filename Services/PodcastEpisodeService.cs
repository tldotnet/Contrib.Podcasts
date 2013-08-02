using System.Linq;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.ViewModels;
using Orchard.ContentManagement;
using Orchard.Tasks.Scheduling;
using Orchard.Data;

namespace Contrib.Podcasts.Services {
  public class PodcastEpisodeService : IPodcastEpisodeService {
    private readonly IContentManager _contentManager;
    private readonly IPublishingTaskManager _publishingTaskManager;
    private readonly IRepository<PersonRecord> _personRepository;
    private readonly IRepository<EpisodePersonRecord> _episodePersonRepository;

    public PodcastEpisodeService(IContentManager contentManager, IPublishingTaskManager publishingTaskManager, IRepository<PersonRecord> personRepository, IRepository<EpisodePersonRecord> episodePersonRepository) {
      _contentManager = contentManager;
      _publishingTaskManager = publishingTaskManager;
      _personRepository = personRepository;
      _episodePersonRepository = episodePersonRepository;
    }

    public PodcastEpisodePart Get(int id) {
      return Get(id, VersionOptions.Published);
    }

    public PodcastEpisodePart Get(int id, VersionOptions versionOptions) {
      return _contentManager.Get<PodcastEpisodePart>(id, versionOptions);
    }

    /// <summary>
    /// Update an episode using the specified view model part. This is needed as some things in the add/edit UI aren't
    /// handled automatically by Orchard (like hosts & guests selection).
    /// </summary>
    public void Update(PodcastEpisodeViewModel viewModel, PodcastEpisodePart part) {
      part.PodcastId = part.PodcastPart.Id;
      part.EnclosureUrl = viewModel.EnclosureUrl;
      part.EnclosureFilesize = viewModel.EnclosureFileSize;
      part.Duration = viewModel.Duration;
      part.Notes = viewModel.Notes;
      part.Transcription = viewModel.Transcription;
      part.Rating = viewModel.Rating;
      part.EpisodeNumber = viewModel.EpisodeNumber;

      #region handle hosts
      // get list of all hosts currently in DB for this episode
      var oldHosts = _episodePersonRepository.Fetch(p => p.PodcastEpisodePartRecord.Id == part.Id && p.IsHost).Select(r => r.PersonRecord.Id).ToList();
      // remove all hosts not in the new list from the DB
      foreach (var oldHostId in oldHosts.Except(viewModel.Hosts)) {
        _episodePersonRepository.Delete(_episodePersonRepository.Get(record => record.PersonRecord.Id == oldHostId));
      }
      // add all new hosts not in the DB that are in the new list
      foreach (var newHostId in viewModel.Hosts.Except(oldHosts)) {
        var host = _personRepository.Get(newHostId);
        _episodePersonRepository.Create(new EpisodePersonRecord() {
          PersonRecord = host,
          PodcastEpisodePartRecord = part.Record,
          IsHost = true
        });
      }      
      #endregion

      #region handle guests
      // get list of all guests currently in DB for this episode
      var oldGuests = _episodePersonRepository.Fetch(p => p.PodcastEpisodePartRecord.Id == part.Id && !p.IsHost).Select(r => r.PersonRecord.Id).ToList();
      // remove all guests not in the new list from the DB
      foreach (var oldGuestId in oldGuests.Except(viewModel.Guests)) {
        _episodePersonRepository.Delete(_episodePersonRepository.Get(record => record.PersonRecord.Id == oldGuestId));
      }
      // add all new guests not in the DB that are in the new list
      foreach (var newGuestId in viewModel.Guests.Except(oldGuests)) {
        var guest = _personRepository.Get(newGuestId);
        _episodePersonRepository.Create(new EpisodePersonRecord() {
          PersonRecord = guest,
          PodcastEpisodePartRecord = part.Record,
          IsHost = false
        });
      }
      #endregion
    }

    public void Delete(ContentItem episodePart) {
      // todo: remove episodes?
      _contentManager.Remove(episodePart);
    }
  }
}