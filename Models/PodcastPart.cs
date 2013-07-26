using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;

namespace Contrib.Podcasts.Models {
  public class PodcastPart : ContentPart<PodcastPartRecord> {

    public string Title {
      get { return this.As<TitlePart>().Title; }
      set { this.As<TitlePart>().Title = value; }
    }

    public string Description {
      get { return Record.Description; }
      set { Record.Description = value; }
    }

    public IEnumerable<PersonRecord> Hosts {
      get { return Record.Hosts.ToList().Select(host => host.PersonRecord); }
    }

    public SimpleRatingTypes Rating {
      get { return Record.Rating; }
      set { Record.Rating = value; }
    }

    public CreativeCommonsLicenseTypes CreativeCommonsLicense {
      get { return Record.CreativeCommonsLicense; }
      set { Record.CreativeCommonsLicense = value; }
    }

    public bool IncludeTranscriptInFeed {
      get { return Record.IncludeTranscriptInFeed; }
      set { Record.IncludeTranscriptInFeed = value; }
    }
  }

}