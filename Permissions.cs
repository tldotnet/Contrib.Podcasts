using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Contrib.Podcasts {
  public class Permissions : IPermissionProvider {
    public static readonly Permission ManagePodcasts = new Permission {Description = "Manage podcasts for others", Name = "ManagePodcasts"};
    public static readonly Permission ManageOwnPodcasts = new Permission { Description = "Manage own podcasts", Name = "ManageOwnPodcasts", ImpliedBy = new[] { ManagePodcasts } };

    public static readonly Permission PublishPodcastEpisode = new Permission { Description = "Publish or unpublish podcast episode for others", Name = "PublishPodcastEpisode", ImpliedBy = new[] { ManagePodcasts } };
    public static readonly Permission PublishOwnPodcastEpisode = new Permission { Description = "Publish or unpublish own podcast episode", Name = "PublishOwnPodcastEpisode", ImpliedBy = new[] { PublishPodcastEpisode, ManageOwnPodcasts } };
    public static readonly Permission EditPodcastEpisode = new Permission { Description = "Edit podcast episodes for others", Name = "EditPodcastEpisode", ImpliedBy = new[] { PublishPodcastEpisode } };
    public static readonly Permission EditOwnPodcastEpisode = new Permission { Description = "Edit own podcast episodes", Name = "EditOwnPodcastEpisode", ImpliedBy = new[] { EditPodcastEpisode, PublishOwnPodcastEpisode } };
    public static readonly Permission DeletePodcastEpisode = new Permission { Description = "Delete podcast episode for others", Name = "DeletePodcastEpisode", ImpliedBy = new[] { ManagePodcasts } };
    public static readonly Permission DeleteOwnPodcastEpisode = new Permission { Description = "Delete own podcast episode", Name = "DeleteOwnPodcastEpisode", ImpliedBy = new[] { DeletePodcastEpisode, ManageOwnPodcasts } };

    public static readonly Permission MetaListPodcasts = new Permission { ImpliedBy = new[] { EditPodcastEpisode, PublishPodcastEpisode, DeletePodcastEpisode } };
    public static readonly Permission MetaListOwnPodcasts = new Permission { ImpliedBy = new[] { EditOwnPodcastEpisode, PublishOwnPodcastEpisode, DeleteOwnPodcastEpisode } };

    public virtual Feature Feature { get; set; }

    public IEnumerable<Permission> GetPermissions() {
      return new[] {
                ManageOwnPodcasts,
                ManagePodcasts,
                EditOwnPodcastEpisode,
                EditPodcastEpisode,
                PublishOwnPodcastEpisode,
                PublishPodcastEpisode,
                DeleteOwnPodcastEpisode,
                DeletePodcastEpisode,
            };
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
      return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManagePodcasts}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {PublishPodcastEpisode,EditPodcastEpisode,DeletePodcastEpisode}
                },
                new PermissionStereotype {
                    Name = "Moderator",
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = new[] {ManageOwnPodcasts}
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = new[] {EditOwnPodcastEpisode}
                },
            };
    }
  }
}