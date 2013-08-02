using System.Linq;
using System.Web.Mvc;
using Contrib.Podcasts.Extensions;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Services;
using Contrib.Podcasts.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Contents.Controllers;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;

namespace Contrib.Podcasts.Controllers {
  [ValidateInput(true), Admin]
  public class PodcastAdminController : Controller, IUpdateModel {
    private readonly IContentManager _contentManager;
    private readonly ISiteService _siteService;
    private readonly IPodcastService _podcastService;

    dynamic Shape { get; set; }
    public Localizer T { get; set; }
    public IOrchardServices Services { get; set; }

    public PodcastAdminController(IOrchardServices services, ISiteService siteService, IContentManager contentManager, IPodcastService podcastService, IShapeFactory shapeFactory) {
      Services = services;
      Shape = shapeFactory;
      _siteService = siteService;
      _contentManager = contentManager;
      _podcastService = podcastService;
    }

    /// <summary>
    /// Handles request for getting list of all podcasts.
    /// </summary>
    public ActionResult List() {
      var list = Services.New.List();
      list.AddRange(_podcastService.Get()
                                   .Select(p => {
                                     var podcast = Services.ContentManager.BuildDisplay(p, "SummaryAdmin");
                                     // TODO: add total episodes count (see same function in blogs module)
                                     return podcast;
                                   }));

      // create viewmodel
      dynamic viewModel = Services.New.ViewModel().ContentItems(list);
      return View((object) viewModel);
    }

    /// <summary>
    /// Handles showing the podcast admin display page: contains basics about podcast + all episode list + link to podcast edit page.
    /// </summary>
    public ActionResult Item(int podcastId, PagerParameters pagerParameters) {
      Pager pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
      PodcastPart podcastPart = _podcastService.Get(podcastId).As<PodcastPart>();
      if (podcastPart == null)
        return HttpNotFound();

      // TODO: get all episodes for the podcast
      // TODO: get all episode shapes

      // create dynamic podcast object
      dynamic podcast = Services.ContentManager.BuildDisplay(podcastPart, "DetailAdmin");

      // TODO: add all episode shapes to the podcast

      // add pager to the 
      int totalEpisodes = 5; // TODO: don't hardcode this
      podcast.Content.Add(Shape.Pager(pager).TotalItemCount(totalEpisodes), "Content:after");

      return View((object)podcast);
    }

    /// <summary>
    /// Handles obtaining the requested podcast and displaying the edit view.
    /// </summary>
    [HttpGet]
    public ActionResult Edit(int podcastId) {
      var podcast = _podcastService.Get(podcastId);

      dynamic model = Services.ContentManager.BuildEditor(podcast);

      return View((object)model);
    }

    /// <summary>
    /// Handles POST of saving podcast from edit page.
    /// </summary>
    [HttpPost, ActionName("Edit")]
    [FormValueRequired("submit.Save")]
    public ActionResult EditPOST(int podcastId) {
      var podcast = _podcastService.Get(podcastId);

      dynamic model = Services.ContentManager.UpdateEditor(podcast, this);
      if (!ModelState.IsValid) {
        Services.TransactionManager.Cancel();
        // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
        return View((object)model);
      }

      _contentManager.Publish(podcast);
      Services.Notifier.Information(T("Podcast information updated"));

      return Redirect(Url.PodcastsForAdmin());
    }

    /// <summary>
    /// Handles POST of deleting podcast from edit page.
    /// </summary>
    [HttpPost, ActionName("Edit")]
    [FormValueRequired("submit.Delete")]
    public ActionResult EditDeletePOST(int podcastId) {
      var podcast = _podcastService.Get(podcastId);
      if (podcast == null)
        return HttpNotFound();
      _podcastService.Delete(podcast);

      Services.Notifier.Information(T("Podcast deleted"));

      return Redirect(Url.PodcastsForAdmin());
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