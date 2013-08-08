using System.Web.Mvc;
using Contrib.Podcasts.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;

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

    public static string PodcastEpisodeEdit(this UrlHelper urlHelper, PodcastEpisodePart podcastEpisdePart) {
      return urlHelper.Action("Edit", "PodcastEpisodeAdmin", new { podcastId = podcastEpisdePart.PodcastPart.Id, episodeId=podcastEpisdePart.Id, area = "Contrib.Podcasts" });
    }

    public static string PodcastArchiveYear(this UrlHelper urlHelper, PodcastPart blogPart, int year) {
      var blogPath = blogPart.As<IAliasAspect>().Path;
      return urlHelper.Action("ListByArchive", "PodcastEpisode", new { path = (string.IsNullOrWhiteSpace(blogPath) ? "archive/" : blogPath + "/archive/") + year.ToString(), area = "Contrib.Podcasts" });
    }

    public static string PodcastArchiveMonth(this UrlHelper urlHelper, PodcastPart blogPart, int year, int month) {
      var blogPath = blogPart.As<IAliasAspect>().Path;
      return urlHelper.Action("ListByArchive", "PodcastEpisode", new { path = (string.IsNullOrWhiteSpace(blogPath) ? "archive/" : blogPath + "/archive/") + string.Format("{0}/{1}", year, month), area = "Contrib.Podcasts" });
    }

    public static string PodcastArchiveDay(this UrlHelper urlHelper, PodcastPart blogPart, int year, int month, int day) {
      var blogPath = blogPart.As<IAliasAspect>().Path;
      return urlHelper.Action("ListByArchive", "PodcastEpisode", new { path = (string.IsNullOrWhiteSpace(blogPath) ? "archive/" : blogPath + "/archive/") + string.Format("{0}/{1}/{2}", year, month, day), area = "Contrib.Podcasts" });
    }
  }
}