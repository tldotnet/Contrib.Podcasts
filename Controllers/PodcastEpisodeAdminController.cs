using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Contents.Controllers;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using Contrib.Podcasts.Extensions;

namespace Contrib.Podcasts.Controllers {
  [ValidateInput(false), Admin]
  public class PodcastEpisodeAdminController : Controller, IUpdateModel {
    private readonly IContentManager _contentManager;
    private readonly ISiteService _siteService;
    private readonly IPodcastService _podcastService;
    private readonly IPodcastEpisodeService _podcastEpisodeService;
    private readonly IRepository<PersonRecord> _personRepository;
    private readonly IRepository<EpisodePersonRecord> _episodePersonRepository;

    public PodcastEpisodeAdminController(IOrchardServices services, IContentManager contentManager, ISiteService siteService, IPodcastService podcastService, IPodcastEpisodeService podcastEpisodeService, IRepository<PersonRecord> personRepository, IRepository<EpisodePersonRecord> episodePersonRepository) {
      Services = services;
      _contentManager = contentManager;
      _siteService = siteService;
      _podcastService = podcastService;
      _podcastEpisodeService = podcastEpisodeService;
      _personRepository = personRepository;
      _episodePersonRepository = episodePersonRepository;
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

      // init the episode number
      var lastEpisode = _podcastEpisodeService.Get(podcast, VersionOptions.Latest)
        .OrderByDescending(e => e.EpisodeNumber).FirstOrDefault();

      // init the rating of episode = rating of podcast
      podcastEpisode.EpisodeNumber = lastEpisode == null ? 1 : lastEpisode.EpisodeNumber + 1;
      podcastEpisode.Rating = podcast.Rating;

      dynamic model = Services.ContentManager.BuildEditor(podcastEpisode);
      return View((object)model);
    }

    /// <summary>
    /// Handles POST of creating episode from create page.
    /// </summary>
    [HttpPost, ActionName("Create")]
    [FormValueRequired("submit.Save")]
    public ActionResult CreatePOST(int podcastId) {
      return CreatePOST(podcastId, false);
    }

    /// <summary>
    /// Handles POST of creating & publishing episode from create page.
    /// </summary>
    [HttpPost, ActionName("Create")]
    [FormValueRequired("submit.Publish")]
    public ActionResult CreateAndPublishPOST(int podcastId) {
      //todo: add permissions here
      return CreatePOST(podcastId, true);
    }

    private ActionResult CreatePOST(int podcastId, bool publish = false) {
      var podcast = _podcastService.Get(podcastId).As<PodcastPart>();

      if (podcast == null)
        return HttpNotFound();

      var episode = Services.ContentManager.New<PodcastEpisodePart>("PodcastEpisode");
      episode.PodcastPart = podcast;

      //todo: add permissions here

      Services.ContentManager.Create(episode, VersionOptions.Draft);
      var model = Services.ContentManager.UpdateEditor(episode, this);

      if (!ModelState.IsValid) {
        Services.TransactionManager.Cancel();
        return View((object)model);
      }

      if (publish) {
        //todo: add permissions here
        Services.ContentManager.Publish(episode.ContentItem);
      }

      Services.Notifier.Information(T("Your {0} has been created.", episode.TypeDefinition.DisplayName));
      return Redirect(Url.PodcastEpisodeEdit(episode));
    }

    /// <summary>
    /// Handles obtaining the requested episode and displaying the edit view.
    /// </summary>
    [HttpGet]
    public ActionResult Edit(int podcastId, int episodeId) {
      var podcast = _podcastService.Get(podcastId);
      if (podcast == null)
        return HttpNotFound();

      var episode = _podcastEpisodeService.Get(episodeId, VersionOptions.Latest);
      if (episode == null)
        return HttpNotFound();

      //todo: add permissions here

      dynamic model = Services.ContentManager.BuildEditor(episode);
      return View((object) model);
    }

    /// <summary>
    /// Handles POST of saving episode from edit page.
    /// </summary>
    [HttpPost, ActionName("Edit")]
    [FormValueRequired("submit.Save")]
    public ActionResult EditPOST(int podcastId, int episodeId) {
      var podcast = _podcastService.Get(podcastId);
      if (podcast == null)
        return HttpNotFound();
      
      var episode = _podcastEpisodeService.Get(episodeId, VersionOptions.Latest);
      if (episode == null)
        return HttpNotFound();

      dynamic model = Services.ContentManager.UpdateEditor(episode, this);
      if (!ModelState.IsValid) {
        Services.TransactionManager.Cancel();
        // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
        return View((object)model);
      }

      _contentManager.Publish(episode.ContentItem);
      Services.Notifier.Information(T("Episode information updated"));

      return Redirect(Url.PodcastForAdmin(podcast.As<PodcastPart>()));
    }

    /// <summary>
    /// Handles POST of deleting episode from edit page.
    /// </summary>
    [HttpPost, ActionName("Edit")]
    [FormValueRequired("submit.Delete")]
    public ActionResult EditDeletePOST(int podcastId, int episodeId) {
      var podcast = _podcastService.Get(podcastId);
      if (podcast == null)
        return HttpNotFound();

      var episode = _podcastEpisodeService.Get(episodeId, VersionOptions.Latest);
      if (episode == null)
        return HttpNotFound();
      _podcastService.Delete(episode.ContentItem);

      Services.Notifier.Information(T("Episode deleted"));

      return Redirect(Url.PodcastForAdmin(podcast.As<PodcastPart>()));
    }

    #region IUpdateModel members
    bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
      return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
    }

    void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
      ModelState.AddModelError(key, errorMessage.ToString());
    }
    #endregion
  }
}