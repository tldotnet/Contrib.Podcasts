using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Contrib.Podcasts.Services;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Contrib.Podcasts {
  public class AdminMenu : INavigationProvider {

    private readonly IPodcastService _podcastService;

    public AdminMenu(IPodcastService podcastService) {
      _podcastService = podcastService;
    }

    public string MenuName {
      get { return "admin"; }
    }

    public Localizer T { get; set; }

    public void GetNavigation(NavigationBuilder builder) {
      builder.Add(T("Podcast"), "2", BuildMenu);
    }

    private void BuildMenu(NavigationItemBuilder menu) {
      // get a list of all podcasts
      var podcasts = _podcastService.Get();
      var podcastCount = podcasts.Count();
      var singlePodcast = podcastCount == 1 ? podcasts.ElementAt(0) : null;

      // if there are multiple podcasts...
      if (podcastCount > 0 && singlePodcast == null) {
        menu.Add(T("Podcast List"), "2.0", item =>
          item.Action("List", "PodcastAdmin", new { area = "Contrib.Podcasts" })
        );
      } // else if only one podcast...
      else if (singlePodcast != null) {
        menu.Add(T("View Podcast"), "2.0", item =>
          item.Action("Item", "PodcastAdmin", new { area = "Contrib.Podcasts", podcastId = singlePodcast.Id })
        );
      }

      // if only one podcast, add shortcut to create a new episode in it
      if (singlePodcast != null) { 
        menu.Add(T("New Episode"), "2.1", item =>
          item.Action("Create", "PodcastEpisodeAdmin", new { area = "Contrib.Podcasts", podcastId = singlePodcast.Id })
        );
      }

      // alsways show new podcast & people management nav links
      menu.Add(T("New Podcast"), "2.2", item =>
        item.Action("Create", "Admin", new { area = "Contents", id = "Podcast" })
        );
      menu.Add(T("Manage People"), "2.3", item =>
        item.Action("Index", "PersonAdmin", new { area = "Contrib.Podcasts" })
        );
    }
  }
}