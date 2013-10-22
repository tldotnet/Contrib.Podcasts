using System.Web.Routing;
using System.Web.UI.WebControls;
using Contrib.Podcasts.Models;
using Contrib.Podcasts.Routing;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Contrib.Podcasts.Handlers {
  public class PodcastHandler : ContentHandler {
    private readonly IPodcastPathConstraint _podcastPathConstraint;

    public PodcastHandler(IRepository<PodcastPartRecord> podcastPartRepository, IPodcastPathConstraint podcastPathConstraint) {
      _podcastPathConstraint = podcastPathConstraint;

      Filters.Add(StorageFilter.For(podcastPartRepository));

      OnCreated<PodcastPart>((context, podcast) => _podcastPathConstraint.AddPath(podcast.As<IAliasAspect>().Path));
      OnRemoved<PodcastPart>((context, podcast) => _podcastPathConstraint.RemovePath(podcast.As<IAliasAspect>().Path));
    }

    protected override void GetItemMetadata(GetContentItemMetadataContext context) {
      var podcast = context.ContentItem.As<PodcastPart>();
      if (podcast == null) return;

      context.Metadata.DisplayRouteValues = new RouteValueDictionary {
                {"Area", "Contrib.Podcasts"},
                {"Controller", "Podcast"},
                {"Action", "Item"},
                {"podcastId", context.ContentItem.Id}
            };
    }
  }
}