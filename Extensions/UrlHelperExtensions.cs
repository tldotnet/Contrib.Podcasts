using System.Web.Mvc;
using Contrib.Podcasts.Models;

namespace Contrib.Podcasts.Extensions {
  public static class UrlHelperExtensions {
    public static string PodcastsForAdmin(this UrlHelper urlHelper) {
      return urlHelper.Action("List", "PodcastAdmin", new { area = "Contrib.Podcasts" });
    }

    public static string PodcastForAdmin(this UrlHelper urlHelper, PodcastPart podcastPart) {
      return urlHelper.Action("Item", "PodcastAdmin", new { podcastId = podcastPart.Id, area = "Contrib.Podcasts" });
    }

    public static string PodcastCreate(this UrlHelper urlHelper) {
      return urlHelper.Action("Create", "Admin", new { area = "Contents", id = "Podcast" });
    }

    public static string PodcastEdit(this UrlHelper urlHelper, PodcastPart podcastPart) {
      return urlHelper.Action("Edit", "PodcastAdmin", new { podcastId = podcastPart.Id, area = "Contrib.Podcasts" });
    }

    public static string PodcastEpisodeCreate(this UrlHelper urlHelper, PodcastPart podcastPart) {
      return urlHelper.Action("Create", "PodcastEpisodeAdmin", new { podcastId = podcastPart.Id, area = "Contrib.Podcasts" });
    }
  }
}