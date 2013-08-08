using System.Web.Mvc;
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

    public PodcastEpisodeController(IOrchardServices services, IPodcastService podcastService, IPodcastEpisodeService podcastEpisodeService,IShapeFactory shapeFactory) {
      _services = services;
      _podcastService = podcastService;
      _podcastEpisodeService = podcastEpisodeService;
      T = NullLocalizer.Instance;
      Shape = shapeFactory;
    }

    dynamic Shape { get; set; }
    public Localizer T { get; set; }

    public ActionResult ListByArchive(string path) {
      
    }
  }
}