using System.Collections.Generic;
using System.Web.Routing;
using Orchard;

namespace Contrib.Podcasts.Routing {
  public interface IPodcastPathConstraint : IRouteConstraint, ISingletonDependency {
    void SetPaths(IEnumerable<string> paths);
    string FindPath(string path);
    void AddPath(string path);
    void RemovePath(string path);     
  }
}