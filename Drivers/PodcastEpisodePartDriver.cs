using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Services;
using Contrib.Podcasts.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Services;

namespace Contrib.Podcasts.Drivers {
  public class PodcastEpisodePartDriver : ContentPartDriver<PodcastEpisodePart> {
    private readonly IRepository<PersonRecord> _personRepository;
    private readonly IPodcastService _podcastService;
    private readonly IPodcastEpisodeService _podcastEpisodeService;
    private readonly IClock _clock;
    private readonly Lazy<CultureInfo> _cultureInfo;

    public PodcastEpisodePartDriver(IRepository<PersonRecord> personRepository, IPodcastService podcastService, IPodcastEpisodeService podcastEpisodeService, IClock clock, IOrchardServices services) {
      _personRepository = personRepository;
      _podcastService = podcastService;
      _podcastEpisodeService = podcastEpisodeService;
      _clock = clock;
      Services = services;

      // initializing the culture info lazy initializer
      _cultureInfo = new Lazy<CultureInfo>(() => CultureInfo.GetCultureInfo(Services.WorkContext.CurrentCulture));
    }

    protected override string Prefix {
      get { return "PodcastEpisode"; }
    }

    public Localizer T { get; set; }
    public IOrchardServices Services { get; set; }

    /// <summary>
    /// GET method used for loading the editor.
    /// </summary>
    protected override DriverResult Editor(PodcastEpisodePart part, dynamic shapeHelper) {
      // get default shapes
      var shapes = new List<DriverResult> {
        ContentShape("Parts_Podcasts_PodcastEpisode_Fields",
          () => shapeHelper.EditorTemplate(TemplateName: "Parts.Podcasts.PodcastEpisode.Fields", Model: BuildViewModel(part), Prefix: Prefix))
      };

      // if the ID of current podcast isn't zero, add a delete button
      if (part.Id > 0)
        shapes.Add(ContentShape("PodcastEpisode_DeleteButton", deleteButton => deleteButton));

      return Combined(shapes.ToArray());
    }

    /// <summary>
    /// POST method used for persisting the data
    /// </summary>
    protected override DriverResult Editor(PodcastEpisodePart part, IUpdateModel updater, dynamic shapeHelper) {
      var viewModel = new PodcastEpisodeViewModel();
      updater.TryUpdateModel(viewModel, Prefix, null, new[] { "AvailableHosts", "AvailableGuests" });
      _podcastEpisodeService.Update(viewModel, part);
      return Editor(part, shapeHelper);
    }

    private PodcastEpisodeViewModel BuildViewModel(PodcastEpisodePart part) {
      return new PodcastEpisodeViewModel() {
        PodcastId = part.PodcastPart.Id,
        EnclosureUrl = part.EnclosureUrl,
        EnclosureFileSize = part.EnclosureFilesize,
        Duration = part.Duration,
        Notes = part.Notes,
        Transcription = part.Transcription,
        Rating = part.Rating,
        AvailablePeople = _personRepository.Table.OrderBy(p => p.Name).ToList(),
        Hosts = part.Hosts.Any()
                ? part.Hosts.Select(h => h.Id).ToList()
                : part.PodcastPart.Hosts.Select(h => h.Id).ToList(),
        Guests = part.Guests.Select(g => g.Id).ToList(),
      };
    }
  }

}