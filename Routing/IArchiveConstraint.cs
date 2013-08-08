using System.Web.Routing;
using Contrib.Podcasts.Models;
using Orchard;

namespace Contrib.Podcasts.Routing {
  public interface IArchiveConstraint :IRouteConstraint, ISingletonDependency {
    string FindPath(string path);
    ArchiveData FindArchiveData(string path);
  }
}