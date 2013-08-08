using System.Linq;
using System.Web.Mvc;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Routing;
using Contrib.Podcasts.Services;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Themes;

namespace Contrib.Podcasts.Controllers {
  [Themed]
  public class PodcastEpisodeController : Controller {
    private readonly IOrchardServices _services;
    private readonly IPodcastService _podcastService;
    private readonly IPodcastEpisodeService _podcastEpisodeService;
    private readonly IArchiveConstraint _archiveConstraint;

    public PodcastEpisodeController(IOrchardServices services, IPodcastService podcastService, IPodcastEpisodeService podcastEpisodeService,IShapeFactory shapeFactory, IArchiveConstraint archiveConstraint) {
      _services = services;
      _podcastService = podcastService;
      _podcastEpisodeService = podcastEpisodeService;
      _archiveConstraint = archiveConstraint;
      T = NullLocalizer.Instance;
      Shape = shapeFactory;
    }

    dynamic Shape { get; set; }
    public Localizer T { get; set; }

    public ActionResult ListByArchive(string path) {
      var podcastPath = _archiveConstraint.FindPath(path);
      var archive = _archiveConstraint.FindArchiveData(path);

      if (podcastPath == null) return HttpNotFound();
      if (archive == null) return HttpNotFound();

      PodcastPart podcastPart = _podcastService.Get(podcastPath);

      if (podcastPart == null) return HttpNotFound();

      var list = Shape.List();
      list.AddRange(_podcastEpisodeService.Get(podcastPart, archive).Select(p => _services.ContentManager.BuildDisplay(p, "Summary")));

      //todo: add feed

      dynamic viewModel = Shape.ViewModel()
        .ContentItems(list)
        .Podcast(podcastPart)
        .ArchiveData(archive);

      return View((object) viewModel);
    }
  }
}