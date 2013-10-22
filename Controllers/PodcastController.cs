using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Feeds;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Shapes;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc;
using Orchard.Settings;
using Orchard.Themes;
using Orchard.UI.Navigation;

namespace Contrib.Podcasts.Controllers {
  [Themed]
  public class PodcastController : Controller {
    private readonly IOrchardServices _services;
    private readonly IPodcastService _podcastService;
    private readonly IPodcastEpisodeService _podcastEpisodeService;
    private readonly ISiteService _siteService;

    public PodcastController(IOrchardServices services, IPodcastService podcastService, IPodcastEpisodeService podcastEpisodeService, IShapeFactory shapeFactory, ISiteService siteService) {
      _services = services;
      _podcastService = podcastService;
      _podcastEpisodeService = podcastEpisodeService;
      _siteService = siteService;
      Logger = NullLogger.Instance;
      Shape = shapeFactory;
      T = NullLocalizer.Instance;
    }

    dynamic Shape { get; set; }
    protected ILogger Logger { get; set; }
    public Localizer T { get; set; }

    public ActionResult Item(int podcastId, PagerParameters pagerParameters) {
      Pager pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);

      var podcastPart = _podcastService.Get(podcastId).As<PodcastPart>();
      if (podcastPart == null)
        return HttpNotFound();

      if (!_services.Authorizer.Authorize(Orchard.Core.Contents.Permissions.ViewContent, podcastPart, T("Cannot view content"))) {
        return new HttpUnauthorizedResult();
      }


      var podcastEpisodes = _podcastEpisodeService.Get(podcastPart, pager.GetStartIndex(), pager.PageSize)
          .Select(b => _services.ContentManager.BuildDisplay(b, "Summary"));
      dynamic podcast = _services.ContentManager.BuildDisplay(podcastPart);

      var list = Shape.List();
      list.AddRange(podcastEpisodes);
      podcast.Content.Add(Shape.Parts_Podcasts_PodcastEpisode_List(ContentItems: list), "5");

      var totalItemCount = _podcastEpisodeService.EpisodeCount(podcastPart);
      podcast.Content.Add(Shape.Pager(pager).TotalItemCount(totalItemCount), "Content:after");

      return new ShapeResult(this, podcast);
    }

  }
}