using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Contrib.Podcasts {
  public class Routes : IRouteProvider {

    public Routes() { }

    public void GetRoutes(ICollection<RouteDescriptor> routes) {
      foreach (var routeDescriptor in GetRoutes())
        routes.Add(routeDescriptor);
    }

    public IEnumerable<RouteDescriptor> GetRoutes() {
      var podcastRoutes = new[] {        
        // admin route for listing all podcasts
        new RouteDescriptor {
          Route = new Route("Admin/Podcasts",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PodcastAdmin"},
                              {"action", "List"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        },
        // route for viewing details on a podcast
        new RouteDescriptor {
          Route = new Route("Admin/Podcasts/{podcastId}",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PodcastAdmin"},
                              {"action", "Item"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        },
        new RouteDescriptor {
          Route = new Route("Admin/Podcasts/{podcastId}/Edit",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PodcastAdmin"},
                              {"action", "Edit"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler())
                    },
      };

      var episodeRoutes = new[] {
        // admin route for creating new episode
        new RouteDescriptor {
          Route = new Route("Admin/Podcasts/{podcastId}/Episodes/Create",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PodcastEpisodeAdmin"},
                              {"action", "Create"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        },
        // admin route for creating new episode
        new RouteDescriptor {
          Route = new Route("Admin/Podcasts/{podcastId}/Episodes/{episodeId}/Edit",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PodcastEpisodeAdmin"},
                              {"action", "Edit"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        }
      };

      var personRoutes = new[] {
        // admin route for listing people
        new RouteDescriptor {
          Route = new Route("Admin/People",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PersonAdmin"},
                              {"action", "Index"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        },
        // admin route for editing person
        new RouteDescriptor {
          Route = new Route("Admin/People/{personId}/Edit",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PersonAdmin"},
                              {"action", "Edit"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        },  
        // admin route for deleting person
        new RouteDescriptor {
          Route = new Route("Admin/People/{personId}/Delete",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PersonAdmin"},
                              {"action", "Delete"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        },        
        // admin route for people actions
        new RouteDescriptor {
          Route = new Route("Admin/People/{action}",
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"},
                              {"controller", "PersonAdmin"}
                            },
                            new RouteValueDictionary(),
                            new RouteValueDictionary {
                              {"area", "Contrib.Podcasts"}
                            },
                            new MvcRouteHandler()
          )
        }
      };


      var routes = new List<RouteDescriptor>();
      routes.AddRange(podcastRoutes);
      routes.AddRange(episodeRoutes);
      routes.AddRange(personRoutes);

      return routes;
    }
  }
}