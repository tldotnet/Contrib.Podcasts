using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.UI.Admin;

namespace Contrib.Podcasts.Controllers {
  [ValidateInput(true), Admin]
  public class PodcastEpisodeAdminController : Controller, IUpdateModel {
    private readonly IContentManager _contentManager;
    private readonly ISiteService _siteService;
    private readonly IPodcastService _podcastService;
    private readonly IPodcastEpisodeService _podcastEpisodeService;

    public PodcastEpisodeAdminController(IOrchardServices services, IContentManager contentManager, ISiteService siteService, IPodcastService podcastService, IPodcastEpisodeService podcastEpisodeService) {
      Services = services;
      _contentManager = contentManager;
      _siteService = siteService;
      _podcastService = podcastService;
      _podcastEpisodeService = podcastEpisodeService;
    }

    public IOrchardServices Services { get; set; }
    public Localizer T { get; set; }

    /// <summary>
    /// Handles action request to create a podcast episode.
    /// </summary>
    public ActionResult Create(int podcastId) {
      var podcast = _podcastService.Get(podcastId).As<PodcastPart>();
      if (podcast == null)
        return HttpNotFound();

      var podcastEpisode = Services.ContentManager.New<PodcastEpisodePart>("PodcastEpisode");
      podcastEpisode.PodcastPart = podcast;

      dynamic model = Services.ContentManager.BuildEditor(podcastEpisode);

      return View((object) model);
    }

    #region IUpdateModel members

    public new bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class {
      return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
    }

    public void AddModelError(string key, LocalizedString errorMessage) {
      ModelState.AddModelError(key, errorMessage.ToString());
    }

    #endregion
  }
}