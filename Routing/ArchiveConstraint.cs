using System;
using Contrib.Podcasts.Models;

namespace Contrib.Podcasts.Routing {
  public class ArchiveConstraint : IArchiveConstraint {
    private readonly IPodcastPathConstraint _podcastPathConstraint;

    public ArchiveConstraint(IPodcastPathConstraint podcastPathConstraint) {
      _podcastPathConstraint = podcastPathConstraint;
    }

    public string FindPath(string path) {
      var archiveIndex = path.IndexOf("/archive/", StringComparison.OrdinalIgnoreCase);

      if (archiveIndex == -1) {
        // is the archive for podcast the hompeage?
        if (path.StartsWith("archive/", StringComparison.OrdinalIgnoreCase)) {
          return string.Empty;
        }
        return null;
      }

      return path.Substring(0, archiveIndex);
    }

    public ArchiveData FindArchiveData(string path) {
      var archiveIndex = path.IndexOf("/archive/", StringComparison.OrdinalIgnoreCase);

      if (archiveIndex == -1) {
        // is the archive for podcast the hompeage?
        if (path.StartsWith("archive/", StringComparison.OrdinalIgnoreCase)) {
          return new ArchiveData(path.Substring("archive/".Length));         
        }
        return null;
      }

      try {
        return new ArchiveData(path.Substring(archiveIndex +"/archive/".Length));
      }
      catch (Exception) {
        return null;
      }
    }

    /// <summary>
    /// Defined in IRouteConstraint to find see if the URL matches allowed patterns for Podcasts.
    /// </summary>
    public bool Match(System.Web.HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName, System.Web.Routing.RouteValueDictionary values, System.Web.Routing.RouteDirection routeDirection) {
      object value;

      if (values.TryGetValue(parameterName, out value)) {
        var parameterValue = Convert.ToString(value);

        var path = FindPath(parameterValue);
        if (path == null) {
          return false;
        }

        var archiveData = FindArchiveData(parameterValue);
        if (archiveData == null) {
          return false;
        }

        return _podcastPathConstraint.FindPath(path) != null;
      }

      return false;
    }
  }
}